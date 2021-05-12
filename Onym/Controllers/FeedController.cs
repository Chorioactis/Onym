using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Onym.Data;
using Onym.Models;
using Onym.ViewModels.Feed;
using UnidecodeSharpCore;

#nullable enable

namespace Onym.Controllers
{
    public class FeedController : Controller
    {
        /* CUSTOM CLASSES */
        private readonly TextTransform _textTransform = new TextTransform();
        private readonly Random _random = new Random();
        /* CONSTANTS */
        private const int MaxTags = 8;
        /* CONTROLLER VARS */
        private readonly UserManager<User> _userManager;  
        private readonly SignInManager<User> _signInManager;  
        private readonly OnymDbContext<User> _db;
        private readonly IWebHostEnvironment _appEnvironment;
        public FeedController(UserManager<User> userManager, SignInManager<User> signInManager, OnymDbContext<User> db, IWebHostEnvironment appEnvironment)
        {
            _db = db;
            _userManager = userManager;  
            _signInManager = signInManager;  
            _appEnvironment = appEnvironment;
        }
        private IQueryable<Publication>? _publications;
        private IQueryable<PublicationMedia>? _publicationMedia;
        private IQueryable<PublicationTag>? _publicationTags;
        private IQueryable<PublicationRatingTally>? _publicationRatingTallies;
        private IQueryable<Favorite>? _favorites;

        /* INDEX */
        [HttpGet, AllowAnonymous]
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        
        /* BEST */
        [HttpGet, AllowAnonymous]
        [Route("/best")]
        public async Task<IActionResult> Best()
        {
            return View();
        }
        
        /* NEW */
        [HttpGet, AllowAnonymous]
        [Route("/new")]
        public async Task<IActionResult> New()
        {
            return View();
        }
        
        /* RANDOM */
        [HttpGet, AllowAnonymous]
        [Route("/new")]
        public async Task<IActionResult> Random()
        {
            return View();
        }
        
        /* SEARCH */
        public async Task<IActionResult> Search()
        {
            return View();
        }
        
        /* RATED */
        [HttpGet, AllowAnonymous]
        [Route("/rated")]
        public async Task<IActionResult> Rated()
        {
            return View();
        }
        
        /* FAVORITE */
        [HttpGet, AllowAnonymous]
        [Route("/favorite")]
        public async Task<IActionResult> Favorite()
        {
            return View();
        }
        
        /* COMMENTS */
        public async Task<IActionResult> Comments()
        {
            return View();
        }
        
        /* ADD POST */
        [HttpGet, Authorize]
        [ImportModelState]
        [Route("/add_post")]
        public async Task<IActionResult> AddPost()
        {
            AddPostViewModel model;
            model = TempData["model"] == null ? new AddPostViewModel() : JsonConvert.DeserializeObject<AddPostViewModel>((string) TempData["model"]);
            return View(model);
        }
        
        [HttpPost, Authorize, ValidateAntiForgeryToken]
        [ExportModelState]
        [Route("/add_post")]
        public async Task<IActionResult> AddPost(AddPostViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.Content == null && model.Uploads == null)
            {
                ModelState.AddModelError("Name", "Добавьте содержимого публикации - текст или изображение.");
                return View(model);
            }
            var author = await _db.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity!.Name);
            if (author == null) RedirectToAction("Index", "Feed");
            List<Tag> pickedTags = new List<Tag>();
            List<Tag> allTags = _db.Tags.ToListAsync().Result;
            List<Media> uploadedMedia = new List<Media>();
            /* PUBLICATION */
            var post = new Publication
            {
                Name = model.Name!,
                UserId = author!.Id,
                UrlSlug = _textTransform.Slugify(model.Name!),
                Status = 1
            };
            if (model.Content != null) post.Content = Regex.Replace(model.Content, @"\s+", " ").Trim();
            var createdPost = (await _db.Publications.AddAsync(post)).Entity;
            /* TAGS */
            string?[] tags = model.Tags.Split(',',MaxTags + 1, StringSplitOptions.RemoveEmptyEntries);
            if (tags.Length > MaxTags)
            {
                tags[MaxTags] = null;
            }
            for (var i = 0; i < tags.Length; i++)
            {
                if (tags[i] == null) continue;
                tags[i] = _textTransform.CheckTag(tags[i]);
            }
            foreach (var tag in tags)
            {
                if (tag == null) continue;
                var checkTag = allTags.FirstOrDefault(t => t.Name == tag);
                if (checkTag != null)
                {
                    pickedTags.Add(checkTag);
                }
                else
                {
                    var newTag = new Tag{
                        Name = tag,
                        TagRatingCounting = true,
                    };
                    pickedTags.Add((await _db.Tags.AddAsync(newTag)).Entity);
                }
            }
            /* MEDIA UPLOADING*/
            if (model.Uploads != null)
            {
                foreach (var uploadedFile in model.Uploads)
                {
                    string subPath = "/media/post_img/" + DateTime.Today.Year + '/' + DateTime.Today.Month;
                    string imagePath = '/' + subPath + createdPost.UrlSlug + _random.Next() + ".png";
                    string path = _appEnvironment.WebRootPath + subPath ;

                    DirectoryInfo directory = new DirectoryInfo(path);
                    if (!directory.Exists)
                    {
                        directory.Create();
                    }

                    await using (var fileStream = new FileStream(_appEnvironment.WebRootPath + imagePath, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }

                    Media file = new Media{FileLink = imagePath};
                    uploadedMedia.Add((await _db.Media.AddAsync(file)).Entity);
                }
            }
            await _db.SaveChangesAsync();
            createdPost.Id = _db.Publications.FirstOrDefault(p => p.UrlSlug == createdPost.UrlSlug)!.Id;
            /* LINK TO PUB-TAG TABLE */
            if (pickedTags.Count > 0)
            {
                foreach (var tag in pickedTags)
                {
                    var publicationTag = new PublicationTag()
                    {
                        PublicationId = createdPost.Id,
                        TagId =(await _db.Tags.FirstOrDefaultAsync(t => t.Name == tag.Name)).Id
                    };
                    await _db.PublicationTags.AddAsync(publicationTag);
                    await _db.SaveChangesAsync();
                }
            }
            /* LINK TO PUB-MEDIA TABLE */
            if (uploadedMedia.Count > 0)
            {
                foreach (var media in uploadedMedia)
                {

                    var publicationMedia = new PublicationMedia
                    {
                        PublicationId = createdPost.Id,
                        MediaId =(await _db.Media.FirstOrDefaultAsync(m => m.FileLink == media.FileLink)).Id
                    };
                    await _db.PublicationMedia.AddAsync(publicationMedia);
                    await _db.SaveChangesAsync();
                }
            }
            model.Name = null;
            model.Content = null;
            model.Tags = null;
            model.Uploads = null;
            model.UrlSlug = post.UrlSlug;
            model.PostCreated = true;
            TempData["model"] = JsonConvert.SerializeObject(model);
            return RedirectToAction("AddPost", "Feed");
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Onym.Data;
using Onym.Data.Enums;
using Onym.Models;
using Onym.ViewModels.Feed;

#nullable enable

namespace Onym.Controllers
{
    public class FeedController : Controller
    {
        /* CUSTOM CLASSES */
        private static readonly TextTransform TextTransform = new TextTransform();
        private readonly Random _random = new Random();
        /* CONTROLLER VARS */
        private readonly UserManager<User> _userManager;  
        private SignInManager<User> _signInManager;  
        private static OnymDbContext<User> _db = null!;
        private readonly IWebHostEnvironment _appEnvironment;
        private static IOptions<AppSettings>? _settings;
        private List<Publication> _publications;
        private readonly string _oldestCreationDate = DateTime.Parse("01.01.2021").ToShortDateString();
        private readonly string _newestCreationDate = DateTime.Today.ToShortDateString();
        
        public FeedController(UserManager<User> userManager, SignInManager<User> signInManager, OnymDbContext<User> db, IWebHostEnvironment appEnvironment, IOptions<AppSettings> settings)
        {
            _db = db;
            _userManager = userManager;  
            _signInManager = signInManager;  
            _appEnvironment = appEnvironment;
            _settings = settings;
            _publications = _db.Publications
                .Include(publication => publication.User)
                .ThenInclude(user => user.UserProfilePictureNavigation)
                .Include(publication => publication.PublicationMedia)
                .ThenInclude(media => media.Media)
                .Include(publication => publication.PublicationTags)
                .ThenInclude(tag => tag.Tag)
                .Include(publication => publication.PublicationRatingTallies)
                .Include(publication => publication.Favorites)
                .Include(publication => publication.Comments)
                .Include(publication => publication.PublicationStatusNavigation)
                .AsSplitQuery()
                .ToList();
        }

        /* INDEX */
        [HttpGet, AllowAnonymous]
        [Route("")]
        public IActionResult Index(string q, int mnr, int mxr,
            string ocd, string ncd, string tg, int page = 1, SortState sortOrder = SortState.PublicationRatingDesc)
        {
            /* SORT */
            _publications = _publications.Where(p =>
                p.CreationDate >= DateTime.Now.AddDays(-_settings!.Value.ActualPostAge)).ToList();  
            _publications = _publications.Where(publication => publication.PublicationStatusNavigation.Name == "Default").ToList();
            _publications = SetSortOrder(_publications, sortOrder);
            _publications = SetFilter(_publications, q, mnr, mxr, ocd, ncd, tg);
           /* PAGINATION */
            var count = _publications!.Count;
            var pagedPublications = _publications!.Skip((page - 1) * _settings!.Value.PageSize).Take(_settings!.Value.PageSize).ToList();
            /* FORMING VIEW MODEL */
            var model = new FeedViewModel
            {
                FeedPageViewModel = new FeedPageViewModel(count, page, _settings!.Value.PageSize, FeedAction.Index),
                FeedRateViewModel =  new FeedRateViewModel(),
                FeedSortViewModel = new FeedSortViewModel(sortOrder),
                FeedFilterViewModel = new FeedFilterViewModel(tg, q, mnr, mxr, _oldestCreationDate, _newestCreationDate),
                FeedModerateViewModel = new FeedModerateViewModel(),
                Publications = pagedPublications,
            };
            ViewBag.Title = "Популярное";
            ViewBag.Page = "Index";
            return View("Feed",model);
        }
        
        /* BEST */
        [HttpGet, AllowAnonymous]
        [Route("best")]
        public IActionResult Best(string q, int mnr, int mxr,
            string ocd, string ncd, string tg, int page = 1, SortState sortOrder = SortState.PublicationRatingDesc)
        {
            /* SORT */
            _publications = _publications.Where(publication => publication.PublicationStatusNavigation.Name == "Default").ToList();
            _publications = SetSortOrder(_publications, sortOrder);
            _publications = SetFilter(_publications, q, mnr, mxr, ocd, ncd, tg);
            /* PAGINATION */
            var count = _publications!.Count;
            var pagedPublications = _publications!.Skip((page - 1) * _settings!.Value.PageSize).Take(_settings!.Value.PageSize).ToList();
            /* FORMING VIEW MODEL */
            var model = new FeedViewModel
            {
                FeedPageViewModel = new FeedPageViewModel(count, page, _settings!.Value.PageSize, FeedAction.Best),
                FeedRateViewModel =  new FeedRateViewModel(),
                FeedSortViewModel = new FeedSortViewModel(sortOrder),
                FeedFilterViewModel = new FeedFilterViewModel(tg, q, mnr, mxr, _oldestCreationDate, _newestCreationDate),
                FeedModerateViewModel = new FeedModerateViewModel(),
                Publications = pagedPublications,
            };
            ViewBag.Title = "Лучшее";
            ViewBag.Page = "Best";
            return View("Feed",model);
        }
        
        /* NEW */
        [HttpGet, AllowAnonymous]
        [Route("new")]
        public IActionResult New(string q, int mnr, int mxr,
            string ocd, string ncd, string tg, int page = 1, SortState sortOrder = SortState.PublicationAgeDesc)
        {
            /* SORT */
            _publications = _publications.Where(publication => publication.PublicationStatusNavigation.Name == "Default").ToList();
            _publications = SetSortOrder(_publications, sortOrder);
            _publications = SetFilter(_publications, q, mnr, mxr, ocd, ncd, tg);
            /* PAGINATION */
            var count = _publications!.Count;
            var pagedPublications = _publications!.Skip((page - 1) * _settings!.Value.PageSize).Take(_settings!.Value.PageSize).ToList();
            /* FORMING VIEW MODEL */
            var model = new FeedViewModel
            {
                FeedPageViewModel = new FeedPageViewModel(count, page, _settings!.Value.PageSize, FeedAction.New),
                FeedRateViewModel =  new FeedRateViewModel(),
                FeedSortViewModel = new FeedSortViewModel(sortOrder),
                FeedFilterViewModel = new FeedFilterViewModel(tg, q, mnr, mxr, _oldestCreationDate, _newestCreationDate),
                FeedModerateViewModel = new FeedModerateViewModel(),
                Publications = pagedPublications,
            };
            ViewBag.Title = "Новое";
            ViewBag.Page = "New";
            return View("Feed",model);
        }
        
        /* RANDOM */
        [HttpGet, AllowAnonymous]
        [Route("random")]
        public IActionResult Random(string q, int mnr, int mxr,
            string ocd, string ncd, string tg)
        {
            /* SORT */
            _publications = _publications.Where(publication => publication.PublicationStatusNavigation.Name == "Default").ToList();
            _publications = SetFilter(_publications, q, mnr, mxr, ocd, ncd, tg);
            /* RANDOM */
            var randomId = RandomNumberGenerator.GetInt32(0, _publications.Count);
            var randomPublication = _publications[randomId];
            /* FORMING VIEW MODEL */
            var model = new FeedViewModel
            {
                FeedRateViewModel =  new FeedRateViewModel(),
                FeedModerateViewModel = new FeedModerateViewModel(),
                FeedFilterViewModel = new FeedFilterViewModel(tg, q, mnr, mxr, _oldestCreationDate, _newestCreationDate),
                Publications = _publications,
                Publication = randomPublication
            };
            ViewBag.Title = "Случайное";
            ViewBag.Page = "Random";
            return View("Random",model);
        }
        
        /* HIDDEN */
        [HttpGet, Authorize]
        [Route("stories/hidden")]
        public IActionResult HiddenPublications(string q, int mnr, int mxr,
            string ocd, string ncd, string tg, int page = 1, SortState sortOrder = SortState.PublicationAgeDesc)
        {
            /* SORT */
            _publications = _publications.Where(publication => publication.PublicationStatusNavigation.Name == "Hidden").ToList();
            _publications = SetSortOrder(_publications, sortOrder);
            _publications = SetFilter(_publications, q, mnr, mxr, ocd, ncd, tg);
            /* PAGINATION */
            var count = _publications!.Count;
            var pagedPublications = _publications!.Skip((page - 1) * _settings!.Value.PageSize).Take(_settings!.Value.PageSize).ToList();
            /* FORMING VIEW MODEL */
            var model = new FeedViewModel
            {
                FeedPageViewModel = new FeedPageViewModel(count, page, _settings!.Value.PageSize, FeedAction.New),
                FeedRateViewModel =  new FeedRateViewModel(),
                FeedSortViewModel = new FeedSortViewModel(sortOrder),
                FeedFilterViewModel = new FeedFilterViewModel(tg, q, mnr, mxr, _oldestCreationDate, _newestCreationDate),
                FeedModerateViewModel = new FeedModerateViewModel(),
                Publications = pagedPublications,
            };
            ViewBag.Title = "HiddenPublications";
            ViewBag.Page = "HiddenPublications";
            return View("Feed",model);
        }

        /* RATED */
        [HttpGet, Authorize]
        [Route("rated")]
        public IActionResult Rated(string q, int mnr, int mxr,
            string ocd, string ncd, string tg, int page = 1, SortState sortOrder = SortState.PublicationAgeDesc)
        {
            /* SORT */
            _publications = _publications.Where(publication => publication.PublicationRatingTallies.Any(tally => tally.User.UserName == User.Identity!.Name)).ToList();
            _publications = _publications.Where(publication => publication.PublicationStatusNavigation.Name != "Deleted").ToList();
            _publications = SetSortOrder(_publications, sortOrder);
            _publications = SetFilter(_publications, q, mnr, mxr, ocd, ncd, tg);
            /* PAGINATION */
            var count = _publications!.Count;
            var pagedPublications = _publications!.Skip((page - 1) * _settings!.Value.PageSize).Take(_settings!.Value.PageSize).ToList();
            /* FORMING VIEW MODEL */
            var model = new FeedViewModel
            {
                FeedPageViewModel = new FeedPageViewModel(count, page, _settings!.Value.PageSize, FeedAction.Rated),
                FeedRateViewModel =  new FeedRateViewModel(),
                FeedSortViewModel = new FeedSortViewModel(sortOrder),
                FeedFilterViewModel = new FeedFilterViewModel(tg, q, mnr, mxr, _oldestCreationDate, _newestCreationDate),
                FeedModerateViewModel = new FeedModerateViewModel(),
                Publications = pagedPublications,
            };
            ViewBag.Title = "Оценённое";
            ViewBag.Page = "Rated";
            return View("Feed",model);
        }
        
        /* FAVORITE */
        [HttpGet, AllowAnonymous]
        [Route("favorite")]
        public IActionResult Favorite(string q, int mnr, int mxr,
            string ocd, string ncd, string tg, int page = 1, SortState sortOrder = SortState.PublicationAgeDesc)
        {
            /* SORT */
            _publications = _publications.Where(publication => publication.Favorites.Any(favorite => favorite.User.UserName == User.Identity!.Name)).ToList();
            _publications = _publications.Where(publication => publication.PublicationStatusNavigation.Name != "Deleted").ToList();
            _publications = SetSortOrder(_publications, sortOrder);
            _publications = SetFilter(_publications, q, mnr, mxr, ocd, ncd, tg);
            /* PAGINATION */
            var count = _publications!.Count;
            var pagedPublications = _publications!.Skip((page - 1) * _settings!.Value.PageSize).Take(_settings!.Value.PageSize).ToList();
            /* FORMING VIEW MODEL */
            var model = new FeedViewModel
            {
                FeedPageViewModel = new FeedPageViewModel(count, page, _settings!.Value.PageSize, FeedAction.Favorite),
                FeedRateViewModel =  new FeedRateViewModel(),
                FeedSortViewModel = new FeedSortViewModel(sortOrder),
                FeedFilterViewModel = new FeedFilterViewModel(tg, q, mnr, mxr, _oldestCreationDate, _newestCreationDate),
                FeedModerateViewModel = new FeedModerateViewModel(),
                Publications = pagedPublications,
            };
            ViewBag.Title = "Избранное";
            ViewBag.Page = "Favorite";
            return View("Feed",model);
        }
        
        /* PUBLICATION */
        [HttpGet, AllowAnonymous]
        [Route("story/{urlSlug}")]
        public IActionResult Publication(string urlSlug, int? er, int? repl)
        {
            /* FORMING VIEW MODEL */
            var model = new FeedViewModel
            {
                FeedAddCommentViewModel = new FeedAddCommentViewModel
                {
                    ParentalComment = repl
                },
                FeedRateViewModel = new FeedRateViewModel(),
                FeedModerateViewModel = new FeedModerateViewModel(),
                Publications = _publications,
                Publication = _publications.FirstOrDefault(publication => publication.UrlSlug == urlSlug)
            };
            model.Favorite = model.Publication!.Favorites.FirstOrDefault(f => f.PublicationId == model.Publication.Id && f.User.UserName == User.Identity!.Name);
            model.Tally = model.Publication.PublicationRatingTallies.FirstOrDefault(t => t.PublicationId == model.Publication.Id && t.User.UserName == User.Identity!.Name)!;
            switch (er)
            {
                case 1:
                    ModelState.AddModelError("FeedAddCommentViewModel.Content", "Добавьте содержимое комментария - текст или изображение.");
                    break;
            }
            ViewBag.Title = model.Publication.Name;
            ViewBag.Page = "Publication";
            return View(model);
        }
        
        /* USER PUBLICATIONS */
        [HttpGet, AllowAnonymous]
        [Route("/@{userName}/posts")]
        public IActionResult UserPublications(string q, int mnr, int mxr,
            string ocd, string ncd, string tg, string userName, int page = 1, SortState sortOrder = SortState.PublicationAgeAsc)
        {
            var soughtUser = _db.Users.FirstOrDefaultAsync(u => u.UserName == userName).Result;
            if (soughtUser == null) return View("_Error");
            ViewBag.SoughtUserName = soughtUser.UserName;
            /* SORT */
            _publications = _publications.Where(p => p.User.UserName == userName).ToList();
            _publications = _publications.Where(publication => publication.PublicationStatusNavigation.Name != "Deleted").ToList();
            _publications = SetSortOrder(_publications, sortOrder);
            _publications = SetFilter(_publications, q, mnr, mxr, ocd, ncd, tg);
            /* PAGINATION */
            var count = _publications!.Count;
            var pagedPublications = _publications!.Skip((page - 1) * _settings!.Value.PageSize).Take(_settings!.Value.PageSize).ToList();
            /* FORMING VIEW MODEL */
            var model = new FeedViewModel
            {
                FeedPageViewModel =
                    new FeedPageViewModel(count, page, _settings!.Value.PageSize, FeedAction.UserPublications),
                FeedRateViewModel = new FeedRateViewModel(),
                FeedSortViewModel = new FeedSortViewModel(sortOrder),
                FeedFilterViewModel =
                    new FeedFilterViewModel(tg, q, mnr, mxr, _oldestCreationDate, _newestCreationDate),
                FeedModerateViewModel = new FeedModerateViewModel(),
                Publications = pagedPublications,
            };
            ViewBag.Title = userName;
            ViewBag.Page = "UserPublications";
            return View("UserPublications", model);
        }
        
        /*TODO COMMENTS */
        [HttpGet, AllowAnonymous]
        [Route("comments")]
        public IActionResult Comments()
        {
            return View();
        }
        
        /* ADD COMMENT */
        [HttpPost, Authorize, ValidateAntiForgeryToken]
        [ExportModelState]
        [Route("story/{urlSlug}#add-comment")]
        public async Task<IActionResult> AddComment(FeedViewModel model, string? urlSlug, int repl)
        {
            if (model.FeedAddCommentViewModel!.Content == null && model.FeedAddCommentViewModel.Uploads == null)
            {
                return RedirectToAction("Publication", "Feed", new { urlSlug = urlSlug, er = 1, repl = repl}, "add-comment");
            }
            var author = await _db.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity!.Name);
            if (author == null) RedirectToAction("Index", "Feed");
            List<Media> uploadedMedia = new List<Media>();
            /* COMMENT */
            var comment = new Comment
            {
                UserId = author!.Id,
                PublicationId = model.FeedAddCommentViewModel.Publication,
                ParentalCommentId = model.FeedAddCommentViewModel.ParentalComment,
                Status = 1
            };
            if (model.FeedAddCommentViewModel.Content != null) comment.Content = Regex.Replace(model.FeedAddCommentViewModel.Content, @"\s+", " ").Trim();
            var createdComment = (await _db.Comments.AddAsync(comment)).Entity;
            /* MEDIA UPLOADING*/
            if (model.FeedAddCommentViewModel.Uploads != null)
            {
                var filesCounter = 0;
                foreach (var uploadedFile in model.FeedAddCommentViewModel.Uploads)
                {
                    if (filesCounter >= _settings!.Value.MaxFilesInPost) break;
                        string subPath = "/media/comment_img/" + DateTime.Today.Year + '/' + DateTime.Today.Month;
                        string filePath = subPath + '/' + createdComment.User.UserName + '_' + createdComment.Publication.UrlSlug + _random.Next() + ".png";
                        string path = _appEnvironment.WebRootPath + subPath;
                        DirectoryInfo directory = new DirectoryInfo(path);
                        if (!directory.Exists) directory.Create();
                        await using (var fileStream = new FileStream(_appEnvironment.WebRootPath + filePath, FileMode.Create))
                        {
                            await uploadedFile.CopyToAsync(fileStream);
                        }
                        Media file = new Media {FileLink = filePath};
                        uploadedMedia.Add((await _db.Media.AddAsync(file)).Entity);
                        filesCounter++;
                }
            }
            await _db.SaveChangesAsync();
            createdComment.Id = _db.Comments.FirstOrDefault(c => c.PublicationId == createdComment.Publication!.Id && c.User.Id == author.Id && c.Content == createdComment.Content)!.Id;
            /* LINK TO COM-MEDIA TABLE */
            if (uploadedMedia.Count > 0)
            {
                foreach (var media in uploadedMedia)
                {

                    var commentMedia = new CommentMedia()
                    {
                        CommentId = createdComment.Id,
                        MediaId =(await _db.Media.FirstOrDefaultAsync(m => m.FileLink == media.FileLink)).Id
                    };
                    await _db.CommentMedia.AddAsync(commentMedia);
                    await _db.SaveChangesAsync();
                }
            }
            return RedirectToAction("Publication", "Feed", new { urlSlug = urlSlug}, "comment_" + createdComment.Id);
        }
        
        /* ADD POST */
        [HttpGet, Authorize]
        [ImportModelState]
        [Route("add_post")]
        public IActionResult AddPost()
        {
            var model = TempData["AddPostViewModel"] == null ? new AddPostViewModel() : JsonConvert.DeserializeObject<AddPostViewModel>((string) TempData["AddPostViewModel"]);
            return View(model);
        }
        
        [HttpPost, Authorize, ValidateAntiForgeryToken]
        [ExportModelState]
        [Route("add_post")]
        public async Task<IActionResult> AddPost(AddPostViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.Content == null && model.Uploads == null)
            {
                ModelState.AddModelError("Content", "Добавьте содержимое публикации - текст или изображение.");
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
                UrlSlug = TextTransform.Slugify(model.Name!),
                Status = 1
            };
            if (model.Content != null) post.Content = Regex.Replace(model.Content, @"\s+", " ").Trim();
            var createdPost = (await _db.Publications.AddAsync(post)).Entity;
            /* TAGS */
            string?[] tags = model.Tags!.Split(',',_settings!.Value.MaxTagsInPost + 1, StringSplitOptions.RemoveEmptyEntries);
            var tagsCounter = 0;
            if (tags.Length > _settings.Value.MaxTagsInPost)
            {
                tags[_settings.Value.MaxTagsInPost] = null;
            }
            for (var i = 0; i < tags.Length; i++)
            {
                if (tags[i] == null) continue;
                tags[i] = TextTransform.CheckTag(tags[i]!);
                if (tags[i] != null) tagsCounter++;
            }
            if (tagsCounter == 0)
            {
                ModelState.AddModelError("Tags", "Введите хотя бы одну метку.");
                return View(model);
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
            if (pickedTags.Count == 0)
            {
                ModelState.AddModelError("Tags", "Введите хотя бы одну метку.");
                return View(model);
            }
            /* MEDIA UPLOADING*/
            if (model.Uploads != null)
            {
                var filesCounter = 0;
                foreach (var uploadedFile in model.Uploads)
                {
                    if (!(uploadedFile.Length <= _settings.Value.MaxFileSize))
                    {
                        ModelState.AddModelError("Uploads", "Один или несколько файлов превысили максимальный размер и не были загружены.");
                        continue;
                    }
                    if (filesCounter >= _settings.Value.MaxFilesInPost) break;
                        string subPath = "/media/post_img/" + DateTime.Today.Year + '/' + DateTime.Today.Month;
                        string filePath = subPath + '/' + createdPost.UrlSlug + _random.Next() + ".png";
                        string path = _appEnvironment.WebRootPath + subPath;
                        DirectoryInfo directory = new DirectoryInfo(path);
                        if (!directory.Exists) directory.Create();
                        await using (var fileStream = new FileStream(_appEnvironment.WebRootPath + filePath, FileMode.Create))
                        {
                            await uploadedFile.CopyToAsync(fileStream);
                        }
                        Media file = new Media {FileLink = filePath};
                        uploadedMedia.Add((await _db.Media.AddAsync(file)).Entity);
                        filesCounter++;
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
            TempData["AddPostViewModel"] = JsonConvert.SerializeObject(model);
            return RedirectToAction("AddPost", "Feed");
        }
        
        /* EDIT POST */
        [HttpGet, Authorize]
        [ImportModelState]
        [Route("edit_post/{urlSlug}")]
        public IActionResult EditPost(string urlSlug)
        {
            var model = new EditPostViewModel();
            var editablePublication = _db.Publications.FirstOrDefaultAsync(publication => publication.UrlSlug == urlSlug).Result;
            if (!(User.IsInRole("Moderator") || User.Identity!.Name == editablePublication.User.UserName &&
                DateTime.Now - editablePublication.CreationDate <= TimeSpan.FromHours(1)))
            {
                return RedirectToAction("Index", "Feed");
            }
            var tagCounter = 1;
            model.Id = editablePublication.Id;
            model.Name = editablePublication.Name;
            model.Content = editablePublication.Content;
            foreach (var tag in editablePublication.PublicationTags)
            {
                if (tagCounter < editablePublication.PublicationTags.Count)
                {
                    model.OldTags += tag.Tag.Name + ", ";
                    tagCounter++;
                }
                else
                {
                    model.OldTags += tag.Tag.Name;
                }
            }
            model.NewTags = model.OldTags;
            model.OldUploads = new List<PublicationMedia>();
            foreach (var upload in editablePublication.PublicationMedia)
            {
                model.OldUploads?.Add(upload);
            }
            return View("EditPost", model);
        }
        
        [HttpPost, Authorize, ValidateAntiForgeryToken]
        [ExportModelState]
        [Route("edit_post/{urlSlug}")]
        public async Task<IActionResult> EditPost(EditPostViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var editablePublication = _db.Publications.FirstOrDefaultAsync(publication => publication.Id == model.Id).Result;
            if (!(User.IsInRole("Moderator") || User.Identity!.Name == editablePublication.User.UserName &&
                DateTime.Now - editablePublication.CreationDate <= TimeSpan.FromHours(1)))
            {
                return RedirectToAction("Index", "Feed");
            }
            var oldSlug = editablePublication.UrlSlug;
            model.OldUploads = new List<PublicationMedia>();
            foreach (var upload in editablePublication.PublicationMedia)
            {
                model.OldUploads?.Add(upload);
            }
            List<Tag> pickedTags = new List<Tag>();
            List<Tag> allTags = _db.Tags.ToListAsync().Result;
            List<Media> uploadedMedia = new List<Media>();
            if (model.Content == null && (model.SavedUploads?.Count == 0 || model.SavedUploads == null) && (model.NewUploads?.Count == 0 || model.NewUploads == null))
            {
                ModelState.AddModelError("Content", "Добавьте содержимое публикации - текст или изображение.");
                return RedirectToAction("EditPost", "Feed");
            }
            if (editablePublication.Name != model.Name)
            {
                editablePublication.Name = model.Name;
                editablePublication.UrlSlug = TextTransform.Slugify(model.Name);
            }
            editablePublication.Content = model.Content;
            _db.Publications.Update(editablePublication);
            /* TAGS */
            string?[] oldTags = model.OldTags!.Split(',',_settings!.Value.MaxTagsInPost + 1, StringSplitOptions.RemoveEmptyEntries);
            string?[] newTags = model.NewTags!.Split(',',_settings!.Value.MaxTagsInPost + 1, StringSplitOptions.RemoveEmptyEntries);
            if (newTags != oldTags)
            {
                foreach(var tag in _db.PublicationTags.Where(tag => tag.PublicationId == editablePublication.Id))
                {
                    _db.PublicationTags.Remove(tag);
                } 
                var tagsCounter = 0;
                if (newTags.Length > _settings.Value.MaxTagsInPost)
                {
                    newTags[_settings.Value.MaxTagsInPost] = null;
                }
                for (var i = 0; i < newTags.Length; i++)
                {
                    if (newTags[i] == null) continue;
                    newTags[i] = TextTransform.CheckTag(newTags[i]!);
                    if (newTags[i] != null) tagsCounter++;
                }
                if (tagsCounter == 0)
                {
                    ModelState.AddModelError("Tags", "Введите хотя бы одну метку.");
                    return View(model);
                }
                foreach (var tag in newTags)
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
                if (pickedTags.Count == 0)
                {
                    ModelState.AddModelError("Tags", "Введите хотя бы одну метку.");
                }
            }
            /* MEDIA UPLOADING AND DELETING */
            var filesCounter = 0;
            string subPath = "/media/post_img/" + DateTime.Today.Year + '/' + DateTime.Today.Month;
            string path = _appEnvironment.WebRootPath + subPath;
            if (oldSlug != editablePublication.UrlSlug)
            {
                foreach (var media in editablePublication.PublicationMedia)
                {
                    var newSlug = subPath + '/' + editablePublication.UrlSlug + _random.Next() + ".png";
                    var newMedia = media.Media;
                    FileInfo file = new FileInfo(_appEnvironment.WebRootPath + newMedia.FileLink);
                    file.MoveTo(_appEnvironment.WebRootPath + newSlug);
                    newMedia.FileLink = newSlug;
                    _db.Media.Update(newMedia);
                }
            }
            if (model.SavedUploads?.Count < model.OldUploads?.Count || model.SavedUploads == null)
            {
                if (model.OldUploads != null)
                    foreach (var oldUpload in model.OldUploads)
                    {
                        if (model.SavedUploads != null && model.SavedUploads.Contains(oldUpload.MediaId))
                        {
                            filesCounter++;
                            continue;
                        }

                        _db.PublicationMedia.Remove(oldUpload);
                        var deletedMedia = oldUpload.Media;
                        FileInfo file = new FileInfo(_appEnvironment.WebRootPath + deletedMedia.FileLink);
                        file.Delete();
                        deletedMedia.FileLink = "/media/img-placeholder.webp";
                        _db.Media.Update(deletedMedia);
                    }
            }
            if (model.NewUploads != null)
            {
                foreach (var uploadedFile in model.NewUploads)
                {
                    if (!(uploadedFile.Length <= _settings!.Value.MaxFileSize))
                    {
                        ModelState.AddModelError("Uploads", "Один или несколько файлов превысили максимальный размер или количество и не были загружены.");
                        continue;
                    }
                    if (filesCounter >= _settings.Value.MaxFilesInPost) break;
                    DirectoryInfo directory = new DirectoryInfo(path);
                    if (!directory.Exists) directory.Create();
                    string filePath = subPath + '/' + editablePublication.UrlSlug + _random.Next() + ".png";
                    await using (var fileStream = new FileStream(_appEnvironment.WebRootPath + filePath, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    Media file = new Media {FileLink = filePath};
                    uploadedMedia.Add((await _db.Media.AddAsync(file)).Entity);
                    filesCounter++;
                }
            }
            if (!(User.IsInRole("Moderator") || User.Identity!.Name == editablePublication.User.UserName &&
                DateTime.Now - editablePublication.CreationDate <= TimeSpan.FromHours(1)))
            {
                return RedirectToAction("Index", "Feed");
            }
            await _db.SaveChangesAsync();
            /* LINK TO PUB-TAG TABLE */
            if (pickedTags.Count > 0)
            {
                foreach (var tag in pickedTags)
                {
                    var publicationTag = new PublicationTag()
                    {
                        PublicationId = editablePublication.Id,
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
                        PublicationId = editablePublication.Id,
                        MediaId =(await _db.Media.FirstOrDefaultAsync(m => m.FileLink == media.FileLink)).Id
                    };
                    await _db.PublicationMedia.AddAsync(publicationMedia);
                    await _db.SaveChangesAsync();
                }
            }
            return RedirectToAction("Publication", "Feed", new { urlSlug = editablePublication.UrlSlug});
        }

        /* SET SORT ORDER */
        [AllowAnonymous]
        private static List<Publication> SetSortOrder(List<Publication> publications, SortState sortOrder)
        {
            publications = new List<Publication>(sortOrder switch
            {
                SortState.PublicationNameAsc => publications!.OrderBy(s => s.Name),
                SortState.PublicationNameDesc => publications!.OrderByDescending(s => s.Name),
                SortState.PublicationAgeAsc => publications!.OrderBy(s => s.CreationDate),
                SortState.PublicationAgeDesc => publications!.OrderByDescending(s => s.CreationDate),
                SortState.PublicationRatingAsc => publications!.OrderBy(s => s.RatingTotal),
                SortState.PublicationRatingDesc => publications!.OrderByDescending(s => s.RatingTotal),
                _ => publications!.OrderBy(s => s.Name)
            });
            return publications;
        }
        
        /* SET FILTER */
        [AllowAnonymous]
        private static List<Publication> SetFilter(List<Publication> publications, string q, int mnr, int mxr, string ocd, string ncd, string tg)
        {
            var pickedTags = new List<Tag>();
            if (mnr < mxr)
            {
                publications = publications.Where(publication => publication.RatingTotal >= mnr).ToList();
                publications = publications.Where(publication => publication.RatingTotal <= mxr).ToList();
            }
            if (!string.IsNullOrEmpty(q))
            {
                publications = publications.Where(publication => publication.Name.ToUpper().Contains(q.ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(ocd))
            {
                publications = publications.Where(publication => publication.CreationDate >= DateTime.Parse(ocd).AddDays(-1))
                    .ToList();
            }
            if (!string.IsNullOrEmpty(ncd))
            {
                publications = publications.Where(publication => publication.CreationDate <= DateTime.Parse(ncd).AddDays(1))
                    .ToList();
            }

            if (string.IsNullOrEmpty(tg)) return publications;
            {
                string?[] tags = tg.Split(',',_settings!.Value.MaxTagsInPost + 1, StringSplitOptions.RemoveEmptyEntries);
                var tagsCounter = 0;
                if (tags.Length > _settings.Value.MaxTagsInPost)
                {
                    tags[_settings.Value.MaxTagsInPost] = null;
                }
                for (var counter = 0; counter < tags.Length; counter++)
                {
                    tags[counter] = TextTransform.CheckTag(tags[counter]!);
                    if (tags[counter] == null) continue;
                    var pickedTag = _db.Tags.FirstOrDefaultAsync(tag => tag.Name == tags[counter]).Result ?? new Tag
                    {
                        Name = tags[counter],
                        TagRatingCounting = false
                    };
                    pickedTags.Add(pickedTag);
                    tagsCounter++;
                }
                if (tagsCounter == 0) return publications;
            }
            return pickedTags.Aggregate(publications, (current, tag) => current.Where(publication => publication.PublicationTags.Any(publicationTag => publicationTag.Tag == tag)).ToList());
        }

        /* SET FAVORITE */
        [Authorize]
        public async Task<Action?> PublicationSetFavorite(FeedRateViewModel viewModel)
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user == null) return null;
            // SetFavorite
            if (!viewModel.PublicationSetFavorite.HasValue) return null;
            {
                var publication =
                    await _db.Publications.FirstOrDefaultAsync(p =>
                        p.Id == (int) viewModel.PublicationSetFavorite);
                var favorite = await _db.Favorites.FirstOrDefaultAsync(f =>
                    User.Identity != null && f.PublicationId == publication.Id &&
                    f.User.UserName == User.Identity.Name);
                // Check favorites For Null and remove from favorites if already added
                if (favorite != null)
                {
                    _db.Favorites.Remove(favorite);
                    await _db.SaveChangesAsync();
                }
                // Add to favorite
                else
                {
                    favorite = new Favorite
                    {
                        UserId = user.Id,
                        PublicationId = publication.Id
                    };
                    await _db.Favorites.AddAsync(favorite);
                    await _db.SaveChangesAsync();
                }
            }
            return null;
        }

        /* RATE PUBLICATION */
        [Authorize]
        public async Task<Action?> PublicationRate(FeedRateViewModel viewModel)
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user == null) return null;
            var isCounted = true;
            // RateUp
            if (viewModel.PublicationRateUp.HasValue)
            {
                var publication = await _db.Publications.FirstOrDefaultAsync(p => p.Id == (int) viewModel.PublicationRateUp);
                var tags = publication.PublicationTags.ToList();
                if (tags.Any(tag => tag.Tag.TagRatingCounting == false))
                {
                    isCounted = false;
                }
                var publicationAuthor = await _db.Users.FirstOrDefaultAsync(u => u.Id == publication.UserId);
                var tally = _db.PublicationRatingTallies.FirstOrDefault(t => User.Identity != null && t.PublicationId == publication.Id && t.User.UserName == User.Identity.Name);
                // Check Tally For Null
                if (tally != null)
                {
                    // Remove RateUp if Already Rated
                    if (tally.PublicationRating)
                    {
                        publication.RatingTotal -= 1;
                        if (isCounted)
                        {
                            publicationAuthor.RatingTotal -= 1;
                        }
                        _db.Publications.Update(publication);
                        _db.Users.Update(publicationAuthor);
                        _db.PublicationRatingTallies.Remove(tally);
                        await _db.SaveChangesAsync();
                    }
                    // Change RateDown for RateUp
                    else
                    {
                        publication.RatingTotal += 2;
                        if (isCounted)
                        {
                            publicationAuthor.RatingTotal += 2;
                        }
                        tally.PublicationRating = true;
                        _db.Publications.Update(publication);
                        _db.Users.Update(publicationAuthor);
                        _db.PublicationRatingTallies.Update(tally);
                        await _db.SaveChangesAsync();
                    }
                }
                // Add RateUp
                else
                {
                    tally = new PublicationRatingTally
                    {
                        UserId = user.Id,
                        PublicationId = publication.Id,
                        PublicationRating = true
                    };
                    publication.RatingTotal += 1;
                    if (isCounted)
                    {
                        publicationAuthor.RatingTotal += 1;
                    }
                    _db.Users.Update(publicationAuthor);
                    await _db.PublicationRatingTallies.AddAsync(tally);
                    await _db.SaveChangesAsync();
                }
            }
            // RateDown
            else if (viewModel.PublicationRateDown.HasValue)
            {
                var publication = await _db.Publications.FirstOrDefaultAsync(p => p.Id == (int) viewModel.PublicationRateDown);
                var tags = publication.PublicationTags.ToList();
                if (tags.Any(tag => tag.Tag.TagRatingCounting == false))
                {
                    isCounted = false;
                }
                var publicationAuthor = await _db.Users.FirstOrDefaultAsync(u => u.Id == publication.UserId);
                var tally = _db.PublicationRatingTallies.FirstOrDefault(t => User.Identity != null && t.PublicationId == publication.Id && t.User.UserName == User.Identity.Name);
                // Check Tally For Null
                if (tally != null)
                {
                    // Remove RateDown if Already Rated
                    if (tally.PublicationRating == false)
                    {
                        publication.RatingTotal += 1;
                        if (isCounted)
                        {
                            publicationAuthor.RatingTotal += 1;
                        }
                        _db.Publications.Update(publication);
                        _db.Users.Update(publicationAuthor);
                        _db.PublicationRatingTallies.Remove(tally);
                        await _db.SaveChangesAsync();
                    }
                    // Change RateUp for RateDown
                    else
                    {
                        publication.RatingTotal -= 2;
                        if (isCounted)
                        {
                            publicationAuthor.RatingTotal -= 2;
                        }
                        tally.PublicationRating = false;
                        _db.Publications.Update(publication);
                        _db.Users.Update(publicationAuthor);
                        _db.PublicationRatingTallies.Update(tally);
                        await _db.SaveChangesAsync();
                    }
                }
                // Add RateDown
                else
                {
                    tally = new PublicationRatingTally
                    {
                        UserId = user.Id,
                        PublicationId = publication.Id,
                        PublicationRating = false
                    };
                    publication.RatingTotal -= 1;
                    if (isCounted)
                    {
                        publicationAuthor.RatingTotal -= 1;
                    }
                    _db.Users.Update(publicationAuthor);
                    await _db.PublicationRatingTallies.AddAsync(tally);
                    await _db.SaveChangesAsync();
                }
            }
            return null;
        }
        /* RATE COMMENT- */
        [Authorize]
        public async Task<Action?> CommentRate(FeedRateViewModel viewModel)
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user == null) return null;
            var isCounted = true;
            // RateUp
            if (viewModel.CommentRateUp.HasValue)
            {
                var comment = await _db.Comments.FirstOrDefaultAsync(p => p.Id == (int) viewModel.CommentRateUp);
                var commentAuthor = await _db.Users.FirstOrDefaultAsync(u => u.Id == comment.UserId);
                var tally = _db.CommentRatingTallies.FirstOrDefault(t => User.Identity != null && t.CommentId == comment.Id && t.User.UserName == User.Identity.Name);
                // Check Tally For Null
                if (tally != null)
                {
                    // Remove RateUp if Already Rated
                    if (tally.Rating)
                    {
                        comment.RatingTotal -= 1;
                        if (isCounted)
                        {
                            commentAuthor.RatingTotal -= 1;
                        }
                        _db.Comments.Update(comment);
                        _db.Users.Update(commentAuthor);
                        _db.CommentRatingTallies.Remove(tally);
                        await _db.SaveChangesAsync();
                    }
                    // Change RateDown for RateUp
                    else
                    {
                        comment.RatingTotal += 2;
                        if (isCounted)
                        {
                            commentAuthor.RatingTotal += 2;
                        }
                        tally.Rating = true;
                        _db.Comments.Update(comment);
                        _db.Users.Update(commentAuthor);
                        _db.CommentRatingTallies.Update(tally);
                        await _db.SaveChangesAsync();
                    }
                }
                // Add RateUp
                else
                {
                    tally = new CommentRatingTally()
                    {
                        UserId = user.Id,
                        CommentId = comment.Id,
                        Rating = true
                    };
                    comment.RatingTotal += 1;
                    if (isCounted)
                    {
                        commentAuthor.RatingTotal += 1;
                    }
                    _db.Users.Update(commentAuthor);
                    await _db.CommentRatingTallies.AddAsync(tally);
                    await _db.SaveChangesAsync();
                }
            }
            // RateDown
            else if (viewModel.CommentRateDown.HasValue)
            {
                var comment = await _db.Comments.FirstOrDefaultAsync(p => p.Id == (int) viewModel.CommentRateDown);
                var commentAuthor = await _db.Users.FirstOrDefaultAsync(u => u.Id == comment.UserId);
                var tally = _db.CommentRatingTallies.FirstOrDefault(t => User.Identity != null && t.CommentId == comment.Id && t.User.UserName == User.Identity.Name);
                // Check Tally For Null
                if (tally != null)
                {
                    // Remove RateDown if Already Rated
                    if (tally.Rating == false)
                    {
                        comment.RatingTotal += 1;
                        if (isCounted)
                        {
                            commentAuthor.RatingTotal += 1;
                        }
                        _db.Comments.Update(comment);
                        _db.Users.Update(commentAuthor);
                        _db.CommentRatingTallies.Remove(tally);
                        await _db.SaveChangesAsync();
                    }
                    // Change RateUp for RateDown
                    else
                    {
                        comment.RatingTotal -= 2;
                        if (isCounted)
                        {
                            commentAuthor.RatingTotal -= 2;
                        }
                        tally.Rating = false;
                        _db.Comments.Update(comment);
                        _db.Users.Update(commentAuthor);
                        _db.CommentRatingTallies.Update(tally);
                        await _db.SaveChangesAsync();
                    }
                }
                // Add RateDown
                else
                {
                    tally = new CommentRatingTally
                    {
                        UserId = user.Id,
                        CommentId = comment.Id,
                        Rating = false
                    };
                    comment.RatingTotal -= 1;
                    if (isCounted)
                    {
                        commentAuthor.RatingTotal -= 1;
                    }
                    _db.Users.Update(commentAuthor);
                    await _db.CommentRatingTallies.AddAsync(tally);
                    await _db.SaveChangesAsync();
                }
            }
            return null;
        }
    }
}
using System;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Onym.Models;

#nullable disable

namespace Onym.Data
{
    public class OnymDbContext<TUser> : IdentityDbContext<
        TUser, IdentityRole<int>, int, IdentityUserClaim<int>, IdentityUserRole<int>,
        IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
        where TUser : IdentityUser<int>
    {
        public OnymDbContext()
        {
        }

        public OnymDbContext(DbContextOptions<OnymDbContext<User>> options)
            : base(options)
        {
        }
        
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<CommentMedia> CommentMedia { get; set; }
        public virtual DbSet<CommentRatingTally> CommentRatingTallies { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<Publication> Publications { get; set; }
        public virtual DbSet<PublicationMedia> PublicationMedia { get; set; }
        public virtual DbSet<PublicationRatingTally> PublicationRatingTallies { get; set; }
        public virtual DbSet<PublicationTag> PublicationTags { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public new virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserStatus> UserStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            optionsBuilder
                .ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning))
                .UseLazyLoadingProxies()
                .UseNpgsql(configurationBuilder.Build().GetConnectionString("OnymDb"));
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("Relational:Collation", "Russian_Russia.1251")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                    .HasColumnName("id");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("text")
                    .HasColumnName("role_concurrency_stamp");

                b.Property<string>("Name")
                    .HasMaxLength(256)
                    .HasColumnType("character varying(256)")
                    .HasColumnName("role_name");

                b.Property<string>("NormalizedName")
                    .HasMaxLength(256)
                    .HasColumnType("character varying(256)")
                    .HasColumnName("role_normalized_name");

                b.HasKey("Id");

                b.HasIndex("NormalizedName")
                    .IsUnique()
                    .HasDatabaseName("IX_role_name");

                b.ToTable("roles");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                    .HasColumnName("role_id");

                b.Property<string>("ClaimType")
                    .HasColumnType("text")
                    .HasColumnName("role_claim_type");

                b.Property<string>("ClaimValue")
                    .HasColumnType("text")
                    .HasColumnName("role_claim_value");

                b.Property<int>("RoleId")
                    .HasColumnType("integer")
                    .HasColumnName("role_id");

                b.HasKey("Id");

                b.HasIndex(new[] {"RoleId"}, "IX_role_id");

                b.ToTable("role_claims");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                    .HasColumnName("user_claim_id");

                b.Property<string>("ClaimType")
                    .HasColumnType("text")
                    .HasColumnName("user_claim_type");

                b.Property<string>("ClaimValue")
                    .HasColumnType("text")
                    .HasColumnName("user_claim_value");

                b.Property<int>("UserId")
                    .HasColumnType("integer")
                    .HasColumnName("user_id");

                b.HasKey("Id");

                b.HasIndex(new[] {"UserId"}, "IX_user_id");

                b.ToTable("user_claims");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
            {
                b.Property<string>("LoginProvider")
                    .HasColumnType("text")
                    .HasColumnName("login_provider");

                b.Property<string>("ProviderKey")
                    .HasColumnType("text")
                    .HasColumnName("provider_key");

                b.Property<string>("ProviderDisplayName")
                    .HasColumnType("text")
                    .HasColumnName("provider_display_name");

                b.Property<int>("UserId")
                    .HasColumnType("integer")
                    .HasColumnName("user_id");

                b.HasKey("LoginProvider", "ProviderKey");

                b.HasIndex(new[] {"UserId"}, "IX_user_id");

                b.ToTable("user_logins");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
            {
                b.Property<int>("UserId")
                    .HasColumnType("integer")
                    .HasColumnName("user_id");

                b.Property<int>("RoleId")
                    .HasColumnType("integer")
                    .HasColumnName("role_id");

                b.HasKey("UserId", "RoleId");

                b.HasIndex(new[] {"RoleId"}, "IX_role_id");

                b.ToTable("user_roles");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
            {
                b.Property<int>("UserId")
                    .HasColumnType("integer")
                    .HasColumnName("user_id");

                b.Property<string>("LoginProvider")
                    .HasColumnType("text")
                    .HasColumnName("login_provider");

                b.Property<string>("Name")
                    .HasColumnType("text")
                    .HasColumnName("user_token_name");

                b.Property<string>("Value")
                    .HasColumnType("text")
                    .HasColumnName("user_token_value");

                b.HasKey("UserId", "LoginProvider", "Name");

                b.ToTable("user_tokens");
            });

            modelBuilder.Entity("Onym.Models.Comment", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasColumnName("comment_id")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                b.Property<string>("Content")
                    .HasMaxLength(500)
                    .HasColumnType("character varying(500)")
                    .HasColumnName("comment_content");

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("comment_creation_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<int?>("ParentalCommentId")
                    .HasColumnType("integer")
                    .HasColumnName("parental_comment_id");

                b.Property<int>("PublicationId")
                    .HasColumnType("integer")
                    .HasColumnName("publication_id");

                b.Property<int>("RatingTotal")
                    .HasColumnType("integer")
                    .HasColumnName("comment_rating_total")
                    .HasDefaultValueSql("0");

                b.Property<int>("Status")
                    .HasColumnType("integer")
                    .HasColumnName("comment_status")
                    .HasDefaultValueSql("1");

                b.Property<int>("UserId")
                    .HasColumnType("integer")
                    .HasColumnName("user_id");

                b.HasKey("Id");

                b.HasIndex(new[] {"Status"}, "IX_comments_comment_status");

                b.HasIndex(new[] {"ParentalCommentId"}, "IX_parental_comment_id");

                b.HasIndex(new[] {"PublicationId"}, "IX_publication_id");

                b.HasIndex(new[] {"UserId"}, "IX_user_id");

                b.ToTable("comments");
            });

            modelBuilder.Entity("Onym.Models.CommentMedia", b =>
            {
                b.Property<int>("CommentId")
                    .HasColumnType("integer")
                    .HasColumnName("comment_id");

                b.Property<int>("MediaId")
                    .HasColumnType("integer")
                    .HasColumnName("media_id");

                b.HasKey("CommentId", "MediaId");

                b.HasIndex(new[] {"MediaId"}, "IX_comment_media_media_id");

                b.ToTable("comment_media");
            });

            modelBuilder.Entity("Onym.Models.CommentRatingTally", b =>
            {
                b.Property<int>("UserId")
                    .HasColumnType("integer")
                    .HasColumnName("user_id");

                b.Property<int>("CommentId")
                    .HasColumnType("integer")
                    .HasColumnName("comment_id");

                b.Property<bool>("Rating")
                    .HasColumnType("boolean")
                    .HasColumnName("comment_rating");

                b.HasKey("UserId", "CommentId");

                b.HasIndex(new[] {"CommentId"}, "IX_comment_rating_tally_comment_id");

                b.ToTable("comment_rating_tally");
            });

            modelBuilder.Entity("Onym.Models.Favorite", b =>
            {
                b.Property<int>("UserId")
                    .HasColumnType("integer")
                    .HasColumnName("user_id");

                b.Property<int>("PublicationId")
                    .HasColumnType("integer")
                    .HasColumnName("publication_id");

                b.HasKey("UserId", "PublicationId");

                b.HasIndex(new[] {"PublicationId"}, "IX_favorite_publication_id");

                b.ToTable("favorite");
            });

            modelBuilder.Entity("Onym.Models.Media", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasColumnName("media_id")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                b.Property<string>("FileLink")
                    .HasColumnType("text")
                    .HasColumnName("file_link");

                b.HasKey("Id");

                b.ToTable("media");
            });

            modelBuilder.Entity("Onym.Models.Publication", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasColumnName("publication_id")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                b.Property<string>("Content")
                    .HasColumnType("varchar")
                    .HasColumnName("publication_content");

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("publication_creation_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("character varying(100)")
                    .HasColumnName("publication_name");
                
                b.Property<string>("UrlSlug")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("character varying(100)")
                    .HasColumnName("publication_url_slug");

                b.Property<int>("RatingTotal")
                    .HasColumnType("integer")
                    .HasColumnName("publication_rating_total")
                    .HasDefaultValueSql("0");

                b.Property<int>("Status")
                    .HasColumnType("integer")
                    .HasColumnName("publication_status")
                    .HasDefaultValueSql("1");

                b.Property<int>("UserId")
                    .HasColumnType("integer")
                    .HasColumnName("user_id");

                b.HasKey("Id");

                b.HasIndex(new[] {"UserId"}, "IX_publication_author");

                b.HasIndex(new[] {"Status"}, "IX_publication_status");
                
                b.HasIndex(new[] {"UrlSlug"}, "IX_publication_url_slug")
                    .IsUnique();

                b.ToTable("publications");
            });

            modelBuilder.Entity("Onym.Models.PublicationMedia", b =>
            {
                b.Property<int>("PublicationId")
                    .HasColumnType("integer")
                    .HasColumnName("publication_id");

                b.Property<int>("MediaId")
                    .HasColumnType("integer")
                    .HasColumnName("media_id");

                b.HasKey("PublicationId", "MediaId");

                b.HasIndex(new[] {"MediaId"}, "IX_publication_media_media_id");

                b.ToTable("publication_media");
            });

            modelBuilder.Entity("Onym.Models.PublicationRatingTally", b =>
            {
                b.Property<int>("UserId")
                    .HasColumnType("integer")
                    .HasColumnName("user_id");

                b.Property<int>("PublicationId")
                    .HasColumnType("integer")
                    .HasColumnName("publication_id");

                b.Property<bool>("PublicationRating")
                    .HasColumnType("boolean")
                    .HasColumnName("publication_rating");

                b.HasKey("UserId", "PublicationId");

                b.HasIndex(new[] {"PublicationId"}, "IX_publication_rating_tally_publication_id");

                b.ToTable("publication_rating_tally");
            });

            modelBuilder.Entity("Onym.Models.PublicationTag", b =>
            {
                b.Property<int>("TagId")
                    .HasColumnType("integer")
                    .HasColumnName("tag_id");

                b.Property<int>("PublicationId")
                    .HasColumnType("integer")
                    .HasColumnName("publication_id");

                b.HasKey("TagId", "PublicationId");

                b.HasIndex(new[] {"PublicationId"}, "IX_publication_tags_publication_id");

                b.ToTable("publication_tags");
            });

            modelBuilder.Entity("Onym.Models.Status", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasColumnName("status_id")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("character varying(50)")
                    .HasColumnName("status_name");

                b.HasKey("Id");

                b.ToTable("statuses");
            });

            modelBuilder.Entity("Onym.Models.Tag", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasColumnName("tag_id")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("character varying(20)")
                    .HasColumnName("tag_name");

                b.Property<bool?>("TagRatingCounting")
                    .IsRequired()
                    .ValueGeneratedOnAdd()
                    .HasColumnType("boolean")
                    .HasColumnName("tag_rating_counting")
                    .HasDefaultValueSql("true");

                b.HasKey("Id");

                b.HasIndex(new[] {"Name"}, "IX_tag_name")
                    .IsUnique();

                b.ToTable("tags");
            });

            modelBuilder.Entity("Onym.Models.User", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasColumnName("user_id")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                b.Property<int>("AccessFailedCount")
                    .HasColumnType("integer")
                    .HasColumnName("user_access_failed_tally");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("text")
                    .HasColumnName("user_concurrency_stamp");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("character varying(50)")
                    .HasColumnName("user_email");

                b.Property<bool>("EmailConfirmed")
                    .HasColumnType("boolean")
                    .HasColumnName("user_email_confirmed")
                    .HasDefaultValueSql("false");

                b.Property<bool>("LockoutEnabled")
                    .HasColumnType("boolean")
                    .HasColumnName("user_lockout_enabled")
                    .HasDefaultValueSql("false");

                b.Property<DateTimeOffset?>("LockoutEnd")
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("user_lockout_end");

                b.Property<string>("NormalizedEmail")
                    .HasMaxLength(256)
                    .HasColumnType("character varying(256)")
                    .HasColumnName("normalized_user_email");

                b.Property<string>("NormalizedUserName")
                    .HasMaxLength(256)
                    .HasColumnType("character varying(256)")
                    .HasColumnName("normalized_user_name");
                
                b.Property<int>("ProfilePicture")
                    .HasColumnType("integer")
                    .HasColumnName("user_profile_picture")
                    .HasDefaultValueSql("1");
                
                b.Property<string>("PasswordHash")
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("user_password_hash");

                b.Property<string>("PhoneNumber")
                    .HasColumnType("text")
                    .HasColumnName("user_phone_number");

                b.Property<bool>("PhoneNumberConfirmed")
                    .HasColumnType("boolean")
                    .HasColumnName("user_phone_number_confirmed")
                    .HasDefaultValueSql("false");

                b.Property<int>("RatingTotal")
                    .HasColumnType("integer")
                    .HasColumnName("user_rating_total")
                    .HasDefaultValueSql("0");

                b.Property<DateTime>("RegistrationDate")
                    .ValueGeneratedOnAdd()
                    .HasPrecision(2)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("user_registration_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<string>("SecurityStamp")
                    .HasColumnType("text")
                    .HasColumnName("user_security_stamp");

                b.Property<bool>("TwoFactorEnabled")
                    .HasColumnType("boolean")
                    .HasColumnName("user_two_factors_enabled")
                    .HasDefaultValueSql("false");
                    

                b.Property<string>("UserName")
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("character varying(20)")
                    .HasColumnName("user_name");

                b.HasKey("Id");
                
                b.HasOne("Onym.Models.Media", "UserProfilePictureNavigation")
                    .WithMany("Users")
                    .HasForeignKey("ProfilePicture")
                    .HasConstraintName("FK_profile_picture")
                    .IsRequired();

                b.HasIndex("NormalizedEmail")
                    .HasDatabaseName("IX_user_email");

                b.HasIndex("NormalizedUserName")
                    .IsUnique()
                    .HasDatabaseName("IX_user_name");
                
                b.HasIndex("ProfilePicture")
                    .HasDatabaseName("IX_user_profile_picture");

                b.HasIndex(new[] {"Email"}, "IX_user_email")
                    .IsUnique();

                b.HasIndex(new[] {"UserName"}, "IX_user_name")
                    .IsUnique();

                b.ToTable("users");
            });

            modelBuilder.Entity("Onym.Models.UserStatus", b =>
            {
                b.Property<int>("UserId")
                    .HasColumnType("integer")
                    .HasColumnName("user_id");

                b.Property<int>("StatusId")
                    .HasColumnType("integer")
                    .HasColumnName("status_id");

                b.Property<DateTime>("ExpirationTime")
                    .HasColumnType("timestamp with time zone");

                b.HasKey("UserId", "StatusId");

                b.HasIndex(new[] {"StatusId"}, "IX_user_statuses");

                b.ToTable("user_statuses");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .HasConstraintName("FK_role_id")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
            {
                b.HasOne("Onym.Models.User", null)
                    .WithMany("Claims")
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_user_claims")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
            {
                b.HasOne("Onym.Models.User", null)
                    .WithMany("Logins")
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_user_logins")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .HasConstraintName("FK_role_id")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Onym.Models.User", null)
                    .WithMany("UserRoles")
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_user_roles")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
            {
                b.HasOne("Onym.Models.User", null)
                    .WithMany("Tokens")
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_user_tokens")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Onym.Models.Comment", b =>
            {
                b.HasOne("Onym.Models.Comment", "ParentalComment")
                    .WithMany("InverseParentalComment")
                    .HasForeignKey("ParentalCommentId")
                    .HasConstraintName("FK_parental_comment");

                b.HasOne("Onym.Models.Publication", "Publication")
                    .WithMany("Comments")
                    .HasForeignKey("PublicationId")
                    .HasConstraintName("FK_comment-publication")
                    .IsRequired();

                b.HasOne("Onym.Models.Status", "CommentStatusNavigation")
                    .WithMany("Comments")
                    .HasForeignKey("Status")
                    .HasConstraintName("FK_comment_status")
                    .IsRequired();

                b.HasOne("Onym.Models.User", "User")
                    .WithMany("Comments")
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_comment_author")
                    .IsRequired();

                b.Navigation("CommentStatusNavigation");

                b.Navigation("ParentalComment");

                b.Navigation("Publication");

                b.Navigation("User");
            });

            modelBuilder.Entity("Onym.Models.CommentMedia", b =>
            {
                b.HasOne("Onym.Models.Comment", "Comment")
                    .WithMany("CommentMedia")
                    .HasForeignKey("CommentId")
                    .HasConstraintName("FK_comment-media")
                    .IsRequired();

                b.HasOne("Onym.Models.Media", "Media")
                    .WithMany("CommentMedia")
                    .HasForeignKey("MediaId")
                    .HasConstraintName("FK_media-comment")
                    .IsRequired();

                b.Navigation("Comment");

                b.Navigation("Media");
            });

            modelBuilder.Entity("Onym.Models.CommentRatingTally", b =>
            {
                b.HasOne("Onym.Models.Comment", "Comment")
                    .WithMany("CommentRatingTallies")
                    .HasForeignKey("CommentId")
                    .HasConstraintName("FK_comment-users_rating")
                    .IsRequired();

                b.HasOne("Onym.Models.User", "User")
                    .WithMany("CommentRatingTallies")
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_users-comment_rating")
                    .IsRequired();

                b.Navigation("Comment");

                b.Navigation("User");
            });

            modelBuilder.Entity("Onym.Models.Favorite", b =>
            {
                b.HasOne("Onym.Models.Publication", "Publication")
                    .WithMany("Favorites")
                    .HasForeignKey("PublicationId")
                    .HasConstraintName("FK_publications-users_favorite")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Onym.Models.User", "User")
                    .WithMany("Favorites")
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_users-publication_favorite")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Publication");

                b.Navigation("User");
            });

            modelBuilder.Entity("Onym.Models.Publication", b =>
            {
                b.HasOne("Onym.Models.Status", "PublicationStatusNavigation")
                    .WithMany("Publications")
                    .HasForeignKey("Status")
                    .HasConstraintName("FK_publication_status")
                    .IsRequired();

                b.HasOne("Onym.Models.User", "User")
                    .WithMany("Publications")
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_publication author")
                    .IsRequired();

                b.Navigation("PublicationStatusNavigation");

                b.Navigation("User");
            });

            modelBuilder.Entity("Onym.Models.PublicationMedia", b =>
            {
                b.HasOne("Onym.Models.Media", "Media")
                    .WithMany("PublicationMedia")
                    .HasForeignKey("MediaId")
                    .HasConstraintName("FK_media-publication")
                    .IsRequired();

                b.HasOne("Onym.Models.Publication", "Publication")
                    .WithMany("PublicationMedia")
                    .HasForeignKey("PublicationId")
                    .HasConstraintName("FK_publication-media")
                    .IsRequired();

                b.Navigation("Media");

                b.Navigation("Publication");
            });

            modelBuilder.Entity("Onym.Models.PublicationRatingTally", b =>
            {
                b.HasOne("Onym.Models.Publication", "Publication")
                    .WithMany("PublicationRatingTallies")
                    .HasForeignKey("PublicationId")
                    .HasConstraintName("FK_publications-users_rating")
                    .IsRequired();

                b.HasOne("Onym.Models.User", "User")
                    .WithMany("PublicationRatingTallies")
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_users-publications_rating")
                    .IsRequired();

                b.Navigation("Publication");

                b.Navigation("User");
            });

            modelBuilder.Entity("Onym.Models.PublicationTag", b =>
            {
                b.HasOne("Onym.Models.Publication", "Publication")
                    .WithMany("PublicationTags")
                    .HasForeignKey("PublicationId")
                    .HasConstraintName("FK_publication-tag")
                    .IsRequired();

                b.HasOne("Onym.Models.Tag", "Tag")
                    .WithMany("PublicationTags")
                    .HasForeignKey("TagId")
                    .HasConstraintName("FK_tag-publication")
                    .IsRequired();

                b.Navigation("Publication");

                b.Navigation("Tag");
            });

            modelBuilder.Entity("Onym.Models.UserStatus", b =>
            {
                b.HasOne("Onym.Models.Status", "Status")
                    .WithMany("UserStatuses")
                    .HasForeignKey("StatusId")
                    .HasConstraintName("FK_status-user")
                    .IsRequired();

                b.HasOne("Onym.Models.User", "User")
                    .WithMany("UserStatuses")
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_user-statuses")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Status");

                b.Navigation("User");
            });

            modelBuilder.Entity("Onym.Models.Comment", b =>
            {
                b.Navigation("CommentMedia");

                b.Navigation("CommentRatingTallies");

                b.Navigation("InverseParentalComment");
            });

            modelBuilder.Entity("Onym.Models.Media", b =>
            {
                b.Navigation("CommentMedia");

                b.Navigation("PublicationMedia");

                b.Navigation("Users");
            });

            modelBuilder.Entity("Onym.Models.Publication", b =>
            {
                b.Navigation("Comments");

                b.Navigation("Favorites");

                b.Navigation("PublicationMedia");

                b.Navigation("PublicationRatingTallies");

                b.Navigation("PublicationTags");
            });

            modelBuilder.Entity("Onym.Models.Status", b =>
            {
                b.Navigation("Comments");

                b.Navigation("Publications");

                b.Navigation("UserStatuses");
            });

            modelBuilder.Entity("Onym.Models.Tag", b => { b.Navigation("PublicationTags"); });

            modelBuilder.Entity("Onym.Models.User", b =>
            {
                b.Navigation("Claims");

                b.Navigation("CommentRatingTallies");

                b.Navigation("Comments");

                b.Navigation("Favorites");

                b.Navigation("Logins");

                b.Navigation("PublicationRatingTallies");

                b.Navigation("Publications");

                b.Navigation("Tokens");

                b.Navigation("UserRoles");

                b.Navigation("UserStatuses");
                
                b.Navigation("UserProfilePictureNavigation");
            });
        }
    }
}
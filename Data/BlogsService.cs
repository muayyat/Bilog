using System;
using Bilog.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Z.EntityFramework.Plus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace Bilog.Data
{
    public class BlogsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;
      
        public BlogsService(ApplicationDbContext context,
            IWebHostEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _environment = environment;
            _userManager = userManager;
           

        }

        // Blogs

        #region public async Task<BlogsPaged> GetBlogsAsync(int page, int CategoryID)
        public async Task<BlogsPaged> GetBlogsAsync(int page, int CategoryID)
        {
            page = page - 1;
            BlogsPaged objBlogsPaged = new BlogsPaged();

            if (CategoryID == 0)
            {
                objBlogsPaged.BlogCount = await _context.Blogs
                    .CountAsync();

                objBlogsPaged.Blogs = await _context.Blogs
                    .Include(x => x.BlogCategory)
                    .OrderByDescending(x => x.BlogDate)
                    .Skip(page * 5)
                    .Take(5).ToListAsync();
            }
            else
            {
                objBlogsPaged.BlogCount = await _context.Blogs
                    .Include(x => x.BlogCategory)
                    .Where(x => x.BlogCategory.Any(y => y.CategoryId == CategoryID))
                    .CountAsync();

                objBlogsPaged.Blogs = await _context.Blogs
                    .Include(x => x.BlogCategory)
                    .Where(x => x.BlogCategory.Any(y => y.CategoryId == CategoryID))
                    .OrderByDescending(x => x.BlogDate)
                    .Skip(page * 5)
                    .Take(5).ToListAsync();
            }

            return objBlogsPaged;
        }
        #endregion

        #region public async Task<BlogDTO> GetBlogAsync(int BlogId)
        public async Task<BlogDTO> GetBlogAsync(int BlogId)
        {
            BlogDTO objBlog = await (from blog in _context.Blogs
                                     .Where(x => x.BlogId == BlogId)
                                     select new BlogDTO
                                     {
                                         BlogId = blog.BlogId,
                                         BlogTitle = blog.BlogTitle,
                                         Link = blog.Link,
                                         BlogDate = blog.BlogDate,
                                         BlogUserName = blog.BlogUserName,
                                         BlogSummary = blog.BlogSummary,
                                         BlogContent = blog.BlogContent,
                                         Banner = blog.Banner,
                                         BlogDisplayName = "",
                                     }).AsNoTracking().FirstOrDefaultAsync();

            // Add Blog Categories
            objBlog.BlogCategory = new List<BlogCategory>();

            var BlogCategories = await _context.BlogCategory
                .Where(x => x.BlogId == objBlog.BlogId)
                .AsNoTracking().ToListAsync();

            foreach (var item in BlogCategories)
            {
                objBlog.BlogCategory.Add(item);
            }

            // Try to get name

            var objUser = await _userManager.FindByEmailAsync(objBlog.BlogUserName);
                
            if (objUser != null)
            {
                if (objUser.DisplayName != null)
                {
                    objBlog.BlogDisplayName = objUser.DisplayName;
                }
            }

            return objBlog;
        }
        #endregion

        #region public async Task<BlogDTO> GetBlogLinkAsync(string Link)
        public async Task<BlogDTO> GetBlogLinkAsync(string Link)
        {
            BlogDTO objBlog = await (from blog in _context.Blogs
                                     .Where(x => x.Link == Link)
                                     select new BlogDTO
                                     {
                                         BlogId = blog.BlogId,
                                         BlogTitle = blog.BlogTitle,
                                         Link = blog.Link,
                                         BlogDate = blog.BlogDate,
                                         BlogUserName = blog.BlogUserName,
                                         BlogSummary = blog.BlogSummary,
                                         BlogContent = blog.BlogContent,
                                         Banner = blog.Banner,
                                         BlogDisplayName = "",
                                     }).AsNoTracking().FirstOrDefaultAsync();

            // Add Blog Categories
            objBlog.BlogCategory = new List<BlogCategory>();

            var BlogCategories = await _context.BlogCategory
                .Where(x => x.BlogId == objBlog.BlogId)
                .AsNoTracking().ToListAsync();

            foreach (var item in BlogCategories)
            {
                objBlog.BlogCategory.Add(item);
            }

            var objUser = await _userManager.FindByEmailAsync(objBlog.BlogUserName);
           
            if (objUser != null)
            {
                if (objUser.DisplayName != null)
                {
                    objBlog.BlogDisplayName = objUser.DisplayName;
                }
            }

            return objBlog;
        }
        #endregion

        #region public async Task<BlogsPaged> GetBlogsAdminAsync(string strUserName, int page)
        public async Task<BlogsPaged> GetBlogsAdminAsync(string strUserName, int page)
        {
            page = page - 1;
            BlogsPaged objBlogsPaged = new BlogsPaged();

            objBlogsPaged.BlogCount = await _context.Blogs
                .Where(x => x.BlogUserName == strUserName)
                .AsNoTracking()
                .CountAsync();

            objBlogsPaged.Blogs = await _context.Blogs
                .Include(x => x.BlogCategory)
                .Where(x => x.BlogUserName == strUserName)
                .OrderByDescending(x => x.BlogDate)
                .Skip(page * 5)
                .Take(5).ToListAsync();

            return objBlogsPaged;
        }
        #endregion

        #region public Task<Blogs> CreateBlogAsync(BlogDTO newBlog, IEnumerable<String> BlogCatagories)
        public Task<Blogs> CreateBlogAsync(BlogDTO newBlog, IEnumerable<String> BlogCatagories)
        {
            try
            {
                Blogs objBlogs = new Blogs();

                objBlogs.BlogId = 0;
                objBlogs.BlogContent = newBlog.BlogContent;
                objBlogs.BlogDate = newBlog.BlogDate;
                objBlogs.BlogSummary = newBlog.BlogSummary;
                objBlogs.BlogTitle = newBlog.BlogTitle;
                objBlogs.Link = newBlog.Link;
                objBlogs.BlogUserName = newBlog.BlogUserName;
                objBlogs.BlogContent = newBlog.BlogContent;
                objBlogs.Banner = newBlog.Banner;

                if (BlogCatagories == null)
                {
                    objBlogs.BlogCategory = null;
                }
                else
                {
                    objBlogs.BlogCategory =
                        GetSelectedBlogCategories(newBlog, BlogCatagories);
                }

                _context.Blogs.Add(objBlogs);
                _context.SaveChanges();

                return Task.FromResult(objBlogs);
            }
            catch (Exception ex)
            {
                DetachAllEntities();
                throw ex;
            }
        }
        #endregion

        #region public Task<bool> DeleteBlogAsync(BlogDTO objBlogs)
        public Task<bool> DeleteBlogAsync(BlogDTO objBlogs)
        {
            var ExistingBlogs =
                _context.Blogs
                .Where(x => x.BlogId == objBlogs.BlogId)
                .FirstOrDefault();

            if (ExistingBlogs != null)
            {
                _context.Blogs.Remove(ExistingBlogs);
                _context.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
        #endregion

        #region public Task<bool> UpdateBlogAsync(BlogDTO objBlogs, IEnumerable<String> BlogCategories)
        public Task<bool> UpdateBlogAsync(BlogDTO objBlogs, IEnumerable<String> BlogCategories)
        {
            try
            {
                var ExistingBlogs =
                    _context.Blogs
                    .Include(x => x.BlogCategory)
                    .Where(x => x.BlogId == objBlogs.BlogId)
                    .FirstOrDefault();

                if (ExistingBlogs != null)
                {
                    ExistingBlogs.BlogDate =
                        objBlogs.BlogDate;

                    ExistingBlogs.BlogTitle =
                        objBlogs.BlogTitle;

                    ExistingBlogs.BlogSummary =
                        objBlogs.BlogSummary;

                    ExistingBlogs.BlogContent =
                        objBlogs.BlogContent;
                    ExistingBlogs.Banner =
                                            objBlogs.Banner;

                    if (BlogCategories == null)
                    {
                        ExistingBlogs.BlogCategory = null;
                    }
                    else
                    {
                        ExistingBlogs.BlogCategory =
                            GetSelectedBlogCategories(objBlogs, BlogCategories);
                    }

                    _context.SaveChanges();
                }
                else
                {
                    return Task.FromResult(false);
                }

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                DetachAllEntities();
                throw ex;
            }
        }
        #endregion


      

        // Category

        #region public async Task<CategoryDTO> GetCategorysAsync()
        public async Task<List<CategoryDTO>> GetCategorysAsync()
        {
            return await (from category in _context.Categorys
                          select new CategoryDTO
                          {
                              CategoryId = category.CategoryId.ToString(),
                              Description = category.Description,
                              Title = category.Title
                          }).AsNoTracking().OrderBy(x => x.Title).ToListAsync();
        }
        #endregion

        #region public async Task<CategorysPaged> GetCategorysAsync(int page)
        public async Task<CategorysPaged> GetCategorysAsync(int page)
        {
            page = page - 1;
            CategorysPaged objCategorysPaged = new CategorysPaged();

            objCategorysPaged.CategoryCount = await _context.Categorys
                 // Use AsNoTracking to disable EF change tracking
                 .AsNoTracking()
                .CountAsync();

            objCategorysPaged.Categorys = await (from category in _context.Categorys
                                                 select new CategoryDTO
                                                 {
                                                     CategoryId = category.CategoryId.ToString(),
                                                     Description = category.Description,
                                                     Title = category.Title
                                                 }).AsNoTracking()
                                                 .OrderBy(x => x.Title)
                                                 .Skip(page * 5)
                                                 .Take(5)
                                                 .ToListAsync();
            return objCategorysPaged;
        }
        #endregion

        #region public Task<bool> CreateCategoryAsync(CategoryDTO objCategoryDTO)
        public Task<bool> CreateCategoryAsync(CategoryDTO objCategoryDTO)
        {
            try
            {
                Categorys objCategorys = new Categorys();
                objCategorys.Title = objCategoryDTO.Title;
                objCategorys.Description = objCategoryDTO.Description;

                _context.Categorys.Add(objCategorys);
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                DetachAllEntities();
                throw ex;
            }
        }
        #endregion

        #region public Task<bool> UpdateCategoryAsync(CategoryDTO objCategoryDTO)
        public Task<bool> UpdateCategoryAsync(CategoryDTO objCategoryDTO)
        {
            try
            {
                int intCategoryId = Convert.ToInt32(objCategoryDTO.CategoryId);

                var ExistingCategory =
                    _context.Categorys
                    .Where(x => x.CategoryId == intCategoryId)
                    .FirstOrDefault();

                if (ExistingCategory != null)
                {
                    ExistingCategory.Title =
                        objCategoryDTO.Title;

                    ExistingCategory.Description =
                        objCategoryDTO.Description;

                    _context.SaveChanges();
                }
                else
                {
                    return Task.FromResult(false);
                }

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                DetachAllEntities();
                throw ex;
            }
        }
        #endregion

        #region public Task<bool> DeleteCategoryAsync(CategoryDTO objCategoryDTO)
        public Task<bool> DeleteCategoryAsync(CategoryDTO objCategoryDTO)
        {
            int intCategoryId = Convert.ToInt32(objCategoryDTO.CategoryId);

            var ExistingCategory =
                _context.Categorys
                .Where(x => x.CategoryId == intCategoryId)
                .FirstOrDefault();

            if (ExistingCategory != null)
            {
                _context.Categorys.Remove(ExistingCategory);
                _context.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
        #endregion

        // Files

        #region public async Task<FilesDTO> GetFilesAsync()
        public async Task<List<FilesDTO>> GetFilesAsync()
        {
            return await (from Files in _context.Files
                          select new FilesDTO
                          {
                              FileId = Files.FileId.ToString(),
                              CreateDate = Files.CreateDate,
                              DownloadCount = Files.DownloadCount,
                              FileName = Files.FileName,
                              FilePath = Files.FilePath,
                          }).AsNoTracking().OrderBy(x => x.FileName).ToListAsync();
        }
        #endregion

        #region public async Task<FilesPaged> GetFilesAsync(int page)
        public async Task<FilesPaged> GetFilesAsync(int page)
        {
            page = page - 1;
            FilesPaged objFilesPaged = new FilesPaged();

            objFilesPaged.FilesCount = await _context.Files
                 // Use AsNoTracking to disable EF change tracking
                 .AsNoTracking()
                .CountAsync();

            objFilesPaged.Files = await (from Files in _context.Files
                                         select new FilesDTO
                                         {
                                             FileId = Files.FileId.ToString(),
                                             CreateDate = Files.CreateDate,
                                             DownloadCount = Files.DownloadCount,
                                             FileName = Files.FileName,
                                             FilePath = Files.FilePath,
                                         }).AsNoTracking()
                                                 .OrderBy(x => x.FileName)
                                                 .Skip(page * 10)
                                                 .Take(10)
                                                 .ToListAsync();
            return objFilesPaged;
        }
        #endregion

        #region public Task<bool> CreateFilesAsync(FilesDTO objFilesDTO)
        public Task<bool> CreateFilesAsync(FilesDTO objFilesDTO)
        {
            try
            {
                Files objFiles = new Files();
                objFiles.CreateDate = DateTime.Now;
                objFiles.DownloadCount = 0;
                objFiles.FileName = objFilesDTO.FileName;
                objFiles.FilePath = objFilesDTO.FilePath;

                _context.Files.Add(objFiles);
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                DetachAllEntities();
                throw ex;
            }
        }
        #endregion

        #region public Task<bool> UpdateFilesAsync(FilesDTO objFilesDTO)
        public Task<bool> UpdateFilesAsync(FilesDTO objFilesDTO)
        {
            try
            {
                int intFilesId = Convert.ToInt32(objFilesDTO.FileId);

                var ExistingFiles =
                    _context.Files
                    .Where(x => x.FileId == intFilesId)
                    .FirstOrDefault();

                if (ExistingFiles != null)
                {
                    ExistingFiles.DownloadCount =
                        objFilesDTO.DownloadCount;

                    ExistingFiles.FileName =
                        objFilesDTO.FileName;

                    ExistingFiles.FilePath =
                        objFilesDTO.FilePath;

                    _context.SaveChanges();
                }
                else
                {
                    return Task.FromResult(false);
                }

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                DetachAllEntities();
                throw ex;
            }
        }
        #endregion

        #region public Task<bool> DeleteFilesAsync(FilesDTO objFilesDTO)
        public Task<bool> DeleteFilesAsync(FilesDTO objFilesDTO)
        {
            int intFilesId = Convert.ToInt32(objFilesDTO.FileId);

            var ExistingFiles =
                _context.Files
                .Where(x => x.FileId == intFilesId)
                .FirstOrDefault();

            if (ExistingFiles != null)
            {
                _context.Files.Remove(ExistingFiles);
                _context.SaveChanges();

                // Delete the file
                FileController objFileController = new FileController(_environment);
                objFileController.DeleteFile(objFilesDTO.FilePath);
            }
            else
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
        #endregion

        // Logs

        #region public async Task<LogsPaged> GetLogsAsync(int page)
        public async Task<LogsPaged> GetLogsAsync(int page)
        {
            page = page - 1;
            LogsPaged objLogsPaged = new LogsPaged();

            objLogsPaged.LogCount = await _context.Logs
                 // Use AsNoTracking to disable EF change tracking
                 .AsNoTracking()
                .CountAsync();

            objLogsPaged.Logs = await _context.Logs
                 // Use AsNoTracking to disable EF change tracking
                 .AsNoTracking()
                .OrderByDescending(x => x.LogId)
                .Skip(page * 10)
                .Take(10).ToListAsync();

            return objLogsPaged;
        }
        #endregion

        #region public Task<bool> CreateLogAsync(Logs objLogs)
        public Task<bool> CreateLogAsync(Logs objLogs)
        {
            _context.Logs.Add(objLogs);
            _context.SaveChanges();
            return Task.FromResult(true);
        }
        #endregion

        #region public async Task<bool> DelteLogsAsync(string UserName)
        public async Task<bool> DelteLogsAsync(string UserName)
        {
            await _context.Logs.AsNoTracking().DeleteAsync();

            Logs objLog = new Logs();
            objLog.LogDate = DateTime.Now;
            objLog.LogAction = "Logs cleared";
            objLog.LogUserName = UserName;
            objLog.LogIpaddress = "127.0.0.1";

            _context.Logs.Add(objLog);
            _context.SaveChanges();
            return true;
        }
        #endregion

        // Users

        #region public async Task<ApplicationUserPaged> GetUsersAsync(string paramSearch, int page)
        public async Task<ApplicationUserPaged> GetUsersAsync(string paramSearch, int page)
        {
            page = page - 1;
            ApplicationUserPaged objApplicationUserPaged = new ApplicationUserPaged();
            objApplicationUserPaged.ApplicationUsers = new List<ApplicationUser>();

            objApplicationUserPaged.ApplicationUserCount = _userManager.Users.Count();
                 

            var users = await _userManager.Users
                 // Use AsNoTracking to disable EF change tracking
                 .AsNoTracking()
                 .Where(x => x.UserName.ToLower().Contains(paramSearch)
                 || x.Email.ToLower().Contains(paramSearch)
                 || x.DisplayName.ToLower().Contains(paramSearch))
                 .OrderByDescending(x => x.Id)
                 .Skip(page * 5)
                 .Take(5).ToListAsync();

            foreach (var item in users)
            {
                ApplicationUser objApplicationUser = new ApplicationUser();

                objApplicationUser.Id = item.Id;
                objApplicationUser.UserName = item.UserName;
                objApplicationUser.Email = item.Email;
                objApplicationUser.DisplayName = item.DisplayName;
                objApplicationUser.EmailConfirmed = item.EmailConfirmed;
                objApplicationUser.NewsletterSubscriber = item.NewsletterSubscriber;
                objApplicationUser.PhoneNumber = item.PhoneNumber;
                objApplicationUser.PasswordHash = "*****";

                objApplicationUserPaged.ApplicationUsers.Add(objApplicationUser);
            }

            return objApplicationUserPaged;
        }
        #endregion
        //Contacts
        #region public Task<Contacts> SendContactAsync(ContactDTO newBlog)
        public Task<Contacts> SendContactAsync(ContactDTO newContact)
        {
            try
            {
                Contacts ocontact = new Contacts();

                ocontact.Id = 0;
                ocontact.Date = DateTime.UtcNow;
                ocontact.Seen = null;
                ocontact.Replied = null;
                ocontact.Title = newContact.Title;
                ocontact.Fullname = newContact.Fullname;
                ocontact.Email = newContact.Email;
                ocontact.Phone = newContact.Phone;
                ocontact.Details = newContact.Details;
                ocontact.Source = newContact.Source;
                ocontact.Reply = null;
                ocontact.User = null;


                _context.Contacts.Add(ocontact);
                _context.SaveChanges();

                return Task.FromResult(ocontact);
            }
            catch (Exception ex)
            {
                DetachAllEntities();
                throw ex;
            }
        }
        #endregion

        #region public async Task<ContactsPaged> GetContactsAsync(int page)
        public async Task<ContactsPaged> GetContactsAsync(int page)
        {
            page = page - 1;
            ContactsPaged objContactsPaged = new ContactsPaged();

            objContactsPaged.ContactCount = await _context.Contacts
                 // Use AsNoTracking to disable EF change tracking
                 .AsNoTracking()
                .CountAsync();

            objContactsPaged.Contacts = await _context.Contacts
                 // Use AsNoTracking to disable EF change tracking
                 .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip(page * 10)
                .Take(10).ToListAsync();

            return objContactsPaged;
        }
        #endregion


        // Utility

        #region private List<BlogCategory> GetSelectedBlogCategories(BlogDTO objBlogs, IEnumerable<string> blogCatagories)
        private List<BlogCategory> GetSelectedBlogCategories(BlogDTO objBlogs, IEnumerable<string> blogCatagories)
        {
            List<BlogCategory> colBlogCategory = new List<BlogCategory>();

            foreach (var item in blogCatagories)
            {
                int intBlogCatagoryId = Convert.ToInt32(item);

                // Get the Category
                var Category = _context.Categorys
                    .Where(x => x.CategoryId == intBlogCatagoryId)
                    .AsNoTracking()
                    .FirstOrDefault();

                // Create a new BlogCategory
                BlogCategory NewBlogCategory = new BlogCategory();
                NewBlogCategory.BlogId = objBlogs.BlogId;
                NewBlogCategory.CategoryId = Category.CategoryId;

                // Add it to the list
                colBlogCategory.Add(NewBlogCategory);
            }

            return colBlogCategory;
        }
        #endregion

        #region public void DetachAllEntities()
        public void DetachAllEntities()
        {
            var changedEntriesCopy = _context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();
            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace ImageManipulation.Data.Services
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile imageFile, string[] allowedFileExtensions);

        void DeleteFile(string fileNameWithExtension);
    }

    public class FileService(IWebHostEnvironment environment) : IFileService
    {
        public async void DeleteFile(string fileNameWithExtension)
        {
            if (string.IsNullOrEmpty(fileNameWithExtension))
            {
                throw new NotImplementedException();
            }

            var contentPath = environment.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(path);
            }
                
            File.Delete(path);

        }

        public async Task<string> SaveFileAsync(IFormFile imageFile, string[] allowedFileExtensions)
        {
            if (imageFile == null)
                throw new NotImplementedException(nameof(imageFile));
            var contentPath = environment.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var ext = Path.GetExtension(imageFile.FileName);
            if (!allowedFileExtensions.Contains(ext))
            {
                throw new ArgumentException (nameof(allowedFileExtensions));   //error not correct way jist formality
            }
            var filename= $"{Guid.NewGuid().ToString()} {ext}";
            var fileNamewithPath = Path.Combine(path, filename);
            using var stream = new FileStream(fileNamewithPath, FileMode.Create);
            await imageFile.CopyToAsync(stream);
            return filename;
        }
    }
}

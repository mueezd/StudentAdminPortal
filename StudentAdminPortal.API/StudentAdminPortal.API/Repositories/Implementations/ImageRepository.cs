using Microsoft.AspNetCore.Http;
using StudentAdminPortal.API.Repositories.Interface;
using System;
using System.IO;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Repositories.Implementations
{
    public class ImageRepository : IImageRepository
    {
        public async Task<string> UploadImage(IFormFile file, string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", fileName);
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return GetServerRelativePath(filePath);

        }

        private string GetServerRelativePath(string fileName)
        {
            return Path.Combine(@"Resources\Images", fileName);
        }
    }
}

using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Repositories.Interface
{
    public interface IImageRepository
    {
        Task<string> UploadImage(IFormFile file, string fileName);
    }
}

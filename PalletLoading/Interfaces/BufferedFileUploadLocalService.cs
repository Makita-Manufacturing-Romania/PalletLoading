using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace PalletLoading.Interfaces
{
    public interface IBufferedFileUploadService
    {
        Task<bool> UploadFile(IFormFile file, string path);
    }
}

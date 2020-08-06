using System.Drawing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EmailMarketing.Web.Core
{
    public interface IFileStorage
    {
        Task<string> StoreFileAsync(string uploadsFolderPath, IFormFile file);
    }
}
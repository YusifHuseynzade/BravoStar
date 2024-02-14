using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Core.Helpers
{
    public class FileManager
    {
        public static string Save(IFormFile file, IHostEnvironment env, string folder)
        {
            try
            {
                string webRootPath = env.ContentRootPath;
                string folderPath = Path.Combine(webRootPath, "wwwroot", folder);

                
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileName = file.FileName;
                string newFileName = Guid.NewGuid().ToString() + (fileName.Length > 64 ? fileName.Substring(fileName.Length - 64) : fileName);
                string path = Path.Combine(folderPath, newFileName);

                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fs);
                }

                string relativePath = Path.Combine(folder, newFileName).Replace("\\", "/");
                return $"/{relativePath}"; 
            }
            catch (Exception ex)
            {
                
                return null; 
            }
        }

        public static void Delete(string rootPath, string folder, string filename)
        {
            string path = Path.Combine(rootPath, folder, filename).Replace("\\", "/");

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void DeleteDesc(string filename, string folder)
        {
            string rootPath = "uploads"; 
            string path = Path.Combine(rootPath, folder, filename).Replace("\\", "/");

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}

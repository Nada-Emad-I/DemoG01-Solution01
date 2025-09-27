using DemoG01.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoG01.BLL.Services.Classes
{
    public class AttachmentSerivce : IAttachmentSerivce
    {
        List<string> AllowedExtensions = new List<string>() { ".jpg", ".png", ".jpeg" };
        const int MaxSize = 2 * 1024 * 1024;
        public string? Upload(IFormFile file, string FolderName)
        {

            //1. Check Extension
            var extension = Path.GetExtension(file.FileName);
            if (!AllowedExtensions.Contains(extension))
            {
                return null;
            }

            //2. Check Size
            if (file.Length > MaxSize|| file.Length==0)
            {
                return null;
            }

            //3. Get Located Folder Path

            //C:\Users\nadaa\source\repos\DemoG01 Solution\DemoG01.PL\wwwroot\Files\Images\

            //var FolderPath = $"{Directory.GetCurrentDirectory()}///wwwroot/Files//{FolderName}";
            var FolderPath=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","Files", FolderName);

            //4. Make File Name Unique
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";

            //5. Get File Path
            var filePath= Path.Combine(FolderPath,fileName);

            //6. Create File Stream(Unmanaged)
            using FileStream fs = new FileStream(filePath, FileMode.Create);

            //7. use Stream To Copy File
            file.CopyTo(fs);

            //8.  Return File Name
            return fileName;
        }
        public bool Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }

        
    }
}

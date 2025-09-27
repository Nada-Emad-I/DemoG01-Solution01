using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoG01.BLL.Services.Interfaces
{
    public interface IAttachmentSerivce
    {
        //upload
        public string? Upload(IFormFile file, string FolderName);
        //Delete
        public bool Delete(string filePath); 
    }
}

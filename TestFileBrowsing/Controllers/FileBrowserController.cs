using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TestFileBrowsing.FileManagerHelper;

namespace TestFileBrowsing.Controllers
{
    public class FileBrowserController : ApiController
    {


        private readonly FileManager fileManager;

        FileBrowserController()
        {
            fileManager = new FileManager();
        }

        [HttpGet]
        public IHttpActionResult GetGetAllDrives()
        {
            List<string> result = fileManager.GetAllDrives();

            var json = new
            {
                data = result
            };

            return Ok(json);

        }


        [HttpGet]
        public IHttpActionResult GetSubFolders(string path)
        {
            
            var result = fileManager.GetSubFolders(HttpUtility.HtmlDecode(path));

            var json = new
            {
                data = result
            };

            return Ok(json);
        }


        [HttpGet]
        public IHttpActionResult GetFolderFiles(string path)
        {
            var result = fileManager.GetFolderFiles(HttpUtility.HtmlDecode(path));

            var json = new
            {
                data = result,
            };

            return Ok(json);
        }

        [HttpGet]
        public IHttpActionResult GetFolderFilesCount(string path)
        {
            var count = fileManager.GetFolderFilesCount(path);

            var json = new
            {
                data = count,
            };

            return Ok(json);
        }
    }
}

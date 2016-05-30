using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TestFileBrowsing.FileManagerHelper
{
    public class FileManager
    {

        static System.Collections.Specialized.StringCollection log = new System.Collections.Specialized.StringCollection();



        public List<string> GetAllDrives()
        {
            string[] drives = System.Environment.GetLogicalDrives();

            List<string> drivesRoot = new List<string>();

            //System.IO.DirectoryInfo rootDir = null;

            foreach (string dr in drives)
            {
                System.IO.DriveInfo di = new System.IO.DriveInfo(dr);


                if (!di.IsReady)
                {
                    continue;
                }

                drivesRoot.Add(di.RootDirectory.ToString());
            }

            return drivesRoot;
        }


        public string[] GetSubFolders(string root)
        {
            string[] subdirectoryEntries = Directory.GetDirectories(root);

            return subdirectoryEntries;
        }

        public string[] GetFolderFiles(string root)
        {
            string[] subdirectoryFiles = Directory.GetFiles(root);

            return subdirectoryFiles;
        }


        public int[] GetFolderFilesCount(string root)
        {
            string[] files = null;

            int[] result = new int[3];

            int small = 0, middle = 0, big = 0;

            try
            {
                files = System.IO.Directory.GetFiles(root, "*.*", SearchOption.AllDirectories);
            }
            catch (UnauthorizedAccessException e)
            {
                log.Add(e.Message);
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                log.Add(e.Message);
            }

            if (files != null)
            {
                foreach (var file in files)
                {
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);

                    if (fileInfo.Length < 10485760)
                    {
                        small++;
                    }
                    else if (fileInfo.Length > 10485760 && fileInfo.Length < 50485760)
                    {
                        middle++;
                    }
                    else if (fileInfo.Length < 50485760)
                    {
                        big++;
                    }
                }
                result[0] = small;
                result[1] = middle;
                result[2] = small;
            }

            return result;
        }
    }

}
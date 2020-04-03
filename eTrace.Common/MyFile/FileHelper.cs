using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Common
{
    public class FileHelper
    {
        private static FileHelper instance = new FileHelper();
        public static FileHelper Instance
        {
            get { return instance; }
        }

        public bool MoveFile(string fileName, string oldFilePath, string newFilePath, ref string msg)
        {
            string oldFile = Path.Combine(oldFilePath, fileName);
            string newFile = Path.Combine(newFilePath, fileName);

            if (!File.Exists(oldFile))
            {
                msg = string.Format("File is not exists:{0}", oldFile);
                return false;
            }
            if (File.Exists(newFile))
            {
                msg = string.Format("File is exists:{0}", newFile);
                return false;
            }
            File.Move(oldFile, newFile);

            return true;
        }

        public IList<string> GetFolders(string path)
        {
            return Directory.GetDirectories(path).ToList();
        }

        public bool IsExistedFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                return true;
            }
            return false;
        }

        public bool IsExistedFileInFolder(string fileName, string folder)
        {
            IList<string> folders = this.GetFolders(folder).OrderByDescending(o => o).ToList();

            foreach (var item in folders)
            {
                string filePath = Path.Combine(item, fileName);
                if (this.IsExistedFile(filePath))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

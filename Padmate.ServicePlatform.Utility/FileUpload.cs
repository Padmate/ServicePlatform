using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Utility
{
    public class FileUpload
    {
        public FileUpload() { }

        /// <summary>
        /// 
        /// </summary>
        string _mapPath;
        public FileUpload(string mapPath)
        {
            _mapPath = mapPath;
        }

        /// <summary>
        /// 构造文件虚拟保存路径
        /// </summary>
        /// <param name="virtualDirectory"></param>
        /// <param name="fileName"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public string ConstructVirtualSavePath(string virtualDirectory, string fileName, string extension)
        {
            //生成新的文件名称
            string saveName = GenerateSaveName(fileName, extension);
            string virtualSavePath = Path.Combine(virtualDirectory,saveName);
            return virtualSavePath;
        }

        /// <summary>
        /// 构造文件物理保存路径
        /// </summary>
        /// <param name="virtualDirectory">虚拟文件夹</param>
        /// <param name="fileName">文件名</param>
        /// <param name="extension">文件后缀</param>
        /// <returns></returns>
        public string ConstructPhysicalSavePath(string virtualDirectory, string fileName,string extension)
        {
            //生成新的文件名称
            string saveName = GenerateSaveName(fileName,extension);
            //物理文件夹路径
            string physicleDirectory = Path.Combine(_mapPath,virtualDirectory);
            //如果文件夹不存在，则新建文件夹
            if (!System.IO.Directory.Exists(physicleDirectory))
            {
                System.IO.Directory.CreateDirectory(physicleDirectory);
            }

            //物理保存路径
            string physicalSavePath = Path.Combine(physicleDirectory, saveName);

            return physicalSavePath;
            
        }

        /// <summary>
        /// 生成文件保存名称
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public string GenerateSaveName(string fileName,string extension)
        {
            string saveName = fileName + Guid.NewGuid().ToString() + extension;
            return saveName;
        }
    }
}

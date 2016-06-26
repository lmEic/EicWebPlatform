using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace Lm.Eic.Uti.Common.YleeExtension.FileOperation
{
   public static class FileOperationExtension
    {
       /// <summary>
       /// 删除文件夹内的所有文件
       /// </summary>
       /// <param name="directory"></param>
       public static void DeleteFiles(this string directory)
       {
           if (Directory.Exists(directory))
           {
               var filesArr = Directory.GetFiles(directory);
               if (filesArr != null && filesArr.Length > 0)
               {
                   foreach (string  filename in filesArr)
                   {
                       File.Delete(filename);
                   }
               }
           }
       }
       /// <summary>
       /// 获取文件中的内容,按行存储到列表中
       /// </summary>
       /// <param name="filePath">文件路径</param>
       /// <param name="encoding">读取编码</param>
       /// <returns></returns>
       public static List<string> GetFileLines(this string filePath,Encoding encoding)
       {
           List<string> Lines = new List<string>();
           if (!File.Exists(filePath))
           {
               Lm.Eic.Uti.Common.YleeMessage.Windows.WinMessageHelper.ShowMsg(string.Format("{0}文件不存在！", filePath), 3);
               return Lines;
           }
           using (StreamReader sr = new StreamReader(filePath, encoding))
           {
               string line = sr.ReadLine();
               while (!string.IsNullOrEmpty(line))
               {
                   Lines.Add(line);
                   line = sr.ReadLine();
               }
           }
           return Lines;
       }
       /// <summary>
       /// 获取文件中的内容,按行存储到列表中
       /// </summary>
       /// <param name="filePath"></param>
       /// <returns></returns>
       public static List<string> GetFileLines(this string filePath)
       {
           return filePath.GetFileLines(Encoding.Default);
       }

       /// <summary>
       /// 对已经存在的文件进行内容附加
       /// </summary>
       /// <param name="filePath">文件路径</param>
       /// <param name="fileContent">要附加的文件内容</param>
       /// <param name="encoding">写入文件时采用的编码格式</param>
       public static void AppendFile(this string filePath, string fileContent,Encoding encoding)
       {
           string DirectoryPath = Path.GetDirectoryName(filePath);
           if (!Directory.Exists(DirectoryPath)) Directory.CreateDirectory(DirectoryPath);
           using (StreamWriter sw = new StreamWriter(filePath, true, encoding))
           {
               sw.WriteLine(fileContent);
               sw.Flush();
           }
       }
       /// <summary>
       /// 向文件中写入文本内容
       /// </summary>
       /// <param name="filePath">文件路径</param>
       /// <param name="fileContent">文件内容</param>
       public static void AppendFile(this string filePath, string fileContent)
       {
           filePath.AppendFile(fileContent, Encoding.Default);
       }

       /// <summary>
       /// 创建文件
       /// </summary>
       /// <param name="filePath">文件路径</param>
       /// <param name="fileContent">创建文件时要写入的文件内容</param>
       /// <param name="encoding">写入文件时采用的编码格式</param>
       public static void CreateFile(this string filePath, string fileContent,Encoding encoding)
       {
           string DirectoryPath = Path.GetDirectoryName(filePath);
           if (!Directory.Exists(DirectoryPath)) Directory.CreateDirectory(DirectoryPath);
           using (StreamWriter sw = new StreamWriter(filePath, false, encoding))
           {
               sw.WriteLine(fileContent);
               sw.Flush();
           }
       }
       /// <summary>
       /// 创建文件
       /// </summary>
       /// <param name="filePath">文件路径</param>
       /// <param name="fileContent">文件内容</param>
       public static void CreateFile(this string filePath, string fileContent)
       {
           filePath.CreateFile(fileContent, Encoding.Default);
       }

       /// <summary>
       /// 判定文件夹是否存在，若不存在，则自动创建
       /// </summary>
       /// <param name="directoryPath">文件夹路径</param>
       public static void ExistDirectory(this string directoryPath)
       {
           directoryPath = directoryPath.EndsWith("\\") ? directoryPath : directoryPath + "\\";
           if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
       }

       /// <summary>
       /// 判定文件是否存在
       /// </summary>
       /// <param name="filePath">文件名称路径</param>
       /// <returns></returns>
       public static bool ExistFile(this string filePath)
       {
           return File.Exists(filePath);
       }
    }
}

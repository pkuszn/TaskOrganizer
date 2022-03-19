using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace TaskOrganizer.Helpers
{
    public static class PomodoroViewModelExtensions
    {
        public static string getFilePath(this string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            string filePath = fi.FullName;
            return filePath;
        }

        public static string getRelativeFilePath(this string fileName)
        {
            var env = System.Environment.CurrentDirectory;
            string projDir = Directory.GetParent(env).Parent.FullName;
            return projDir + fileName;
        }

    }
}

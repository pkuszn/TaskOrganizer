using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TaskOrganizer.Helpers
{
    public static class PomodoroViewModelExtensions
    {
        public static string getTomatoFile(this string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            string filePath = fi.FullName;
            return filePath;
        }

    }
}

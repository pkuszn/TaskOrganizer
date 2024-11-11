using System;
using System.IO;

namespace TaskOrganizer.Helpers;
internal static class FileHelpers
{
    private const string Bin = "bin";
    public static string GetFilePath(this string fileName)
    {
        if (!string.IsNullOrEmpty(fileName))
        {
            FileInfo fi = new(fileName);
            string filePath = fi.FullName;
            return filePath;
        }
        return null;
    }

    public static string GetRelativeFilePath(this string fileName)
    {
        if (!string.IsNullOrEmpty(fileName))
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            if (baseDir.Contains(Bin))
            {
                int index = baseDir.IndexOf(Bin);
                baseDir = baseDir[..index];
            }
            return baseDir;
        }
        return null;
    }
}
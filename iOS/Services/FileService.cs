using System;
using System.IO;
using CarDetector.Interfaces;

namespace CarDetector.iOS.Services
{
    public class FileService : IFileService
    {
        public string Personal => Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public string GetFullPathIfExists(string fileName)
        {
            var path = Path.Combine(Personal, fileName);
            if (!File.Exists(path))
                return null;
            return path;
        }

        public void WriteAllBytes(byte[] bytes, string fileName)
        {
            var path = Path.Combine(Personal, fileName);
            File.WriteAllBytes(path, bytes);
        }

        public byte[] ReadAllBytes(string fileName)
        {
            var path = GetFullPathIfExists(fileName);
            if (path == null)
                throw new FileNotFoundException();

            var bytes = File.ReadAllBytes(path);
            return bytes;
        }

        public void Delete(string fileName)
        {
            var path = GetFullPathIfExists(fileName);
            if (path == null)
                throw new FileNotFoundException();

            File.Delete(path);
        }
    }
}
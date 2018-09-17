using System;
namespace CarDetector.Interfaces
{
    public interface IFileService
    {
        string Personal { get; }

        void WriteAllBytes(byte[] bytes, string fileName);

        byte[] ReadAllBytes(string fileName);

        string GetFullPathIfExists(string fileName);

        void Delete(string fileName);
    }
}

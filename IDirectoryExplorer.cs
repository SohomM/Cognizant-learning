public interface IDirectoryExplorer
{
    ICollection<string> GetFiles(string path);
}

// File: DirectoryExplorer.cs
using System.Collections.Generic;
using System.IO;

namespace MagicFilesLib
{
    public class DirectoryExplorer : IDirectoryExplorer
    {
        public ICollection<string> GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }
    }
}

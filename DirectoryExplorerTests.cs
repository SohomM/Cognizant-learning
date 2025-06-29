using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using MagicFilesLib;

namespace DirectoryExplorer.Tests
{
    [TestFixture]
    public class DirectoryExplorerTests
    {
        private readonly string _file1 = "file.txt";
        private readonly string _file2 = "file2.txt";

        private Mock<IDirectoryExplorer> _mockExplorer;

        [OneTimeSetUp]
        public void Setup()
        {
            _mockExplorer = new Mock<IDirectoryExplorer>();
            _mockExplorer.Setup(x => x.GetFiles(It.IsAny<string>())).Returns(new List<string> { _file1, _file2 });
        }

        [Test]
        public void GetFiles_ShouldReturnMockedFiles()
        {
            var files = _mockExplorer.Object.GetFiles("C:/Test");

            Assert.IsNotNull(files);
            Assert.AreEqual(2, files.Count);
            Assert.Contains(_file1, new List<string>(files));
        }
    }
}

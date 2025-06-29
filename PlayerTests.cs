using NUnit.Framework;
using Moq;
using PlayersManagerLib;
using System;

namespace PlayerManager.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        private Mock<IPlayerMapper> _mockMapper;

        [OneTimeSetUp]
        public void Setup()
        {
            _mockMapper = new Mock<IPlayerMapper>();
            _mockMapper.Setup(x => x.IsPlayerNameExistsInDb(It.IsAny<string>())).Returns(false);
        }

        [Test]
        public void RegisterNewPlayer_ValidName_ReturnsPlayer()
        {
            var player = Player.RegisterNewPlayer("Sachin", _mockMapper.Object);
            Assert.IsNotNull(player);
            Assert.AreEqual("Sachin", player.Name);
            Assert.AreEqual("India", player.Country);
        }

        [Test]
        public void RegisterNewPlayer_EmptyName_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Player.RegisterNewPlayer("", _mockMapper.Object));
        }
    }
}


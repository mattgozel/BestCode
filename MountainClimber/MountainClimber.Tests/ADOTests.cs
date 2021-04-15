using GameLogic;
using MountainClimber.Data.ADO;
using MountainClimber.Data.InMemory;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainClimber.Tests
{
    [TestFixture]
    public class ADOTests
    {
        [Test]
        public void CanLoadProducts()
        {
            var repo = new MountainClimberProductsRepositoryADO();

            var list = repo.GetAll().ToList();

            Assert.AreEqual(1, list[0].MountainId);
            Assert.AreEqual("Instant Pot", list[0].ProductName);
            Assert.AreEqual(80, list[0].ProductPrice);
            Assert.AreEqual("instantpot.jpg", list[0].ImageFileName);
        }

        [Test]
        public void DoesGameLogicWork()
        {
            var repo = new GameLogic.GameLogic();

            var list = repo.Get3();

            Assert.AreEqual(3, list.Count);
        }

        [Test]
        public void CanLoadProductsInMemory()
        {
            var repo = new MountainClimberProductsRepositoryInMemory();

            var list = repo.GetAll().ToList();

            Assert.AreEqual(1, list[0].MountainId);
            Assert.AreEqual("Instant Pot", list[0].ProductName);
            Assert.AreEqual(80, list[0].ProductPrice);
            Assert.AreEqual("instantpot.jpg", list[0].ImageFileName);
        }
    }
}

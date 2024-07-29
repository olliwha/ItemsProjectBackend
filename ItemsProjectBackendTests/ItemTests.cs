using Microsoft.VisualStudio.TestTools.UnitTesting;
using ItemLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemLibrary.Tests
{
    [TestClass()]
    public class ItemTests
    {
        Item item = new Item(1, "Pear", 1);
        Item item2 = new Item(1, "Pear", 1);
        Item item3 = new Item(3, "Banana", 3);

        [TestMethod()]
        public void ValidateNameTest()
        {
            item.ValidateName();
            item.Name = null;
            Assert.ThrowsException<ArgumentNullException>(() => item.ValidateName());
            item.Name = "";
            Assert.ThrowsException<ArgumentException>(() => item.ValidateName());
        }

        [TestMethod()]
        public void ValidatePriceWithoutMomsTest()
        {
            item.ValidatePriceWithoutMoms();
            item.PriceWithoutMoms = 100000;
            item.ValidatePriceWithoutMoms();
            item.PriceWithoutMoms = 0;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => item.ValidatePriceWithoutMoms());
            item.PriceWithoutMoms = 100001;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => item.ValidatePriceWithoutMoms());
        }

        [TestMethod()]
        public void ValidatePriceTest()
        {
            item.ValidatePrice();
            decimal correctPrice = item.PriceWithoutMoms * 1.25m;
            Assert.AreEqual(correctPrice, item.Price);
        }

        [TestMethod()]
        public void ValidateTest()
        {
            item.Validate();
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual("1, Pear, 1,25kr.", item.ToString());
            Assert.AreNotEqual("kjkj", item.ToString());
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Assert.AreEqual(true, item.Equals(item2));
            Assert.AreEqual(false, item.Equals(item3));
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            Assert.AreEqual(1, item.GetHashCode());
            Assert.AreNotEqual(2, item.GetHashCode());
        }
    }
}
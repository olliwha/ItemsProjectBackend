using Microsoft.VisualStudio.TestTools.UnitTesting;
using ItemsProjectBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemLibrary;

namespace ItemsProjectBackend.Repositories.Tests
{
    [TestClass()]
    public class ItemsRepositoryTests
    {
        ItemsRepository _repository = new ItemsRepository();
        Item item = new Item(1, "Pear", 2);

        [TestMethod()]
        public void GetAllTest()
        {
            List<Item> Items = _repository.GetAll();
            Assert.IsNotNull(Items);
            Assert.AreEqual(3, Items.Count);
            Assert.AreEqual(typeof(List<Item>), Items.GetType());
            Assert.AreNotEqual(4, Items.Count);
            var hs = Items.ToHashSet();
            Assert.AreEqual(hs.Count, Items.Count);
            foreach (var Item in Items)
            {
                int foundIds = Items.FindAll(x => x.Id == Item.Id).Count;
                if (foundIds > 1)
                {
                    Assert.Fail($"ID: {Item.Id} exists {foundIds} times in the list");
                }
            }
        }

        [TestMethod()]
        public void GetByIDTest()
        {
            Item? foundItem = _repository.GetById(3);
            Assert.IsNotNull(foundItem);
            Assert.AreEqual(3, foundItem.Id);
            Assert.AreEqual("Bread", foundItem.Name);
            Item? notFoundItem = _repository.GetById(30);
            Assert.IsNull(notFoundItem);
        }

        [TestMethod()]
        public void AddTest()
        {
            List<Item> Items = _repository.GetAll();
            Item addedItem = _repository.Add(item);
            Assert.IsNotNull(addedItem);
            Assert.AreEqual(Items.Count + 1, _repository.GetAll().Count);
            Assert.AreNotEqual(Items.Count, _repository.GetAll().Count);
            Assert.AreEqual("Pear", _repository.GetById(addedItem.Id)?.Name);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            List<Item> Items = _repository.GetAll();
            Item? deletedItem = _repository.Delete(3);
            Assert.IsNotNull(deletedItem);
            Assert.IsNull(_repository.GetById(3));
            Assert.AreEqual(Items.Count - 1, _repository.GetAll().Count);
            Assert.AreNotEqual(Items.Count, _repository.GetAll().Count);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Item? oldItem = _repository.GetById(1);
            Item? newData = _repository.Update(1, item);
            Item? updatedItem = _repository.GetById(1);
            Assert.IsNotNull(updatedItem);
            Assert.AreEqual(newData?.Name, updatedItem.Name);
            Assert.AreEqual(1, updatedItem.Id);
            Assert.IsNull(_repository.Update(15, item));
            Assert.AreSame(oldItem, updatedItem);
        }
    }
}
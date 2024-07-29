using System.Xml.Linq;
using ItemLibrary;

namespace ItemsProjectBackend.Repositories
{
    public class ItemsRepository
    {
        private int _nextId;
        private List<Item> _items;

        public ItemsRepository()
        {
            _nextId = 1;
            _items = new List<Item>()
            {
                new Item(_nextId++, "apple", 2),
                new Item(_nextId++, "TV", 2000),
                new Item(_nextId++, "Bread", 20)
            };
        }

        public List<Item> GetAll()
        {
            //new list so the OG cant be edited by others
            return new List<Item>(_items);
        }
        public Item? GetById(int id)
        {
            return _items.Find(x => x.Id == id);
        }
        public Item Add(Item item)
        {
            Item itemToAdd = new Item(_nextId++, item.Name, item.PriceWithoutMoms);
            itemToAdd.Validate();
            _items.Add(itemToAdd);
            return itemToAdd;
        }

        public Item? Update(int id, Item item)
        {
            Item? itemToUpdate = GetById(id);
            if (itemToUpdate == null) return null;

            item.Validate();
            itemToUpdate.Name = item.Name;
            itemToUpdate.PriceWithoutMoms = item.PriceWithoutMoms;
            return itemToUpdate;

        }
        public Item? Delete(int id)
        {
            Item? itemToDelete = GetById(id);
            if (itemToDelete == null) return null;
            _items.Remove(itemToDelete);
            return itemToDelete;
        }
    }
}


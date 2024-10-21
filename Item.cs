using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Unicode;

namespace lab4
{
    public class Item
    {
        protected string _name, _key;
        protected int _cost, _count, _width, _height;
        public Item(string name, int cost, int count, int width, int height, string key)
        {
            _name = name;
            _cost = cost;
            _count = count;
            _width = width;
            _height = height;
            _key = key;
        }
        public bool isInStock()
        {
            return _count > 0;
        }

        public bool UpdItemStock(Item item, int count)
        {
            if (isInStock()) { item._count -= count; return true; }
            else { Console.WriteLine($"{item._name} отсутствует в наличии."); return false; }
        }

        public void ReturnItemToStock(Item item, int count)
        {
            item._count += count;
        }
        public void PrintItems(List <Item> items)
        {
            int count = 1;
            foreach (Item item in items) 
            {
                Console.WriteLine($"{count}. {item._name} ({item._width}×{item._height} см.) - {item._cost} руб. ({item._count} шт. в наличии)");
                count++;
            }
        }

        public string getName()
        {
            return _name;
        }
        public int getCount()
        {
            return _count;
        }
        public int getCost() 
        {
            return _cost;   
        }
        public int getWidth()
        { 
            return _width; 
        }
        public int getHeight() 
        { 
            return _height;
        }
        public string getKey() 
        {
            return _key;
        }
    }
}



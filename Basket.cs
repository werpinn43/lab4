using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class Basket : Item
    {
        protected int _b_count;

        public Basket(string name, int cost, int count, int width, int height, string key, int b_count) 
            :base(name, cost, count, width, height, key)
        {
            if (b_count > count)
            {
                _b_count = count;
            }
            else
            {
                _b_count = b_count;
            }
        }
        public void PrintBasket(List <Basket> basket)
        {
            int count = 1;
            foreach (Basket item in basket)
            {
                Console.WriteLine($"{count}. {item._name} ({item._width}×{item._height} см.) - {item._cost} руб. ({item._b_count} шт.)");
                count++;
            }
        }
        public bool AddItemToBasket(List <Basket> basket, Basket item)
        {
            if (item._count < item._b_count)
            {
                Console.WriteLine($"Количество {item._name} в корзине ({item._b_count}) превышает их количество в ассортименте ({item._count})");
                return false;
            }
            else
            {
                basket.Add(item);
                return true;
            }
        }
        public bool DeleteItemFromBasket(List <Basket> basket, int indx)
        {
            if (basket.Count == 0)
            {
                Console.WriteLine("Корзина пуста, удалять нечего)");
                return false;
            }
            else
            {
                basket.RemoveAt(indx - 1);
                return true;
            }
        }
        public int MakeBill(List<Basket> basket_list)
        {
            int bill = 0;

            foreach (Basket item in basket_list)
            {
                bill += item._cost * item._b_count;
            }

            return bill;
        }
        public void MakeOrder(List<Basket> basket_list)
        {
            Console.Clear();
            Console.WriteLine("Заказ оформлен: ");

            PrintBasket(basket_list);
            int bill = MakeBill(basket_list);

            Console.WriteLine($"\nИтого к оплате: {bill} руб.");


            while (Console.ReadKey().Key != ConsoleKey.Enter) { }

            basket_list.Clear();
        }

        int FindDeletedItem(List<Basket> basket, List<Item> items, int number)
        {
            int index = -1;
            bool flag = false;

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].getKey() == basket[number - 1].getKey())
                { 
                    index = i; 
                    break;
                }
            }
            return index;
        }

        public void EditBasket(Item item, List<Basket> basket_list, List<Item> items_list)
        {
            Console.Clear();
            if (basket_list.Count == 0)
            {
                Console.WriteLine("Вы не добавили ни одного товара в корзину. Желаете добавить?");
                Console.WriteLine("1.Добавить товар\n2.Выход на главный экран");

                string temp = Console.ReadLine();
                int cmd = Convert.ToInt32(temp);

                switch (cmd)
                {
                    case 1:
                        Console.Clear();
                        item.PrintItems(items_list);

                        Console.WriteLine("Выберите товар по номеру слева: ");
                        int indx = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Количество: ");
                        int count = Convert.ToInt32(Console.ReadLine());

                        Basket item_to_basket = new Basket(items_list[indx - 1].getName(), items_list[indx - 1].getCost(),
                            items_list[indx - 1].getCount(), items_list[indx - 1].getWidth(), items_list[indx - 1].getHeight(), 
                            items_list[indx - 1].getKey(), count);

                        bool flag = item_to_basket.AddItemToBasket(basket_list, item_to_basket);
                        items_list[indx - 1].UpdItemStock(items_list[indx - 1], count);

                        if (!flag)
                        {
                            Thread.Sleep(1000);
                            Console.Clear();

                            goto case 1;
                        }
                        break;
                    case 2:
                        Console.WriteLine("Выход...");
                        Thread.Sleep(3000);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("1.Посмотреть корзину\n2.Изменить корзину\n3.Оформить заказ\n4.Выход на экран");

                int cmd = Convert.ToInt32(Console.ReadLine());
                switch (cmd)
                {
                    case 1:
                        Console.Clear();

                        PrintBasket(basket_list);
                        Console.WriteLine("Нажмите на клавишу Enter для выхода...");

                        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("1.Добавить в корзину\n2.Удалить из корзины");

                        int cmd1 = Convert.ToInt32(Console.ReadLine());

                        if (cmd1 == 1)
                        {
                            Console.Clear();
                            PrintItems(items_list);

                            Console.WriteLine("Выберите номер товара для добавления в корзину: ");
                            int indx = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Количество: ");
                            int count = Convert.ToInt32(Console.ReadLine());

                            Basket item_to_basket = new Basket(items_list[indx - 1].getName(), items_list[indx - 1].getCost(),
                            items_list[indx - 1].getCount(), items_list[indx - 1].getWidth(), items_list[indx - 1].getHeight(),
                            items_list[indx - 1].getKey() ,count);

                            bool flag = item_to_basket.AddItemToBasket(basket_list, item_to_basket);
                            items_list[indx - 1].UpdItemStock(items_list[indx - 1], count);

                            if (!flag)
                            {
                                Thread.Sleep(3000);
                                Console.Clear();
                            }
                        }
                        else if (cmd1 == 2)
                        {
                            Console.Clear();
                            PrintBasket(basket_list);

                            Console.WriteLine("Выберите номер товара, который хотите удалить: ");
                            int number = Convert.ToInt32(Console.ReadLine());

                            int index = FindDeletedItem(basket_list, items_list, number);
                            item.ReturnItemToStock(items_list[index], basket_list[number - 1]._b_count);

                            bool flag = DeleteItemFromBasket(basket_list, number);

                            
                            if (!flag)
                            {
                                Thread.Sleep(2000);
                                Console.Clear();
                            }
                        }
                        break;
                    case 3:
                        MakeOrder(basket_list);
                        break;
                }

            }
        }
    }
}



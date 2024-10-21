using lab4;
using System;
using System.Net.Http.Headers;
using System.Windows;

class Programm
{
    static void Main(string []args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        List<Item> items_list = new List<Item>() {
        new Item("Турецкий орнамент", 7500, 15, 160, 230, "A1B"),
        new Item("Современная абстракция", 10000, 10, 200, 300, "3C4"),
        new Item("Классика Востока", 5000, 20, 120, 170, "D5E"),
        new Item("Морская волна", 8500, 12, 160, 240, "F6G"),
        new Item("Пастельные геометрические фигуры", 9000, 8, 180, 250, "H7I"),
        new Item("Скандинавский стиль", 6000, 18, 140, 200, "8J9"),
        new Item("Детский мир", 4500, 25, 130, 190, "K0L"),
        new Item("Этно шик", 11500, 5, 200, 300, "M2N"),
        new Item("Зеленый лес", 7800, 10, 160, 230, "O3P"),
        new Item("Минимализм", 3500, 30, 90, 150, "Q4R")
        };
        List<Basket> basket_list = new List<Basket>();

        Item item = new Item("", 0, 0, 0, 0, "");
        Basket basket = new Basket("", 0, 0, 0, 0, "", 0);
        int cmd = -1;
        

        while (cmd != 3)
        {
            Console.Clear();

            Console.WriteLine("1.Посмотреть ассортимент магазина\n2.Корзина\n3.Выход из программы");
            string temp = Console.ReadLine();
            cmd = Convert.ToInt32(temp);

            switch (cmd - 1)
            {
                case 0:
                    Console.Clear();
                    item.PrintItems(items_list);

                    Console.WriteLine("Для выхода нажмите на клавишу Enter...");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    break;
                case 1:
                    basket.EditBasket(item, basket_list, items_list);
                    break;
                case 2:
                    Console.WriteLine("Выход из программы...");
                    Thread.Sleep(2000);

                    break;
                default:
                    Console.WriteLine("Ошибка ввода. (Введите цифру от 1 до 3)");
                    break;
            }
        }
        System.Environment.Exit(0);
    }
}
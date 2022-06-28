using System;
using System.Collections.Generic;
using System.Linq;

namespace shop
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Player player = new Player();
            Seller seller = new Seller();
            Product product = new Product();
            string[] menu = {"Товар в магазине", "Мой товар", "Передать товар","Выход" };
            int index = 0;
            bool launchingTheProgram = true;

            while (launchingTheProgram)
            {
                Console.SetCursorPosition(0, 0);
                Console.ResetColor();
                Console.WriteLine("\t\tМагазин");

                for (int i = 0; i < menu.Length; i++)
                {
                    if (index == i)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine(menu[i]);
                    Console.ResetColor();
                }

                ConsoleKeyInfo userInput = Console.ReadKey(true);

                switch (userInput.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (index != 0) index--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (index != menu.Length - 1) index++;
                        break;
                    case ConsoleKey.Enter:

                        switch (index)
                        {
                            case 0:
                                seller.ShowProsuct(false);
                                break;
                            case 1:
                                player.ShowPlayerProduct(seller);
                                break;
                            case 2:
                                seller.ShowProsuct(true);
                                player.BuyProduct(seller, product);
                                break;
                            case 3:
                                launchingTheProgram = !launchingTheProgram;
                                break;
                        }
                        break;
                }
            }
        }
    }

    class Player
    {
        private List<Product> _playerProducts = new List<Product>();
        private int _money = 100;

        public void BuyProduct(Seller seller, Product product)
        {
            _playerProducts.Add(seller.CopyProdect()[Exchange(seller)]);
            seller.Clear();
        }

        public int Exchange (Seller seller)
        {
            int indexExchangeProduct = 0;
            Console.Write("Введите номер товара: ");
            switch (int.Parse(Console.ReadLine()))
            {
                case 1:
                    indexExchangeProduct = 0;
                    TakeAwayMoney(0, seller);
                    seller.DeleteProducts(0);
                    break;
                case 2:
                    indexExchangeProduct = 1;
                    TakeAwayMoney(1, seller);
                    seller.DeleteProducts(1);
                    break;
                case 3:
                    indexExchangeProduct = 2;
                    TakeAwayMoney(2, seller);
                    seller.DeleteProducts(2);
                    break;
                default:
                    Console.WriteLine("Вы ввели не верное значение!");
                    break;
            }
            return indexExchangeProduct;
        }

        public void TakeAwayMoney(int index, Seller seller)
        {
            _money -= seller.CopyProdect()[index].MoneyPraic;
            seller.Money(seller.CopyProdect()[index].MoneyPraic);

        }

        public void ShowPlayerProduct(Seller seller)
        {
            Console.WriteLine($"\nУ вас денег {_money}");

            if (CopyProdect().Count > 0)
            {
                Console.WriteLine("Ваши продукты");

                for (int i = 0; i < CopyProdect().Count; i++)
                {
                    CopyProdect()[i].ShowDetalis(i + 1, false);
                }
            }
            else Console.WriteLine("У вас нет продуктов");
            seller.Clear();
        }

        public List<Product> CopyProdect()
        {
            List<Product> products = _playerProducts.ToList();
            return products;
        }
    }

    class Seller
    {
        private List<Product> _products = new List<Product>();
        public int _money { get; private set; }

        public Seller()
        {
            _products.Add(new Product("Хлеб", 5));
            _products.Add(new Product("Рыба", 15));
            _products.Add(new Product("Шоколад", 10));
        }

        public void Money (int money)
        {
            _money = money;
        }

        public void DeleteProducts(int indexDelete)
        {
            _products.RemoveAt(indexDelete);
        }

        public void ShowProsuct(bool isClear)
        {
            if (isClear == true)
            {
                Console.WriteLine();

                IsClear();
            }
            else
            {
                Console.WriteLine($"\nВ магазине {_money} денег");

                IsClear();
                Clear();
            }
        }

        private void IsClear()
        {
            if (CopyProdect().Count >= 1)
            {
                Console.WriteLine("Товар в магазине");

                for (int i = 0; i < CopyProdect().Count; i++)
                {
                    CopyProdect()[i].ShowDetalis(i + 1, true);
                }
            }
            else Console.WriteLine("Больше нет товара в магазине");
        }

        public List<Product> CopyProdect()
        {
            List<Product> products = _products.ToList();
            return products;
        }

        public void Clear()
        {
            Console.ReadKey();
            Console.SetCursorPosition(0, 5);

            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("\t\t\t\t\t\t\t\t\t");
            }
        }
    }

    class Product
    {
        public string NameProduct { get; private set; }
        public int MoneyPraic { get; private set; }

        public Product (string name, int moneyPraic)
        {
            NameProduct = name;
            MoneyPraic = moneyPraic;
        }
        
        public Product() { }
        public void ShowDetalis(int namberProduct, bool isTovar)
        {
            if (isTovar == true)
            {
                Console.WriteLine($"{namberProduct}. Товар {NameProduct}, стоит {MoneyPraic}");
            }
            else Console.WriteLine($"{namberProduct}. Товар {NameProduct}");
        }
    }
}
/*Задача:
Существует продавец, он имеет у себя список товаров и при нужде может вам его показать, также продавец может продать вам товар. 
После продажи товар переходит к вам, и вы можете также посмотреть свои вещи.

Возможные классы – игрок, продавец, товар.

Вы можете сделать так, как вы видите это.*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace shop
{
    class Program
    {
        static void Main ( string [] args )
        {
            Console.CursorVisible = false;
            Player player = new Player ();
            Seller seller = new Seller ();
            string [] menu = { "Товар в магазине", "Мой товар", "Передать товар", "Выход" };
            int index = 0;
            bool launchingTheProgram = true;
            bool isShowProsuct = true;

            while ( launchingTheProgram )
            {
                Console.SetCursorPosition ( 0, 0 );
                Console.ResetColor ();
                Console.WriteLine ( "\t\tМагазин" );

                for ( int i = 0; i < menu.Length; i++ )
                {
                    if ( index == i )
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine ( menu [ i ] );
                    Console.ResetColor ();
                }

                ConsoleKeyInfo userInput = Console.ReadKey ( true );

                switch ( userInput.Key )
                {
                    case ConsoleKey.UpArrow:
                        if ( index != 0 ) index--;
                        break;
                    case ConsoleKey.DownArrow:
                        if ( index != menu.Length - 1 ) index++;
                        break;
                    case ConsoleKey.Enter:

                        switch ( index )
                        {
                            case 0:
                                seller.ShowProsuct ( !isShowProsuct, player );
                                break;
                            case 1:
                                player.ShowPlayerProduct ();
                                break;
                            case 2:
                                seller.ShowProsuct ( isShowProsuct, player );
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
        private List<Product> _playerProducts = new List<Product> ();

        private int _money = 100;

        public void Money ( int money )
        {
            _money -= money;
        }

        public void AddProdukt ( Product product )
        {
            _playerProducts.Add ( product );
        }

        public void ShowPlayerProduct ()
        {
            Console.WriteLine ( $"\nУ вас денег {_money}" );

            if ( CopyProdect ().Count > 0 )
            {
                Console.WriteLine ( "Ваши продукты" );

                for ( int i = 0; i < CopyProdect ().Count; i++ )
                {
                    CopyProdect () [ i ].ShowDetalis ( i + 1);
                }
            }
            else Console.WriteLine ( "У вас нет продуктов" );
            Clear ();
        }

        public List<Product> CopyProdect ()
        {
            List<Product> products = _playerProducts.ToList ();
            return products;
        }

        private void Clear ()
        {
            Console.ReadKey ();
            int numberVacation = 5;
            int numberOfRepetitions = 20;
            Console.SetCursorPosition ( 0, numberVacation );

            for ( int i = 0; i < numberOfRepetitions; i++ )
            {
                Console.WriteLine ( "\t\t\t\t\t\t\t\t\t" );
            }
        }

    }

    class Seller
    {
        private List<Product> _products = new List<Product> ();
        private int _indexExchangeProduct = 0;

        public Seller ()
        {
            _products.Add ( new Product ( "Хлеб", 5 ) );
            _products.Add ( new Product ( "Рыба", 15 ) );
            _products.Add ( new Product ( "Шоколад", 10 ) );
        }

        public int MoneyMa { get; private set; }

        public void Money ( int money )
        {
            MoneyMa += money;
        }

        public void ShowProsuct ( bool isClear, Player player )
        {
            if ( isClear == true )
            {
                Console.WriteLine ();

                IsClear ();
                SellProduct ( player );
            }
            else
            {
                Console.WriteLine ( $"\nВ магазине {MoneyMa} денег" );

                IsClear ();
                Clear ();
            }
        }

        public void SellProduct ( Player player )
        {
            Console.Write ( "Введите номер товара: " );
            _indexExchangeProduct = int.Parse ( Console.ReadLine () );

            player.AddProdukt ( Exchange ( _indexExchangeProduct ) );
            TakeAwayMoney ( _indexExchangeProduct - 1, player );
            DeleteProducts ( _indexExchangeProduct - 1 );
            Clear ();
        }

        private Product Exchange ( int indexExchangeProduct )
        {
            return _products [ indexExchangeProduct - 1 ];
        }

        private void TakeAwayMoney ( int index, Player player )
        {
            player.Money ( CopyProdect () [ index ].MoneyPraic );
            Money ( CopyProdect () [ index ].MoneyPraic );

        }

        private void DeleteProducts ( int indexDelete )
        {
            _products.RemoveAt ( indexDelete );
        }

        private List<Product> CopyProdect ()
        {
            List<Product> products = _products.ToList ();
            return products;
        }

        private void Clear ()
        {
            Console.ReadKey ();
            int numberVacation = 5;
            int numberOfRepetitions = 20;
            Console.SetCursorPosition ( 0, numberVacation );

            for ( int i = 0; i < numberOfRepetitions; i++ )
            {
                Console.WriteLine ( "\t\t\t\t\t\t\t\t\t" );
            }
        }

        private void IsClear ()
        {
            if ( CopyProdect ().Count >= 1 )
            {
                Console.WriteLine ( "Товар в магазине" );

                for ( int i = 0; i < CopyProdect ().Count; i++ )
                {
                    CopyProdect () [ i ].ShowDetalis ( i + 1);
                }
            }
            else Console.WriteLine ( "Больше нет товара в магазине" );
        }

    }

    class Product
    {
        public Product ( string name, int moneyPraic )
        {
            NameProduct = name;
            MoneyPraic = moneyPraic;
        }

        public string NameProduct { get; private set; }

        public int MoneyPraic { get; private set; }

        public void ShowDetalis ( int namberProduct)
        {
           Console.WriteLine ( $"{namberProduct}. Товар {NameProduct}, стоит {MoneyPraic}" );
        }
    }
}
/*Задача:
Существует продавец, он имеет у себя список товаров и при нужде может вам его показать, также продавец может продать вам товар. 
После продажи товар переходит к вам, и вы можете также посмотреть свои вещи.

Возможные классы – игрок, продавец, товар.

Вы можете сделать так, как вы видите это.*/

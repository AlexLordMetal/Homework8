using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace ChristmasToys
{
    public class ProductsActions
    {
        public Products ChristmasProducts { get; set; }

        public void MainMethod()
        {
            //ChristmasProducts = new Products { ProductsList = new List<Product>() };
            //ChristmasProducts = WorkWithFiles.ProductsFromJson("PresentsJson.txt");
            ChristmasProducts = WorkWithFiles.ProductsFromHttpJson(@"https://catalog.api.onliner.by/search/christmasdecor?group=1&page=3");
            
            AreThereProductsWithPriceLessThan(10);
            GetProductWithMinPrice();
            GetProductWithMaxPrice();
            GetProductsWithMinPriceForTotal(50);
            GetRandomProductsForTotal(80);
            SumOfAllProductsToConsole();
            SumOfAllProductsWithPriceLessThanToConsole(40);
            GetProductsWithPriceLessThan(25);
            Console.ReadKey();
        }

        public void AreThereProductsWithPriceLessThan(double maxPrice)
        {
            var ifExists = ChristmasProducts.ProductsList.Any(x => x.Prices.PriceMin.Amount < maxPrice);
            if (ifExists) Console.WriteLine($"Есть товары с ценой меньше {maxPrice} BYN.\n");
            else Console.WriteLine($"Нет товаров с ценой меньше {maxPrice} BYN.\n");
        }

        public void GetProductWithMinPrice()
        {
            Console.WriteLine("Самый дешевый товар:");
            ChristmasProducts.ProductsList = ChristmasProducts.ProductsList.OrderBy(x => x.Prices.PriceMin.Amount).ToList();
            ProductToConsoleWithException(ChristmasProducts.ProductsList.FirstOrDefault());
        }

        public void GetProductWithMaxPrice()
        {
            Console.WriteLine("Самый дорогой товар:");
            ChristmasProducts.ProductsList = ChristmasProducts.ProductsList.OrderBy(x => x.Prices.PriceMin.Amount).ToList();
            ProductToConsoleWithException(ChristmasProducts.ProductsList.LastOrDefault());
        }

        public void GetProductsWithMinPriceForTotal(double totalAmount)
        {
            ChristmasProducts.ProductsList = ChristmasProducts.ProductsList.OrderBy(x => x.Prices.PriceMin.Amount).ToList();

            var products = new Products() { ProductsList = new List<Product>() };
            var sumPrices = 0.0;
            foreach (var product in ChristmasProducts.ProductsList)
            {
                if (sumPrices + product.Prices.PriceMin.Amount <= totalAmount)
                {
                    sumPrices += product.Prices.PriceMin.Amount;
                    products.ProductsList.Add(product);
                }
                else break;
            }

            Console.WriteLine($"Дешевые товары на общую сумму {sumPrices} BYN:");
            ProductsToConsoleWithException(products);
        }

        public void GetRandomProductsForTotal(double totalAmount)
        {
            ChristmasProducts.ProductsList = ChristmasProducts.ProductsList.OrderBy(x => Guid.NewGuid()).ToList();

            var products = new Products() { ProductsList = new List<Product>() };
            var sumPrices = 0.0;
            foreach (var product in ChristmasProducts.ProductsList)
            {
                if (sumPrices + product.Prices.PriceMin.Amount <= totalAmount)
                {
                    sumPrices += product.Prices.PriceMin.Amount;
                    products.ProductsList.Add(product);
                }
            }

            Console.WriteLine($"Случайные товары на общую сумму {sumPrices} BYN:");
            ProductsToConsoleWithException(products);
        }

        public void SumOfAllProductsToConsole()
        {
            var sumOfAllProducts = ChristmasProducts.ProductsList.Sum(x => x.Prices.PriceMin.Amount);
            Console.WriteLine($"Общая стоимость всех товаров - {sumOfAllProducts} BYN.\n");
        }

        public void SumOfAllProductsWithPriceLessThanToConsole(double maxPrice)
        {
            var productList = ChristmasProducts.ProductsList.Where(x => x.Prices.PriceMin.Amount < maxPrice).ToList();
            var sumOfAllProducts = productList.Sum(x => x.Prices.PriceMin.Amount);
            Console.WriteLine($"Общая стоимость всех товаров c ценой до {maxPrice} BYN - {sumOfAllProducts} BYN.\n");
        }

        public void GetProductsWithPriceLessThan(double maxPrice)
        {
            var products = new Products() { ProductsList = new List<Product>() };
            products.ProductsList = ChristmasProducts.ProductsList.Where(x => x.Prices.PriceMin.Amount < maxPrice).ToList();

            Console.WriteLine($"Ссисок товаров с ценой меньше {maxPrice} BYN:");
            ProductsToConsoleWithException(products);
        }

        public void ProductToConsoleWithException(Product product)
        {
            if (product == null) Console.WriteLine("Продукта не существует!");
            else
            {
                Console.Write("\t");
                product.ToConsole();
                ProductRequestOpenURL(product);
            }
            Console.WriteLine();
        }

        public void ProductsToConsoleWithException(Products products)
        {
            if (products.ProductsList == null || products.ProductsList.Count == 0) Console.WriteLine("Таких продуктов не существует!");
            else
            {
                products.ToConsole();
                ProductsRequestOpenURL(products);
            }
            Console.WriteLine();
        }

        public void ProductRequestOpenURL(Product product)
        {
            if (product.URL != null)
            {
                Console.WriteLine("Открыть URL-страницу данного товара?\n1 - да\n2 - нет");
                if (ConsoleRequestForInt(2) == 1) Process.Start(product.URL);
            }
        }

        public void ProductsRequestOpenURL(Products products)
        {
            Console.WriteLine($"Выберите, URL-страницу какого товара хотите открыть:\n1-{products.ProductsList.Count} - выберите товар\n{products.ProductsList.Count + 1} - не открывать");
            var productIndex = ConsoleRequestForInt(products.ProductsList.Count + 1) - 1;
            if (productIndex < products.ProductsList.Count) Process.Start(products.ProductsList[productIndex].URL);
        }

        public static int ConsoleRequestForInt(int condition = Int32.MaxValue)
        {
            var isCorrect = false;
            int area = 0;
            while (isCorrect != true)
            {
                isCorrect = Int32.TryParse(Console.ReadLine(), out area);
                if (area <= 0 || area > condition) isCorrect = false;
                if (isCorrect == false) Console.Write("Ввод некорректен! Еще раз: ");
            }
            return area;
        }

    }
}
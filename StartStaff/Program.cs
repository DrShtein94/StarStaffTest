using StartStaff.Model;
using System;
using System.Collections.Generic;

namespace StartStaff
{
    class Program
    {
        static void Main(string[] args)
        {
            var someData = new List<TravelCard>()
            {   new TravelCard("Питер", "Владивосток"),
                new TravelCard("Казань","Можайск"),
                new TravelCard("Владивосток", "Тверь"),
                new TravelCard("Сочи", "Анапа"),
                new TravelCard("Тверь", "Сочи"),
                new TravelCard("Москва", "Питер"),
                new TravelCard("Калуга", "Казань"),
                new TravelCard("Можайск", "Москва")
             };

            Console.WriteLine("Входные данные:");
            Console.WriteLine(TravelCard.CollectionToString(someData));
            var result = TravelCard.Sort(someData);
            Console.WriteLine("Результат:");
            Console.WriteLine(TravelCard.CollectionToString(result));
            Console.ReadKey();
        }
    }
}

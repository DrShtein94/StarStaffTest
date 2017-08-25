using System;
using System.Collections.Generic;

namespace StartStaff.Model
{
    /// <summary>
    /// Класс описывающий карточку путешествия
    /// </summary>
    public class TravelCard
    {
        /// <summary>
        /// Название пункта отправления
        /// </summary>
        public string Departure { get; set; }
        /// <summary>
        /// Название пункта прибытия
        /// </summary>
        public string Arrival { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="departure">Пункт отправления</param>
        /// <param name="arrival">Пункт прибытия</param>
        public TravelCard(string departure, string arrival)
        {
            Departure = departure;
            Arrival = arrival;
        }

        /// <summary>
        /// Сортировка карточек путешествия
        /// </summary>
        /// <param name="travelCards">Коллекция неотсортированных карточек</param>
        /// <returns></returns>
        public static IEnumerable<TravelCard> Sort(IEnumerable<TravelCard> travelCards)
        {
            //Создаем список, в который сохраним результат
            var result = new List<TravelCard>();
            //Создаем хеш таблицу, в которой в качестве ключа будем хранить пункт отправления,
            //а в качестве значения - ссылку на карточку
            var hashTable = new Dictionary<string, TravelCard>();
            //Создадим множество, которое будет содержать пункты маршрута, указанные в карточках
            var cities = new HashSet<string>();
            //Размер коллекции карточек
            ulong cardsCount = 0;

            //Перебираем все карточки
            foreach (var card in travelCards)
            {
                try
                {
                    //Каждую карточку заносим в хеш таблицу
                    hashTable.Add(card.Departure, card);
                }
                // Если данный пункт отправления уже занесен в хеш таблицу, как ключ
                // это означает, что путешественник уже побывал в этом городе => в коллекции карточек присутствует цикличность
                catch (ArgumentException)
                {
                    throw new TravelCardException("Ошибка: в списке карточек присутствует цикличность!");
                }

                //Добавляем пункт отправления и пункт прибытия в множество городов
                //Если город уже содержиться в множестве, то удаляем его
                //Если в коллекции карточек отсутствуют разрывы и цикличность, после прохождения цикла
                //в множестве должно остаться 2 пункта маршрута - начальный пункт и конечный
                AddElementToSet<string>(ref cities, card.Departure);
                AddElementToSet<string>(ref cities, card.Arrival);
                cardsCount++;
            }

            if (cardsCount < 1)
            {
                throw new TravelCardException("Ошибка: список карточек пуст");
            }

            //Если в множестве содержится более 2 элементов => в коллекции карточек присутствует разрыв
            if (cities.Count > 2)
            {
                throw new TravelCardException("Ошибка: список карточек не является непрерывным!");
            }

            //Если в множестве содержится менее 2 элементов => в коллекции карточек присутствует цикличность
            if (cities.Count < 2)
            {
                throw new TravelCardException("Ошибка: в списке карточек присутствует цикличность!");
            }

            //Перебираем оставшиеся (2) пункта маршрута
            foreach (var city in cities)
            {
                //Если пункта маршрута нет в хеш таблице => это конечный пункт назначения
                //Что означает, что другой элемент множества - начальный пункт назначения
                if (!hashTable.ContainsKey(city)) continue;

                //Выбираем из хеш таблицы начальный пункт маршрута (начальную карточку)
                //Записываем данную карточку в список с результатом
                //И удаляем карточку из хеш таблицы
                var firstElement = hashTable[city];
                result.Add(firstElement);
                hashTable.Remove(firstElement.Departure);

                //Далее выбираем карточки из хеш таблицы, у которых пункт отправления равен пункту назначения
                //в последней отсортированной карточке
                //После "выбора" карточки из хеш таблицы, она оттуда удаляется
                for (var i = 1; i < hashTable.Count; i++)
                {
                    var elementToAdd = hashTable[result[i - 1].Arrival];
                    result.Add(elementToAdd);
                    hashTable.Remove(elementToAdd.Departure);
                }
            }

            return result;
        }

        /// <summary>
        /// Представление коллекции карточек путешествия в строчном виде
        /// </summary>
        /// <param name="cards">Коллекция карточек путешествия</param>
        /// <returns>Коллекция карточек путешествия, в строчном представлении</returns>
        public static string CollectionToString(IEnumerable<TravelCard> cards)
        {
            var result = "";
            foreach (var card in cards)
            {
                result += card.Departure + " -> " + card.Arrival + "\n";
            }
            return result;
        }

        /// <summary>
        /// Добавление элемента в множество.
        /// По следующему правилу, если равный добовляемому элементу, уже содержиться в множестве
        /// то удаляем его
        /// </summary>
        /// <typeparam name="T">Тип элемента</typeparam>
        /// <param name="set">Множество, в которое производится добавление</param>
        /// <param name="element">Добовляемый элемент</param>
        private static void AddElementToSet<T>(ref HashSet<T> set, T element)
        {
            if (set.Contains(element)) set.Remove(element);
            else set.Add(element);
        }
    }

    /// <summary>
    /// Тип исключения для операций, связанных с карточками путешествий
    /// </summary>
    public class TravelCardException : Exception
    {
        public TravelCardException(string message) : base(message) { }
    }
}
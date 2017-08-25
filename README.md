# Тестовое задание для Star-Staff
## Описание задания

**Задача:**

Вы собираетесь совершить долгое путешествие через множество населенных пунктов. Чтобы не запутаться, вы сделали карточки вашего путешествия. Каждая карточка содержит в себе пункт отправления и пункт назначения.
Гарантируется, что если упорядочить эти карточки так, чтобы для каждой карточки в упорядоченном списке пункт назначения на ней совпадал с пунктом отправления в следующей карточке в списке, получится один список карточек без циклов и пропусков.
Например, у нас есть карточки
* Мельбурн > Кельн
* Москва > Париж
* Кельн > Москва


Если упорядочить их в соответствии с требованиями выше, то получится следующий список:
Мельбурн > Кельн, Кельн > Москва, Москва > Париж 

**Требуется:**
1. Написать функцию, которая принимает набор неупорядоченных карточек и возвращает набор упорядоченных карточек в соответствии с требованиями выше, то есть в возвращаемом из функции списке карточек для каждой карточки пункт назначения на ней должен совпадать с пунктом отправления на следующей карточке.
2. Дать оценку сложности получившегося алгоритма сортировки
3. Написать тесты

## Оценка сложности алгоритма
**Время выполнения - O(8n)**

Т.к. в первом цикле, где перебираются карточки путешествия
```csharp
foreach (var card in travelCards)
{ ... }
```
Идет построение одной хеш таблицы - О(1), и хеш множества для всех пунктов маршрута - О(4) (т.к. в карточке два города, и сначала производится поиск элемента, а затем добавление/удаление)
=> данный участок алгоритма имеет сложность - O(5n), так как сложность перебора карточек O(n), а на каждом шаге алгоритма О(5). 
Далее после проверки всех исключительных ситуаций, зная начальную карточку, мы "достаем" из хеш таблицы все карточки, пока они не закончатся:
```csharp
for (var i = 1; i < hashTable.Count; i++)
{ ... }
```
Поиск карточки в хеш таблице - О(1), добавление карточки в список - О(1), удаление карточки из хеш таблицы - О(1) => 
Получаем сложность данного блока алгоритма О(3n)

Складывая получившееся время выполнения, получаем: O(5n) + O(3n) = **O(8n)**

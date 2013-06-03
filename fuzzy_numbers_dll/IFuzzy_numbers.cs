using System;
namespace fuzzy_numbers_dll
{
    public interface IFuzzy_numbers<Tip_Element> 
     where Tip_Element : IComparable<Tip_Element>, IEquatable<Tip_Element>
    {
        /// <summary>
        /// Добавляет в нечёткое число новый элемент.
        /// </summary>
        /// <param name="Element">Элемент нечёткого числа</param>
        void Add(Element_Fuzzy_numbers<Tip_Element> Element);

        /// <summary>
        /// Добавляет в нечёткое число новый элемент.
        /// </summary>
        /// <param name="Element">Элемент нечёткого числа</param>
        /// <param name="Accessory_Function">Значение функции принадлежества элемента к нечёткому числу</param>
        void Add(Tip_Element Element, double Accessory_Function = 0);

        /// <summary>
        /// Возвращает глубокую копию нечёткого числа
        /// </summary>
        /// <returns>Возвращает глубокую копию нечёткого числа</returns>
        Object Clone();

        /// <summary>
        /// Определяет содержится ли в данном нечётком числе элемент с указанном значением функции принадлежности
        /// </summary>
        /// <param name="AccessoryFunction">Значение функции принадлежности</param>
        /// <returns>true - если содержится, иначе false</returns>
        bool ContainsAccessoryFunction(double AccessoryFunction);

        /// <summary>
        /// Определяет содержатся ли в нечетком числе заданный элемент
        /// </summary>
        /// <param name="Element">Элемент при записи (функция принадложности)/(Элемент)</param>
        /// <returns>true - если содержится, иначе false</returns>
        bool ContainsElement(Tip_Element Element);

        /// <summary>
        /// Определяет содержится ли в данном нечётком числе указанный элемент нечёткого числа
        /// </summary>
        /// <param name="FuzzyElement">Элемент нечёткого числа</param>
        /// <returns>true - если содержится, иначе false</returns>
        bool ContainsFuzzyElement(Element_Fuzzy_numbers<Tip_Element> FuzzyElement);

        /// <summary>
        /// Получает количество элементов в нечётком числе
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Элемент на текущей позиции
        /// </summary>
        object Current { get; }

        /// <summary>
        /// Удаляет из нечёткого числа указанный элемент.
        /// </summary>
        /// <param name="Element">Элемент нечёткого числа</param>
        void Delete(Element_Fuzzy_numbers<Tip_Element> Element);

        /// <summary>
        /// Удаляет из нечёткого числа указанный элемент.
        /// </summary>
        /// <param name="Element">Элемент нечёткого числа</param>
        /// <param name="Accessory_Function">Значение функции принадлежества элемента к нечёткому числу</param>
        void Delete(Tip_Element Element, double Accessory_Function = 0);

        /// <summary>
        /// Удаляет из нечёткого числа элемент по указанному индексу.
        /// </summary>
        /// <param name="Index">Индекс элемента нечёткого числа</param>
        void DeleteAt(int Index);

        /// <summary>
        /// Переопределение базового сравнения
        /// </summary>
        /// <param name="obj">Объект для сравнения с текущим</param>
        /// <returns>true если равны false иначе</returns>
        bool Equals(object obj);

        /// <summary>
        /// Создает и возвращает "перечислитель", позволяющий перебирать в цикле все элементы списка.
        /// </summary>
        /// <returns>возвращает "перечислитель", позволяющий перебирать в цикле все элементы списка.</returns>
        System.Collections.IEnumerator GetEnumerator();

        /// <summary>
        /// Возвращает хеш-код
        /// </summary>
        /// <returns>Возвращает хеш-код нечеткого числа, уникальность не гарантируется</returns>
        int GetHashCode();

        /// <summary>
        /// Сдвинуть счетчик на +1 и если индекс не превысил допустимый вернуть true
        /// </summary>
        /// <returns></returns>
        bool MoveNext();

        /// <summary>
        /// [read only] Возвращает название числа
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Сброс счетчика
        /// </summary>
        void Reset();

        /// <summary>
        /// Определяет содержится ли в данном нечётком числе элемент с указанном значением функции принадлежности
        /// И возвращает ПЕРВЫЙ элемент содержащий её если такой есть
        /// </summary>
        /// <param name="AccessoryFunction">Значение функции принадлежности</param>
        /// <returns>Елемент с данной вункцией принадлежности</returns>
        Element_Fuzzy_numbers<Tip_Element> ReturnElementForAccessoryFunction(double AccessoryFunction);
        
        /// <summary>
        /// Определяет содержатся ли в нечетком числе заданный элемент
        /// И возвращает первое вхождение элемента в данном числе
        /// </summary>
        /// <param name="Element">Элемент при записи (функция принадложности)/(Элемент)</param>
        /// <returns>Елемент нечеткого множества</returns>
        Element_Fuzzy_numbers<Tip_Element> ReturnElementForElement(Tip_Element Element);

        /// <summary>
        /// Определяет содержатся ли в нечетком числе заданный элемент
        /// И возвращает номер первого вхождения
        /// </summary>
        /// <param name="Element">Элемент при записи (функция принадложности)/(Элемент)</param>
        /// <returns>Номер первого вхождения</returns>
        int ReturnIndexForElement(Tip_Element Element);

        /// <summary>
        /// Возвращает отсортированное по Функции принадлежности  в порядке возрастания нечеткое число
        /// </summary>
        Fuzzy_numbers<Tip_Element> Sort_from_Accessory_Function { get; }

        /// <summary>
        /// Возвращает отсортированное по Элементу в порядке возрастания принадлежности нечеткое число
        /// </summary>
        Fuzzy_numbers<Tip_Element> Sort_from_Element { get; }

        /// <summary>
        /// [Желательно Read only] Возвращает или задаёт элемент по указанному индексу
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        Element_Fuzzy_numbers<Tip_Element> this[int i] { get; set; }

        /// <summary>
        /// Возвращает строковое представление нечеткого числа;
        /// </summary>
        /// <returns>Возвращает строковое представление нечеткого числа;</returns>
        string ToString();
    }
}

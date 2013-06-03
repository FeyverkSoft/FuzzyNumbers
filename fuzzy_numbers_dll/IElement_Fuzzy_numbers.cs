using System;
namespace fuzzy_numbers_dll
{
   public interface IElement_Fuzzy_numbers<Tip_Element> 
     where Tip_Element : IComparable<Tip_Element>, IEquatable<Tip_Element>
    {
        /// <summary>
        /// [read only] Возвращает значение функции принадлежности элемента к нечёткому числу
        /// </summary>
        double Accessory_Function { get; }

        /// <summary>
        /// Создаёт глубокую копию объекта
        /// </summary>
        /// <returns>Возвращает глубокую копию объекта</returns>
        Object Clone();

        /// <summary>
        /// [read only] Возвращает элемент нечёткого числу
        /// </summary>
        Tip_Element Element { get; }

        /// <summary>
        /// Переопределение базового сравнения
        /// </summary>
        /// <param name="obj">Объект для сравнения с текущим</param>
        /// <returns>true если равны false иначе</returns>
        bool Equals(object obj);

        /// <summary>
        /// Возвращает хеш-код
        /// </summary>
        /// <returns></returns>
        int GetHashCode();

        /// <summary>
        /// Возвращает строковое представление данного элемента нечеткого числа
        /// </summary>
        /// <returns>Строковое предствавление элемента нечеткого числа</returns>
        string ToString();
    }
}

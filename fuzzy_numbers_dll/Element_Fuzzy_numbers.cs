using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fuzzy_numbers_dll
{
    /// <summary>
    /// Элемент нечёткого числа
    /// </summary>
 public class Element_Fuzzy_numbers<Tip_Element> : fuzzy_numbers_dll.IElement_Fuzzy_numbers<Tip_Element>  where Tip_Element : System.IComparable<Tip_Element>, IEquatable<Tip_Element>
    {
        #region Переменные класса
        /// <summary>
        /// Элемент числа
        /// </summary>
        fuzzu element = new fuzzu();
        #endregion

        #region Структуры класса
        /// <summary>
        /// структура описывающая 1 элемент нечёткого числа
        /// </summary>
        struct fuzzu
        {
            Tip_Element element;//элемент числа
            Double accessory_Function;//функция принадлежности элемента, данному числу (от 0 до 1)
            /// <summary>
            /// Элемент нечеткого числа
            /// </summary>
            public Tip_Element Element
            {
                get { return element; }
                set { element = value; }
            }
            /// <summary>
            /// функция принадлежности элемента, данному числу (от 0 до 1)
            /// </summary>
            public Double Accessory_Function
            {
                get { return accessory_Function; }//если не null тогда возвращаем значение, иначе 0
                set { accessory_Function = value; }//присваиваем значение
            }
        }
        #endregion

        #region Свойства класса

        /// <summary>
        /// [read only] Возвращает значение функции принадлежности элемента к нечёткому числу
        /// </summary>
        public Double Accessory_Function
        {
            get { return element.Accessory_Function; }
        }

        /// <summary>
        /// [read only] Возвращает элемент нечёткого числу
        /// </summary>
        public Tip_Element Element
        {
            get { return element.Element; }
        }



        #endregion

        #region Функции класса
        /// <summary>
        /// Конструктор, принимает элемент числа, и функцию принадлежности
        /// </summary>
        /// <param name="Element">Элемент числа</param>
        /// <param name="Accessory_Function">Функция принадлежности</param>
        public Element_Fuzzy_numbers(Tip_Element Element, Double Accessory_Function = 0)
        {
            element.Element = Element;//присваиваем элементу значение
            element.Accessory_Function = Accessory_Function;//присваиваем функции принадлежности значение
        }
        /// <summary>
        /// Конструктор, по умолчанию
        /// </summary>
        public Element_Fuzzy_numbers()
        {
            element.Accessory_Function = Accessory_Function;//присваиваем функции принадлежности значение
        }
        #endregion

        #region Операторы класса
        /// <summary>
        /// Сравнивает 2 элемента числа, и если они РАВНЫ возвращает true в противном случае false 
        /// </summary>
        /// <param name="a">Первый элемент для сравнения</param>
        /// <param name="b">Второй элемент для сравнения</param>
        /// <returns>Если элементы равны true иначе false</returns>
        public static Boolean operator ==(Element_Fuzzy_numbers<Tip_Element> a, Element_Fuzzy_numbers<Tip_Element> b)
        {
            if ((Object)a == null & (Object)b == null)//проверить на null
                return true;

            if ((Object)a == null || (Object)b == null)//проверить на null
                return false;
            return ((a.Element.CompareTo(b.Element) == 0) && (a.Accessory_Function == b.Accessory_Function));
        }

        /// <summary>
        /// Сравнивает 2 элемента числа, и если они НЕ РАВНЫ возвращает true в противном случае false 
        /// </summary>
        /// <param name="a">Первый элемент для сравнения</param>
        /// <param name="b">Второй элемент для сравнения</param>
        /// <returns>Если элементы НЕ РАВНЫ true иначе false</returns>
        public static Boolean operator !=(Element_Fuzzy_numbers<Tip_Element> a, Element_Fuzzy_numbers<Tip_Element> b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Отрицает элемент нечёткого числа
        /// </summary>
        /// <param name="a">Элемент для отрицания</param>
        /// <returns>Возвращает обратный элемент данному</returns>
        public static Element_Fuzzy_numbers<Tip_Element> operator !(Element_Fuzzy_numbers<Tip_Element> a)
        {
            return new Element_Fuzzy_numbers<Tip_Element>(a.Element, 1 - a.Accessory_Function);
        }
        #endregion

        #region ICloneable, IComparable, IEquatable
        /// <summary>
        /// Создаёт глубокую копию объекта
        /// </summary>
        /// <returns>Возвращает глубокую копию объекта</returns>
        public Element_Fuzzy_numbers<Tip_Element> Clone()
        {
            return new Element_Fuzzy_numbers<Tip_Element>(Element, Accessory_Function);
        }

        /// <summary>
        /// Возвращает хеш-код
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (Accessory_Function + " " + Element).GetHashCode();
        }

        /// <summary>
        /// Переопределение базового сравнения
        /// </summary>
        /// <param name="obj">Объект для сравнения с текущим</param>
        /// <returns>true если равны false иначе</returns>
        public override bool Equals(System.Object obj)
        {
            Element_Fuzzy_numbers<Tip_Element> p = obj as Element_Fuzzy_numbers<Tip_Element>;
            if ((System.Object)p == null || obj == null)
            {
                return false;
            }
            return (Element.CompareTo(p.Element) == 0) && (Accessory_Function == p.Accessory_Function);
        }

        /// <summary>
        /// Возвращает строковое представление данного элемента нечеткого числа
        /// </summary>
        /// <returns>Строковое предствавление элемента нечеткого числа</returns>
        public override string ToString()
        {
            return Accessory_Function.ToString() + "/" + Element.ToString();
        }
        #endregion
    }
}

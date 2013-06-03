using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fuzzy_numbers_dll
{
    /// <summary>
    /// Класс сортирующий нечёткие числа
    /// </summary>
    class sort<Tip_Element> where Tip_Element : System.IComparable<Tip_Element>, IEquatable<Tip_Element>
    {
        /// <summary>
        /// Сортировка нечёткого числа по значению элемента числа
        /// </summary>
        /// <param name="A">Нечёткое число</param>
        /// <param name="low">Начальный элемент</param>
        /// <param name="high">Конечный элемент</param>
        public static Fuzzy_numbers<Tip_Element> qSort_from_Element(Fuzzy_numbers<Tip_Element> A, int low = 0, int high = 0)
        {
            int i = low;
            int j = high;
            Element_Fuzzy_numbers<Tip_Element> x = A[(low + high) / 2];  // x - опорный элемент посредине между low и high
            do
            {
                while (A[i].Element.CompareTo(x.Element) < 0) ++i;  // поиск элемента для переноса в старшую часть
                while (A[j].Element.CompareTo(x.Element) > 0) --j;  // поиск элемента для переноса в младшую часть
                if (i <= j)
                {
                    // обмен элементов местами:
                    Element_Fuzzy_numbers<Tip_Element> temp = A[i];
                    A[i] = A[j];
                    A[j] = temp;
                    // переход к следующим элементам:
                    i++; j--;
                }
            } while (i < j);
            if (low < j) qSort_from_Element(A, low, j);
            if (i < high) qSort_from_Element(A, i, high);
            return A;
        }

        /// <summary>
        /// Сортировка нечёткого числа по значению функции принадлежности элемента числа
        /// </summary>
        /// <param name="A">Нечёткое число</param>
        /// <param name="low">Начальный элемент</param>
        /// <param name="high">Конечный элемент</param>
        public static Fuzzy_numbers<Tip_Element> qSort_from_Accessory_Function(Fuzzy_numbers<Tip_Element> A, int low = 0, int high = 0)
        {
            int i = low;
            int j = high;
            Element_Fuzzy_numbers<Tip_Element> x = A[(low + high) / 2];  // x - опорный элемент посредине между low и high
            do
            {
                while (A[i].Accessory_Function < x.Accessory_Function) ++i;  // поиск элемента для переноса в старшую часть
                while (A[j].Accessory_Function > x.Accessory_Function) --j;  // поиск элемента для переноса в младшую часть
                if (i <= j)
                {
                    // обмен элементов местами:
                    Element_Fuzzy_numbers<Tip_Element> temp = A[i];
                    A[i] = A[j];
                    A[j] = temp;
                    // переход к следующим элементам:
                    i++; j--;
                }
            } while (i < j);
            if (low < j) qSort_from_Accessory_Function(A, low, j);
            if (i < high) qSort_from_Accessory_Function(A, i, high);
            return A;
        }
    }

}

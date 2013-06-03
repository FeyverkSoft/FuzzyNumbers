using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fuzzy_numbers_dll
{
    /// <summary>
    /// Нечёткое число с базовыми операторами
    /// </summary>
    /// <typeparam name="Tip_Element">Тип значения нечеткого числа (необходима реализация IComparable)</typeparam>
    public class Fuzzy_numbers<Tip_Element> : IEnumerable, IEnumerator, IEquatable<Object>, fuzzy_numbers_dll.IFuzzy_numbers<Tip_Element> where Tip_Element : System.IComparable<Tip_Element>, IEquatable<Tip_Element>
    {
        #region Переменные класса
        /// <summary>
        /// Список элементов нечеткого числа
        /// </summary>
        List<Element_Fuzzy_numbers<Tip_Element>> Mass = new List<Element_Fuzzy_numbers<Tip_Element>>();
        /// <summary>
        /// Название числа если нужно
        /// </summary>
        String name = "";
        int position = -1;//текущая позиция для реализации IEnumerator, IEnumerable
        #endregion

        #region Реализация интерфейсов IEnumerator, IEnumerable
        /// <summary>
        /// Создает и возвращает "перечислитель", позволяющий перебирать в цикле все элементы списка.
        /// </summary>
        /// <returns>возвращает "перечислитель", позволяющий перебирать в цикле все элементы списка.</returns>
        public IEnumerator GetEnumerator()
        {
            position = -1;
            return (IEnumerator)this;
        }

        /// <summary>
        /// Элемент на текущей позиции
        /// </summary>
        public object Current
        {
            get { return Mass[position]; }
        }

        /// <summary>
        /// Сдвинуть счетчик на +1 и если индекс не превысил допустимый вернуть true
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            position++;
            return (position < Count);
        }

        /// <summary>
        /// Сброс счетчика
        /// </summary>
        public void Reset()
        {
            position = 0;
        }
        #endregion

        #region Свойства класса

        /// <summary>
        /// [read only] Возвращает название числа
        /// </summary>
        public String Name
        {
            get { return name; }
        }

        /// <summary>
        /// [Желательно Read only] Возвращает или задаёт элемент по указанному индексу
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Element_Fuzzy_numbers<Tip_Element> this[Int32 i]
        {
            get { return Mass[i]; }
            set { Mass[i] = value; }
        }

        /// <summary>
        /// Получает количество элементов в нечётком числе
        /// </summary>
        public Int32 Count
        {
            get { return Mass.Count; }
        }

        /// <summary>
        /// Возвращает отсортированное по Функции принадлежности  в порядке возрастания нечеткое число
        /// </summary>
        public Fuzzy_numbers<Tip_Element> Sort_from_Accessory_Function
        {
            get
            {
                return sort<Tip_Element>.qSort_from_Accessory_Function(this, 0, (this.Count - 1));
            }
        }

        /// <summary>
        /// Возвращает отсортированное по Элементу в порядке возрастания принадлежности нечеткое число
        /// </summary>
        public Fuzzy_numbers<Tip_Element> Sort_from_Element
        {
            get
            {
                return sort<Tip_Element>.qSort_from_Element(this, 0, (this.Count - 1));
            }
        }
        #endregion

        #region Конструкторы класса

        /// <summary>
        /// Конструктор нечёткого числа 
        /// </summary>
        /// <param name="Mass">Нечёткое число</param>
        /// <param name="name">Название нечёткого числа</param>
        public Fuzzy_numbers(Fuzzy_numbers<Tip_Element> FuzzyNumbers, String name = "")
        {
            this.Mass = FuzzyNumbers.Mass;
            if (name.Equals(""))
                this.name = FuzzyNumbers.Name;
            else
                this.name = name;
        }

        /// <summary>
        /// Конструктор нечёткого числа 
        /// </summary>
        /// <param name="Mass">Список элементов нечёткого числа</param>
        /// <param name="name">Название нечёткого числа</param>
        public Fuzzy_numbers(List<Element_Fuzzy_numbers<Tip_Element>> Mass, String name = "")
        {
            foreach (Element_Fuzzy_numbers<Tip_Element> e in Mass)
                this.Add(e);
            this.name = name;
        }

        /// <summary>
        /// Конструктор нечёткого числа 
        /// </summary>
        /// <param name="Mass">Массив элементов нечёткого числа</param>
        /// <param name="name">Название нечёткого числа</param>
        public Fuzzy_numbers(Element_Fuzzy_numbers<Tip_Element>[] Mass, String name = "")
        {
            foreach (Element_Fuzzy_numbers<Tip_Element> e in Mass)
                this.Add(e);
            this.name = name;
        }

        /// <summary>
        /// Конструктор нечёткого числа 
        /// </summary>
        /// <param name="name">Название нечёткого числа</param>
        public Fuzzy_numbers(String name = "")
        {
            this.Mass = new List<Element_Fuzzy_numbers<Tip_Element>>();
            this.name = name;
        }
        #endregion

        #region GetHashCode, Equals, ToString, Clone
        /// <summary>
        /// Возвращает хеш-код
        /// </summary>
        /// <returns>Возвращает хеш-код нечеткого числа, уникальность не гарантируется</returns>
        public override int GetHashCode()
        {
            return Mass.GetHashCode();
        }

        /// <summary>
        /// Переопределение базового сравнения
        /// </summary>
        /// <param name="obj">Объект для сравнения с текущим</param>
        /// <returns>true если равны false иначе</returns>
        public override bool Equals(System.Object obj)
        {
            Fuzzy_numbers<Tip_Element> p = obj as Fuzzy_numbers<Tip_Element>;
            if ((System.Object)p == null || obj == null)
            {
                return false;
            }
            return (Mass == p.Mass);
        }

        /// <summary>
        /// Возвращает строковое представление нечеткого числа;
        /// </summary>
        /// <returns>Возвращает строковое представление нечеткого числа;</returns>
        public override string ToString()
        {
            String s = "";
            Int32 D = Count;
            for (Int32 i = 0; i < D; i++)
            {
                if (i < D - 1)
                {
                    s += Mass[i].ToString() + " + ";
                }
                else
                {
                    s += Mass[i].ToString();
                }
            }
            if (Name != "")
                return Name + "=" + s;
            else
                return s;
        }

        /// <summary>
        /// Возвращает глубокую копию нечёткого числа
        /// </summary>
        /// <returns>Возвращает глубокую копию нечёткого числа</returns>
        public Fuzzy_numbers<Tip_Element> Clone()
        {
            return new Fuzzy_numbers<Tip_Element>(Mass, Name);
        }

        #endregion

        #region Функции add, delete, deleteAlt, ContainsElement, ContainsAccessoryFunction, ContainsFuzzyElement

        /// <summary>
        /// Определяет содержатся ли в нечетком числе заданный элемент
        /// </summary>
        /// <param name="Element">Элемент при записи (функция принадложности)/(Элемент)</param>
        /// <returns>true - если содержится, иначе false</returns>
        public Boolean ContainsElement(Tip_Element Element)
        {
            foreach (Element_Fuzzy_numbers<Tip_Element> e in Mass)
                if (e.Element.CompareTo(Element) == 0)
                    return true;
            return false;
        }

        /// <summary>
        /// Определяет содержатся ли в нечетком числе заданный элемент
        /// И возвращает номер первого вхождения
        /// </summary>
        /// <param name="Element">Элемент при записи (функция принадложности)/(Элемент)</param>
        /// <returns>Номер первого вхождения</returns>
        public Int32 ReturnIndexForElement(Tip_Element Element)
        {
            for (Int32 i = 0; i < Mass.Count; i++)
                if (this[i].Element.CompareTo(Element) == 0)
                    return i;
            return -1;
        }

        /// <summary>
        /// Определяет содержатся ли в нечетком числе заданный элемент
        /// И возвращает первое вхождение элемента в данном числе
        /// </summary>
        /// <param name="Element">Элемент при записи (функция принадложности)/(Элемент)</param>
        /// <returns>Елемент нечеткого множества</returns>
        public Element_Fuzzy_numbers<Tip_Element> ReturnElementForElement(Tip_Element Element)
        {
            foreach (Element_Fuzzy_numbers<Tip_Element> e in Mass)
                if (e.Element.CompareTo(Element) == 0)
                    return e;
            return null;
        }

        /// <summary>
        /// Определяет содержится ли в данном нечётком числе элемент с указанном значением функции принадлежности
        /// </summary>
        /// <param name="AccessoryFunction">Значение функции принадлежности</param>
        /// <returns>true - если содержится, иначе false</returns>
        public Boolean ContainsAccessoryFunction(Double AccessoryFunction)
        {
            foreach (Element_Fuzzy_numbers<Tip_Element> e in Mass)
                if (e.Accessory_Function == AccessoryFunction)
                    return true;
            return false;
        }

        /// <summary>
        /// Определяет содержится ли в данном нечётком числе элемент с указанном значением функции принадлежности
        /// И возвращает ПЕРВЫЙ элемент содержащий её если такой есть
        /// </summary>
        /// <param name="AccessoryFunction">Значение функции принадлежности</param>
        /// <returns>Елемент с данной вункцией принадлежности</returns>
        public Element_Fuzzy_numbers<Tip_Element> ReturnElementForAccessoryFunction(Double AccessoryFunction)
        {
            foreach (Element_Fuzzy_numbers<Tip_Element> e in Mass)
                if (e.Accessory_Function == AccessoryFunction)
                    return e;
            return null;
        }

        /// <summary>
        /// Определяет содержится ли в данном нечётком числе указанный элемент нечёткого числа
        /// </summary>
        /// <param name="FuzzyElement">Элемент нечёткого числа</param>
        /// <returns>true - если содержится, иначе false</returns>
        public Boolean ContainsFuzzyElement(Element_Fuzzy_numbers<Tip_Element> FuzzyElement)
        {
            return this.Mass.Contains(FuzzyElement);
        }

        /// <summary>
        /// Добавляет в нечёткое число новый элемент.
        /// </summary>
        /// <param name="Element">Элемент нечёткого числа</param>
        public void Add(Element_Fuzzy_numbers<Tip_Element> Element)
        {
            if (Mass == null)//если число не объявлено, объявляем его
                Mass = new List<Element_Fuzzy_numbers<Tip_Element>>();

            if (!Mass.Contains(Element))//если такого элемента еще в числе нету
                Mass.Add(Element);//добавляем элемент
        }

        /// <summary>
        /// Добавляет в нечёткое число новый элемент.
        /// </summary>
        /// <param name="Element">Элемент нечёткого числа</param>
        /// <param name="Accessory_Function">Значение функции принадлежества элемента к нечёткому числу</param>
        public void Add(Tip_Element Element, Double Accessory_Function = 0)
        {
            if (Mass == null)//если число не объявлено, объявляем его
                Mass = new List<Element_Fuzzy_numbers<Tip_Element>>();

            if (!Mass.Contains(new Element_Fuzzy_numbers<Tip_Element>(Element, Accessory_Function)))//если такого элемента еще в числе нету
                Mass.Add(new Element_Fuzzy_numbers<Tip_Element>(Element, Accessory_Function));//добавляем элемент
        }

        /// <summary>
        /// Удаляет из нечёткого числа указанный элемент.
        /// </summary>
        /// <param name="Element">Элемент нечёткого числа</param>
        public void Delete(Element_Fuzzy_numbers<Tip_Element> Element)
        {
            if (Mass == null)//если число не объявлено, объявляем его
                Mass = new List<Element_Fuzzy_numbers<Tip_Element>>();

            if (Mass.Contains(Element))//если такого элемента еще в числе нету
                Mass.Remove(Element);//добавляем элемент
        }

        /// <summary>
        /// Удаляет из нечёткого числа указанный элемент.
        /// </summary>
        /// <param name="Element">Элемент нечёткого числа</param>
        /// <param name="Accessory_Function">Значение функции принадлежества элемента к нечёткому числу</param>
        public void Delete(Tip_Element Element, Double Accessory_Function = 0)
        {
            if (Mass == null)//если число не объявлено, объявляем его
                Mass = new List<Element_Fuzzy_numbers<Tip_Element>>();

            if (Mass.Contains(new Element_Fuzzy_numbers<Tip_Element>(Element, Accessory_Function)))//если такой элемент в числе есь
                Mass.Remove(new Element_Fuzzy_numbers<Tip_Element>(Element, Accessory_Function));//удаляем элемент
        }

        /// <summary>
        /// Удаляет из нечёткого числа элемент по указанному индексу.
        /// </summary>
        /// <param name="Index">Индекс элемента нечёткого числа</param>
        public void DeleteAt(int Index)
        {
            if (Mass == null)//если множество не объявлено, объявляем его
                Mass = new List<Element_Fuzzy_numbers<Tip_Element>>();
            if (Index < Count && Index >= 0)
                Mass.RemoveAt(Index);
        }

        #endregion

        #region Операторы класса
        /// <summary>
        /// Складывает 2 нечётких числа
        /// </summary>
        /// <param name="a">Первое нечёткое число для сложения</param>
        /// <param name="b">Второе нечёткое число для сложения</param>
        /// <returns>Нечёткое число результат сложения двух нечётких чисел</returns>
        public static Fuzzy_numbers<Tip_Element> operator +(Fuzzy_numbers<Tip_Element> a, Fuzzy_numbers<Tip_Element> b)
        {
            Fuzzy_numbers<Tip_Element> _Temp = new Fuzzy_numbers<Tip_Element>("(" + a.Name + "+" + b.Name + ")");
            foreach (Element_Fuzzy_numbers<Tip_Element> aa in a)
                foreach (Element_Fuzzy_numbers<Tip_Element> bb in b)
                {
                    Element_Fuzzy_numbers<Tip_Element> TempElement = new Element_Fuzzy_numbers<Tip_Element>((dynamic)aa.Element + (dynamic)bb.Element, Math.Min(aa.Accessory_Function, bb.Accessory_Function));
                    if (!_Temp.ContainsElement(TempElement.Element))
                    {
                        _Temp.Add(TempElement);
                    }
                    else
                    {
                        Element_Fuzzy_numbers<Tip_Element> ss = _Temp.ReturnElementForElement(TempElement.Element);
                        if (ss != null)
                            if (TempElement.Accessory_Function.CompareTo(ss.Accessory_Function) > 0)
                            {
                                _Temp.Delete(ss);
                                _Temp.Add(TempElement);
                            }
                    }

                }
            return _Temp;
        }

        /// <summary>
        /// Вычитает 2 нечётких числа
        /// </summary>
        /// <param name="a">Первое нечёткое число для разницы</param>
        /// <param name="b">Второе нечёткое число для разницы</param>
        /// <returns>Нечёткое число результат разницы двух нечётких чисел</returns>
        public static Fuzzy_numbers<Tip_Element> operator -(Fuzzy_numbers<Tip_Element> a, Fuzzy_numbers<Tip_Element> b)
        {
            Fuzzy_numbers<Tip_Element> _Temp = new Fuzzy_numbers<Tip_Element>("(" + a.Name + "-" + b.Name + ")");
            foreach (Element_Fuzzy_numbers<Tip_Element> aa in a)
                foreach (Element_Fuzzy_numbers<Tip_Element> bb in b)
                {
                    Element_Fuzzy_numbers<Tip_Element> TempElement = new Element_Fuzzy_numbers<Tip_Element>((dynamic)aa.Element - (dynamic)bb.Element, Math.Min(aa.Accessory_Function, bb.Accessory_Function));
                    if (!_Temp.ContainsElement(TempElement.Element))
                    {
                        _Temp.Add(TempElement);
                    }
                    else
                    {
                        Element_Fuzzy_numbers<Tip_Element> ss = _Temp.ReturnElementForElement(TempElement.Element);
                        if (ss != null)
                            if (TempElement.Accessory_Function.CompareTo(ss.Accessory_Function) > 0)
                            {
                                _Temp.Delete(ss);
                                _Temp.Add(TempElement);
                            }
                    }

                }
            return _Temp;
        }

        /// <summary>
        /// Умножает 2 нечётких числа
        /// </summary>
        /// <param name="a">Первое нечёткое число для умножения</param>
        /// <param name="b">Второе нечёткое число для умножения</param>
        /// <returns>Нечёткое число результат произведения двух нечётких чисел</returns>
        public static Fuzzy_numbers<Tip_Element> operator *(Fuzzy_numbers<Tip_Element> a, Fuzzy_numbers<Tip_Element> b)
        {
            Fuzzy_numbers<Tip_Element> _Temp = new Fuzzy_numbers<Tip_Element>("(" + a.Name + "*" + b.Name + ")");
            foreach (Element_Fuzzy_numbers<Tip_Element> aa in a)
                foreach (Element_Fuzzy_numbers<Tip_Element> bb in b)
                {
                    Element_Fuzzy_numbers<Tip_Element> TempElement = new Element_Fuzzy_numbers<Tip_Element>((dynamic)aa.Element * (dynamic)bb.Element, aa.Accessory_Function * bb.Accessory_Function);
                    if (!_Temp.ContainsElement(TempElement.Element))
                    {
                        _Temp.Add(TempElement);
                    }
                    else
                    {
                        Element_Fuzzy_numbers<Tip_Element> ss = _Temp.ReturnElementForElement(TempElement.Element);
                        if (ss != null)
                            if (TempElement.Accessory_Function.CompareTo(ss.Accessory_Function) < 0)
                            {
                                _Temp.Delete(ss);
                                _Temp.Add(TempElement);
                            }
                    }

                }
            return _Temp;
        }

        /// <summary>
        /// Делит 2 нечётких числа
        /// </summary>
        /// <param name="a">Первое нечёткое число делимое</param>
        /// <param name="b">Второе нечёткое число делитель</param>
        /// <returns>частное двух нечётких чисел</returns>
        public static Fuzzy_numbers<Tip_Element> operator /(Fuzzy_numbers<Tip_Element> a, Fuzzy_numbers<Tip_Element> b)
        {
            Fuzzy_numbers<Tip_Element> _Temp = new Fuzzy_numbers<Tip_Element>("(" + a.Name + "/" + b.Name + ")");
            foreach (Element_Fuzzy_numbers<Tip_Element> aa in a)
                foreach (Element_Fuzzy_numbers<Tip_Element> bb in b)
                {
                    Element_Fuzzy_numbers<Tip_Element> TempElement = new Element_Fuzzy_numbers<Tip_Element>((dynamic)aa.Element / (dynamic)bb.Element, aa.Accessory_Function * bb.Accessory_Function);
                    if (!_Temp.ContainsElement(TempElement.Element))
                    {
                        _Temp.Add(TempElement);
                    }
                    else
                    {
                        Element_Fuzzy_numbers<Tip_Element> ss = _Temp.ReturnElementForElement(TempElement.Element);
                        if (ss != null)
                            if (TempElement.Accessory_Function.CompareTo(ss.Accessory_Function) < 0)
                            {
                                _Temp.Delete(ss);
                                _Temp.Add(TempElement);
                            }
                    }

                }
            return _Temp;
        }

        /// <summary>
        /// Сравнивает 2 нечётких числа если равны true
        /// </summary>
        /// <param name="a">Первое число для сравнения</param>
        /// <param name="b">Второе число для сравнения</param>
        /// <returns>Результат сравнения двух нечётких чисел, если числа равны то true иначе false</returns>
        public static Boolean operator ==(Fuzzy_numbers<Tip_Element> a, Fuzzy_numbers<Tip_Element> b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Сравнивает 2 нечётких числа если не равны true
        /// </summary>
        /// <param name="a">Первое число для сравнения</param>
        /// <param name="b">Второе число для сравнения</param>
        /// <returns>Результат сравнения двух нечётких чисел, если числа не равны то true иначе false</returns>
        public static Boolean operator !=(Fuzzy_numbers<Tip_Element> a, Fuzzy_numbers<Tip_Element> b)
        {
            return !a.Equals(b);
        }
        #endregion
    }

}

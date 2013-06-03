using System;
namespace fuzzy_numbers_dll
{
    interface IFuzzy_numbers<Tip_Element>
     where Tip_Element : IComparable<Tip_Element>, IEquatable<Tip_Element>
    {
        void Add(Element_Fuzzy_numbers<Tip_Element> Element);
        void Add(Tip_Element Element, double Accessory_Function = 0);
        Fuzzy_numbers<Tip_Element> Clone();
        int Count { get; }
        object Current { get; }
        void Delete(Element_Fuzzy_numbers<Tip_Element> Element);
        void Delete(Tip_Element Element, double Accessory_Function = 0);
        void DeleteAt(int Index);
        bool Equals(object obj);
        System.Collections.IEnumerator GetEnumerator();
        int GetHashCode();
        bool MoveNext();
        string Name { get; }
        void Reset();
        Fuzzy_numbers<Tip_Element> Sort_from_Accessory_Function { get; }
        Fuzzy_numbers<Tip_Element> Sort_from_Element { get; }
        Element_Fuzzy_numbers<Tip_Element> this[int i] { get; set; }
        string ToString();
    }
}

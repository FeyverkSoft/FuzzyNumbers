using System;
namespace fuzzy_numbers_dll
{
    interface IElement_Fuzzy_numbers<Tip_Element>
     where Tip_Element : IComparable<Tip_Element>, IEquatable<Tip_Element>
    {
        double Accessory_Function { get; }
        Element_Fuzzy_numbers<Tip_Element> Clone();
        Tip_Element Element { get; }
        bool Equals(object obj);
        int GetHashCode();
        string ToString();
    }
}

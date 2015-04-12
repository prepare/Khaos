using System;
namespace CascadingStyleSheets
{
    interface IStyleProperty<T>
    {
        string StyleValue { get; set; }
        T RealValue { get; set; }
    }
}

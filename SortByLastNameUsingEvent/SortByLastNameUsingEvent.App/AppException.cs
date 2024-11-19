//-
using System;


namespace SortByLastNameUsingEvent.App;

/// <summary>
/// Исключение при некорректном вводе фамилии
/// </summary>
public class SurnameFormatException : FormatException
{
    public SurnameFormatException(string message) : base(message) { }
}

/// <summary>
/// Исключение при некорректном вводе варианта сортировки
/// </summary>
public class SortOptionOutOfRangeException : ArgumentOutOfRangeException
{
    public SortOptionOutOfRangeException(string message) : base(message) { }
}


//-
using System;
using System.Collections.Generic;


namespace SortByLastNameUsingEvent.App;

/// <summary>
/// Варианты сортировки
/// </summary>
enum SortOption
{
    Undefined,
    ASC,
    DSC
}


/// <summary>
/// Класс, содержащий метод получения фамилий через консольную строку
/// </summary>
internal class SurnameReader
{
    internal List<string> GetStringsFromUserInput()
    {
        Console.WriteLine("enter 5 users' surnames one by one");

        string sInput;
        List<string> res = new List<string>();

        int n = 1;
        do
        {
            Console.WriteLine($"surname {n}: ");
            sInput = Console.ReadLine() ?? string.Empty;

            sInput = sInput.Trim();

            if (res.Contains(sInput))
                throw new ArgumentOutOfRangeException("it can't be repeated");
            if (sInput.Length == 0)
                throw new ArgumentNullException("it can't be zero length");
            if (sInput.IndexOf('\\') != -1)
                throw new ArgumentException("it can't contain slashes");
            if (sInput.IndexOf(' ') != -1)
                throw new NotSupportedException("it can't contain spaces");
            if (!Char.IsUpper(sInput.ToCharArray()[0]))
                throw new SurnameFormatException("it must start with upper letter");
            
            res.Add(sInput);
            n++;
        } while (n <= 5);

        return res;
    }
}


/// <summary>
/// Класс, содержащий метод получения способа сортировки через консольную строку
/// Полученные данные передаются через аргумент делегата события
/// </summary>
internal class SortOptionReader
{
    public delegate void SortOptionEnteredHandler(SortOption sortOption);
    public event SortOptionEnteredHandler? SortOptionEvent;

    protected virtual void RaiseSortOptionEntered(SortOption sortOption)
    {
        SortOptionEvent?.Invoke(sortOption);
    }


    internal void GetSortOptionFromUserInput()
    {
        Console.WriteLine("type 1 for ASC or 2 for DSC:");

        string sInput = Console.ReadLine() ?? string.Empty;
        char keyChar = (sInput.Trim().Length == 1) ? sInput.ToCharArray()[0] : '0';

        List<char> acceptedKeyChars = new List<char>() { '1', '2'};

        if (!acceptedKeyChars.Contains(keyChar))
            throw new SortOptionOutOfRangeException(sInput);

        SortOption res = keyChar switch {
            '1' => SortOption.ASC,
            '2' => SortOption.DSC,
            _   => SortOption.Undefined
        };

        RaiseSortOptionEntered(res);
    }
}


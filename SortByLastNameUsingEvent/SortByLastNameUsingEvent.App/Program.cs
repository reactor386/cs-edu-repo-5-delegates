// -
using System;
using System.Collections.Generic;


namespace SortByLastNameUsingEvent.App;

/*

Задание 1

Создайте свой тип исключения.
Сделайте массив из пяти различных видов исключений, включая собственный
 тип исключения. Реализуйте конструкцию TryCatchFinally, в которой
 будет итерация на каждый тип исключения (блок finally по желанию).
В блоке catch выведите в консольном сообщении текст исключения.

Задание 2

Создайте консольное приложение, в котором будет происходить сортировка
 списка фамилий из пяти человек. Сортировка должна происходить
 при помощи события. Сортировка происходит при введении пользователем
 либо числа 1 (сортировка А-Я), либо числа 2 (сортировка Я-А).

Дополнительно реализуйте проверку введённых данных от пользователя
 через конструкцию TryCatchFinally с использованием собственного
 типа исключения.

*/

internal class Program
{
    private delegate bool CompareStringMethod(string s1, string s2);

    private static SortOption m_sortOption;

    /// <summary>
    /// Запрашиваем у пользователя 5 фамилий,
    ///  выводим на экран полученные значения,
    ///  запрашиваем вид сортировки,
    ///  выводим на экран отсортированны значения
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        
        Console.WriteLine("Sort surnames asc or dsc");
        Console.WriteLine("---");
        int result = 0;

        List<string> surnameList = new List<string>();
        SurnameReader surnameReader = new SurnameReader();
        do
        {
            try{
                surnameList = surnameReader.GetStringsFromUserInput();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"err: wrong surname. {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"err: wrong surname. {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"err: wrong surname. {ex.Message}");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"err: wrong surname. {ex.Message}");
            }
            catch (SurnameFormatException ex)
            {
                Console.WriteLine($"err: wrong surname. {ex.Message}");
            }
            catch
            {
                Console.WriteLine("err: wrong surname");
            }
        } while (surnameList.Count < 5);

        Console.WriteLine("now we have 5 surnames:");
        Console.WriteLine($"  [{string.Join("], [", surnameList)}]");
        Console.WriteLine("let's sort them!");

        m_sortOption = SortOption.Undefined;
        SortOptionReader sortOptionReader = new SortOptionReader();
        sortOptionReader.SortOptionEvent += SortOptionReader_SortOptionEvent;
        do
        {
            try
            {
                sortOptionReader.GetSortOptionFromUserInput();
            }
            catch (SortOptionOutOfRangeException ex)
            {
                Console.WriteLine($"err: wrong sort option. {ex.Message}");
            }
            catch
            {
                Console.WriteLine("err: wrong sort option");
            }
        } while (m_sortOption == SortOption.Undefined);
        sortOptionReader.SortOptionEvent -= SortOptionReader_SortOptionEvent;

        CompareStringMethod CompareStringDelegate = CompareStringAsc;
        if (m_sortOption == SortOption.DSC)
            CompareStringDelegate = CompareStringDsc;

        Console.WriteLine("sorted surnames:");
        Console.WriteLine($"  [{string.Join("], [", GetSortedList(surnameList, CompareStringDelegate))}]");

        Console.WriteLine("---");
        Console.WriteLine("return: [" + result.ToString() + "]");
        Console.ReadKey();
    }


    /// <summary>
    /// Делегат события, получающий результат запрошенных данных через аргумент
    /// </summary>
    /// <param name="sortOption">результат запрошенных данных</param>
    internal static void SortOptionReader_SortOptionEvent(SortOption sortOption)
    {
        Console.WriteLine($"  your choice: {sortOption.ToString()}");

        m_sortOption = sortOption;
    }


    /// <summary>
    /// Делегат сравнения строк по возрастанию
    /// </summary>
    /// <param name="s1">строка для сравнения</param>
    /// <param name="s2">строка для сравнения</param>
    /// <returns></returns>
    private static bool CompareStringAsc(string s1, string s2)
    {
        return s1.CompareTo(s2) > 0;
    }


    /// <summary>
    /// Делегат сравнения строк по убыванию
    /// </summary>
    /// <param name="s1">строка для сравнения</param>
    /// <param name="s2">строка для сравнения</param>
    /// <returns></returns>
    private static bool CompareStringDsc(string s1, string s2)
    {
        return s1.CompareTo(s2) < 0;
    }


    /// <summary>
    /// Метод сортировки списка
    /// </summary>
    /// <param name="strings">принимаемый список</param>
    /// <param name="CompareStringDelegate">делегат, задающий способ сортировки</param>
    /// <returns>отсортированный список</returns>
    private static List<string> GetSortedList(
        List<string> strings, CompareStringMethod CompareStringDelegate)
    {
        List<string> res = strings;

        // res.Sort();
        // res.Reverse();

        string txt;
        for (int i = 0; i < res.Count; i++)
        {
            for (int j = i + 1; j < res.Count; j++)
            {
                if (CompareStringDelegate(res[i], res[j]))
                {
                    txt = res[i];
                    res[i] = res[j];
                    res[j] = txt;
                }
            }
        }

        return res;
    }

}


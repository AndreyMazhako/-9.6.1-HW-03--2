using System;
using System.Collections.Generic;
using System.Linq;

// Собственный тип исключения для некорректного ввода
public class InvalidInputException : Exception
{
    public InvalidInputException(string message) : base(message) { }
}

public class NameSorter
{
    public event EventHandler<List<string>> Sorted;

    public void SortNames(List<string> names, int sortOrder)
    {
        if (sortOrder == 1)
        {
            names.Sort(); // Сортировка по возрастанию (A-Я)
        }
        else if (sortOrder == 2)
        {
            names.Sort((x, y) => y.CompareTo(x)); // Сортировка по убыванию (Я-А)
        }
        else
        {
            throw new InvalidInputException("Некорректный номер сортировки. Введите 1 или 2.");
        }

        Sorted?.Invoke(this, names); // Вызов события после сортировки
    }
}


public class Program
{
    public static void Main(string[] args)
    {
        List<string> surnames = new List<string>() { "Иванов", "Петров", "Сидоров", "Смирнов", "Кузнецов" };
        NameSorter sorter = new NameSorter();

        // Подписка на событие Sorted
        sorter.Sorted += (sender, e) =>
        {
            Console.WriteLine("Отсортированный список:");
            foreach (string surname in e)
            {
                Console.WriteLine(surname);
            }
        };

        while (true)
        {
            Console.WriteLine("Введите 1 для сортировки А-Я или 2 для сортировки Я-А:");
            string input = Console.ReadLine();

            try
            {
                int sortOrder = int.Parse(input);
                sorter.SortNames(surnames.ToList(), sortOrder); // Создаем копию списка для сортировки
                break; // Выходим из цикла после успешной сортировки
            }
            catch (FormatException)
            {
                Console.WriteLine("Некорректный формат ввода. Введите число.");
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Пожалуйста, повторите попытку.");
            }
        }

        Console.ReadKey();
    }
}
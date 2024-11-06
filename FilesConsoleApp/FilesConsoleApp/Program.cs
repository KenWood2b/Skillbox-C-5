using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "employees.txt";

            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Вывести данные на экран");
                Console.WriteLine("2 - Добавить новую запись");
                Console.WriteLine("3 - Выйти");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    DisplayEmployees(filePath);
                }
                else if (choice == "2")
                {
                    AddEmployee(filePath);
                }
                else if (choice == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                }
            }
        }

        // Метод для вывода данных о сотрудниках
        static void DisplayEmployees(string filePath)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] data = line.Split('#');
                    Console.WriteLine($"ID: {data[0]}");
                    Console.WriteLine($"Дата и время добавления: {data[1]}");
                    Console.WriteLine($"ФИО: {data[2]}");
                    Console.WriteLine($"Возраст: {data[3]}");
                    Console.WriteLine($"Рост: {data[4]}");
                    Console.WriteLine($"Дата рождения: {data[5]}");
                    Console.WriteLine($"Место рождения: {data[6]}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Файл сотрудников не найден.");
            }
        }

        // Метод для добавления нового сотрудника
        static void AddEmployee(string filePath)
        {
            int id = GetNextId(filePath);
            string dateAdded = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

            Console.Write("Введите ФИО: ");
            string fullName = Console.ReadLine();

            Console.Write("Введите возраст: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Введите рост: ");
            int height = int.Parse(Console.ReadLine());

            Console.Write("Введите дату рождения (дд.мм.гггг): ");
            string birthDate = Console.ReadLine();

            Console.Write("Введите место рождения: ");
            string birthPlace = Console.ReadLine();

            string newEmployee = $"{id}#{dateAdded}#{fullName}#{age}#{height}#{birthDate}#{birthPlace}";

            File.AppendAllText(filePath, newEmployee + Environment.NewLine);
            Console.WriteLine("Запись добавлена.");
        }

        // Метод для получения следующего ID
        static int GetNextId(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return 1;
            }

            string[] lines = File.ReadAllLines(filePath);

            // Вместо использования array[^1] для получения последней строки
            string lastLine = lines[lines.Length - 1];
            string[] lastData = lastLine.Split('#');

            int lastId;
            if (!int.TryParse(lastData[0], out lastId))
            {
                Console.WriteLine("Ошибка при чтении ID последнего сотрудника. Присваивается ID = 1.");
                lastId = 0;
            }

            return lastId + 1;
        }
    }
}

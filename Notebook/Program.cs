using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook
{
	class Program
	{
		static string ProgramName { get; set; } = "ZalNotes";

		static void Main(string[] args)
		{
			PrintEntryMessage();
			Console.WriteLine();

			List<Note> notes = new List<Note>();
			AddTestData(notes);


			bool isWorking = true;
			while (isWorking)
			{
				isWorking = ConsoleInteractions.CommandHandling(notes);
			}

			PrintExitMessage();
			Console.WriteLine();

			Console.WriteLine("Нажмите любую клавишу для выхода из программы...");
			Console.ReadKey();
		}

		public static void PrintNotes(List<Note> notes)
		{
			for (int i = 0; i < notes.Count; i++)
			{
				Console.WriteLine($"\t{i}. {notes[i]}");
			}
		}

		public static int GetNoteIndex(List<Note> notes, string userInput)
		{
			int index;

			if (!int.TryParse(userInput, out index))
			{
				Console.WriteLine("Номером записи может быть только число!");
				return -1;
			}

			if (index < 0 || notes.Count - 1 < index)
			{
				Console.WriteLine("Записи с таким номером не существует.");
				return -1;
			}

			return index;
		}

		public static void PrintEntryMessage()
		{
			Console.WriteLine($"Добро пожаловать в записную книжку {ProgramName}!");
			ConsoleInteractions.PrintAvailableCommands();
			Console.WriteLine($"Для отмены выполняемой операции, оставьте поле ввода пустым.");
		}

		public static void PrintExitMessage()
		{
			Console.WriteLine($"Благодарим Вас за использование {ProgramName}!");
			Console.WriteLine($"Все введённые вами записи удалены, так как {ProgramName} использует сессионное хранение информации.");
			Console.WriteLine("До встречи!");
		}

		static void AddTestData(List<Note> notes)
		{
			Note testNote1 = new Note()
			{
				FirstName = "Виктор",
				LastName = "Залкин",
				Patronymic = "Михайлович",
				BirthDate = new DateTime(1999, 04, 22),
				PhoneNumber = 1234567,
				Country = "Россия",
				Organization = "ИТМО",
				Position = "Студент",
				Info = "Создатель этой программы",
			};

			Note testNote2 = new Note()
			{
				FirstName = "Василий",
				LastName = "Ведёрочкин",
				Country = "USA",
				PhoneNumber = 8910123,
			};

			notes.Add(testNote1);
			notes.Add(testNote2);
		}
	}
}

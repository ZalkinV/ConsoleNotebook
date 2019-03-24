using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook
{
	class ConsoleInteractions
	{
		public static bool CommandHandling(List<Note> notes)
		{
			Console.Write("Введите команду: ");
			string command = Console.ReadLine();

			switch (command)
			{
				case "h":
					PrintAvailableCommands();
					Console.WriteLine();
					break;

				case "l":
					if (notes.Count > 0)
					{
						HandlePrintCommand(notes);
					}
					else
					{
						PrintEmptinessMessage();
					}
					Console.WriteLine();
					break;

				case "a":
					Console.WriteLine("Режим добавления новой записи.");
					HandleAddCommand(notes);
					Console.WriteLine();
					break;

				case "d":
					if (notes.Count > 0)
					{
						Console.WriteLine("Режим удаления записи:");
						Notebook.PrintNotes();
						HandleDeleteCommand(notes);
					}
					else
					{
						PrintEmptinessMessage();
					}
					Console.WriteLine();
					break;

				case "e":
					if (notes.Count > 0)
					{
						Console.WriteLine("Режим изменения записи:");
						Notebook.PrintNotes();
						HandleEditCommand(notes);
					}
					else
					{
						PrintEmptinessMessage();
						Console.WriteLine();
					}
					break;

				case "x":
					return false;

				case "":
					break;

				default:
					Console.WriteLine("Вы ввели неверную команду! Введите команду из списка ниже:");
					PrintAvailableCommands();
					Console.WriteLine();
					break;
			}

			return true;
		}

		public static void PrintAvailableCommands()
		{
			Console.WriteLine("Для работы с записной книжкой вам доступны следущие команды:");
			Console.WriteLine("\th — получить список доступных команд;");
			Console.WriteLine("\tl — просмотреть все записи или конкретную;");
			Console.WriteLine("\ta — добавить запись;");
			Console.WriteLine("\td — удалить запись;");
			Console.WriteLine("\te — редактировать запись;");
			Console.WriteLine("\tx — выйти из записной книжки.");
		}

		static void HandlePrintCommand(List<Note> notes)
		{
			Console.WriteLine("Для просмотра дополнительной информации о записи, введите её номер из списка ниже.");
			Notebook.PrintNotes();

			int indexForPrint = SelectingNoteIndex();
			if (indexForPrint > -1)
			{
				string[] fullInfo = notes[indexForPrint].GetFullInfo();
				foreach (string infoLine in fullInfo)
				{
					Console.WriteLine(infoLine);
				}
			}
		}

		static void HandleAddCommand(List<Note> notes)
		{
			Console.WriteLine("Последовательно вводите запрашиваемую информацию:");

			Note note = new Note();

			note.LastName = AddingField("фамилию", true);
			note.FirstName = AddingField("имя", true);
			note.Patronymic = AddingField("отчество", false);

			while (true)
			{
				string birthDateString = AddingField("дату рождения в формате ДД.ММ.ГГГГ", false);
				if (string.IsNullOrEmpty(birthDateString))
				{
					break;
				}

				DateTime birthDate;
				if (DateTime.TryParse(birthDateString, out birthDate))
				{
					note.BirthDate = birthDate;
					break;
				}
			}

			while (true)
			{
				string phoneNumberString = AddingField("номер телефона (только цифры)", true);
				int phoneNumber;
				if (int.TryParse(phoneNumberString, out phoneNumber))
				{
					note.PhoneNumber = phoneNumber;
					break;
				}
			}


			note.Country = AddingField("страну", true);
			note.Organization = AddingField("организацию", false);
			note.Position = AddingField("должность", false);
			note.Info = AddingField("дополнительную информацию", false);

			notes.Add(note);
			Console.WriteLine($"Запись была добавлена в записную книжку под номером {notes.Count - 1}.");
		}

		static void HandleDeleteCommand(List<Note> notes)
		{
			int indexForDelete = SelectingNoteIndex();

			if (indexForDelete > -1)
			{
				notes.RemoveAt(indexForDelete);
				Console.WriteLine($"Запись под номером {indexForDelete} была удалена.");
			}
		}

		static void HandleEditCommand(List<Note> notes)
		{
			int indexForEdit = SelectingNoteIndex();

			if (indexForEdit > -1)
			{
				Console.WriteLine("Для того, чтобы очистить значение, поставьте один пробел. Чтобы не изменять значение, оставьте поле ввода пустым.");
				EdititngNoteFields(notes[indexForEdit]);
				Console.WriteLine($"Запись под номером {indexForEdit} была изменена.");
			}
		}



		static string AddingField(string fieldName, bool isNecessaryField)
		{
			string isNecessaryInfo = isNecessaryField ? "(обязательно)" : "(необязательно)";
			string userInfoString = $"\tВведите {fieldName} {isNecessaryInfo}: ";

			string inputValue;
			while (true)
			{
				Console.Write(userInfoString);

				inputValue = Console.ReadLine();
				if (!isNecessaryField)
				{
					break;
				}
				else if (!string.IsNullOrWhiteSpace(inputValue))
				{
					break;
				}
			}
			return inputValue;
		}

		static void EdititngNoteFields(Note note)
		{
			note.LastName = EditingField("Фамилия", note.LastName, true);
			note.FirstName = EditingField($"Имя", note.FirstName, true);
			note.Patronymic = EditingField($"Отчество", note.Patronymic, false);

			while (true)
			{
				string birthDateString = EditingField($"Дата рождения в формате ДД.ММ.ГГГГ", note.BirthDate?.ToShortDateString(), false);
				if (birthDateString == null)
				{
					note.BirthDate = null;
					break;
				}

				if (birthDateString.Equals(string.Empty))
				{
					break;
				}

				DateTime birthDate;
				if (DateTime.TryParse(birthDateString, out birthDate))
				{
					note.BirthDate = birthDate;
					break;
				}
			}

			while (true)
			{
				string phoneNumberString = EditingField($"Номер телефона (только цифры)", note.PhoneNumber.ToString(), true);
				int phoneNumber;
				if (int.TryParse(phoneNumberString, out phoneNumber))
				{
					note.PhoneNumber = phoneNumber;
					break;
				}
			}

			note.Country = EditingField($"Страна", note.Country, true);
			note.Organization = EditingField($"Организация", note.Organization, false);
			note.Position = EditingField($"Должность", note.Position, false);
			note.Info = EditingField($"Дополнительная информация", note.Info, false);

			Console.WriteLine();
		}

		static string EditingField(string fieldName, string fieldValue, bool isNecessaryField)
		{
			string isNecessaryInfo = isNecessaryField ? "(обязательно)" : "(необязательно)";
			string userInfo = $"{fieldName} {isNecessaryInfo}: {fieldValue} -> ";

			string inputValue;
			while (true)
			{
				Console.Write(userInfo);
				inputValue = Console.ReadLine();

				if (string.IsNullOrEmpty(inputValue))
				{
					inputValue = fieldValue;
					break;
				}

				if (!isNecessaryField)
				{
					if (string.IsNullOrWhiteSpace(inputValue))
					{
						inputValue = null;
					}
					break;
				}
				else
				{
					if (!string.IsNullOrWhiteSpace(inputValue))
					{
						break;
					}
				}
			}

			return inputValue;
		}

		static int SelectingNoteIndex()
		{
			int noteIndex = -1;
			while (true)
			{
				Console.Write("Введите номер записи: ");
				string userInput = Console.ReadLine();

				if (string.IsNullOrEmpty(userInput))
				{
					break;
				}
				else
				{
					noteIndex = Notebook.GetNoteIndex(userInput);

					if (noteIndex < 0)
						Console.WriteLine("Попробуйте ещё раз или оставьте поле ввода пустым для отмены операции.");
					else
						break;
				}
			}

			return noteIndex;
		}



		static void PrintEmptinessMessage()
		{
			Console.WriteLine("Ваша записная книжка пуста. Добавьте запись с помощью команды \"a\".");
		}
	}
}

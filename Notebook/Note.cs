using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook
{
	class Note
	{
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string Patronymic { get; set; }
		public DateTime? BirthDate { get; set; } = null;

		public int PhoneNumber { get; set; }
		public string Country { get; set; }

		public string Organization { get; set; }
		public string Position { get; set; }

		public string Info { get; set; }


		public string[] GetFullInfo()
		{
			string[] fullInfo = new string[]
			{
				$"\tФамилия: {LastName}",
				$"\tИмя: {FirstName}",
				$"\tОтчество: {Patronymic}",
				$"\tДата рождения: {BirthDate?.ToShortDateString()}",
				$"\tНомер телефона: {PhoneNumber}",
				$"\tСтрана: {Country}",
				$"\tОрганизация: {Organization}",
				$"\tДолжность: {Position}",
				$"\tДополнительная информация: {Info}",
			};

			return fullInfo;
		}

		public override string ToString()
		{
			return $"{LastName} {FirstName} {PhoneNumber}";
		}
	}
}

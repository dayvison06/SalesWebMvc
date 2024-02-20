using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
	public class Seller
	{

		public int Id { get; set; }

		[Display(Name = "Nome")] 
		public string Name { get; set; }
		[Display(Name = "E-mail")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Display(Name = "Salário")]
		[DisplayFormat(DataFormatString = "{0:C2}")]

		public double BaseSalary { get; set; }
		[Display(Name = "Data de nascimento")]
		[DataType(DataType.Date)]
		public DateTime	BirthDate{ get; set; }
		public Departament Departament { get; set; }
		public int DepartamentId { get; set; }
		public ICollection<SalesRecord> Sales{ get; set; } = new List<SalesRecord>();

		public Seller()
		{

		}

		public Seller(int id, string name, string email, double baseSalary, DateTime birthDate, Departament departament)
		{
			Id = id;
			Name = name;
			Email = email;
			BaseSalary = baseSalary;
			BirthDate = birthDate;
			Departament = departament;
		}

		public void AddSales(SalesRecord sr) 
		{
			Sales.Add(sr);
		}

		public void RemoveSales(SalesRecord sr) 
		{
		
			Sales.Remove(sr);

		}

		public double TotalSales(DateTime initial, DateTime final) 
		
		{

			return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
		
		}

	}
}

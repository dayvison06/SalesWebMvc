using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
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
		[Required(ErrorMessage ="{0} é obrigatório")] //Torna obrigatório
        //Restringe o máximo de comprimento de nome para 60 caracteres e o minimo 3, impedindo o usuário de inserir algo fora desse escopo.
		[StringLength(60, MinimumLength = 3, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres ")] 
		[Display(Name = "Nome")] 
		public string Name { get; set; }
		[Display(Name = "E-mail")]
		[DataType(DataType.EmailAddress)]
		[Required(ErrorMessage = "{0} é obrigatório")] //Torna obrigatório
		public string Email { get; set; }
		[Display(Name = "Salário")]
		[DisplayFormat(DataFormatString = "{0:C2}")]
		[Required(ErrorMessage = "{0} é obrigatório")] //Torna obrigatório
		[Range(1000.0, 50000.0, ErrorMessage = "{0} deve ser entre {1} e {2}")]
		public double BaseSalary { get; set; }
		[Display(Name = "Data de nascimento")]
		[DataType(DataType.Date)]
		[Required(ErrorMessage = "{0} é obrigatório")] //Torna obrigatório
		public DateTime	BirthDate{ get; set; }
		public Departament Departament { get; set; }
		[Display(Name ="Departamento")]
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

﻿using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System.Collections.Generic;
using System.Diagnostics;

namespace SalesWebMvc.Controllers
{
	public class SellersController : Controller
	{

		private readonly SellerService _sellerService;

		private readonly DepartamentService _departamentService;

		public SellersController(SellerService sellerService, DepartamentService departamentService)
		{
			_departamentService = departamentService;
			_sellerService = sellerService;
		}



		// Ação de Criar dados

		public IActionResult Create()
		{
			var departaments = _departamentService.FindAll();
			var viewModel = new SellerFormViewModel { Departaments = departaments };

			return View(viewModel);

		}

		// Ação de Criar dados POST

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Seller seller)
		{
			// Impede a criação de campo vazio
			if (!ModelState.IsValid)
			{
				var departaments = _departamentService.FindAll();
				var viewModel = new SellerFormViewModel { Seller= seller, Departaments = departaments };
				return View(viewModel);
			}

			_sellerService.Insert(seller);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Index()
		{

			var list = _sellerService.FindAll();

			return View(list);
		}

		// Ação de deletar dados

		public IActionResult Delete(int? id)
		{

			if (id == null)
			{
				return RedirectToAction(nameof(Error), new { message = "Id not provided"});
			}

			var obj = _sellerService.FindById(id.Value);
			if (obj == null)
			{
				return RedirectToAction(nameof(Error), new { message = "Id not found" });
			}

			return View(obj);
		}

		// Ação de deletar dados POST

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int id)
		{
			_sellerService.Remove(id);
			return RedirectToAction(nameof(Index));
		}

		// Ação de exibir dados

		public IActionResult Details(int? id)
		{
			if (id == null)
			{
				return RedirectToAction(nameof(Error), new { message = "Id not provided" });
			}

			var obj = _sellerService.FindById(id.Value);
			
			if (obj == null)
			{
				return RedirectToAction(nameof(Error), new { message = "Id not found" });
			}

			return View(obj);
		}


		// Ação de atualizar dados

		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return RedirectToAction(nameof(Error), new { message = "Id not provided" });
			}

			var obj = _sellerService.FindById(id.Value);

			if (obj == null)
			{
				return RedirectToAction(nameof(Error), new { message = "Id not found" });
			}

			List<Departament> departaments = _departamentService.FindAll();
			SellerFormViewModel viewModel= new SellerFormViewModel {Seller = obj, Departaments = departaments };

			return View(viewModel);

		}

		// Ação de atualizar dados com POST

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, Seller seller)
		{
			// Impede a atualização com campo vazio
			if (!ModelState.IsValid)
			{
				var departaments = _departamentService.FindAll();
				var viewModel = new SellerFormViewModel { Seller = seller, Departaments = departaments };
				return View(viewModel);
			}

			if (id != seller.Id)
			{
				return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
			}

			try
			{
				_sellerService.Update(seller);
				return RedirectToAction(nameof(Index));
			}
			catch (NotFoundException e)
			{
				return RedirectToAction(nameof(Error), new { message = e.Message });

			}
			catch (DbConcurrencyException e)
			{
				return RedirectToAction(nameof(Error), new { message = e.Message });
			}
		}

		public IActionResult Error(string message)
		{
			var viewModel = new ErrorViewModel()
			{
				Message = message,
				RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
			};
			return View(viewModel);
		}
	}
}

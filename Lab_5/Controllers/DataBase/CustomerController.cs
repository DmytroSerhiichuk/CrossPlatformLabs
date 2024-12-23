﻿using Lab_5.DTO;
using Lab_5.HttpClients;
using Lab_5.RequestDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Lab_5.Controllers.DataBase
{
	[Authorize]
	[Route("DataBase/Customer")]
	public class CustomerController : Controller
	{
		private readonly Lab6HttpClient _lab6HttpClient;
		private readonly AuthConfig _authConfig;

		public CustomerController(Lab6HttpClient lab6HttpClient, IOptions<AuthConfig> options)
		{
			_lab6HttpClient = lab6HttpClient;
			_authConfig = options.Value;
		}

		[HttpGet]
		public async Task<IActionResult> GetCustomers()
		{
			var token = Request.Cookies[_authConfig.CookieName] ?? "";

			try
			{
				var res = await _lab6HttpClient.GetData<List<CustomerDTO>>(token, "customer");

				return View("Customers", res);
			}
			catch
			{
				return RedirectToAction("Login", "Account");
			}
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCustomer(int id)
		{
			var token = Request.Cookies[_authConfig.CookieName] ?? "";

			try
			{
				var res = await _lab6HttpClient.GetData<CustomerDetailedDTO>(token, $"customer/{id}");

				return View("Customer", res);
			}
			catch
			{
				return RedirectToAction("Login", "Account");
			}
		}

		[HttpGet("Create")]
		public IActionResult Create()
		{
			return View("Create");
		}
		[HttpPost("Create")]
		public async Task<IActionResult> Create(CustomerRequestDTO customer)
		{
			if (!ModelState.IsValid)
			{
				return View("Create", customer);
			}

			var token = Request.Cookies[_authConfig.CookieName] ?? "";

			try
			{
				await _lab6HttpClient.PostData(token, "customer", customer);

				ViewData["alertMessage"] = "Success";
				return View("Create");
			}
			catch (HttpRequestException ex)
			{
				if (ex.StatusCode == System.Net.HttpStatusCode.Forbidden || ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
				{
					return RedirectToAction("Login", "Account");
				}
				else
				{
					ModelState.AddModelError(String.Empty, ex.Message);
					return View("Create", customer);
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(String.Empty, ex.Message);
				return View("Create", customer);
			}
		}
	}
}

using System.Globalization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using ShoppingCart.Data;

namespace ShoppingCart.Model
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly IDatabase _database;

		public IndexModel(ILogger<IndexModel> logger)
		{
			this._logger = logger;
			this._database = new Database();
			CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("fr-FR");
		}

		// Select list to populate the DropDownList
		public SelectList Items { get; set; } = default!;

		// Bound properties for form inputs
		[BindProperty]
		public string? SelectedItem { get; set; }

		// Labels shown on the page (not bound)
		public string Label1 { get; private set; } = string.Empty;
		public string Label2 { get; private set; } = string.Empty;
		public string Label3 { get; private set; } = string.Empty;
		public string Label4 { get; private set; } = string.Empty;
		public string Label5 { get; private set; } = string.Empty;

		public string Product1 { get; private set; } = string.Empty;
		public string Product2 { get; private set; } = string.Empty;
		public string Product3 { get; private set; } = string.Empty;

		[BindProperty]
		public string? Text1 { get; set; }

		[BindProperty]
		public string? Text2 { get; set; }

		[BindProperty]
		public string? Text3 { get; set; }

		public string TotalPrice { get; private set; } = string.Empty;

		public void OnGet()
		{
			PopulateItems();

			// set defaults
			SelectedItem ??= Items.FirstOrDefault()?.Value;
			SetLabelsFromSelection();
			PopulateProducts();
		}

		public IActionResult OnPost()
		{
			PopulateItems();
			SetLabelsFromSelection();
			PopulateProducts();

			var list = new List<Tuple<int, int>>();
			AppendProductTuple(list, 1, Text1);
			AppendProductTuple(list, 2, Text2);
			AppendProductTuple(list, 3, Text3);
			if (list.Count > 0 && SelectedItem != null)
			{
				var calculator = new Calculator(this._database);
				decimal total = calculator.Compute(SelectedItem, list);

				TotalPrice = total.ToString("C");
			}
			else
				TotalPrice = string.Empty;

			return Page();
		}

		private void PopulateItems()
		{
			var items = new List<SelectListItem>();

			// Example items - fill by code
			foreach (string clientId in this._database.GetListOfClientIds())
			{
				var c = this._database.GetClient(clientId);

				if (c != null)
					items.Add(new SelectListItem(c.ClientName, clientId));
			}

			this.Items = new SelectList(items, "Value", "Text", SelectedItem);
		}

		private void PopulateProducts()
		{
			var p = this._database.GetProduct(1);
			Product1 = p?.Label ?? string.Empty;
			p = this._database.GetProduct(2);
			Product2 = p?.Label ?? string.Empty;
			p = this._database.GetProduct(3);
			Product3 = p?.Label ?? string.Empty;
		}

		private void SetLabelsFromSelection()
		{
			string id = SelectedItem ?? Items.FirstOrDefault()?.Value ?? string.Empty;
			var client = this._database.GetClient(id);

			Label1 = id;
			if (client is PrivateIndividual pi)
			{
				Label2 = pi.FirstName ?? string.Empty;
				Label3 = pi.LastName;
				Label4 = string.Empty;
				Label5 = string.Empty;
			}
			else if (client is Company co)
			{
				Label2 = co.BusinessName;
				Label3 = co.RegistrationNumber ?? string.Empty;
				Label4 = co.VAT ?? string.Empty;
				Label5 = co.Turnover.ToString("C");
			}
			else
			{
				Label2 = string.Empty;
				Label3 = string.Empty;
				Label4 = string.Empty;
				Label5 = string.Empty;
			}
		}

		private static void AppendProductTuple(List<Tuple<int, int>> list, int productId, string? text)
		{
			if (int.TryParse(text, out int quantity) && quantity > 0)
				list.Add(Tuple.Create(productId, quantity));
		}
	}
}

namespace ShoppingCart.Data
{
	public class Database : IDatabase
	{
		private readonly Dictionary<int, PrivateIndividual> _privateIndividuals;
		private readonly Dictionary<int, Company> _companies;
		private readonly Dictionary<int, Product> _products;
		private readonly Dictionary<Tuple<int, int>, decimal> _prices;

		public Database()
		{
			this._privateIndividuals = new Dictionary<int, PrivateIndividual>
			{
				{ 1, new PrivateIndividual { Id = 1, LastName = "A" } },
				{ 2, new PrivateIndividual { Id = 2, LastName = "B" } },
				{ 3, new PrivateIndividual { Id = 3, LastName = "E" } },
			};
			this._companies = new Dictionary<int, Company>
			{
				{ 1, new Company {
					Id = 1,
					BusinessName = "C",
					RegistrationNumber = "12345",
					Turnover = 1_000_000m }
				},
				{ 2, new Company {
					Id = 2,
					BusinessName = "D",
					RegistrationNumber = "67890",
					VAT = "9876543210",
					Turnover = 100_000_000m }
				},
				{ 3, new Company {
					Id = 3,
					BusinessName = "F",
					RegistrationNumber = "54321",
					VAT = "1234567890",
					Turnover = 500_000m }
				},
				{ 4, new Company {
					Id = 4,
					BusinessName = "G",
					RegistrationNumber = "45678",
					Turnover = 10_000_000m }
				},
			};
			this._products = new Dictionary<int, Product>
			{
				{ 1, new Product { Id = 1, Label = "High-end phone" } },
				{ 2, new Product { Id = 2, Label = "Basic model phone" } },
				{ 3, new Product { Id = 3, Label = "Computer" } },
			};
			this._prices = new Dictionary<Tuple<int, int>, decimal>
			{
				{ Tuple.Create(1, 1), 1_500m },
				{ Tuple.Create(2, 1), 800m },
				{ Tuple.Create(3, 1), 1_200m },
				{ Tuple.Create(1, 2), 1_150m },
				{ Tuple.Create(2, 2), 600m },
				{ Tuple.Create(3, 2), 1_000m },
				{ Tuple.Create(1, 3), 1_000m },
				{ Tuple.Create(2, 3), 550m },
				{ Tuple.Create(3, 3), 900m },
			};
		}

		public PrivateIndividual? GetPrivateIndividual(int id)
		{
			this._privateIndividuals.TryGetValue(id, out var result);
			return result;
		}

		public Company? GetCompany(int id)
		{
			this._companies.TryGetValue(id, out var result);
			return result;
		}

		public Client? GetClient(string clientId)
		{
			if (string.IsNullOrEmpty(clientId))
				return null;
			else if (clientId.StartsWith("PI") && int.TryParse(clientId.AsSpan(2), out int id))
				return this.GetPrivateIndividual(id);
			else if (clientId.StartsWith("C") && int.TryParse(clientId.AsSpan(1), out id))
				return this.GetCompany(id);
			else
				return null;
		}

		public Product? GetProduct(int id)
		{
			this._products.TryGetValue(id, out var result);
			return result;
		}

		public decimal GetPrice(int productId, string clientId)
		{
			var client = GetClient(clientId) ?? throw new ArgumentException("Invalid client Id", nameof(clientId));

			if (this._prices.TryGetValue(Tuple.Create(productId, client.ClientType), out var result))
				return result;
			else
			{
				var product = this.GetProduct(productId) ?? throw new ArgumentException("Unknown product Id",
					nameof(productId));
				throw new ArgumentException($"Not registered price for: {product.Label}", nameof(productId));
			}
		}
	}
}

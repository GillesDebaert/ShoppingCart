namespace ShoppingCart.Data
{
	public class Database
	{
		private readonly Dictionary<int, PrivateIndividual> _privateIndividuals;
		private readonly Dictionary<int, Company> _companies;

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
			else if (clientId.StartsWith("PI") && int.TryParse(clientId.Substring(2), out int id))
				return this.GetPrivateIndividual(id);
			else if (clientId.StartsWith("C") && int.TryParse(clientId.Substring(1), out id))
				return this.GetCompany(id);
			else
				return null;
		}
	}
}

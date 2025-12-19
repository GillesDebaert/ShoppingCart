using ShoppingCart.Data;

namespace ShoppingCart.Tests.Data
{
	public class DatabaseTests
	{
		private readonly Database _database = new();

		[Theory]
		[MemberData(nameof(Database_GetPrivateIndividual_TestData))]
		public void Database_GetPrivateIndividual_Expected(int id, string? expectedClientId)
		{
			// prepare
			var pi = this._database.GetPrivateIndividual(id);

			// execute
			string? actualClientId = pi?.ClientId;

			// verify
			Assert.Equal(expectedClientId, actualClientId);
			if (pi != null)
				Assert.Equal(1, pi.ClientType);
		}

		[Theory]
		[MemberData(nameof(Database_GetCompany_TestData))]
		public void Database_GetCompany_Expected(int id, string? expectedClientId, int expectedClientType)
		{
			// prepare
			var c = this._database.GetCompany(id);

			// execute
			string? actualClientId = c?.ClientId;

			// verify
			Assert.Equal(expectedClientId, actualClientId);
			if (c != null)
				Assert.Equal(expectedClientType, c.ClientType);
		}

		[Theory]
		[MemberData(nameof(Database_GetClient_TestData))]
		public void Database_GetClient_Expected(string id, string? expectedClientId, int expectedClientType)
		{
			// prepare
			var c = this._database.GetClient(id);

			// execute
			var actualClientId = c?.ClientId;

			// verify
			Assert.Equal(expectedClientId, actualClientId);
			if (c != null)
				Assert.Equal(expectedClientType, c.ClientType);
		}

		[Theory]
		[MemberData(nameof(Database_GetProduct_TestData))]
		public void Database_GetProduct_Expected(int id, string? expectedLabel)
		{
			// prepare
			var p = this._database.GetProduct(id);

			// execute
			var actualLabel = p?.Label;

			// verify
			Assert.Equal(expectedLabel, actualLabel);
		}

		[Theory]
		[MemberData(nameof(Database_GetPrice_TestData))]
		public void Database_GetPrice_Expected(int productId, string clientId, decimal expected)
		{
			// prepare

			// execute
			var actual = this._database.GetPrice(productId, clientId);

			// verify
			Assert.Equal(expected, actual);
		}

		[Theory]
		[MemberData(nameof(Database_GetPriceWithWrongArguments_TestData))]
		public void Database_GetPriceWithWrongArguments_Throw(int productId, string clientId)
		{
			// prepare

			// execute
			void a()
			{
				_ = this._database.GetPrice(productId, clientId);
			}

			// verify
			Assert.Throws<ArgumentException>(a);
		}


		public static TheoryData<int, string?> Database_GetPrivateIndividual_TestData()
		{
			return new TheoryData<int, string?>
			{
				{ 0, null },
				{ 1, "PI1" },
				{ 2, "PI2" },
				{ 3, "PI3" },
				{ 4, null },
			};
		}

		public static TheoryData<int, string?, int> Database_GetCompany_TestData()
		{
			return new TheoryData<int, string?, int>
			{
				{ 0, null, 0 },
				{ 1, "C1", 2 },
				{ 2, "C2", 3 },
				{ 3, "C3", 2 },
				{ 4, "C4", 3 },
				{ 5, null, 0 },
			};
		}

		public static TheoryData<string, string?, int> Database_GetClient_TestData()
		{
			return new TheoryData<string, string?, int>
			{
				{ string.Empty, null, 0 },
				{ "A", null, 0 },
				{ "C1", "C1", 2 },
				{ "C2", "C2", 3 },
				{ "C3", "C3", 2 },
				{ "C4", "C4", 3 },
				{ "C5", null, 0 },
				{ "PI1", "PI1", 1 },
				{ "PI2", "PI2", 1 },
				{ "PI3", "PI3", 1 },
				{ "PI4", null, 0 },
			};
		}

		public static TheoryData<int, string?> Database_GetProduct_TestData()
		{
			return new TheoryData<int, string?>
			{
				{ 0, null },
				{ 1, "High-end phone" },
				{ 2, "Basic model phone" },
				{ 3, "Computer" },
				{ 4, null },
			};
		}

		public static TheoryData<int, string, decimal> Database_GetPrice_TestData()
		{
			return new TheoryData<int, string, decimal>
			{
				{ 1, "PI1", 1_500m },
				{ 1, "PI2", 1_500m },
				{ 1, "PI3", 1_500m },
				{ 1, "C1", 1_150m },
				{ 1, "C2", 1_000m },
				{ 1, "C3", 1_150m },
				{ 1, "C4", 1_000m },
				{ 2, "PI1", 800m },
				{ 2, "PI2", 800m },
				{ 2, "PI3", 800m },
				{ 2, "C1", 600m },
				{ 2, "C2", 550m },
				{ 2, "C3", 600m },
				{ 2, "C4", 550m },
				{ 3, "PI1", 1_200m },
				{ 3, "PI2", 1_200m },
				{ 3, "PI3", 1_200m },
				{ 3, "C1", 1_000m },
				{ 3, "C2", 900m },
				{ 3, "C3", 1_000m },
				{ 3, "C4", 900m },
			};
		}

		public static TheoryData<int, string> Database_GetPriceWithWrongArguments_TestData()
		{
			return new TheoryData<int, string>
			{
				{ 1, string.Empty },
				{ 0, "PI1" },
				{ 1, "A" },
				{ 4, "C1" },
			};
		}
	}
}

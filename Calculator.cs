using ShoppingCart.Data;

namespace ShoppingCart
{
	public class Calculator(IDatabase database)
	{
		private readonly IDatabase _database = database ?? throw new ArgumentNullException(nameof(database));

		public decimal Compute(string clientId, IEnumerable<Tuple<int, int>> items)
		{
			decimal result = 0m;

			foreach (var i in items)
			{
				int productId = i.Item1;
				int count = i.Item2;
				decimal unitPrice = this._database.GetPrice(productId, clientId);

				if (count < 0)
					throw new ArgumentException($"Invalid item count.", nameof(items));
				result += unitPrice * count;
			}
			return result;
		}
	}
}

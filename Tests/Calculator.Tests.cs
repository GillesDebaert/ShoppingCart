using ShoppingCart.Data;

namespace ShoppingCart.Tests
{
	public class CalculatorTests
	{
		private readonly Calculator _calculator = new(new Database());

		[Theory]
		[MemberData(nameof(Calculator_Compute_TestData))]
		public void Calculator_Compute_Expected(string clientId, Tuple<int, int>[] items, decimal expected)
		{
			// prepare

			// execute
			var actual = this._calculator.Compute(clientId, items);

			// verify
			Assert.Equal(expected, actual);
		}

		[Theory]
		[MemberData(nameof(Calculator_ComputeWithWrongArguments_TestData))]
		public void Calculator_ComputeWithWrongArguments_Throw(string clientId, Tuple<int, int>[] items)
		{
			// prepare

			// execute
			void a()
			{
				_ = this._calculator.Compute(clientId, items);
			}

			// verify
			Assert.Throws<ArgumentException>(a);
		}

		public static TheoryData<string, Tuple<int, int>[], decimal> Calculator_Compute_TestData()
		{
			return new TheoryData<string, Tuple<int, int>[], decimal>
			{
				{ "PI1", [], 0m },
				{ "PI1", new[] { Tuple.Create(1, 1) }, 1_500m },
				{ "PI1", new[] { Tuple.Create(1, 3) }, 4_500m },
				{ "C1", new[] { Tuple.Create(1, 20), Tuple.Create(3, 10) }, 33_000m },
				{ "C2", new[] { Tuple.Create(1, 20), Tuple.Create(3, 10) }, 29_000m },
			};
		}

		public static TheoryData<string, Tuple<int, int>[]> Calculator_ComputeWithWrongArguments_TestData()
		{
			return new TheoryData<string, Tuple<int, int>[]>
			{
				{ string.Empty, new[] { Tuple.Create(1, 1) } },
				{ "PI1", new[] { Tuple.Create(0, 1) } },
				{ "PI1", new[] { Tuple.Create(1, -1) } },
				{ "A", new[] { Tuple.Create(1, 1) } },
				{ "C1", new[] { Tuple.Create(0, 1) } },
				{ "C1", new[] { Tuple.Create(2, -3) } },
			};
		}
	}
}

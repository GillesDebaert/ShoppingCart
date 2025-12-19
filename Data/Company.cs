namespace ShoppingCart.Data
{
	public class Company : Client
	{
		public required int Id { get; set; }
		public required string BusinessName { get; set; }
		public string? VAT { get; set; }
		public required string RegistrationNumber { get; set; }
		public required decimal Turnover { get; set; }

		public override string ClientId => $"C{Id}";

		public override string ClientName => $"Company: {BusinessName}";

		public override int ClientType
		{
			get { return this.Turnover >= 10_000_000m ? 3 : 2; }
		}
	}
}

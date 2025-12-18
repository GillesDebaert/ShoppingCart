namespace ShoppingCart.Data
{
	public class Company
	{
		public required int Id { get; set; }
		public required string BusinessName { get; set; }
		public string? VAT { get; set; }
		public required string RegistrationNumber { get; set; }
		public required decimal Turnover { get; set; }
	}
}

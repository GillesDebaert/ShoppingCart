namespace ShoppingCart.Data
{
	public class PrivateIndividual : Client
	{
		public required int Id { get; set; }
		public string? FirstName { get; set; }
		public required string LastName { get; set; }

		public override string ClientId => $"PI{Id}";

		public override string ClientName
		{
			get
			{
				if (!string.IsNullOrEmpty(this.FirstName) && this.LastName.Length > 0)
					return $"{LastName}, {FirstName}";
				else if (this.LastName.Length > 0)
					return this.LastName;
				else
					return $"Unknown (Id: {Id})";
			}
		}

		public override int ClientType => 1;
	}
}

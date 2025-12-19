namespace ShoppingCart.Data
{
	public abstract class Client
	{
		public abstract string ClientId { get; }
		public abstract string ClientName { get; }
		public abstract int ClientType { get; }
	}
}

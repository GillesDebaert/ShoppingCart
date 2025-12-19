namespace ShoppingCart.Data
{
	public interface IDatabase
	{
		Client? GetClient(string clientId);
		Company? GetCompany(int id);
		decimal GetPrice(int productId, string clientId);
		PrivateIndividual? GetPrivateIndividual(int id);
		Product? GetProduct(int id);
	}
}
using Map.Models;

namespace Map.Data.Services
{
	public interface ISmallUrlService
	{
		small_url getByKey(string key);
	}
}
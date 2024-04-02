using api.Dtos.Stock;
using api.Models;

namespace api.Services.Interfaces
{
	public interface IStockService
	{
		Task<List<Stock>> getAllAsync();
		Task<Stock> getStockByIdAsync(int id);

		Task<Stock?> AddStockAsync(Stock stockModel);

		Task<Stock?> UpdateStockAsync(int id,PutRequestDto putRequest);
		Task<Stock> DeleteStockAsync(int id);

		Task<bool> StockExist(int it);

	}
}

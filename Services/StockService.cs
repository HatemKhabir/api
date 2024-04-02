using api.Data;
using api.Dtos.Stock;
using api.Models;
using api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
	public class StockService : IStockService
	{
		private readonly ApplicationDBContext context;
		public StockService(ApplicationDBContext context) {
			this.context = context;
		
		}

		public async Task<Stock?> AddStockAsync(Stock stockModel)
		{
			await context.Stocks.AddAsync(stockModel);
			await context.SaveChangesAsync();
			return stockModel;

		}

		public async Task<Stock?> DeleteStockAsync(int id)
		{
			var stock =await context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
			if (stock == null)
			{ return null; }
			context.Stocks.Remove(stock);
			await context.SaveChangesAsync();
			return stock;
		}

		public async Task<List<Stock>> getAllAsync()
		{
			return await context.Stocks.Include(c=>c.Comments).ToListAsync();
		}

		public async Task<Stock?> getStockByIdAsync(int id)
		{
			return await context.Stocks.Include(c=>c.Comments).FirstOrDefaultAsync(s => s.Id == id);
		}

		public async Task<bool> StockExist(int id)
		{
			return await context.Stocks.AnyAsync(s=>s.Id== id);
		}

		public async Task<Stock?> UpdateStockAsync(int id, PutRequestDto putRequest)
		{
			var existingStock = await context.Stocks.FirstOrDefaultAsync(s => s.Id==id);
			if (existingStock == null)
			{
				return null;
			}
			existingStock.Symbol = putRequest.Symbol;
			existingStock.CompanyName = putRequest.CompanyName;
			existingStock.Industry = putRequest.Industry;
			existingStock.Purchase = putRequest.Purchase;
			existingStock.MarketCap = putRequest.MarketCap;

			await context.SaveChangesAsync();

			return existingStock;

		}
	}
}

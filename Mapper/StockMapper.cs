using api.Dtos.Socket;
using api.Dtos.Stock;
using api.Models;

namespace api.Mapper
{
	public static class StockMapper
	{
		public static StockDto ToStockDto(this Stock stockModel)
		{
			return new StockDto
			{
				Id = stockModel.Id,
				Symbol = stockModel.Symbol,
				CompanyName = stockModel.CompanyName,
				Purchase = stockModel.Purchase,
				LastDiv=stockModel.LastDiv,
				Industry=stockModel.Industry,
				MarketCap=stockModel.MarketCap,
				Comments=stockModel.Comments.Select(c => c.ToCommentDTO()).ToList()
			};

		}
		public static Stock ToStockFromPostDto(this PostRequestDTO stockModel)
		{
			return new Stock
			{
				Symbol = stockModel.Symbol,
				CompanyName = stockModel.CompanyName,
				Purchase = stockModel.Purchase,
				LastDiv = stockModel.LastDiv,
				Industry = stockModel.Industry,
				MarketCap = stockModel.MarketCap,
			};

		}
	}
}

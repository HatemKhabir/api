using api.Data;
using api.Dtos.Stock;
using api.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
	[Route("api/stocks")]
	[ApiController]
	public class StockController : ControllerBase
	{
		private readonly ApplicationDBContext _context;
		public StockController(ApplicationDBContext context) {
		this._context = context;

		}

		[HttpGet]
		public async Task<IActionResult> GetAll() {
			var stocks = await _context.Stocks.ToListAsync();
			var stockDto=stocks.Select(s=>s.ToStockDto());
			if (stockDto == null)
			{
				return NotFound();
			}
			return Ok(stockDto);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute]int id)
		{var stock=await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
			if (stock == null)
			{
				return NotFound();
			}
			return Ok(stock);

		}
		[HttpPost]
		public async Task<IActionResult> addStock([FromBody] PostRequestDTO stockDto)
		{
			var stockModel=stockDto.ToStockFromPostDto();
			await _context.Stocks.AddAsync(stockModel);
			await _context.SaveChangesAsync();
			return CreatedAtAction(nameof(GetById), new {id=stockModel.Id},stockModel.ToStockDto());

		}
		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Update([FromRoute]int id, [FromBody] PutRequestDto stockModel)
		{
			var findStock=await _context.Stocks.FirstOrDefaultAsync(s=>s.Id == id);
			if (findStock == null)
			{ return NotFound(); }
			findStock.Symbol=stockModel.Symbol;
			findStock.CompanyName=stockModel.CompanyName;
			findStock.Industry=stockModel.Industry;
			findStock.Purchase=stockModel.Purchase;
			findStock.MarketCap=stockModel.MarketCap;

			await _context.SaveChangesAsync();
			return Ok(findStock.ToStockDto());

		}
		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> deleteStock([FromRoute]int id)
		{
			var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
			if (stock==null)
			{ return NotFound(); }
		     _context.Stocks.Remove(stock);
			await _context.SaveChangesAsync();
			return NoContent();
		}

	}
}

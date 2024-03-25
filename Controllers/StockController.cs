using api.Data;
using api.Dtos.Stock;
using api.Mapper;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
	[Route("api/stocks")]
	[ApiController]
	public class StockController : ControllerBase
	{
		private readonly ApplicationDBContext _context;
		private readonly IStockService _stockService;
		public StockController(ApplicationDBContext context,IStockService stockService) 
		{
			_context = context;
			_stockService = stockService;

		}

		[HttpGet]
		public async Task<IActionResult> GetAll() {
			var stocks = await _stockService.getAllAsync();
			var stockDto=stocks.Select(s=>s.ToStockDto());
			if (stockDto == null)
			{
				return NotFound();
			}
			return Ok(stockDto);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute]int id)
		{
			var stock = await _stockService.getStockByIdAsync(id);
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
			await _stockService.AddStockAsync(stockModel);
			return CreatedAtAction(nameof(GetById), new {id=stockModel.Id},stockModel.ToStockDto());
		}
		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Update([FromRoute]int id, [FromBody] PutRequestDto stockModel)
		{
			var stock = await _stockService.UpdateStockAsync(id, stockModel);

			return Ok(stock.ToStockDto());

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

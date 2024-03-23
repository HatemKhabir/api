using api.Data;
using api.Dtos.Stock;
using api.Mapper;
using Microsoft.AspNetCore.Mvc;

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
		public IActionResult GetAll() {
			var stocks = _context.Stocks.ToList().Select(s=>s.ToStockDto());
			if (stocks == null)
			{
				return NotFound();
			}
			return Ok(stocks);
		}

		[HttpGet("{id}")]
		public IActionResult GetById([FromRoute]int id)
		{var stock= _context.Stocks.FirstOrDefault(s => s.Id == id);
			if (stock == null)
			{
				return NotFound();
			}
			return Ok(stock);

		}
		[HttpPost]
		public IActionResult addStock([FromBody] PostRequestDTO stockDto)
		{
			var stockModel=stockDto.ToStockFromPostDto();
			_context.Stocks.Add(stockModel);
			_context.SaveChanges();
			return CreatedAtAction(nameof(GetById), new {id=stockModel.Id},stockModel.ToStockDto());

		}
		[HttpPut]
		[Route("{id}")]
		public IActionResult Update([FromRoute]int id, [FromBody] PutRequestDto stockModel)
		{
			var findStock=_context.Stocks.FirstOrDefault(s=>s.Id == id);
			if (findStock == null)
			{ return NotFound(); }
			findStock.Symbol=stockModel.Symbol;
			findStock.CompanyName=stockModel.CompanyName;
			findStock.Industry=stockModel.Industry;
			findStock.Purchase=stockModel.Purchase;
			findStock.MarketCap=stockModel.MarketCap;

			_context.SaveChanges();
			return Ok(findStock.ToStockDto());

		}
		[HttpDelete]
		[Route("{id}")]
		public IActionResult deleteStock([FromRoute]int id)
		{
			var stock = _context.Stocks.FirstOrDefault(s => s.Id == id);
			if (stock==null)
			{ return NotFound(); }
			_context.Stocks.Remove(stock);
			_context.SaveChanges();
			return NoContent();
		}

	}
}

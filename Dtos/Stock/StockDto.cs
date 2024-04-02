using api.Dtos.Comment;
using api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Dtos.Socket
{
	//what object returned will give back with the mapper as well , for example stock table have comments but i will return everything except comments
	public class StockDto
	{
		public int Id { get; set; }
		public string Symbol { get; set; } = string.Empty;
		public string CompanyName { get; set; } = string.Empty;

		public decimal Purchase { get; set; }
		public decimal LastDiv { get; set; }
		public string Industry { get; set; } = string.Empty;
		public long MarketCap { get; set; }

		public List<CommentDTO> Comments { get; set;}
	}
}


using api.Mapper;
using api.Services.Interfaces;
using api.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using api.Dtos.Comment;
using api.Models;

namespace api.Controllers
{
	[Route("api/comment")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private readonly ICommentsService _comment;
		private readonly IStockService _stock;
        public CommentController(ICommentsService commentsService,IStockService stockService)
        {
            this._comment= commentsService;
			this._stock= stockService;
        }

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var comments = await _comment.getAllCommentsAsync();
			var commentDto = comments.Select(s => s.ToCommentDTO());
			return Ok(commentDto);
		}

		[HttpGet("{commentId}")]
		public async Task<IActionResult> GetCommentById(int commentId)
		{
			var comment = await _comment.GetCommentByIdAsync(commentId);
				if (comment == null)
			{
				return BadRequest("NotFound");
			}
				return Ok(comment.ToCommentDTO());
		}

		[HttpPost("{stockId}")]
		public async Task<IActionResult> CreateComment(int stockId,CreateCommentDTO commentDTO)
		{
			if (!await _stock.StockExist(stockId))
			{
				return BadRequest("stock does not exist");
			}
			var commentModel=commentDTO.ToCommentFromPost(stockId);
			await _comment.CreateAsync(commentModel);
			return CreatedAtAction(nameof(GetCommentById), new { commentId = commentModel.Id }, commentModel.ToCommentDTO());
			
		}

		[HttpDelete("{commentId}")]
		public async Task<IActionResult> DeleteComment(int commentId)
		{var comment =await _comment.DeleteAsync(commentId);
			if (comment == null)
			{
				return BadRequest("Comment Not Found");
			}
			return Ok(comment);

		}


	}
}

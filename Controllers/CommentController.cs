
using api.Mapper;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
	[Route("api/comment")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private readonly ICommentsService _comment;
        public CommentController(ICommentsService commentsService)
        {
            this._comment= commentsService;
        }

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var comments = await _comment.getAllCommentsAsync();
			var commentDto = comments.Select(s => s.ToCommentDTO());
			return Ok(commentDto);
		}

	}
}

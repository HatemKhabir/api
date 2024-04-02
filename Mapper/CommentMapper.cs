using api.Dtos.Comment;
using api.Models;

namespace api.Mapper
{
	public static class CommentMapper
	{
		public static CommentDTO ToCommentDTO(this Comment comment)
		{
			return new CommentDTO
			{
				Id = comment.Id,
				Title = comment.Title,
				Content = comment.Content,
				CreatedOn = comment.CreatedOn,
				StockId = comment.StockId,
			};

		}
		public static Comment ToCommentFromPost(this CreateCommentDTO comment,int stockId)
		{
			return new Comment
			{
				Title = comment.Title,
				Content = comment.Content,
				StockId=stockId
			};

		}
	}
}

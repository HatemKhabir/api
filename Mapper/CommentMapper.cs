using api.Dtos.Comment;
using api.Models;

namespace api.Mapper
{
	public static class CommentMapper
	{
		public static CommentDTO ToCommentDTO(this Comment commentDTO)
		{
			return new CommentDTO
			{
				Id = commentDTO.Id,
				Title = commentDTO.Title,
				Content = commentDTO.Content,
				CreatedOn = commentDTO.CreatedOn,
				StockId = commentDTO.StockId,
			};

		}
	}
}

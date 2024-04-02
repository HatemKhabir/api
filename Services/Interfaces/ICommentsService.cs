using api.Models;

namespace api.Services.Interfaces
{
	public interface ICommentsService
	{
		Task<List<Comment>> getAllCommentsAsync(); 
		Task<Comment?> GetCommentByIdAsync(int id);
		Task<Comment?> CreateAsync(Comment comment);
		Task<Comment?> DeleteAsync(int commentId);
	}
}

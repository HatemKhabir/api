using api.Models;

namespace api.Services.Interfaces
{
	public interface ICommentsService
	{
		Task<List<Comment>> getAllCommentsAsync(); 
	}
}

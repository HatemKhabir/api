using api.Data;
using api.Models;
using api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
	public class CommentService : ICommentsService
	{
		private readonly ApplicationDBContext context;
        public CommentService(ApplicationDBContext context)
        {
			this.context = context;
            
        }
        public async Task<List<Comment>> getAllCommentsAsync()
		{
			return await context.Comments.ToListAsync();
		}
	}
}

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

		public async Task<Comment?> CreateAsync(Comment comment)
		{
             await context.Comments.AddAsync(comment);
			await context.SaveChangesAsync();
			return comment;
		}

		public async Task<Comment?> DeleteAsync(int commentId)
		{
			var comment=await context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
			 if (comment == null) {
				return null;
			}
			context.Comments.Remove(comment);
			await context.SaveChangesAsync();
			return comment;
		}

		public async Task<List<Comment>> getAllCommentsAsync()
		{
			return await context.Comments.ToListAsync();
		}

		public async Task<Comment?> GetCommentByIdAsync(int id)
		{
			return await context.Comments.FirstOrDefaultAsync(c => c.Id == id);
		}
	}
}

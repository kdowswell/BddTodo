using BddTodo.Controllers.Users._Infrastructure;
using BddTodo.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BddTodo.Controllers.Todos.Queries
{
    public class GetTodosQuery : IRequest<IEnumerable<GetTodosResult>>
    {
        public int TodoListId { get; set; }
    }

    public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, IEnumerable<GetTodosResult>>
    {
        private readonly BddTodoDbContext _context;
        private readonly ICurrentUser _currentUser;

        public GetTodosQueryHandler(BddTodoDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<IEnumerable<GetTodosResult>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            var results = await _context.Todo
                .OrderBy(x => x.Id)
                .Take(10)
                .Where(x => x.UserId == _currentUser.Id && x.TodoListId == request.TodoListId)
                .Select(x => new GetTodosResult
                {
                    Id = x.Id,
                    Title = x.Title
                })
                .ToListAsync(cancellationToken);

            return results;
        }

    }

    public class GetTodosResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

}

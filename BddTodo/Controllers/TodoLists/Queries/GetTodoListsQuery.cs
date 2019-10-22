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

namespace BddTodo.Controllers.TodoLists.Queries
{
    public class GetTodoListsQuery : IRequest<IEnumerable<GetTodoListsResult>>
    {
    }

    public class GetTodoListsQueryHandler : IRequestHandler<GetTodoListsQuery, IEnumerable<GetTodoListsResult>>
    {
        private readonly BddTodoDbContext _context;
        private readonly ICurrentUser _currentUser;

        public GetTodoListsQueryHandler(BddTodoDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<IEnumerable<GetTodoListsResult>> Handle(GetTodoListsQuery request, CancellationToken cancellationToken)
        {
            var results = await _context.TodoList
                .OrderBy(x => x.Id)
                .Take(10)
                .Where(x => x.UserId == _currentUser.Id)
                .Select(x => new GetTodoListsResult
                {
                    Id = x.Id,
                    Title = x.Title
                })
                .ToListAsync(cancellationToken);

            return results;
        }

    }

    public class GetTodoListsResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

}

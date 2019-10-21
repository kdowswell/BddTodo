using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BddTodo.Tests._Infrastructure
{
    public class BadRequestServerResult
    {
        public ExpandoObject errors { get; set; }
        public string title { get; set; }
        public int status { get; set; }
        public string traceId { get; set; }
    }
}

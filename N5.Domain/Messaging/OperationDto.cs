using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5.Domain.Messaging
{
    public class OperationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

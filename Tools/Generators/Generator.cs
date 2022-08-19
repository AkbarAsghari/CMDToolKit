using Base.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Generators
{
    public class Generator
    {
        public ToolResult GuidGenerator()
        {
            return new ToolResult { Message = Guid.NewGuid().ToString(), IsSuccess = true };
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting02.Projects
{
    public class InvalidIssueDescriptionException: Exception
    {
        public InvalidIssueDescriptionException(): base("issue description cannot be null or whitespace")
        {
        }
    }
}

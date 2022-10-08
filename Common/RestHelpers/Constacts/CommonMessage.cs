using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestHelpers.Constacts
{
    public class CommonMessage
    {
        public const string NullOrEmptyMessage = "Please Enter {0}";

        public const string WrongFormat = "Entered the wrong format {0}";

        public const string ConnotContainsSpace = "{0} Field Cannot Contain Spaces";

        public const string ServerError = "Error Occurred During Operation";

        public const string Success = "Operation Completed Successfully";

        public const string NotFound = "Record Not Found";
    }
}

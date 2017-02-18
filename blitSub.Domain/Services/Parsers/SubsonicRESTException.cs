using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blitSub.Domain.Services.Parsers
{
    public class SubsonicRESTException : Exception
    {
        private readonly int code;

        public SubsonicRESTException(int code, string message)
        {
            base(message);
            this.code = code;
        }

        public int getCode()
        {
            return code;
        }
    }
}

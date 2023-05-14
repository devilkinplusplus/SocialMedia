using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Exceptions
{
    public class PasswordChangeFailedException : Exception
    {
        public PasswordChangeFailedException():base("Password reset is failed") { }
        public PasswordChangeFailedException(string message) : base(message) { }
        public PasswordChangeFailedException(string message, Exception inner) : base(message, inner) { }
      
    }
}

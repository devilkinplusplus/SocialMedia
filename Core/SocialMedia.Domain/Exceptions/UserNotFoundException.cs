﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException():base("User is not found") { }
        public UserNotFoundException(string message) : base(message) { }
        public UserNotFoundException(string message, Exception inner) : base(message, inner) { }
       
    }
}

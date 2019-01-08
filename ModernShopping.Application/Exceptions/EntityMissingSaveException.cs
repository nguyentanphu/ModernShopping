using System;
using System.Collections.Generic;
using System.Text;

namespace ModernShopping.Application.Exceptions
{
    class EntityMissingSaveException : Exception
    {
        public EntityMissingSaveException(string message) : base(message)
        {
            
        }
    }
}

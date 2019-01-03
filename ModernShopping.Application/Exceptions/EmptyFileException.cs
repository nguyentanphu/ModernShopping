using System;
using System.Collections.Generic;
using System.Text;

namespace ModernShopping.Application.Exceptions
{
    public class EmptyFileException : Exception
    {
        public EmptyFileException(string message = "The input file does not have any content")
            : base(message)
        {
        }
    }
}

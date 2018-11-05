using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.Items.Exceptions
{
    public class ItemInputException : ApplicationException
    {
        public ItemInputException() : base("Please provide all fields required for this Item")
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.Items.Exceptions
{
    public class ItemNotFoundException : ApplicationException
    {
        public ItemNotFoundException() : base("Item with this id don't exist")
        {
        }
    }
}

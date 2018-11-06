using Oder.Services.ItemGroups;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Services.Orders
{
   public class OrderDTO
    {
        public double TotalPrice { get; set; }
        public List<ItemGroupDTO> ItemGroupsDTO { get; set; }
        public int IdOfCustomer { get; set; }
        public int Id { get; set; }
        public OrderDTO()
        {
            ItemGroupsDTO = new List<ItemGroupDTO>();
            IdOfCustomer = -1;
        }
    }
}

using Oder.Services.ItemGroups;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Services.Orders
{
    public class  OrderDTOForInReport
    {
        public double TotalPrice { get; set; }
        public List<ItemGroupDTOForInReport> ItemGroupsDTO { get; set; }
        public int Id { get; set; }
    }
}

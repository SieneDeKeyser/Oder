using Oder.Domain.ItemGroups;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Services.ItemGroups
{
    public interface IItemGroupMapper
    {
        ItemGroup FromItemGroupDTOToItem(ItemGroupDTO itemGroupDTO);
    }
}

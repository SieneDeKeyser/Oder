using Oder.Domain.ItemGroups;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Services.ItemGroups
{
    public interface IItemGroupMapper
    {
        ItemGroup FromItemGroupDTOToItemGroup(ItemGroupDTO itemGroupDTO);
        ItemGroupDTO FromItemGroupToItemGroupDTO(ItemGroup itemGroupToMap);
    }
}

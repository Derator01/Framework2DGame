using SimpleGame.Items.Interfaces;
using System.Collections.Generic;

namespace Framework2DGame
{
    public static class Extensions
    {
        public static IItem ContainsItem(this List<IItem> inventory, IItem itemToCheck)
        {
            foreach (var item in inventory)
            {
                if (item.Id == itemToCheck.Id) 
                    return item;
            }
            return null;
        }
    }
}

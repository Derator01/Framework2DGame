using System.Collections.Generic;

namespace SimpleGame.Items.Interfaces
{
    internal interface IEquipment
    {
        bool IsEquipped { get; }

        Dictionary<string, int> Effects { get; }
    }
}

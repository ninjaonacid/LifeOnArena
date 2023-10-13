using UnityEngine;

namespace Code.UI.Model.AbilityMenu
{
    public class AbilitySlotModel
    {
        public bool IsEquipped;
        public int AbilityId;

        public AbilitySlotModel(bool isEquipped, int abilityId)
        {
            IsEquipped = isEquipped;
            AbilityId = abilityId;
        }
    }
}

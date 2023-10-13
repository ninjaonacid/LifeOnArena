using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.UI.View.AbilityMenu
{
    public class AbilityContainer : MonoBehaviour
    {
        public List<AbilityItemView> AbilityItem;
        

        public void InitializeAbilityContainer()
        {
            foreach (var ability in AbilityItem)
            {
                ability.OnAbilityItemClick += HandleAbilitySelection;
            }
        }

        private void HandleAbilitySelection(AbilityItemView obj)
        {
            throw new NotImplementedException();
        }

        
    }
}

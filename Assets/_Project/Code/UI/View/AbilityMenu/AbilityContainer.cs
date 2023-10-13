using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.UI.View.AbilityMenu
{
    public class AbilityContainer : MonoBehaviour
    {
        private List<AbilityItemView> _abilityItems;

        public AbilityItemView SelectedItem;
        public void InitializeAbilityContainer()
        {

            _abilityItems = GetComponentsInChildren<AbilityItemView>().ToList();
            
            foreach (var ability in _abilityItems)
            {
                ability.OnAbilityItemClick += HandleAbilitySelection;
            }
        }

        private void HandleAbilitySelection(AbilityItemView obj)
        {
            if (SelectedItem && SelectedItem != obj)
            {
                SelectedItem.Deselect();
            }

            SelectedItem = obj;
            SelectedItem.Select();
            
            Debug.Log("Zalupa");
        }

        
    }
}

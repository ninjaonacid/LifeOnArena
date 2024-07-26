using Code.Runtime.Data.PlayerData;
using Code.Runtime.Modules.AbilitySystem;
using Code.Runtime.Services.PersistentProgress;
using UnityEngine;

namespace Code.Runtime.Entity.Hero
{
    public class HeroAbilityController : AbilityController, ISaveLoader
    {
        [SerializeField] private PlayerInputHandler _inputHandler;
        
        public override void Start()
        {
            base.Start();
            _inputHandler.OnAbilityActivated += OnAbilityActivated;
        }

        private void OnDestroy()
        {
            _inputHandler.OnAbilityActivated -= OnAbilityActivated;
        }

        private void OnAbilityActivated(int index)
        {
            var abilityToUse = _abilitySlots[index].AbilityIdentifier;
            if (abilityToUse == null) return;
            TryActivateAbility(abilityToUse);
        }
        
        public void LoadData(PlayerData data)
        {
            var skillsData = data.AbilityData;

            if (skillsData.EquippedAbilities.Count > 0)
            {
                for (var index = 0; index < skillsData.EquippedAbilities.Count; index++)
                {
                    var slot = _abilitySlots[index];

                    var ability = _abilityFactory.CreateActiveAbility(skillsData.EquippedAbilities[index].AbilityId, this);

                    slot.Ability = ability;
                    slot.AbilityIdentifier = ability.AbilityBlueprint.Identifier;
                }
            }
            
            OnAbilityChanged();
        }
    }
}
using Code.StaticData.Ability;
using UnityEngine;

namespace Code.Hero
{
    public class AbilityTest : MonoBehaviour
    {
        public AbilityTemplateBase ability;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                var abilityInstance = ability.GetAbility();
                abilityInstance.Use(transform.gameObject, transform.gameObject);
            }
        }
    }
}

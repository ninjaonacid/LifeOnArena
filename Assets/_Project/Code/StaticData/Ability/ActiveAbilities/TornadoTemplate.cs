using UnityEngine;

namespace Code.StaticData.Ability.ActiveAbilities
{
    [CreateAssetMenu(fileName = "Tornado", menuName = "AbilityData/Cast/Tornado")]
    public class TornadoTemplate : AbilityTemplate<Tornado>
    {
        public GameObject TornadoVFX;
        public override IAbility GetAbility()
        {
            return new Tornado(TornadoVFX, ActiveTime);
        }
    }
}

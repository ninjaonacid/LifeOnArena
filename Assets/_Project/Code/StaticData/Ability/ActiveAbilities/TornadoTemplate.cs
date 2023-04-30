using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.StaticData.Ability.ActiveAbilities
{
    [CreateAssetMenu(fileName = "Tornado", menuName = "AbilityData/Cast/Tornado")]
    public class TornadoTemplate : AbilityTemplate<Tornado>
    {
        public AssetReference TornadoVfx;
        public float CastDistance;
        public override IAbility GetAbility()
        {
            return new Tornado(TornadoVfx, ActiveTime);
        }
    }
}

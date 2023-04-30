using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.StaticData.Ability.ActiveAbilities
{
    public class Tornado : IAbility
    {
        private AssetReference _tornadoVfx;
        private float _duration;
        
        public Tornado(AssetReference tornadoVfx, float duration)
        {
            _tornadoVfx = tornadoVfx;
            _duration = duration;
        }
        public void Use(GameObject caster, GameObject target)
        {
            var casterPosition = caster.transform.position;
            var casterDirection = caster.transform.forward;
            var castOffset = 3f;
            //Object.Instantiate(_tornadoVfx, casterPosition + casterDirection * castOffset, Quaternion.identity);

        }
    }
}

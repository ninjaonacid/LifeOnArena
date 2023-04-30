using UnityEngine;

namespace Code.StaticData.Ability.ActiveAbilities
{
    public class Tornado : IAbility
    {
        private GameObject _tornadoVfx;
        private float _duration;
        public Tornado(GameObject tornadoVFX, float duration)
        {
            _tornadoVfx = tornadoVFX;
            _duration = duration;
        }
        public void Use(GameObject caster, GameObject target)
        {
            var position = caster.transform.position;
            Object.Instantiate(_tornadoVfx, position + new Vector3(0, 0, 3), Quaternion.identity);

        }
    }
}

using System.Collections;
using UnityEngine;

namespace Code.Entity.Hero
{
    public class HeroHurtBox : EntityHurtBox
    {
        
        public void DisableHitBox(float time)
        {
            _hitBoxCollider.enabled = false;
            StartCoroutine(DisabledTime(time));
        }

        private IEnumerator DisabledTime(float time)
        {
            yield return new WaitForSeconds(time);
            _hitBoxCollider.enabled = true;
        }
    }
}

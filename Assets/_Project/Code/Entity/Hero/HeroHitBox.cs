using System.Collections;
using UnityEngine;

namespace Code.Entity.Hero
{
    public class HeroHitBox : MonoBehaviour
    {
        [SerializeField] private BoxCollider _heroCollider;


        public void DisableHitBox(float time)
        {
            _heroCollider.enabled = false;
            StartCoroutine(DisabledTime(time));
        }

        private IEnumerator DisabledTime(float time)
        {
            yield return new WaitForSeconds(time);
            _heroCollider.enabled = true;
        }
    }
}

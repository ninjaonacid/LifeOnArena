using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Logic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace CodeBase.Hero
{
    public class HeroVFX : MonoBehaviour
    {
        public List<Slash> SwordSlashes = new List<Slash>();
        public Transform vfxSlot;

        [Serializable]
        public class Slash
        {
            public GameObject SlashPrefabVFX;
            public float playDelay;
        }

        public void PlaySwordSlash(AnimatorState attackAnimation)
        {
            switch (attackAnimation)
            {
                case AnimatorState.Attack:
                    
                    StartCoroutine(SpawnSwordVfx(SwordSlashes[0]));
                    break;

                case AnimatorState.Attack2:
                    
                    StartCoroutine(SpawnSwordVfx(SwordSlashes[1]));
                    break;

                default: Debug.Log("NoThingVFX");
                    break;
            }
        }

        public IEnumerator SpawnSwordVfx(Slash swordSlash)
        {
            yield return new WaitForSeconds(swordSlash.playDelay);

            var vfx = Instantiate(swordSlash.SlashPrefabVFX, vfxSlot.transform);
            vfx.transform.localPosition = Vector3.zero;
            
            vfx.transform.parent = null;
            
        }
    }
}
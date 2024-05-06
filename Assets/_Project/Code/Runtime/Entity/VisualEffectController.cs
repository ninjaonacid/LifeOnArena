using System;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Factory;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using VContainer;
using Quaternion = UnityEngine.Quaternion;

namespace Code.Runtime.Entity
{
    public class VisualEffectController : MonoBehaviour
    {
        private VisualEffectFactory _visualFactory;

        [Inject]
        public void Construct(VisualEffectFactory visualFactory)
        {
            _visualFactory = visualFactory;
        }
        
        public async UniTask PlayVisualEffect(VisualEffectIdentifier effectId)
        {
            var effect = await _visualFactory.CreateVisualEffect(effectId.Id);
            effect.Play();
            
        }

        public async UniTask PlayVisualEffect(VisualEffectIdentifier effectId, Vector3 position)
        {
            var effect = await _visualFactory.CreateVisualEffect(effectId.Id);
            effect.transform.position = position;
            effect.Play();
        }
        
        public async UniTask PlayVisualEffect(VisualEffectIdentifier effectId, Vector3 position, Quaternion rotation)
        {
            var effect = await _visualFactory.CreateVisualEffect(effectId.Id);
            effect.transform.position = position;
            effect.transform.rotation = rotation;
            effect.Play();
        }
        
        public async UniTask PlayVisualEffect(VisualEffectIdentifier effectId, Transform targetTransform)
        {
            var effect = await _visualFactory.CreateVisualEffect(effectId.Id);
            effect.transform.position = targetTransform.position;
            //effect.transform.rotation = targetTransform.rotation;
            effect.Play();
        }
        
        public async UniTask PlayVisualEffect(VisualEffectIdentifier effectId, Transform targetTransform, Quaternion rotation, float delay)
        {
            var effect = await _visualFactory.CreateVisualEffect(effectId.Id);
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            
            effect.transform.localRotation = targetTransform.transform.rotation;
            effect.transform.position = targetTransform.transform.position;
            effect.transform.rotation = Quaternion.LookRotation(targetTransform.forward);
            effect.Play();
        }
        
        public async UniTask PlayVisualEffect(VisualEffectIdentifier effectId, Transform targetTransform, float delay)
        {
            var effect = await _visualFactory.CreateVisualEffect(effectId.Id);
            await UniTask.Delay(TimeSpan.FromSeconds(delay));

            Vector3 swordDirection = targetTransform.forward;
            Vector3 effectDirection = effect.transform.forward;

            float angle = Vector3.SignedAngle(swordDirection, effectDirection, Vector3.up);
            Quaternion effectRotation = Quaternion.AngleAxis(angle, Vector3.up);
            
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up) * Quaternion.AngleAxis(90f, Vector3.right);
            effect.transform.rotation = rotation * effect.transform.rotation * new Quaternion(100f, 0f, 0f, 1f);

            effect.transform.position = targetTransform.position;
            effect.Play();
        }
        
    }
}

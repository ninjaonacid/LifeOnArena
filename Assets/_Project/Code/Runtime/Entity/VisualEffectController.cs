using System;
using Code.Runtime.ConfigData;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using Quaternion = UnityEngine.Quaternion;

namespace Code.Runtime.Entity
{
    public class VisualEffectController : MonoBehaviour
    {
        private VisualEffectFactory _visualFactory;
        [SerializeField] private Transform _slashVfxPoint;

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
            var transform1 = effect.transform;
            transform1.position = position;
            transform1.rotation = rotation;
            effect.Play();
        }
        
        public async UniTask PlayVisualEffect(VisualEffectIdentifier effectId, Transform targetTransform)
        {
            var effect = await _visualFactory.CreateVisualEffect(effectId.Id);
            effect.transform.position = targetTransform.position;
            //effect.transform.rotation = targetTransform.rotation;
            effect.Play();
        }

        public async UniTask PlayVisualEffect(VisualEffectIdentifier effectId, Transform targetTransform, float delay)
        {
            var effect = await _visualFactory.CreateVisualEffect(effectId.Id);
            await UniTask.Delay(TimeSpan.FromSeconds(delay));


            var transform1 = effect.transform;
            transform1.rotation = _slashVfxPoint.rotation;
            transform1.position = _slashVfxPoint.position;
            
            effect.Play();
        }
        
        
        public async UniTask PlaySlashVisualEffect(VisualEffectIdentifier effectId, SlashDirection direction, Vector3 scale, float delay)
        {
            var effect = await _visualFactory.CreateVisualEffect(effectId.Id);
            await UniTask.Delay(TimeSpan.FromSeconds(delay));

            effect.transform.position = _slashVfxPoint.position;
            effect.transform.localScale = scale;

            Quaternion rotation = default;

            switch (direction)
            {
                case SlashDirection.Left:
                    rotation = Quaternion.Euler(new Vector3(180, 0, 0));
                    break;
                case SlashDirection.Right:
                    rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
                case SlashDirection.Down:
                    rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                    break;
                case SlashDirection.Up:
                    break;
                case SlashDirection.LeftUp:
                    rotation = Quaternion.Euler(new Vector3(0, 0, -325));
                    break;
                case SlashDirection.RightDown:
                    rotation = Quaternion.Euler(new Vector3(0, 0, 225));
                    break;
                case SlashDirection.LeftDown:
                    rotation = Quaternion.Euler(new Vector3(0, 0, 225));
                    break;
                case SlashDirection.RightUp:
                    rotation = Quaternion.Euler(new Vector3(0, 0, -45));
                    break;
            }
            
            effect.transform.rotation = _slashVfxPoint.rotation * rotation;
            
            effect.Play();
        }
    }
}

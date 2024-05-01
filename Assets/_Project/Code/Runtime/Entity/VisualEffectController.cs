using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

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
    }
}

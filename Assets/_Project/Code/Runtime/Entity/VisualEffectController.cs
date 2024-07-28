using System;
using System.Numerics;
using System.Threading;
using Code.Runtime.ConfigData;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Factory;
using Code.Runtime.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Code.Runtime.Entity
{
    public class VisualEffectController : MonoBehaviour
    {
        private VisualEffectFactory _visualFactory;
        [SerializeField] private Transform _slashVfxPoint;
        private CancellationTokenSource _cts = new();
        
        [Inject]
        public void Construct(VisualEffectFactory visualFactory)
        {
            _visualFactory = visualFactory;
        }
        
        public async UniTask PlayVisualEffect(VisualEffectData effectData, Vector3? position = null, Quaternion? rotation = null, GameObject target = null, float delay = 0)
        {
            try
            {
                _cts.Token.ThrowIfCancellationRequested();

                var effect = await _visualFactory.CreateVisualEffect(effectData.Identifier.Id, cancellationToken: _cts.Token);
                

                if (delay > 0)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: _cts.Token);
                }

                var effectTransform = effect.transform;
                
                Vector3 basePosition;

                if (target != null)
                {
                    basePosition = target.transform.position;
                   
                    effect.transform.position = CalculatePlayPositionTarget(effectData.PlayPosition, target);
                    effect.transform.SetParent(target.transform);
                }
                else
                {
                    basePosition = position ?? transform.position;
                }

                effectTransform.position = CalculatePlayPositionCaster(effectData.PlayPosition, basePosition);

                effect.Play();

            }
            catch (OperationCanceledException)
            {
                // Operation was cancelled, handle or ignore
            }
        }


        public async UniTask PlaySlashVisualEffect(VisualEffectIdentifier effectId, SlashDirection direction, Vector3 scale, float delay)
        {
            _cts.Token.ThrowIfCancellationRequested();
            
            var effect = await _visualFactory.CreateVisualEffect(effectId.Id, cancellationToken: _cts.Token);
            await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: _cts.Token);
            
            
            _cts.Token.ThrowIfCancellationRequested();
                
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

        private Vector3 CalculatePlayPositionCaster(PlayPosition playPosition, Vector3 basePosition)
        {
            
            switch (playPosition)
            {
                case PlayPosition.Center :
                {
                    var casterCenter = Utilities.GetCenterOfCollider(gameObject);
                    return casterCenter + basePosition;
                    break;
                }

                case PlayPosition.Above:
                {
                    float height = Utilities.GetColliderHeight(gameObject);
                    return basePosition + (Vector3.up * height);
                }

                case PlayPosition.Below:
                {
                    var center = Utilities.GetCenterOfCollider(gameObject);
                    float height = Utilities.GetColliderHeight(gameObject);
                    return basePosition + center + (Vector3.down * (height * 0.4f));
                }
            }
            
            return Vector3.one;
        }

        private Vector3 CalculatePlayPositionTarget(PlayPosition playPosition, GameObject target)
        {
            switch (playPosition)
            {
                case PlayPosition.Center :
                {
                    return Utilities.GetCenterOfCollider(target.gameObject);
                    break;
                }

                case PlayPosition.Above:
                {
                    float height = Utilities.GetColliderHeight(gameObject);
                    return Vector3.zero;
                }
            }
            return Vector3.one;
        }
        

        private void OnDestroy()
        {
            _cts.Cancel();
        }
    }
}

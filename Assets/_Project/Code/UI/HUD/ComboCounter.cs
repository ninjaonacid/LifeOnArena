using System;
using System.Threading;
using Code.Entity.Hero;
using Code.Utils;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.UI.HUD
{
    public class ComboCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMesh;
        [SerializeField] private HeroAttack _heroAttack;
        [SerializeField] private HeroHealth _heroHealth;
        private int _hitCount = 0;
        private const int CoolComboCap = 5;
        private const int BrutalComboCap = 8;
        private const int ResetCounterTimeInSeconds = 5;

        private CancellationTokenSource _cts;
        public void Construct(HeroAttack heroAttack, HeroHealth heroHealth)
        {
            _heroAttack = heroAttack;
            _heroHealth = heroHealth;
            _heroAttack.OnHit += IncreaseHitCounter;
            _heroHealth.Health.CurrentValueChanged += ResetHitCounter;
        }

        private void ResetHitCounter()
        {
            _hitCount = 0;
            _textMesh.text = $"Combo {_hitCount}";
            _textMesh.color = Color.white;
        }

        private void IncreaseHitCounter(int hits)
        {
            IncreaseHitCounterAsync(hits, TaskHelper.CreateToken(ref _cts)).Forget();
            _textMesh.transform.DOShakePosition(0.5f, 2f * _hitCount);
        }

        private async UniTaskVoid IncreaseHitCounterAsync(int hits, CancellationToken token)
        {
            for (int i = 0; hits > i; i++)
            {
                _hitCount++;
                if (_hitCount < CoolComboCap)
                {
                    _textMesh.text = $"Combo {_hitCount}";
                }
                else if (_hitCount >= CoolComboCap && _hitCount < BrutalComboCap)
                {
                    _textMesh.text = $"COOL! Combo {_hitCount}";
                    _textMesh.color = new Color(0.1f, 0.8f, 0.1f);
                }
                
                else if (_hitCount >= BrutalComboCap)
                {
                    _textMesh.text = $"BRUTAL! Combo {_hitCount}";
                    _textMesh.color = new Color(0.7f, 0.1f, 0.2f);
              
                }
                await UniTask.Delay(10, cancellationToken: token);
            }
            
            ResetTimer(token).Forget();
        }

        private async UniTaskVoid ResetTimer(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(ResetCounterTimeInSeconds), cancellationToken: token);
            ResetHitCounter();
        }

        private void OnDisable()
        {
            _heroAttack.OnHit -= IncreaseHitCounter;
        }
    }
}

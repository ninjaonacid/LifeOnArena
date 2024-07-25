using System.Threading;
using Code.Runtime.Entity.Hero;
using Code.Runtime.Utils;
using Cysharp.Threading.Tasks;
using PrimeTween;
using TMPro;
using UnityEngine;
using Timer = Code.Runtime.Logic.Timer.Timer;


namespace Code.Runtime.UI.View.HUD
{
    public class ComboCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMesh;
        
        private HeroAttackComponent _heroAttackComponent;
        private HeroHealth _heroHealth;
        
        private int _hitCount = 0;
        private const int CoolComboCap = 5;
        private const int BrutalComboCap = 8;
        private readonly float _resetCounterTimeInSeconds = 10;
        private Tween _comboTween;
        private Tween _resetComboTween;
        private Timer _resetTimer;
        private CancellationTokenSource _cts;
        
        public void Construct(HeroAttackComponent heroAttackComponent, HeroHealth heroHealth)
        {
            _heroAttackComponent = heroAttackComponent;
            _heroHealth = heroHealth;
            _heroAttackComponent.OnHit += IncreaseHitCounter;
            _heroHealth.Health.CurrentValueChanged += ResetHitCounter;
        }
        

        private void ResetHitCounter()
        {
            _hitCount = 0;
            _textMesh.text = $"Combo {_hitCount}";
            _textMesh.color = Color.white;
        }
        
        private void Start()
        {
            _resetTimer = new Timer();
        }

        private void IncreaseHitCounter(int hits)
        {
            _resetTimer.Reset();

            _hitCount += hits;
            
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
            
            _textMesh.transform.DOShakePosition(0.5f, 2f * _hitCount)
                .SetLink(gameObject);

            ResetTimer(TaskHelper.CreateToken(ref _cts)).Forget();

        }
        
        private async UniTaskVoid ResetTimer(CancellationToken token)
        {
            while (_resetTimer.Elapsed < _resetCounterTimeInSeconds)
            {
                await UniTask.Yield();
            }
            
            ResetHitCounter();
        }

        private void OnDisable()
        {
            _heroAttackComponent.OnHit -= IncreaseHitCounter;
            _heroHealth.Health.CurrentValueChanged -= ResetHitCounter;
        }
    }
}

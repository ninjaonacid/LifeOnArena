using Code.Entity.Hero;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

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
        }

        private void IncreaseHitCounter(int hits)
        {
            IncreaseHitCounterAsync(hits).Forget();
        }

        private async UniTaskVoid IncreaseHitCounterAsync(int hits)
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
                    _textMesh.color = new Color(0.5f, 0.1f, 0.2f);
              
                }
                await UniTask.Delay(10);
            }
        }

        private void OnDisable()
        {
            _heroAttack.OnHit -= IncreaseHitCounter;
        }
    }
}

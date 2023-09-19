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
                _textMesh.text = $"Combo {_hitCount}";
                await UniTask.Delay(3);
            }
        }

        private void OnDisable()
        {
            _heroAttack.OnHit -= IncreaseHitCounter;
        }
    }
}

using System;
using Code.Runtime.UI.Buttons;
using TMPro;
using UniRx;
using UnityEngine;

namespace Code.Runtime.UI.View.MainMenu
{
    public class StatsUI : MonoBehaviour
    {
        [SerializeField] private AnimatedButton _plusButton;
        [SerializeField] private TextMeshProUGUI _statName;
        [SerializeField] private TextMeshProUGUI _statValue;

        private Subject<StatsUI> _plusClicked;

        private void Awake()
        {
            _plusButton.OnClickAsObservable().Subscribe(x => _plusClicked.OnNext(this));
        }

        public void SetStatValue(int value)
        {
            _statValue.SetText(value.ToString());
        }

        public IObservable<StatsUI> OnPlusClicked()
        {
           return  _plusClicked ??= new Subject<StatsUI>();
        }

        public void ShowPlusButton(bool value)
        {
            _plusButton.Show(value);
            PlayPlusAnimation();
        }

        public void PlayPlusAnimation()
        {
            _plusButton.PlayScaleAnimation();
            
        }
    }
}

using Code.StaticData.StatSystem;
using UniRx;

namespace Code.UI.Model
{
    public class MainMenuModel : IScreenModel
    {
        public ReactiveProperty<int> Health = new ReactiveProperty<int>();
        public ReactiveProperty<int> Attack = new ReactiveProperty<int>();
        public ReactiveProperty<int> Defense = new ReactiveProperty<int>();

        public MainMenuModel(int health, int attack, int defense)
        {
            Health.Value = health;
            Attack.Value = attack;
            Defense.Value = defense;
        }
        
    }
}

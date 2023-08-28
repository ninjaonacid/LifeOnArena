using Code.StaticData.StatSystem;
using UniRx;

namespace Code.UI.Model
{
    public class MainMenuModel : IScreenModel
    {
        public ReactiveProperty<Stat> Health;
        public ReactiveProperty<Stat> Attack;
        public ReactiveProperty<Stat> Defense;
        public ReactiveProperty<Stat> AttackSpeed;
        public ReactiveProperty<Stat> AttackRadius;

    }
}

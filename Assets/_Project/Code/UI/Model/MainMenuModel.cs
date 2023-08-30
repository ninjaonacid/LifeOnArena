using Code.StaticData.StatSystem;
using UniRx;

namespace Code.UI.Model
{
    public class MainMenuModel : IScreenModel
    {
        public ReactiveProperty<int> Health { get; } = new(); 
        public ReactiveProperty<int> Attack { get; } = new();
        public ReactiveProperty<int> Defense { get; } = new();
        
        
    }
}

using Code.Infrastructure.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.MainMenu
{
    public class MainMenuScreen : ScreenBase
    {
        [SerializeField] private Button StartFightButton;
        [SerializeField] private UIStatContainer StatContainer;
        
        private SceneLoader _sceneLoader;
        
        public void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            
            StartFightButton.onClick.AddListener(LoadLevel);
            StatContainer.Construct(Progress);
        }

        private void LoadLevel()
        {
            _sceneLoader.Load("StoneDungeon_Arena_1");
        }
    }
}

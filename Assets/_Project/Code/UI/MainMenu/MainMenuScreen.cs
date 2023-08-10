using Code.Infrastructure.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.MainMenu
{
    public class MainMenuScreen : ScreenBase
    {
        [SerializeField] private Button StartFightButton;
        
        private SceneLoader _sceneLoader;
        
        public void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            
            StartFightButton.onClick.AddListener(LoadLevel);
        }

        private void LoadLevel()
        {
            _sceneLoader.Load("StoneDungeon_Arena_1");
        }
    }
}

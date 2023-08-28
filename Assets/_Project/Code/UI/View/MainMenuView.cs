using Code.Infrastructure.SceneManagement;
using Code.UI.MainMenu;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.View
{
    public class MainMenuView : MonoBehaviour, IScreenView
    {
        [SerializeField] private Button StartFightButton;
        [SerializeField] private UIStatContainer StatContainer;

        private SceneLoader _sceneLoader;

        public void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Close()
        {
            Destroy(gameObject);
        }
    }
}

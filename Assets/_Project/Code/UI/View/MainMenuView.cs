using System;
using Code.Infrastructure.SceneManagement;
using Code.UI.MainMenu;
using Code.UI.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.View
{
    public class MainMenuView : BaseView
    {
        [SerializeField] private Button StartFightButton;
        public UIStatContainer StatContainer;

        public MainMenuModel model;
        private SceneLoader _sceneLoader;

        public void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
    }
}

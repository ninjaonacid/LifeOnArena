using System.Reflection.Emit;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.UI.SkillsMenu;
using UnityEngine;

namespace Code.UI.MainMenu
{
    public class SkillMenuWindow : WindowBase
    {

        public SkillHolderContainer SkillHolderContainer;
        public SkillListContainer SkillListContainer;

        private IPersistentProgressService _progress;

        public void Construct(IPersistentProgressService persistentProgress)
        {
            base.Construct(persistentProgress);
            SkillHolderContainer.Construct(persistentProgress);
        }
        protected override void OnAwake()
        {
            base.OnAwake();

        }

    }
}

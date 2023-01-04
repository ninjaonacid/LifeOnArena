using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.UI.SkillsMenu;

namespace Code.UI.MainMenu
{
    public class SkillMenuWindow : WindowBase
    {

        public SkillHolderContainer SkillHolderContainer;
        public SkillListContainer SkillListContainer;

        private ISaveLoadService _saveLoadService;

        public void Construct(IProgressService progress, IStaticDataService staticData,
            ISaveLoadService saveLoad)
        {
            base.Construct(progress);
            _saveLoadService = saveLoad;

            SkillHolderContainer.Construct(progress, staticData);
            
        }
        protected override void OnAwake()
        {
            base.OnAwake();
           // CloseButton.onClick.AddListener(() => _saveLoadService.SaveProgress());
        }

    }
}

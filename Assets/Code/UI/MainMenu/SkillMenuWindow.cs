using Code.Services.PersistentProgress;
using Code.UI.SkillsMenu;

namespace Code.UI.MainMenu
{
    public class SkillMenuWindow : WindowBase
    {

        public SkillHolderContainer SkillHolderContainer;
        public SkillListContainer SkillListContainer;

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

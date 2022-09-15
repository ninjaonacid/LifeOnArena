using Code.Services;
using Code.Services.PersistentProgress;
using Code.UI.SkillsMenu;

namespace Code.UI.MainMenu
{
    public class SkillMenuWindow : WindowBase
    {

        public SkillHolderContainer SkillHolderContainer;
        public SkillListContainer SkillListContainer;

        public void Construct(IProgressService progress, IStaticDataService staticData)
        {
            base.Construct(progress);
            SkillHolderContainer.Construct(progress, staticData);
            
        }
        protected override void OnAwake()
        {
            base.OnAwake();

        }

    }
}

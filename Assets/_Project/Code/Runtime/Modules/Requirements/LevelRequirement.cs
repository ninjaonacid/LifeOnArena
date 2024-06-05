namespace Code.Runtime.Modules.Requirements
{
    public class LevelRequirement : Requirement<int>
    {
        protected override bool CheckRequirement(int value)
        {
            return value >= _requiredValue;
        }
    }
}
namespace Code.Runtime.Modules.Requirements
{
    public interface IRequirement
    {
        public bool CheckRequirement(object value);
    }
}

namespace Code.Runtime.UI.Model
{
    public interface ILoadableModel
    {
        public void LoadData();
    }

    public interface ISavableModel : ILoadableModel
    {
        public void SaveModelData();
    }
}
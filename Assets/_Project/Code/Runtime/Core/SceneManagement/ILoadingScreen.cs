namespace Code.Runtime.Core.SceneManagement
{
    public interface ILoadingScreen
    {
        void Show();
        void Hide();
        void UpdateProgress(float progress);
    }
}

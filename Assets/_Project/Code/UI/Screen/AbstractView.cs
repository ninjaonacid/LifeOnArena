using Code.UI.Model;
using Code.UI.View;

namespace Code.UI.Screen
{
    public class AbstractView<TModel> : BaseView where TModel : IScreenModel
    {
        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Close()
        {
            Destroy(gameObject);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}

using UnityEngine;

namespace SimpleInputNamespace
{
    public class KeyInputKeyboard : MonoBehaviour
    {
        public SimpleInput.KeyInput key = new SimpleInput.KeyInput();
#pragma warning disable 0649
        [SerializeField] private KeyCode realKey;
#pragma warning restore 0649

        private void OnEnable()
        {
            key.StartTracking();
            SimpleInput.OnUpdate += OnUpdate;
        }

        private void OnDisable()
        {
            key.StopTracking();
            SimpleInput.OnUpdate -= OnUpdate;
        }

        private void OnUpdate()
        {
            key.value = Input.GetKey(realKey);
        }
    }
}
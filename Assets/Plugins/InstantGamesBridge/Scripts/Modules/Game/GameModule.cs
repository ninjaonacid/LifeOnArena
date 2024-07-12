#if UNITY_WEBGL
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

namespace InstantGamesBridge.Modules.Game
{
    public class GameModule : MonoBehaviour
    {
        public event Action<VisibilityState> visibilityStateChanged;

#if !UNITY_EDITOR
        public VisibilityState visibilityState 
        { 
            get
            {
                var state = InstantGamesBridgeGetVisibilityState();

                if (Enum.TryParse<VisibilityState>(state, true, out var value)) {
                    return value;
                }

                return VisibilityState.Visible;
            }
        }

        [DllImport("__Internal")]
        private static extern string InstantGamesBridgeGetVisibilityState();
#else
        public VisibilityState visibilityState { get; } = VisibilityState.Visible;
#endif

        // Called from JS
        private void OnVisibilityStateChanged(string value)
        {
            if (Enum.TryParse<VisibilityState>(value, true, out var state))
            {
                visibilityStateChanged?.Invoke(visibilityState);
            }
        }
    }
}
#endif
#if UNITY_WEBGL
#if !UNITY_EDITOR
using System;
using System.Runtime.InteropServices;
#endif

namespace InstantGamesBridge.Modules.Device
{
    public class DeviceModule
    {
#if !UNITY_EDITOR
        public DeviceType type 
        { 
            get
            {
                var stringType = InstantGamesBridgeGetDeviceType();

                if (Enum.TryParse<DeviceType>(stringType, true, out var value)) {
                    return value;
                }

                return DeviceType.Desktop;
            }
        }

        [DllImport("__Internal")]
        private static extern string InstantGamesBridgeGetDeviceType();
#else
        public DeviceType type { get; } = DeviceType.Desktop;
#endif
    }
}
#endif
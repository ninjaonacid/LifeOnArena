using InstantGamesBridge;
using InstantGamesBridge.Modules.Device;

namespace Code.Runtime.Modules.Utils
{
    public static class WebApplication
    {
        public static bool IsWebApp
        {
            get
            {
                #if UNITY_WEBGL
                return true;
                #endif
#pragma warning disable CS0162
                return false;
#pragma warning restore CS0162
            }
        }

        public static bool IsMobile => Bridge.device.type == DeviceType.Mobile;
        public static bool IsDesktop => Bridge.device.type == DeviceType.Desktop;

        public static bool IsTabletop => Bridge.device.type == DeviceType.Tablet;
    }
}

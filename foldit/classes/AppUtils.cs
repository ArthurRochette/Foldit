using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class AppUtils
{
    private unsafe struct WindowCompositionAttributeData
    {
        public WCA Attribute;
        public void* Data;
        public int DataLength;
    }

    private enum WCA
    {
        ACCENT_POLICY = 19
    }

    public enum ACCENT
    {
        DISABLED = 0,
        ENABLE_GRADIENT = 1,
        ENABLE_TRANSPARENTGRADIENT = 2,
        ENABLE_BLURBEHIND = 3,
        ENABLE_ACRYLICBLURBEHIND = 4,
        INVALID_STATE = 5
    }

    private struct AccentPolicy
    {
        public ACCENT AccentState;
        public uint AccentFlags;
        public uint GradientColor;
        public uint AnimationId;
    }
    [DllImport("user32.dll")]
    private static extern int SetWindowCompositionAttribute(HandleRef hWnd, in WindowCompositionAttributeData data);

    public static void EnableAcrylic(IWin32Window window, Color blurColor, ACCENT accent)
    {
        if (window is null)
            throw new ArgumentNullException(nameof(window));

        var accentPolicy = new AccentPolicy
        {
            AccentState = accent,
            GradientColor = ToAbgr(blurColor)
        };
        unsafe
        {
            SetWindowCompositionAttribute(new HandleRef(window, window.Handle), new WindowCompositionAttributeData
            {
                Attribute = WCA.ACCENT_POLICY,
                Data = &accentPolicy,
                DataLength = Marshal.SizeOf<AccentPolicy>()
            });
        }
    }

    private static uint ToAbgr(Color color)
    {
        return ((uint)color.A << 24)
            | ((uint)color.B << 16)
            | ((uint)color.G << 8)
            | color.R;
    }
}
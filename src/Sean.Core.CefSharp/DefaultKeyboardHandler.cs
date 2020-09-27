using System;
using System.Windows.Forms;
using CefSharp;

namespace Sean.Core.CefSharp
{
   public class DefaultKeyboardHandler: IKeyboardHandler
    {
        public bool OnPreKeyEvent(IWebBrowser chromiumWebBrowser, IBrowser browser, KeyType type, int windowsKeyCode,
            int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey, ref bool isKeyboardShortcut)
        {
            return false;
        }

        public bool OnKeyEvent(IWebBrowser chromiumWebBrowser, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode,
            CefEventFlags modifiers, bool isSystemKey)
        {
            if (type == KeyType.KeyUp && Enum.IsDefined(typeof(Keys), windowsKeyCode))
            {
                var key = (Keys)windowsKeyCode;
                switch (key)
                {
                    case Keys.F12:
                        browser.ShowDevTools();
                        break;
                    case Keys.F5:
                        if (modifiers == CefEventFlags.ControlDown)
                        {
                            //ctrl+f5
                            browser.Reload(true); //强制忽略缓存
                        }
                        else
                        {
                            //f5
                            browser.Reload();
                        }
                        break;
                }
            }
            return false;
        }
    }
}

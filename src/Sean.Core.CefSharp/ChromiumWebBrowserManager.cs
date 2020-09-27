using System;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace Sean.Core.CefSharp
{
    public class ChromiumWebBrowserManager
    {
        private const string DefaultHomepage = "about:blank";

        /// <summary>
        /// Initialize cef with the provided settings.
        /// </summary>
        /// <param name="settingsAction"></param>
        public static void InitializeCef(Action<CefSettings> settingsAction = null)
        {
            if (Cef.IsInitialized)
            {
                return;
            }

            var settings = new CefSettings();
            settingsAction?.Invoke(settings);
            Cef.Initialize(settings);
        }

        /// <summary>
        /// Initialize cef with the provided settings.
        /// </summary>
        /// <param name="performDependencyCheck"></param>
        /// <param name="browserProcessHandler"></param>
        /// <param name="settingsAction"></param>
        public static void InitializeCef(bool performDependencyCheck, IBrowserProcessHandler browserProcessHandler, Action<CefSettings> settingsAction = null)
        {
            if (Cef.IsInitialized)
            {
                return;
            }

            var settings = new CefSettings();
            settingsAction?.Invoke(settings);
            Cef.Initialize(settings, performDependencyCheck, browserProcessHandler);
        }

        /// <summary>
        /// Create a browser component, then add it to the control and fill it to the control.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="optionsAction"></param>
        /// <param name="browserConfig"></param>
        /// <returns></returns>
        public static ChromiumWebBrowser InitializeChromium(Control control, Action<ChromiumWebBrowserOptions> optionsAction = null, Action<ChromiumWebBrowser> browserConfig = null)
        {
            var options = new ChromiumWebBrowserOptions();
            optionsAction?.Invoke(options);

            // Create a browser component
            var chromeBrowser = new ChromiumWebBrowser(options.Url);
            chromeBrowser.DownloadHandler = options.DownloadHandler;// 下载
            chromeBrowser.MenuHandler = options.DisableContextMenu ? new DisableMenuHandler() : options.ContextMenuHandler;// 右键菜单
            chromeBrowser.KeyboardHandler = options.KeyboardHandler;// 键盘事件
            chromeBrowser.Dock = DockStyle.Fill;
            browserConfig?.Invoke(chromeBrowser);

            // Add it to the form and fill it to the form window.
            control.Controls.Add(chromeBrowser);

            return chromeBrowser;
        }

        /// <summary>
        /// Shut down cef.
        /// </summary>
        public static void ShutdownCef()
        {
            Cef.Shutdown();
        }

        /// <summary>
        /// Shut down cef.
        /// </summary>
        public static void ShutdownCefWithoutChecks()
        {
            Cef.ShutdownWithoutChecks();
        }
    }
}

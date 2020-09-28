using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Sean.Core.CefSharp;

namespace Demo.NetCore
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Add AnyCPU Support: https://github.com/cefsharp/CefSharp/issues/1714 £®Option 2£©
            // 1. Add <CefSharpAnyCpuSupport>true</CefSharpAnyCpuSupport> to the first <PropertyGroup> in your project (e.g. .csproj file).
            // 2. Implement the code below (modifying any setting that you require).

            AppDomain.CurrentDomain.AssemblyResolve += Resolver;

            LoadApp();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void LoadApp()
        {
            // Make sure you set performDependencyCheck false
            if (!ChromiumWebBrowserManager.InitializeCef(false, null, settings =>
            {
                // Set BrowserSubProcessPath based on app bitness at runtime
                settings.BrowserSubprocessPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                    Environment.Is64BitProcess ? "x64" : "x86",
                    "CefSharp.BrowserSubprocess.exe");
            }))
            {
                MessageBox.Show("CEF≥ı ºªØ ß∞‹£°");
                return;
            }

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        // Will attempt to load missing assembly from either x86 or x64 subdir
        private static Assembly Resolver(object sender, ResolveEventArgs args)
        {
            if (args.Name.StartsWith("CefSharp"))
            {
                string assemblyName = args.Name.Split(new[] { ',' }, 2)[0] + ".dll";
                string archSpecificPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                    Environment.Is64BitProcess ? "x64" : "x86",
                    assemblyName);

                return File.Exists(archSpecificPath)
                    ? Assembly.LoadFile(archSpecificPath)
                    : null;
            }

            return null;
        }
    }
}

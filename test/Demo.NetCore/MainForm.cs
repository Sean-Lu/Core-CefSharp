using System;
using System.IO;
using System.Windows.Forms;
using CefSharp;
using CefSharp.Internals;
using CefSharp.WinForms;
using Sean.Core.CefSharp;

namespace Demo.NetCore
{
    public partial class MainForm : Form
    {
        private readonly ChromiumWebBrowser _webBrowser;

        public MainForm()
        {
            InitializeComponent();

            this.FormClosing += MainForm_FormClosing;

            _webBrowser = ChromiumWebBrowserManager.InitializeChromium(panel1, options =>
            {
                options.Url = $"file:///{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test.html") }";
                //options.Url = "http://sean.tools.com/";
                //options.DisableContextMenu = true;
                options.DownloadHandler = new DefaultDownLoadHandler
                {
                    OnDownloadComplete = (chromiumWebBrowser, downloadItem) =>
                    {
                        // 跨线程操作
                        Invoke(new Action(() =>
                        {
                            MessageBox.Show("下载完成！", "提示");
                        }));
                    }
                };
            }, browser =>
            {
                #region How do you expose a .NET class to JavaScript?
                // https://github.com/cefsharp/CefSharp/issues/2990
                // https://github.com/cefsharp/CefSharp/wiki/General-Usage#3-how-do-you-expose-a-net-class-to-javascript

                // 只需要全局设置1次即可，建议放到初始化CEF的时候调用
                CefSharpSettings.LegacyJavascriptBindingEnabled = true;
                //CefSharpSettings.WcfEnabled = true;// 当isAsync=false的时候才需要设置

                //browser.RegisterJsObject(nameof(JsEvent), new JsEvent(), new BindingOptions { CamelCaseJavascriptNames = false });// 这是cefsharp老版本支持的写法，最新版本已不支持（运行时会抛出异常）

                browser.JavascriptObjectRepository.Register(nameof(JsEvent), new JsEvent(), isAsync: true, options: new BindingOptions { CamelCaseJavascriptNames = false });
                #endregion
            });
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ChromiumWebBrowserManager.ShutdownCef();
        }

        private void btnGoto_Click(object sender, System.EventArgs e)
        {
            _webBrowser.Load(textBox1.Text);
        }

        private void btnTest_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show($"当前链接：{_webBrowser.Address}");
        }

        /// <summary>
        /// CefSharp调用js
        /// </summary>
        private void InvokeJsTest()
        {
            // 方式1: ExecuteScriptAsync
            _webBrowser.ExecuteScriptAsync("xxx");

            // 方式2: EvaluateScriptAsync
            var jsResult = _webBrowser.EvaluateScriptAsync("xxx").Result?.Result;
            if (jsResult != null)
            {
                MessageBox.Show(jsResult.ToString());
            }
        }
    }
}

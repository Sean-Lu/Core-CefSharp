using System;
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
                options.Url = "http://sean.tools.com/";
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
                #region 允许js调用CefSharp注册的对象
                //browser.RegisterJsObject(nameof(JsEvent), new JsEvent(), new BindingOptions { CamelCaseJavascriptNames = false });// 这是.NET 4.0支持的写法（从cefsharp79版本开始，旧的RegisterJsObject方法被删除了。）

                // https://github.com/cefsharp/CefSharp/issues/2990
                // https://github.com/cefsharp/CefSharp/wiki/General-Usage#3-how-do-you-expose-a-net-class-to-javascript
                // How do you expose a .NET class to JavaScript?
                CefSharpSettings.LegacyJavascriptBindingEnabled = true;
                //CefSharpSettings.WcfEnabled = true;// 当isAsync= false的时候需要设置
                browser.JavascriptObjectRepository.Register(nameof(JsEvent), new JsEvent(), isAsync: true, options: new BindingOptions { CamelCaseJavascriptNames = false });

                // js code:
                // JsEvent.ShowMsg();
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

using System.Windows.Forms;
using Sean.Core.CefSharp;

namespace Demo.NetCore
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.FormClosing += MainForm_FormClosing;

            DefaultDownLoadHandler.OnDownloadComplete += (chromiumWebBrowser, downloadItem) =>
            {
                MessageBox.Show("下载完成！", "提示");
            };

            ChromiumWebBrowserManager.InitializeChromium(this, options =>
            {
                options.Url = "http://sean.tools.com/";
                //options.DisableContextMenu = true;
            });
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ChromiumWebBrowserManager.ShutdownCef();
        }
    }
}

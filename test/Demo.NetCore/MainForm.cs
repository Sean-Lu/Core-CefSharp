using System.Windows.Forms;
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
                        MessageBox.Show("下载完成！", "提示");
                    }
                };
            });
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ChromiumWebBrowserManager.ShutdownCef();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show(_webBrowser.Address);
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            _webBrowser.Load(textBox1.Text);
        }
    }
}

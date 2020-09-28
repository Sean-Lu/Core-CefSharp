using System.Windows.Forms;

namespace Demo.NetCore
{
    public class JsEvent
    {
        public string Message { get; set; }

        public void ShowMsg()
        {
            MessageBox.Show($"This result is from C#：\r\n{Message}");
        }
    }
}

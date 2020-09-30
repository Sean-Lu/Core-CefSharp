using System.Windows.Forms;

namespace Demo.NetCore
{
    public class JsEvent
    {
        public string Tips { get; set; } = "提示";

        public void ShowMsg(string msg)
        {
            MessageBox.Show($"This result is from C#：\r\n{msg}", $"【{Tips}】");
        }
    }
}

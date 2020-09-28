using CefSharp;

namespace Sean.Core.CefSharp
{
    public class ChromiumWebBrowserOptions
    {
        /// <summary>
        /// 默认地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 是否屏蔽右键菜单
        /// </summary>
        public bool DisableContextMenu { get; set; }

        /// <summary>
        /// 下载事件
        /// </summary>
        public IDownloadHandler DownloadHandler { get; set; } = new DefaultDownLoadHandler();
        /// <summary>
        /// 右键菜单事件
        /// </summary>
        public IContextMenuHandler ContextMenuHandler { get; set; } = new DefaultMenuHandler();
        /// <summary>
        /// 键盘事件
        /// </summary>
        public IKeyboardHandler KeyboardHandler { get; set; } = new DefaultKeyboardHandler();
    }
}

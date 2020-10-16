using System;
using CefSharp;

namespace Sean.Core.CefSharp
{
    /// <summary>
    /// 下载文件
    /// </summary>
    public class DefaultDownLoadHandler : IDownloadHandler
    {
#if !NET40
        //public event EventHandler<DownloadItem> OnDownloadComplete;
        public Action<IWebBrowser, DownloadItem> OnDownloadComplete;
#else
        public Action<IBrowser, DownloadItem> OnDownloadComplete;
#endif

#if !NET40
        public void OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            if (!callback.IsDisposed)
            {
                using (callback)
                {
                    callback.Continue(downloadItem.SuggestedFileName, showDialog: true);
                }
            }
        }

        public void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            if (downloadItem.IsComplete)
            {
                OnDownloadComplete?.Invoke(chromiumWebBrowser, downloadItem);
            }
        }
#else
        public void OnBeforeDownload(IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            if (!callback.IsDisposed)
            {
                using (callback)
                {
                    callback.Continue(downloadItem.SuggestedFileName, showDialog: true);
                }
            }
        }

        public void OnDownloadUpdated(IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            if (downloadItem.IsComplete)
            {
                OnDownloadComplete?.Invoke(browser, downloadItem);
            }
        }
#endif
    }
}

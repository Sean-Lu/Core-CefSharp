## 简介

> 支持`Chromium`内核的浏览器控件，基于[CefSharp.WinForms](https://github.com/cefsharp/cefsharp)封装

- 支持`.Net Framework`、`.Net Core App`
- 默认支持下载文件：`DefaultDownLoadHandler` 【下载成功的逻辑可在OnDownloadComplete委托中处理，注意跨线程操作，详见demo】
- 默认支持右键菜单事件：`DefaultMenuHandler` 【增加打开\关闭开发调试工具菜单】
- 默认支持键盘事件：`DefaultKeyboardHandler` 【F5刷新、Ctrl+F5强制刷新、F12打开开发调试工具】
- 如果要屏蔽右键菜单，使用`DisableMenuHandler`即可
- 支持与js交互，具体用法见demo（不同版本的`CefSharp.WinForms`写法会不一样）

## Packages

| Package | NuGet Stable | NuGet Pre-release | Downloads |
| ------- | ------------ | ----------------- | --------- |
| [Sean.Core.CefSharp](https://www.nuget.org/packages/Sean.Core.CefSharp/) | [![Sean.Core.CefSharp](https://img.shields.io/nuget/v/Sean.Core.CefSharp.svg)](https://www.nuget.org/packages/Sean.Core.CefSharp/) | [![Sean.Core.CefSharp](https://img.shields.io/nuget/vpre/Sean.Core.CefSharp.svg)](https://www.nuget.org/packages/Sean.Core.CefSharp/) | [![Sean.Core.CefSharp](https://img.shields.io/nuget/dt/Sean.Core.CefSharp.svg)](https://www.nuget.org/packages/Sean.Core.CefSharp/) |

## Nuget包引用

> **Id：Sean.Core.CefSharp**

- Package Manager

```
PM> Install-Package Sean.Core.CefSharp
```

## 使用示例

> 项目：test\Demo.NetCore

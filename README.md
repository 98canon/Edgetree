# Edgetree 中文社区版 v2.7.0

> **非官方简体中文社区分支，由 [98canon](https://github.com/98canon) 维护。**
> 本版本基于上游 [Edgetree v2.6.0](https://github.com/legendsteel11/Edgetree)，不是原作者发布的官方版本。
>
> **本分支的主要改动：**
> - 新增简体中文界面和帮助内容；
> - 新增 Markdown 和代码文件预览、编辑、自动保存、标签页和独立窗口；
> - 新增可自定义快捷键；
> - 新增 Windows 工作区预留（AppBar），让最大化窗口避开侧边栏；
> - 改进多实例、多显示器、文件类型筛选和自动停靠；
> - 保留上游 v2.6.0 的图片 1:1 像素对齐和剪贴板稳定性修复。
>
> 原项目、原作者和第三方组件的版权信息均保留。详细署名见 [CONTRIBUTORS.md](CONTRIBUTORS.md)，许可证见 [LICENSE.md](LICENSE.md) 和 [THIRD-PARTY-NOTICES.md](THIRD-PARTY-NOTICES.md)。

[上游项目](https://github.com/legendsteel11/Edgetree) · [问题反馈](https://github.com/98canon/Edgetree/issues) · [下载最新版本](https://github.com/98canon/Edgetree/releases/latest)

Edgetree 是一个轻量的 Windows 文件浏览工具，可以像 VS Code 的 Explorer 一样停靠在屏幕左侧或右侧。它不是要替代 Windows 资源管理器，而是让你随时查看文件夹结构，并快速跳转到文件。

## 下载

每个 Release 通常包含以下版本：

- **安装版**：Edgetree-<版本>-win-x64-setup.exe，通过安装向导安装到系统，可从开始菜单启动并正常卸载；
- **独立便携版**：Edgetree-<版本>-win-x64-standalone.exe，单个文件即可运行，不需要安装 .NET；
- **轻量版**：Edgetree-<版本>-win-x64.exe，需要电脑已经安装 [.NET 8 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/8.0)。

配置、书签和颜色保存在 %AppData%/Edgetree。三个版本共用这些设置，切换版本不会丢失配置。卸载程序不会删除这些设置。

## 功能

### 停靠和自动隐藏

- 停靠在屏幕左边或右边，并覆盖当前工作区的高度；
- 点击图钉进入自动隐藏，鼠标移动到屏幕边缘时重新展开；
- 可以使用短把手代替整条边缘，也可以单独设置把手颜色；
- 将标题栏拖离边缘即可变为普通浮动窗口；
- 开启“自动停靠到屏幕边缘”后，把浮动窗口拖到屏幕边缘并短暂停留即可重新停靠；
- 开启“最大化时让出位置”后，其他窗口最大化时会避开侧边栏；
- 支持多显示器、不同 DPI 和任务栏自动隐藏场景。

### 文件树

- 从“此电脑”开始显示所有磁盘；
- 文件夹按需加载，重新启动后可以恢复上次展开的位置；
- 单击文件夹展开，双击文件打开；
- 大文件夹采用分批显示，避免一次渲染大量行；
- 支持按名称、日期、类型和大小排序；
- 支持 Ctrl+单击、Shift+单击多选，并可批量复制、删除和拖出；
- 支持 F2 原地重命名、F5 刷新、Delete 删除等常用操作；
- 支持网络路径，例如 \\server\share；
- 支持书签、隐藏文件夹、实时文件变化和搜索。

### 文件类型筛选

底部筛选条可以按以下类型显示文件：

- 代码
- 图片
- 文档
- 音视频
- 压缩包
- 可执行文件
- 其他
- 自定义扩展名

也可以维护排除扩展名列表。筛选只影响文件树显示，不会删除文件；搜索仍然可以查找被筛选掉的内容。

### 多媒体面板

- 图片：缩放、平移、适应窗口、1:1、填充、幻灯片和设置壁纸；
- 音乐：播放、暂停、音量记忆、专辑封面和后台播放；
- 视频：HDR 修正、字幕、字幕大小/位置/同步、全屏和播放进度记忆；
- 选项可以设置双击文件时在面板中打开，也可以设置选择文件时自动展开面板；
- 上游 v2.6.0 的图片 1:1 显示会对齐到屏幕整数像素，减少边缘发虚。

### 文档预览和编辑

本分支新增文档工具：

- Markdown 文件默认以渲染后的预览打开；
- Markdown 可以切换到编辑模式；
- 代码文件使用带语法高亮的编辑器打开；
- 编辑内容会自动保存到原文件；
- 单个文件超过 2 MB 时不会在内置编辑器中打开，会提示使用系统默认程序；
- 支持在预览面板中弹出独立窗口；
- 独立窗口支持多个标签页，也可以打开图片；
- 文件被其他程序修改时，预览会在没有未保存编辑内容的情况下更新。

> 编辑器会直接写回原文件。修改重要文件前，请先确认文件路径并做好备份。

### 快捷键

可以在选项菜单中打开“快捷键”，为部分功能设置、清除或恢复快捷键。默认快捷键包括：

- Ctrl+F：搜索；
- F1：帮助；
- Ctrl+Shift+S：保存当前预设；
- Ctrl+1 到 Ctrl+5：切换预设；
- F8：幻灯片；
- F9：时钟；
- 预览面板打开时，可用 Backspace 关闭面板；
- 编辑器获得焦点后，Backspace、Ctrl+方向键等会保留给编辑器。

F2、Delete、Ctrl+C 等资源管理器常用按键不会被快捷键设置覆盖。

### 设置和外观

- 韩文、英文和简体中文界面；
- 深色、浅色和自定义颜色；
- 字体大小、缩进、行距、滚动条宽度和缩略图大小；
- 书签、预设、搜索历史和设置导入/导出；
- 开机启动、置顶、最小化到托盘、更新提示；
- 文件夹图标、文件图标、磁盘图标和 Material 图标样式；
- 多实例独立窗口状态，以及每个实例的显示器和位置记忆。

## 系统要求

- Windows 10 或 Windows 11；
- 从源码运行需要 [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)；
- Release 中的独立版不需要目标电脑安装 .NET 运行时。

## 从源码运行

    git clone https://github.com/98canon/Edgetree.git
    cd Edgetree
    dotnet run --project src/Edgetree

构建整个解决方案：

    dotnet build Edgetree.sln

## 发布构建

轻量单文件版本：

    dotnet publish src/Edgetree -c Release -r win-x64 --self-contained false -p:PublishSingleFile=true

独立单文件版本：

    dotnet publish src/Edgetree -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true

安装版使用不合并为单文件的独立发布目录：

    dotnet publish src/Edgetree -c Release -r win-x64 --self-contained true -o publish/folder

然后用 Inno Setup 6 编译：

    ISCC.exe installer/Edgetree.iss

安装器脚本会从待打包的 EXE 读取版本号，避免安装器版本和程序版本不一致。安装器会同时携带 LICENSE.md、THIRD-PARTY-NOTICES.md 和 CONTRIBUTORS.md。

## v2.7.0 变更记录（2026-09-10）

- 新增简体中文界面和帮助内容；
- 新增 Markdown、代码文件预览和编辑；
- 新增编辑器自动保存、标签页和独立文档窗口；
- 新增可配置快捷键；
- 新增 Windows 工作区预留和 AppBar 集成；
- 改进多实例、多显示器、文件筛选和自动停靠；
- 完善应用内上游署名、贡献者说明和第三方许可证声明；
- 更新下载、更新检查和落地页链接，使其指向本 Fork；
- 修复快捷键中 Alt/Windows 组合键以及加号键的保存兼容性；
- 修复独立文档窗口切换图片时平移状态残留的问题。

## 上游 v2.6.0 变更

- 图片以 1:1 显示时对齐到整数屏幕像素，减少半像素造成的模糊；
- 复制路径时，如果其他程序暂时占用剪贴板，Edgetree 不会因此退出。

## 反馈和问题

- 本 Fork 的问题反馈：[98canon/Edgetree Issues](https://github.com/98canon/Edgetree/issues)；
- 上游项目：[legendsteel11/Edgetree](https://github.com/legendsteel11/Edgetree)；
- 上游作者邮箱：pjh85336@gmail.com。

请在提交问题时说明 Windows 版本、程序版本、是否使用独立版，以及能够重现问题的步骤。

## 许可证和署名

### 上游项目

- 项目：[legendsteel11/Edgetree](https://github.com/legendsteel11/Edgetree)；
- 原作者/版权持有人：pjh85336@gmail.com；
- 原项目许可证：MIT License，正文见 [LICENSE.md](LICENSE.md)。

### 本 Fork

- 维护者和贡献者：[98canon](https://github.com/98canon)；
- 贡献内容：简体中文本地化、帮助内容、文档预览/编辑、快捷键、工作区预留，以及多实例、多显示器、文件筛选和自动停靠相关改进；
- 本 Fork 是非官方社区分支，与上游作者没有隶属或背书关系；
- 本 Fork 保留原项目和第三方组件的版权与许可证信息。

第三方组件和资源的说明见 [THIRD-PARTY-NOTICES.md](THIRD-PARTY-NOTICES.md)，包括 Material Icon Theme、Google Material Symbols、MdXaml、MdXaml.Plugins、AvalonEdit 和 .NET 8。

## 相关文件

- [贡献者与署名](CONTRIBUTORS.md)
- [MIT License](LICENSE.md)
- [第三方许可证](THIRD-PARTY-NOTICES.md)
- [韩文说明](README-ko.md)

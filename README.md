# Edgetree 中文社区版 v2.7.0

> **非官方简体中文社区 Fork，由 [98canon](https://github.com/98canon) 维护。**
> 本版本基于上游 [Edgetree v2.6.0](https://github.com/legendsteel11/Edgetree)，不是原作者发布的官方版本。
>
> **重要授权说明：本 Fork 中由 `98canon` 新增的代码、文档、界面功能和修复仅限非商业使用。允许学习、修改和 Fork，但再发布时必须保留 `98canon` 署名、本 Fork 说明以及 [LICENSE-98CANON.md](LICENSE-98CANON.md)。**
> 上游代码和第三方组件不受上述新增内容限制，仍按各自原许可证授权。详细署名见 [CONTRIBUTORS.md](CONTRIBUTORS.md)，许可证见 [LICENSE.md](LICENSE.md)、[LICENSE-98CANON.md](LICENSE-98CANON.md) 和 [THIRD-PARTY-NOTICES.md](THIRD-PARTY-NOTICES.md)。
>
> **本分支的主要改动：**
> - **新增项目管理和项目树切换：只显示用户添加的项目目录树，可通过顶部项目图标切换。**
> - **新增 Markdown、代码、JSON、文本和配置文件的侧边预览与编辑。**
> - **新增文档、图片和媒体侧边查看，以及无边框全屏查看。**
> - **双击可预览文档只在侧边栏打开，不自动启动 Cursor 等外部编辑器。**
> - **Markdown 长代码块自动换行，避免预览内容横向溢出。**
> - **新增文档标签页、自动保存和文件变化更新。**
> - **新增简体中文界面和帮助内容。**
> - **新增可自定义快捷键。**
> - **新增 Windows 工作区预留（AppBar），让最大化窗口避开侧边栏。**
> - 改进多实例、多显示器、文件类型筛选和自动停靠；
> - 保留上游 v2.6.0 的图片 1:1 像素对齐和剪贴板稳定性修复。

[上游项目](https://github.com/legendsteel11/Edgetree) · [问题反馈](https://github.com/98canon/Edgetree/issues) · [下载最新版本](https://github.com/98canon/Edgetree/releases/latest)

Edgetree 是一个轻量的 Windows 文件浏览工具，可以像 VS Code 的 Explorer 一样停靠在屏幕左侧或右侧。它不是要替代 Windows 资源管理器，而是让你随时查看文件夹结构，并快速跳转到文件。

## 工具定位

> **本工具是为不支持在侧边栏直接编辑或预览本地文件的 Agent 工具开发的辅助小工具，例如 Codex、Grok Build 等。**
> 它提供项目目录管理、文件树浏览、文档侧边预览和编辑能力，方便 Agent 工作流中查看和修改本地项目文件。
>
> **这里仅说明工具的使用场景，不代表本项目与 Codex、Grok Build 或其他 Agent 工具存在官方合作、隶属或背书关系。**

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

**项目管理和项目树切换：可添加多个项目目录，通过顶部项目图标在当前项目目录树和全部文件树之间切换；项目模式下只显示已添加项目的目录树。**

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

- **Markdown 文件默认以渲染后的预览打开，并可切换到编辑模式；**
- **代码、JSON、文本和配置文件支持在侧边栏中预览或编辑；**
- **Markdown 长代码块自动换行，避免长 JSON、长 URL 和连续英文内容横向溢出；**
- **双击可预览文档只在侧边栏打开，不自动弹出 Cursor 等外部编辑器；**
- **支持文档、图片和媒体在侧边查看，并可使用无边框全屏查看窗口；**
- **文档查看窗口支持多个标签页，也可以打开图片；**
- **编辑内容会自动保存到原文件；**
- **文件被其他程序修改时，预览会在没有未保存编辑内容的情况下更新；**
- 单个文件超过 2 MB 时不会在内置编辑器中打开，会提示使用系统默认程序。

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

安装器脚本会从待打包的 EXE 读取版本号，避免安装器版本和程序版本不一致。安装器会同时携带 LICENSE.md、LICENSE-98CANON.md、THIRD-PARTY-NOTICES.md 和 CONTRIBUTORS.md。

## v2.7.0 变更记录（2026-09-10）

### 2026-09-11 补充说明

- **新增 `LICENSE-98CANON.md`，明确 `98canon` 新增内容仅限非商业使用。**
- **允许对 98canon 新增内容进行学习、修改和 Fork，但必须保留 `98canon` 署名、本 Fork 说明和独立许可证文件。**
- **增加面向 Codex、Grok Build 等不支持侧边编辑文件的 Agent 工具的辅助工具定位说明。**
- **明确上游 Edgetree 代码和第三方组件继续按各自原许可证执行。**

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

- 本 Fork 新增内容使用 [LICENSE-98CANON.md](LICENSE-98CANON.md)；
- **`98canon` 新增内容仅限非商业使用，允许学习、修改和 Fork；**
- **Fork 和再发布时必须保留 `98canon` 署名、本 Fork 说明以及 `LICENSE-98CANON.md`；**
- 上游 Edgetree 代码继续使用 MIT License；
- 第三方组件继续使用各自原许可证；
- 维护者和贡献者：[98canon](https://github.com/98canon)；
- 贡献内容：简体中文本地化、帮助内容、文档预览/编辑、快捷键、工作区预留，以及多实例、多显示器、文件筛选和自动停靠相关改进；
- 本 Fork 是非官方社区分支，与上游作者没有隶属或背书关系；
- 本 Fork 保留原项目和第三方组件的版权与许可证信息。

第三方组件和资源的说明见 [THIRD-PARTY-NOTICES.md](THIRD-PARTY-NOTICES.md)，包括 Material Icon Theme、Google Material Symbols、MdXaml、MdXaml.Plugins、AvalonEdit 和 .NET 8。

## 相关文件

- [贡献者与署名](CONTRIBUTORS.md)
- [MIT License](LICENSE.md)
- [98CANON 新增内容非商业许可证](LICENSE-98CANON.md)
- [第三方许可证](THIRD-PARTY-NOTICES.md)
- [韩文说明](README-ko.md)

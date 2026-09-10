namespace SidebarExplorer.App.Services;

// Language switch is restart-only (not live): fields default to Korean and
// Initialize(), called once in App.OnStartup before base.OnStartup(e) builds
// the StartupUri window, overwrites them to English if that's the saved
// choice - so every x:Static reference throughout the XAML resolves to the
// right language the first (and only) time it's evaluated, and every plain
// code-behind reference just reads a field like any other string constant.
public static class Strings
{
    // Context menu (file/folder rows)
    //
    // MenuAddFavorite("즐겨찾기에 추가") was here until 2026-08-17. 즐겨찾기와
    // 북마크는 같은 일을 하는 두 목록이었고, 즐겨찾기 쪽에만 있던 것은 순서를
    // 끌어서 정하는 것 하나뿐이라 그것을 북마크 패널로 옮기고 목록을 합쳤다.
    public static string MenuBookmark = "북마크";
    public static string MenuBookmarkAdd = "북마크 추가";
    public static string MenuBookmarkRemove = "북마크 해제";
    public static string MenuBookmarkList = "북마크 목록";
    public static string MenuBookmarkListEmpty = "북마크 없음";
    public static string MenuBookmarkClearAll = "전체 해제";
    // File-kind filter. "전체" is deliberately worded as a state, not a
    // category - picking it clears the rest.
    // "파일 형식"만으로 시작했다가 바꿈 - 그것만으로는 무엇을 하는 줄인지 안
    // 읽힘. 라벨이 그 자리에서 동작을 말해야 함(설명은 아무도 안 읽으므로).
    //
    // 종류였다가 형식으로 바꿈(2026-08-18, 사용자 판단 "종류에는 확장자들을 다
    // 포함하니까요"). 릴리즈 노트를 직접 고치실 때 쓰신 말이기도 하다. 영문은
    // File Types 그대로 - 갈렸던 것은 국어 쪽 두 낱말이고, 영어에는 그 구분이
    // 없다. 아이콘 쪽 "아이콘 종류"는 남는데, 이제 종류를 쓰는 자리가 거기뿐
    // 이라 오히려 안 갈린다.
    // ----- 프리셋 ------------------------------------------------------------
    //
    // The saved shapes of the whole app, at the TOP of the header's right-click
    // menu - no 프리셋 parent row, because the names are the rows.
    //
    // "현재 상태를 프리셋으로 지정" was the first wording and it was explaining
    // rather than labelling: anyone in this menu, looking at a list of their own
    // saved presets, already knows what adding one takes a snapshot of.
    public static string MenuPresetAdd = "프리셋 추가";
    // First item inside a preset's own submenu, and the only one of the four
    // that is not about editing the preset.
    public static string MenuPresetApply = "적용";
    // THE PRIMARY ONE, and it does both: the name box comes up with the slot's
    // current name in it, and pressing 확인 stores the app's present shape under
    // whatever name is left in the box. Renaming and overwriting used to be two
    // separate items, and doing only the first left a preset renamed but still
    // holding the old shape - a half-finished update with nothing to show it
    // (2026-08-11).
    public static string MenuPresetOverwrite = "덮어쓰기…";
    // Kept for the case the item above cannot serve: fixing a name WITHOUT
    // giving up the shape already stored. It sat directly under 덮어쓰기 as
    // "이름만 바꾸기" for a while - the qualifier stopped earning its place once
    // the two were one under the other in a four-item menu, where what each
    // does is read off the pair.
    public static string MenuPresetRename = "이름 바꾸기…";
    public static string MenuPresetDelete = "삭제";
    public static string PresetNameTitle = "프리셋 이름";
    // The name box is the same box for all three, so its TITLE is the only
    // thing saying which one is happening.
    public static string PresetSaveTitle = "현재 상태로 저장";
    public static string PresetRenameTitle = "이름 바꾸기";
    public static string PresetNameHint = "창 위치·크기·도킹·색상·파일 형식·현재 폴더가 함께 저장됩니다";
    // The default name for slot N. Deliberately dull - the user renames it to
    // what the shape is for, and a clever default would get in the way of that.
    public static string PresetDefaultName = "프리셋{0}";
    // Ctrl+Shift+S가 조용히 끝난 뒤 트레이가 말하는 것. 제목이 사실이고 본문은
    // 어느 프리셋이었는지다 - 그 키는 "지금 들어 있는" 칸에 쓰므로 어느 칸이었나가
    // 실제로 궁금한 것이다.
    public static string PresetSavedToast = "프리셋을 저장했습니다";
    // Ctrl+Shift+1~5가 아직 없는 칸에 닿았을 때. 번호를 되읽어 주는 것이 이
    // 물음의 반이다 - 3을 눌렀는데 4번이 만들어지면 그건 다른 일이 일어난 것이고,
    // 슬롯은 목록 순서대로 채워지므로 그럴 수 있다.
    // 즐겨찾기·북마크가 가리키는 폴더가 사라졌을 때. 설정을 다른 PC로 옮기면
    // 드라이브 문자부터 안 맞는 경우가 많고, 지금까지는 눌러도 아무 일이 없어서
    // 고장으로 읽혔다.
    //
    // 지우는 것을 묻되 기본이 아니다 - 잠깐 빠진 외장 드라이브일 수도 있고, 그
    // 경우 목록에서 지우는 것은 되돌릴 수 없다. 경로를 그대로 보여 주는 것이
    // 어느 쪽인지 판단할 유일한 재료다.
    public static string PlaceMissingTitle = "항목 없음";
    public static string PlaceMissingBody = "이 항목을 찾을 수 없습니다.\n\n{0}\n\n목록에서 삭제할까요?";
    public static string PresetSlotEmptyTitle = "프리셋";
    public static string PresetSlotEmptyBody = "{0}번 프리셋이 아직 없습니다.\n지금 상태를 저장할까요?";

    public static string MenuFileFilter = "표시할 파일 형식";
    // 푸터 칩은 여덟 개가 한 줄에 서야 해서 가장 긴 라벨만 줄여 씀. 나머지는
    // 메뉴와 같은 말을 그대로 쓴다 - 두 곳이 다른 이름을 부르면 같은 것인지
    // 알 수 없으므로.
    public static string FilterChipExecutable = "실행";
    public static string MenuFileFilterAll = "전체";
    public static string MenuFileFilterCode = "코드";
    public static string MenuFileFilterImage = "이미지";
    public static string MenuFileFilterDocument = "문서";
    public static string MenuFileFilterMedia = "미디어";
    public static string MenuFileFilterArchive = "압축";
    public static string MenuFileFilterExecutable = "실행 파일 · 바로가기";
    public static string MenuFileFilterOther = "기타";
    // Opens the input. The ellipsis is the whole promise that this row asks
    // something rather than switching something, which is what every other row
    // in this list does.
    public static string MenuFileFilterCustomEdit = "사용자 지정…";
    public static string FilterCustomTitle = "사용자 지정 확장자";
    // At the point of use, because nobody reads help - it has to say both the
    // separator and that a dot is optional, in one line.
    public static string FilterCustomHint = "쉼표로 구분 · txt, png, .mp3";
    // Under the box, only while it holds nothing: an empty box is the way to
    // remove the filter, and that is not guessable.
    public static string FilterCustomEmptyHint = "비워 두고 확인하면 사용자 지정이 없어집니다";
    // The other direction. Same ellipsis promise as 사용자 지정… above - both
    // rows ask, the rest switch.
    public static string MenuFileFilterExcludeEdit = "제외…";
    public static string FilterExcludeTitle = "제외할 확장자";
    // Says what makes this one different in the one place it will be read: it
    // does not join the other kinds, it overrules them.
    public static string FilterExcludeHint = "쉼표로 구분 · 여기 적은 것은 항상 숨깁니다";
    public static string FilterExcludeEmptyHint = "비워 두고 확인하면 제외가 없어집니다";
    public static string ButtonOk = "확인";
    public static string ButtonCancel = "취소";

    public static string MenuFontWeight = "글꼴 굵기";
    public static string MenuFontWeightNormal = "보통";
    public static string MenuFontWeightBold = "굵게";
    public static string MenuFontWeightFoldersOnly = "폴더만 굵게";
    public static string MenuFontWeightFilesOnly = "파일만 굵게";

    // 트리 옆(위 또는 아래)의 목록. 이름에 북마크를 넣은 것은 이 앱에 패널이 셋이라
    // 맨 "패널"이 어느 것인지 안 갈리기 때문이다 - 나머지 둘은 이미 멀티미디어
    // 패널·컨트롤 패널로 수식돼 있고 이것만 맨몸이었다(2026-08-17).
    //
    // 세 모드(즐겨찾기·북마크·표시 안 함)가 있던 자리다. 목록이 하나가 되면서
    // 남은 물음은 "보이느냐"뿐이라 체크 한 줄로 줄었다. 안의 두 줄이 주어를 다시
    // 말하지 않는 것은 멀티미디어 패널 서브메뉴와 같은 이유다.
    public static string MenuSidePanel = "북마크 패널";
    public static string MenuSidePanelShow = "표시";

    // A BARE VERB, because every attempt to name the subject was wrong for some
    // of what this row acts on (2026-08-25, on report). It began as "이 폴더
    // 숨기기" to say the row under the cursor was what went; then drives were
    // allowed to hide the same way, then several rows at once, and a second
    // string ("선택한 폴더 숨기기") was added to cover the plural. Three
    // subjects, two labels, and the one on screen was still wrong often enough
    // to read as a mistake. The row is only ever enabled over something it can
    // hide, and 잘라내기/삭제 beside it name no subject either, so the verb
    // alone is the one wording that is never wrong.
    public static string MenuHideFolder = "숨기기";
    public static string MenuHiddenFolderList = "숨긴 폴더";
    public static string MenuHiddenFolderListEmpty = "숨긴 폴더 없음";
    public static string MenuUnhideFolder = "숨김 해제";

    // 네트워크 위치. Named for the PLACE rather than for "드라이브": the whole
    // point is that it does not need a drive letter, so calling it one would
    // describe the thing it exists to avoid.
    public static string MenuNetworkLocations = "네트워크 위치";
    public static string MenuNetworkLocationAdd = "위치 추가…";
    public static string MenuNetworkLocationsEmpty = "추가된 위치 없음";
    public static string MenuNetworkLocationRemove = "목록에서 제거";
    public static string NetworkLocationPromptTitle = "네트워크 위치 추가";
    // Says the shape of the answer, because that is the one thing someone
    // typing here can get wrong. The mapped case is named too - it is the
    // question this box will otherwise be asked.
    public static string NetworkLocationPromptHint =
        "\\\\서버\\공유 형식이나 폴더 경로를 입력합니다. 드라이브 문자로 연결한 것은 이미 목록에 있습니다.";
    public static string NetworkLocationUnreachableTitle = "연결할 수 없음";
    public static string NetworkLocationUnreachableBody =
        "{0}\n\n응답이 없습니다. 그래도 목록에 추가할까요?";
    public static string NetworkLocationDuplicateTitle = "이미 있는 위치";
    // The button on every row of the bookmark / hidden-folder lists. One word
    // for both because the row it sits on already says what is being released.
    public static string MenuListRowRemove = "해제";
    public static string HiddenClearAllConfirmTitle = "숨긴 폴더 전체 해제";
    public static string HiddenClearAllConfirmBody = "숨긴 폴더 {0}개를 모두 다시 표시하겠습니까?";
    public static string BookmarkClearAllConfirmTitle = "북마크 전체 해제";
    public static string BookmarkClearAllConfirmBody = "북마크 {0}개를 모두 해제하겠습니까?";
    public static string BookmarkShortcutNext = "다음 북마크";
    public static string BookmarkShortcutPrev = "이전 북마크";
    public static string MenuNewFolder = "새 폴더";
    public static string MenuRefresh = "새로고침";
    // THE OPEN GROUP NAMES ITS DESTINATIONS. 열기 on its own said nothing about
    // where, which only became a question once the app grew a viewer of its own
    // - so the two that hand the file OUT say so, and the one that keeps it here
    // says nothing at all. That asymmetry is the point: the unmarked form is
    // "in here". 기본 프로그램에서 열기 is not new wording either - it is what the
    // viewer's own menu has always called this exact action (see
    // ViewerOpenExternally, which shares its handler).
    public static string MenuOpen = "기본 프로그램에서 열기";
    // 같은 줄이 폴더일 때 쓰는 말. 폴더에는 기본 프로그램이라는 것이 없고,
    // 이 줄이 폴더에 대해 실제로 하는 일은 트리에서 펼치는 것이다.
    public static string MenuExpandFolder = "펼치기";
    public static string MenuCollapseFolder = "접기";
    // One item whose word follows the file: a picture is looked at, a track or a
    // film is played.
    public static string MenuViewHere = "보기";
    public static string MenuPlayHere = "재생";
    public static string MenuOpenWith = "연결 프로그램";
    public static string MenuCut = "잘라내기";
    public static string MenuCopy = "복사";
    public static string MenuPaste = "붙여넣기";
    // 썸네일 목록의 오른쪽 클릭 메뉴에서 폴더 전체를 표시할 때.
    public static string MenuSelectAll = "전체 선택";
    public static string MenuCompress = "압축";
    public static string MenuExtract = "압축 풀기";
    public static string MenuRename = "이름 바꾸기";
    public static string MenuDelete = "삭제";
    public static string MenuCopyPath = "경로 복사";
    public static string MenuMultiSelectionInfo = "{0}개 항목 선택됨";
    public static string MenuOpenTerminal = "터미널에서 열기";
    public static string MenuOpenWithCode = "Code로 열기";
    public static string MenuRevealInExplorer = "탐색기에서 위치 열기";
    public static string MenuRevealInTree = "트리에서 보기";
    public static string GestureDoubleClick = "더블클릭";
    // 탐색기의 같은 줄과 글자까지 같게 둔다. 이 줄이 하는 일이 셸에게 그
    // 동사를 그대로 넘기는 것이라, 결과물의 이름(`… - 바로 가기`)도 탐색기가
    // 짓는다 - 이름을 우리가 다르게 부르면 메뉴와 결과가 어긋난다.
    public static string MenuCreateShortcut = "바로 가기 만들기";
    public static string MenuProperties = "속성";

    // Options ("...") menu
    //
    // The English was Accordion Mode until 2026-08-18. It named the same row
    // as the Korean 폴더 자동 접기 but shared no word with it, so someone who
    // switched language could not tell it was the same switch - and the word
    // carries a web page's weight rather than a file tree's. Hyphenated to
    // match the Auto-Hide rows it sits near.
    // 체크가 기본이고, 체크된 상태를 말한다 - 이 묶음의 규칙대로.
    //
    // 풀면 닫힌 폴더도 두 번째 클릭에서 펼쳐진다. 그러면 규칙이 "첫 클릭은
    // 거기로 가고, 두 번째 클릭이 토글한다" 한 줄이 되어 지금보다 오히려 짧다.
    // 화살표는 어느 쪽이든 한 번에 여닫는다.
    public static string MenuPathBarAtTop = "경로 표시줄 위에 표시";
    public static string MenuExpandOnSingleClick = "클릭 시 바로 펼침";
    public static string MenuAutoCollapse = "폴더 자동 접기";
    public static string MenuCollapseAllExpanded = "폴더 전체 접기";
    public static string CollapseAllConfirmTitle = "폴더 전체 접기";
    // 되돌릴 수 없다는 것을 함께 말한다 (2026-08-27). 제목 표시줄의 접기 버튼은
    // 펼쳐져 있던 것을 기억했다가 한 번 더 누르면 되돌리는데, 이 메뉴는 일회성이고
    // 그 기억까지 일부러 지운다(CollapseAllExpandedMenuItem_Click). 겉으로는 같은
    // 동작이라 물어보는 자리에서 구분해 주지 않으면 알 길이 없다.
    public static string CollapseAllConfirmBody =
        "펼쳐진 폴더를 전체 접겠습니까?\n\n펼침 상태는 기억되지 않아 되돌릴 수 없습니다.";
    public static string MenuAlwaysOnTop = "항상 위에 표시";
    // The submenu holding the set-once housekeeping toggles (autostart, tray
    // icon, folder/file icons, title bar text) - they sat as top-level rows
    // until the menu grew long enough that daily rows and once-ever rows were
    // shoulder to shoulder (2026-08-08).
    public static string MenuGeneralSettings = "기본 설정";
    public static string MenuStartWithWindows = "부팅 후 자동 시작";
    public static string MenuAlwaysShowTrayIcon = "트레이 아이콘";
    public static string MenuShowHiddenItems = "숨김·시스템 항목 표시";
    public static string MenuShowFolderIcons = "폴더 아이콘";
    public static string MenuShowFileIcons = "파일 아이콘";
    // 드라이브만 따로. 폴더 아이콘과 나란히 두되 이름이 무엇을 가리키는지가
    // 분명해야 해서 "드라이브 아이콘" 그대로다 - 이 셋은 서로의 예외가 아니라
    // 각자 한 종류의 행을 맡는다.
    public static string MenuShowDriveIcons = "드라이브 아이콘";
    // 2026-08-17: `제목 표시줄 타이틀 제거`였음. 이 묶음의 다른 줄은 전부 켜면
    // 보이는 쪽(`폴더 아이콘`·`드라이브 아이콘`·`영역 구분선`)인데 이 줄만 반대라,
    // 체크가 무엇을 뜻하는지 줄마다 다시 읽어야 했음.
    public static string MenuTitleBarTitle = "제목 표시줄 텍스트";
    // 바로 위 줄과 짝 - 그 줄이 글자를 맡고 이 줄이 그림을 맡는다.
    //
    // `제목 표시줄에 내 PC 아이콘` 이었다가 줄임(2026-08-29, 사용자: "길어서").
    // 주어를 되풀이해서 어느 아이콘인지 갈라 주려던 것인데, 바로 위가 이미
    // `제목 표시줄 텍스트` 라 자리가 그 일을 하고 있었다. `표시` 를 남긴 것은
    // 옆의 `폴더 아이콘`·`드라이브 아이콘` 이 명사구라, 그것들과 같은 모양이면
    // 트리 아이콘 줄로 읽히기 때문이다. `숨김·시스템 항목 표시` 와 같은 짜임.
    public static string MenuTitleBarMyComputerIcon = "내 PC 아이콘 표시";
    // Names the way BACK, because that is what someone looks for first when a
    // drag did something they did not expect. The other half of the rule -
    // across drives it copies - is on the row in the help rather than here,
    // where it would not fit.
    public static string MenuDragMoves = "드래그로 이동 (Ctrl 누르면 복사)";
    // The SAME name the colour row uses (색상 설정's 영역 구분선), because they are
    // the same thing seen twice - one decides whether there is a line, the other
    // what colour it is. Two names for it would read as two features.
    public static string MenuShowPanelDividers = "영역 구분선";
    // MenuShowPathBar removed 2026-08-11 along with the toggle it labelled -
    // the strip is always on now that it carries the history chevrons.
    // 북마크 패널 서브메뉴 안, 표시 아래. 주어는 메뉴 이름이 이미 말했다.
    public static string MenuSidePanelAtBottom = "아래에 표시";
    // In the colour window, not the options menu - among behaviour toggles the
    // word reads as a shadow around the whole app. The tip says which shadow.
    public static string ButtonEdgeShades = "그림자";
    public static string ButtonEdgeShadesTip = "목록의 위아래 끝을 옅게 덮습니다";
    // Inside the 멀티미디어 패널 submenu, so the subject is already named.
    public static string MenuViewerSideSwapped = "좌우 위치 반전";
    public static string MenuDockOnRight = "고정 위치 오른쪽";
    public static string MenuReserveWorkArea = "최대화 시 자리 비우기";
    public static string MenuShortcuts = "바로 가기 키";
    public static string ShortcutSettingsTitle = "바로 가기 키";
    public static string ShortcutUnbound = "없음";
    public static string ShortcutPress = "새 키를 누르세요…";
    public static string ShortcutClear = "지우기";
    public static string ShortcutReset = "기본값";
    public static string ShortcutHint = "행을 클릭한 뒤 원하는 키를 누르세요. Explorer와 같은 F2·Delete·Ctrl+C 는 그대로 둡니다.";
    public static string ShortcutSearch = "검색";
    public static string ShortcutHelp = "도움말";
    public static string ShortcutClosePanel = "미리보기 패널 닫기";
    public static string ShortcutCollapseAll = "폴더 전체 접기";
    public static string ShortcutNextItem = "다음 파일";
    public static string ShortcutHistoryBack = "이전 폴더";
    public static string ShortcutHistoryForward = "다음 폴더";
    public static string ShortcutSlideshow = "슬라이드 쇼";
    public static string ShortcutClock = "시계";
    public static string ShortcutSavePreset = "현재 프리셋 저장";
    public static string MenuAutoDock = "가장자리에 자동 고정";
    public static string MenuAutoHideCloseOnLeave = "마우스 이탈 시 닫기";
    public static string MenuAutoHideUseHandle = "숨김 시 손잡이만";
    public static string MenuAutoHideSlide = "숨김 애니메이션";
    public static string MenuAutoHideSliverWidth = "숨김 시 막대 두께";
    public static string MenuColorSettings = "색상 설정";
    // On every swatch in the colour window. The right-click gesture has to be
    // stated somewhere the hand is already resting, or it may as well not exist.
    public static string ColorSwatchTooltip = "클릭: 색 선택기 · 우클릭: 색상 코드 입력";
    public static string ColorHexInputHint = "#RRGGBB · Enter 적용, Esc 취소";
    // The recovery lever for "something is off and I cannot say what": the
    // fault may not even be this app's (another program fighting over the
    // same edge/hooks), and a restart clears both kinds at once.
    public static string MenuRestart = "다시 시작";
    public static string MenuHelp = "도움말";
    public static string MenuAbout = "앱 정보";
    public static string HelpTitle = "Edgetree 도움말";

    // These labels are initialized with the Korean defaults and replaced by
    // Initialize() for English or Simplified Chinese before the XAML loads.
    public static string MenuLanguage = "언어";
    public static string LanguageRestartNote = "(재시작 필요)";

    // Per-folder right-click menu's own sort submenu ("정렬 방식") reuses
    // MenuSort/MenuSortByName/etc. below - this is only the options-menu
    // ("...") copy, worded as "default" to distinguish "change how every
    // folder sorts from now on" from that per-folder one.
    // "아이콘 종류" submenu: the bundled PNG set (기본) vs. the icons Windows
    // Explorer itself shows (see ShellIconService).
    public static string MenuIconStyle = "아이콘 종류";
    public static string MenuIconStyleDefault = "기본";
    public static string MenuIconStyleShell = "Windows 탐색기";

    public static string MenuDefaultSort = "정렬 기본값";
    public static string MenuSort = "정렬 기준";
    public static string MenuSortByName = "이름";
    public static string MenuSortByDate = "수정한 날짜";
    public static string MenuSortByType = "유형";
    public static string MenuSortBySize = "크기";
    public static string MenuSortAscending = "오름차순";
    public static string MenuSortDescending = "내림차순";

    // Clears a folder's own remembered sort override (see
    // Models.FolderSortOverrideEntry) so it goes back to following
    // MenuDefaultSort - shown in the per-folder "정렬" submenu only when that
    // folder actually has one (see MainWindow.ExplorerItemContextMenu_Opened).
    // 전역 정렬 따르기의 후신 (2026-08-23). 개별 정렬이 없는 폴더는 이제 전역이
    // 아니라 **가장 가까운 상위 폴더의 정렬**을 따르고, 상위에도 없으면 전역으로
    // 떨어진다. 음악 폴더 하나에 이름↑을 지정하면 안의 앨범 폴더 전부가 따라오는
    // 것이 이 줄이 생긴 이유다. 체크된 상태가 기본이다.
    public static string MenuFollowParentSort = "부모 폴더 따르기";

    // Tree folder right-click -> jumps to the search view with this folder
    // already set as the scope (see MainWindow.SearchInFolder_Click).
    public static string MenuSearchInFolder = "이 폴더에서 검색";

    // Both sort icons - a folder row's own override icon (MainWindow.xaml's
    // SortOverrideIconBorder) and the search view's sort button - rotate
    // through their states on click, with nothing but a small image to say
    // which one is active. Their ToolTip names the current state outright,
    // since field (color) and direction (which triangle is filled) are easy to
    // misread at that size. {0} is one of the SortMode* labels below; built by
    // FileSystemService.FormatSortTooltip/NoSortOverrideTooltip.
    public static string SortTooltipFormat = "정렬 기준: {0}";
    // 중립 아이콘의 툴팁. 상속이 생기며 "전역 설정 따름"에서 넓어졌다 - 따르는
    // 곳이 부모일 수도 전역일 수도 있어서 "상위"가 둘을 다 덮는다.
    public static string SortModeFollowGlobal = "상위 정렬 따름";
    public static string SortModeFolderGroup = "폴더별 묶기";
    public static string SortModeNameAsc = "이름 오름차순";
    public static string SortModeNameDesc = "이름 내림차순";
    public static string SortModeDateAsc = "날짜 오름차순";
    public static string SortModeDateDesc = "날짜 내림차순";
    // Carries its own shortcut in the label - this setting existed as Ctrl +/-
    // only, and a user who needed it didn't find it (see the XAML comment on
    // the row itself).
    public static string MenuFontSize = "글꼴 크기 (Ctrl +/-)";
    public static string MenuMaxItemsPerFolder = "폴더에 표시할 항목 개수";
    // Its own row above the count, which it greys out. Says 표시 rather than
    // standing alone as "전체", since the row above it is a number and a bare
    // 전체 next to one reads as a value for it.
    public static string MenuMaxItemsAll = "전체 표시";
    public static string MenuTabSpacing = "들여쓰기 간격";
    public static string MenuRowSpacing = "행 간격";
    public static string MenuScrollBarThickness = "스크롤바 두께";
    public static string MenuExportSettings = "설정 내보내기...";
    public static string MenuImportSettings = "설정 가져오기...";
    public static string MenuResetSettings = "전체 설정 초기화...";

    // Header buttons (ToolTips) and root label

    // Which one shows depends on AppSettings.DockOnRight (see
    // MainWindow.xaml.cs's UpdatePinButtonVisibility) - PinButton's ToolTip
    // is set from code, not bound to a single static string like the others.
    public static string ToolTipPinLeft = "좌측에 고정";
    public static string ToolTipPinRight = "우측에 고정";
    public static string ToolTipPinAutoHide = "자동 숨김";
    public static string ToolTipPinStayOpen = "고정";
    // Shift 조합은 화면에 안 보이므로 툴팁이 유일한 안내 자리다.
    public static string ToolTipCollapseAll = "전체 접기 (Shift+클릭: 복원 안됨)";
    public static string ToolTipRestoreExpanded = "펼침 상태 복원 (Shift+클릭: 복원 안되게 접기)";
    public static string ToolTipOptions = "옵션";
    public static string ToolTipUpdateAvailable = "새 버전 {0} 다운로드 가능";
    public static string ToolTipMinimize = "트레이로 최소화";
    // The HEADER button, which is not the same action as the menu rows above:
    // those go to the tray, this puts the sidebar away in whichever way the
    // current mode offers (tray, auto-hide, or minimise). It wore the menu's
    // words until 2026-08-11 and so promised the tray in two modes out of three.
    // "사이드바"였다가 2026-08-16에 바꿈 - 공식 명칭이 아니라서. 앱이 스스로를
    // 부르는 말은 화면에 나오는 모든 자리에서 하나여야 함.
    public static string ToolTipPutAway = "앱 치우기";
    public static string ToolTipClose = "종료";
    public static string RootPathLabel = "내 PC";

    // Synthetic "show the rest" row appended under a folder capped at
    // FileSystemItem.DisplayCap items. {0} is the hidden count.
    public static string MenuThumbnailMaxSize = "썸네일 최대 크기";

    public static string ShowMoreFormat = "… 더 보기 ({0}개)";

    // The same trailing slot once the reveal has happened: the two states are
    // exclusive, so one row serves both and the folder always ends in a way
    // back out. {0} is the count that would be hidden again.
    public static string ShowLessFormat = "… 접기 ({0}개)";

    // WHY A FOLDER THAT LOOKS EMPTY IS NOT. The file-type filter takes files
    // out of the listing, so a folder holding nothing the filter admits shows
    // no rows at all - and a folder with no rows reads as an empty folder,
    // which is a folder people delete. This row stands in that folder's place
    // and says what is really in there. It only appears when the filter has
    // left nothing else to show. {0} is how many it took out.
    public static string FilterHiddenFormat = "… 필터로 감춰진 파일 {0}개";

    // THE SAME NOTE FOR THE OTHER WAY A FOLDER GETS EMPTIED: its subfolders are
    // on the 숨긴 폴더 list (2026-08-26, on report - a folder whose one child was
    // hidden was calling itself 비어 있음 while that child held files). Its own
    // wording rather than a share of the filter's, because the two are undone in
    // different places: the filter chips sit at the bottom of the window, this
    // one is released from 숨긴 폴더 in the options menu.
    public static string HiddenFolderNoticeFormat = "… 숨긴 폴더 {0}개";

    // Both at once. One row, and it has to name both or it invites a hunt for
    // the wrong switch.
    public static string FilterAndHiddenFormat = "… 필터로 감춰진 파일 {0}개 · 숨긴 폴더 {1}개";

    // THE SAME SLOT FOR THE OTHER EMPTY FOLDER - the one that really is empty.
    // Clicking one already does something (the row answers, the sort icon
    // appears), so the folder looks like it replied and then said nothing.
    // This row stands in a folder with nothing left to show FOR ANY REASON -
    // no files, no subfolders, nothing taken out by the filter and nothing
    // hidden - which is why it says 비어 있음 rather than 파일 없음. 파일 없음
    // was where the wording started and the author changed it on that ground
    // (2026-08-26): the tree carries the app's own particularities (a display
    // cap, a filter, a hidden-folder list), and a row that is nearly right is
    // worse here than one that is plain.
    //
    // The note above this one used to say folders are never taken out. They
    // are - by 숨긴 폴더 - and that is exactly how this row came to stand in a
    // folder that was not empty.
    //
    // It cannot lie about a folder that could not be READ: a failed read never
    // reaches PopulateCapped at all - it keeps the placeholder and stays
    // unloaded so the next expand retries.
    public static string FolderEmptyLabel = "… 비어 있음";

    // File search (Ctrl+F view - see MainWindow's search-view methods and
    // Services/FileSearchService)
    public static string ToolTipSearch = "검색 (Ctrl+F)";
    // 이미지 뷰어 until 2026-08-12. The panel plays film and sound as well as
    // showing pictures, and it goes on playing with nothing on screen - so
    // "뷰어" was naming half of it. Not 미디어 패널 either: 미디어 is already the
    // footer chip that filters to sound and film ONLY, with 이미지 its own chip
    // beside it, so that word is taken and taken narrowly.
    public static string ToolTipViewer = "멀티미디어 패널";
    // Viewer zoom strip - a few chips wide, so all stay short in every language.
    public static string ViewerZoomFit = "맞춤";
    public static string ViewerZoomActual = "1:1";
    // 채우기 (2026-08-17). 자름맞춤이었고, 순화어를 쓰지 않는다는 문체 규칙에서
    // 사용자가 직접 지정한 대체어다. 이름을 바꾸는 것이 라벨 한 줄로 끝나지 않는
    // 이유는 F1이 이 말을 세 곳에서 다시 쓰기 때문 - 화면과 도움말이 서로 다른
    // 이름을 부르면 두 기능으로 읽힌다. 저장되는 값은 "fill"이라 이 변경은
    // 설정 파일에 닿지 않는다(AppSettings.ViewerRest).
    //
    // 이미 나간 릴리즈 노트와 랜딩의 변경 이력에는 자름맞춤이 그대로 남는다.
    // 그때의 기록이라 소급하지 않는다.
    public static string ViewerZoomFill = "채우기";
    public static string ViewerNavigator = "내비게이터";
    public static string ViewerClose = "멀티미디어 패널 닫기";
    // The footer's now-playing row, which only exists while the viewer is shut
    // and 배경 재생 is holding a track.
    public static string FooterNowPlayingOpen = "멀티미디어 패널에서 열기";
    // 같은 줄의 두 번째 일. 트리가 다른 곳으로 가면서 멈춘 영상을 가리키고,
    // 누르면 그 자리에서 이어서 본다 - 위의 열기와 방향은 같고 말이 다르다.
    public static string FooterHeldFilmResume = "이어서 보기";
    // 트랜스포트 위 줄의 머리표. 이름 앞에 붙어 그 이름이 "화면에 있는 파일"이
    // 아니라 "재생 중인 파일"임을 말함 - 둘이 같을 때도 붙는다. 같을 때만 빼면
    // 머리표가 있고 없고가 또 하나의 신호가 되어, 읽는 사람이 그 규칙까지
    // 알아야 함.
    public static string ViewerNowPlayingLabel = "지금 재생 중";
    // 같은 동작이 트리 메뉴에서는 "트리에서 보기"지만, 여기서는 그 말이 무엇을
    // 보러 가는지 안 말함 - 이 줄에 서 있는 사람은 트리가 아니라 *그 곡*을 찾는
    // 중임. 목적지를 이름으로 부름(2026-08-16).
    public static string ViewerBackToPlaying = "재생 중인 곡으로 돌아가기";
    // Explorer's own wording for the same action, so it reads as the familiar
    // thing rather than a new feature.
    public static string MenuSetWallpaper = "바탕 화면 배경으로 설정";
    // The path bar's two chevrons. Plain 뒤로/앞으로 rather than "이전 위치":
    // this is the gesture every browser and file manager has, and naming it
    // anything else would make it read as something new.
    // The key rides in the tooltip because there is nowhere else it could be
    // said - these two are buttons, not menu rows with an InputGestureText
    // column, and a shortcut nobody can discover is one nobody uses.
    public static string TreeHistoryBack = "뒤로  (Ctrl+←)";
    public static string TreeHistoryForward = "앞으로  (Ctrl+→)";
    // The right-click rides in the tooltip for the same reason the keys do:
    // it is the gesture a browser taught, and the two chevrons are where a hand
    // that knows it will try first.
    public static string TreeHistoryList = "다녀온 폴더  (뒤로/앞으로 우클릭)";
    public static string ViewerPrevImage = "이전 이미지";
    public static string ViewerNextImage = "다음 이미지";
    // 썸네일 바, not 필름스트립: the loanword is barely used in Korean, and the
    // help had already been glossing it ("필름스트립(썸네일 바)") - which is a
    // label admitting it needs a translation. The English keeps its own name
    // rather than being translated back.
    public static string ViewerFilmstrip = "썸네일 바";
    // 같은 셀을 한 줄이 아니라 여러 줄로 접어 세로로 훑는 배치. 옵션 메뉴에 있어
    // 주어를 받을 윗줄이 없으므로 `썸네일`을 적는다.
    public static string MenuFilmstripGrid = "썸네일 목록으로 보기";
    // 앱 전체화면이 창 모드에서 어디까지 가는지. 사용자가 고른 말이고, "화면 전체"
    // 대신 `바탕화면`인 것이 요점 - 앱 전체화면 자체가 이미 "화면"을 쓰고 있어서
    // 그 말로는 둘이 구분되지 않는다.
    public static string ViewerFullDesktop = "바탕화면 채우기";
    // 라벨만으로는 옆줄의 `전체화면`과 구분되지 않는다 (2026-08-27, 영문 검수).
    // 한국어는 `바탕화면`이라는 단어가 "작업 표시줄이 아닌 그 화면"을 어느 정도
    // 실어 나르지만 `Fill desktop`은 그것을 못 한다. 차이가 하나뿐이므로 툴팁도
    // 그 하나만 말한다.
    public static string ViewerFullDesktopHint = "작업 표시줄 유지";
    // 들어가고 나오는 문. 지금까지 휠클릭 하나뿐이었고, 그건 배워야 아는 것이라
    // 이 모드가 있다는 사실 자체가 안 보였다 (2026-08-17). 단축키를 아는 사람은
    // 계속 그걸 쓰면 되고, 이 행은 처음 쓰는 사람을 위한 것이다. 바로 아래
    // 바탕화면 전체와 짝이 되도록 그 위에 둔다 - 위가 모드, 아래가 그 범위.
    //
    // `앱 전체화면`이었다가 `전체화면 보기`로 바꿈. 바로 아래 줄과 나란히 놓으면
    // `앱`과 `바탕화면`이 서로 견주는 말로 읽혀서, 두 줄이 같은 종류의 선택으로
    // 보였다 - 실제로는 위가 켜고 끄는 것이고 아래는 그 범위인데. 코드와 주석은
    // 개념 이름으로 계속 `앱 전체화면`을 쓴다: 바탕화면까지 덮는 쪽과 구분해야
    // 하는 자리가 있고, 그건 화면에 안 나온다.
    //
    // `보기`도 뺌 (2026-08-19). 그 말은 이 줄을 동작이라고 말하는데 이 줄은
    // 상태다 - 앞의 체크 표시가 켜졌는지를 이미 말하고 있고, 둘레의 줄들(자막,
    // 썸네일 바, 바탕화면 채우기)도 동작이 아니라 이름이다.
    public static string ViewerFullscreen = "전체화면";
    public static string DocumentPreview = "미리보기";
    public static string DocumentEdit = "편집";
    public static string DocumentPopOut = "새 창에서 보기";
    public static string DocumentTooLarge = "파일이 너무 커서 여기서 열 수 없습니다. 기본 프로그램에서 여세요.";
    public static string DocumentLoadFailed = "파일을 열 수 없습니다.";
    public static string DocumentSaveFailed = "저장하지 못했습니다.";
    public static string DocumentPickFile = "트리에서 파일을 선택하세요";
    public static string DocumentNewTab = "새 탭";
    // 이 줄만 두 언어에 같다 (2026-08-19). 여기는 키 이름이 서는 칸이고, 그 칸의
    // 다른 값은 Space · Home · Insert · F1처럼 키에 적힌 글자 그대로다. 휠클릭
    // 하나만 한글이면 같은 칸에서 혼자 다른 종류의 말이 된다. F1의 같은 칸도 이
    // 말로 맞췄다 - 같은 동작을 두 이름으로 부르면 두 기능으로 읽힌다.
    public static string GestureWheelClick = "Wheel click";
    public static string MenuImageViewer = "멀티미디어 패널";
    // 캐싱 rather than 미리 불러오기, reversing the earlier choice to name it for
    // what it does instead of for the machinery: 캐싱 is the word Korean users
    // read fastest here, and "미리 불러오기" describes the act without naming the
    // thing that is left behind - which is what the row below cleans up. The two
    // rows now share a noun, so the pair reads as one subject.
    public static string MenuPrecacheThumbnails = "이미지 썸네일 캐싱";
    // The SUBJECT IS THE SUBMENU (2026-08-15). This row sits under 멀티미디어
    // 패널, so naming the kinds again made it the one explanatory line in a
    // menu of short ones - the same reason 아래에 표시 does not repeat 패널.
    // What it cannot be shortened to is "미디어": that word is already the
    // footer chip meaning sound and film ONLY, with 이미지 its own chip beside
    // it, so borrowing it here would read as leaving pictures out - and
    // pictures are most of what this setting is for.
    public static string MenuOpenMediaInViewer = "더블클릭으로 열기";
    // "펼치기"이지 "열기"가 아님 - 바로 위 줄의 "열기"는 재생·실행까지 가는
    // 동사이고, 이쪽은 패널을 펼치기만 할 뿐 아무것도 시작하지 않음. 한 메뉴 안에
    // 두 줄이 나란히 있으니 같은 낱말을 쓰면 같은 일로 읽힘(2026-08-15).
    // 닫는 쪽은 만들었다가 뺐음(같은 날, 사용자 판단).
    public static string MenuViewerFollowsSelection = "자동 펼치기";
    // "쇼" spaced off, matching the way Windows itself writes it. The seconds
    // row says its unit in the label rather than beside the number: the
    // stepper's own digits are already narrow, and every other stepper in this
    // menu carries a bare number.
    public static string MenuSlideshow = "슬라이드 쇼";
    public static string MenuSlideshowSeconds = "슬라이드 간격 (초)";
    // 패널 위의 시계. "슬라이드 쇼 시계"가 아님 - 쇼와 무관하게 켜고 끄는
    // 것이라, 이름에 쇼가 들어가면 쇼를 켜야 나오는 것으로 읽힘.
    public static string MenuViewerClock = "시계와 날짜";
    // 크기 하나로 충분하다 - 시·분, 오전/오후, 날짜가 한 배수를 함께 따라간다.
    // 셋을 따로 주면 서로 어긋난 시계를 만들 수 있게 되고, 그건 아무도 원해서
    // 만드는 것이 아니다.
    public static string MenuViewerClockSize = "시계 크기";
    // 잠금 화면이 쓰는 꼴 그대로. 시각에 초는 없음 - 그 자리에서 읽히는 것은
    // 몇 시 몇 분 하나뿐임. 오전/오후는 위에 따로 한 줄.
    public static string ViewerClockTimeFormat = "H:mm";
    public static string ViewerClockDateFormat = "M월 d일 dddd";
    // The size rides in the row itself, because the only reason to press it is
    // that the number has grown - and a row that has to be pressed to find out
    // is a row that gets pressed for no reason.
    public static string MenuClearThumbnailCache = "캐싱 파일 정리";
    public static string MenuClearThumbnailCacheSized = "캐싱 파일 정리 ({0})";
    // {0} is how many are in hand so far. No total: the bar's own counter is
    // right beside it and already says how many the folder holds.
    public static string ViewerPrecaching = "캐싱 중 {0}";
    // 썸네일에 표시해 둔 개수. 끌어다 놓기용 표시이므로 "선택"이 아니라 개수만
    // 말한다.
    public static string ViewerMarkedCount = "{0}개 선택";
    // 북마크, not 위치 기록 (2026-08-11). "기록" reads as logging something
    // rather than as leaving a marker you come back to, and the app already
    // has a word for exactly that act on a tree row. Qualified with 영상 in the
    // help so the two are told apart there; inside a video's own menu the
    // qualifier is what the menu already is.
    // QUALIFIED SINCE 2026-08-11, and the reason the old note gave for leaving
    // it bare has expired. It read "inside a video's own menu the qualifier is
    // what the menu already is" - true while this menu held only the film's own
    // rows, and false the moment the FILE's 북마크 joined it. Two rows both
    // called 북마크, one marking a second of film and one marking a row in the
    // tree, is the kind of thing nobody reads twice. The help already used this
    // longer name for it.
    public static string ViewerMarkAdd = "영상 북마크";
    public static string ViewerMarkList = "영상 북마크 목록";
    public static string ViewerRewind = "처음으로";
    // Two pairs because the transport serves sound and film from the same row,
    // and the tooltip is set to whichever the loaded file is. 곡 over a folder
    // of films would be naming the wrong thing, and "이전 항목" would be naming
    // nothing in particular.
    public static string ViewerPrevTrack = "이전 곡";
    public static string ViewerNextTrack = "다음 곡";
    public static string ViewerPrevVideo = "이전 영상";
    public static string ViewerNextVideo = "다음 영상";
    // "영상 크기" rather than "확대": the row's middle button is 맞춤, which is
    // not a magnification - the row is about what SIZE the film is shown at,
    // and one of the three answers is "as big as it fits" (2026-08-10).
    public static string ViewerZoom = "영상 크기";
    // 같은 행이 그림에도 나오므로(전체 덮기) 주어를 갈아 끼운다. "크기" 하나로
    // 두 경우를 덮을 수도 있지만, 이 메뉴에는 자막 크기도 있어서 무엇의 크기인지가
    // 라벨에 남아야 한다.
    public static string ViewerZoomPicture = "그림 크기";
    // 영상 크기 줄의 반대편이라 바로 아래에 선다. 그 줄은 창 안에서 영상을
    // 어떻게 놓을지 정하고, 이 줄은 영상에 창을 맞춘다. 주어가 창인 것이
    // 이름에 그대로 나와야 해서 "비율 맞춤"이 아니다 - 바뀌는 것은 창이다.
    public static string ViewerFitWindow = "창을 영상에 맞춤";
    public static string ViewerSubtitles = "자막";
    public static string ViewerSubtitleSize = "자막 크기";
    // 높이가 아니라 위치. 이 줄이 바꾸는 것은 자막이 화면에서 어디에 놓이는가이고,
    // 높이라고 하면 글자의 높이로도 읽힌다 - 바로 윗줄이 크기라 더 그렇다.
    public static string ViewerSubtitlePosition = "자막 위치";
    // 단위는 라벨에. 값에 붙이면 같은 뜻을 이 메뉴가 두 가지로 말하게 된다 -
    // 바로 아래 슬라이드 간격이 (초)로 쓰고 있었고, 이 줄만 s 였다(2026-08-18).
    public static string ViewerSubtitleSync = "자막 싱크 (초)";
    // Safe to be this blunt because of WHERE it is: inside the 북마크 목록
    // submenu, under the positions it clears. The word 삭제 means files
    // everywhere else in this app, and it would have needed qualifying if it
    // sat anywhere the file rows could be mistaken for its subject.
    public static string ViewerMarkClear = "전체 삭제";
    // The gap between pressing play and the engine reporting the file open -
    // seconds on a sleeping share, and silent until now. Said in the caption,
    // which is where this panel already talks.
    // Named for the file, not for the maths: nobody looking at a washed-out
    // film is thinking "tone mapping", they are thinking "this is an HDR one".
    public static string ViewerHdrToneMap = "HDR 색 보정";
    // 밝기 rather than 노출: the row is a dial someone turns while looking at
    // the picture, and 노출 names the maths behind it instead of the change.
    public static string ViewerHdrBrightness = "  밝기";
    public static string ViewerHdrSaturation = "  채도";
    public static string ViewerHdrContrast = "  대비";
    public static string ViewerMediaOpening = "여는 중…";
    // Nothing has failed and nothing is being cancelled; the wait is just
    // longer than a wait usually is, and saying so beats a still picture.
    public static string ViewerMediaOpeningSlow = "여는 중… 조금 오래 걸리고 있습니다";
    // The wait ended without an open. Says what happened and leaves it there -
    // the play button is right beside the line, so telling the reader to press
    // it would be naming a control they are already looking at.
    public static string ViewerMediaOpenGaveUp = "응답이 없어 재생을 취소했습니다";
    // Playing, but the picture has stopped moving - the file is not arriving
    // fast enough to decode. Named for the cause rather than for the symptom.
    public static string ViewerMediaStalled = "파일을 기다리는 중…";
    public static string ViewerPlay = "재생";
    public static string ViewerPause = "일시정지";
    public static string ViewerStop = "정지";
    public static string ViewerMute = "음소거";
    // The four states of the transport's leftmost switch, said plainly and in
    // the words a player uses - these are the tooltip AND the menu, so a name
    // that needed explaining would need it twice (2026-08-11).
    public static string ViewerRepeatOff = "이어서 재생 안 함";
    public static string ViewerRepeatAll = "폴더 반복";
    public static string ViewerRepeatOne = "한 곡 반복";
    // "셔플 재생"이었다가 2026-08-16에 바꿈. 이 모드는 한 바퀴로 끝나지 않고
    // 가방이 비면 다시 섞어 무한히 돈다(NextViewerPlaybackItem) - 끝이 없으니
    // 관찰로는 확인할 수가 없어서, 라벨이 대신 말해야 하는 사실임. 나머지 셋도
    // 전부 "반복"으로 끝나므로 한 묶음으로도 읽힘.
    public static string ViewerRepeatShuffle = "셔플 반복";
    // Appended to whichever of the four is in force. The switch turns the thing
    // on and off, and nothing about it said where the other three modes live -
    // which was the first thing asked about it (2026-08-11).
    public static string ViewerRepeatHint = "우클릭으로 변경";
    // The switch that lets sound outlive the selection. Named for what it buys
    // rather than for the mechanism: "백그라운드 재생" is what other apps call
    // it, and it says the sound keeps going while you are doing something else.
    public static string ViewerBackgroundPlay = "백그라운드 재생 · 다른 폴더로 가도 계속";
    // A selected folder's headline, in the caption slot a file's pixel size
    // uses. Counts what the PANEL can show, which is not the same number the
    // tree is listing beside it - so the sentence says which question it is
    // answering.
    public static string ViewerFolderItemCount = "볼 수 있는 파일 {0}개";
    // The folder's own play button. "전체" is the load-bearing word: pressing it
    // does not play one file, it puts the folder on and keeps it on.
    public static string ViewerFolderPlayAll = "폴더 전체 재생";
    // Shown in the caption when Windows has no codec for the file - the panel
    // says what happened and what still works, rather than sitting blank.
    public static string ViewerPlaybackUnsupported = "이 형식은 재생할 수 없습니다 · Enter로 기본 프로그램에서 열기";
    // Said only when playback had ALREADY started - "형식을 재생할 수 없다"는 말은
    // 20초를 잘 재생한 뒤에 나오면 거짓이고, 있지도 않은 코덱을 찾으러 가게 만듦.
    public static string ViewerPlaybackInterrupted = "재생이 중단되었습니다 · 다시 누르면 이어서, Enter로 기본 프로그램에서 열기";
    // Some files play their picture and nothing else - DTS and TrueHD sound
    // are both undecodable on Windows, and the engine simply drops the track.
    // Silence with no explanation reads as the app being broken, so the
    // caption says which it is. Stated, not apologised for.
    public static string ViewerNoAudio = "소리 없음";
    // The transport strip's last button. Named for where it sends the file,
    // not for the act of leaving here.
    public static string ViewerOpenExternally = "기본 프로그램에서 열기";
    public static string ToolTipExitSearch = "탐색기로 (Ctrl+E)";
    public static string SearchTooltipBrowseFolder = "검색할 폴더 선택";
    public static string SearchTooltipRefresh = "다시 인덱싱";
    public static string SearchTooltipRefreshStale = "다시 인덱싱 · 이 폴더가 바뀌었습니다";
    public static string SearchTooltipHistory = "최근 검색어";
    public static string SearchHistoryDeleteTooltip = "이 검색어 삭제";
    public static string SearchBrowseFolderDialogTitle = "검색할 폴더를 선택하세요";
    public static string SearchScopeNone = "검색할 폴더를 선택하세요 →";
    public static string SearchBoxPlaceholder = "검색";
    // Only ever seen with nothing selected (startup before the last selection
    // is restored) - the box is otherwise full of the current folder's path.
    // That one moment is where "you can type in here" has to be said, since
    // the menu label deliberately doesn't say it.
    public static string PathBarPlaceholder = "경로 입력 후 Enter";
    // {0} = files scanned so far. The "검색 가능" half is the whole point:
    // results stream in while the scan runs, but nothing said so - and the
    // pulsing bar that used to sit under this line actively implied the
    // opposite (see the note where it was removed in MainWindow.xaml).
    public static string SearchStatusScanning = "인덱싱 중… ({0}) · 검색 가능";
    // {0} = result count.
    public static string SearchStatusResults = "{0}개 결과";
    // {0} = shown, {1} = total (results were capped for display).
    public static string SearchStatusResultsCapped = "{0} / {1}개 결과 표시";
    public static string SearchStatusNoResults = "결과 없음";
    public static string SearchStatusNoResultsCached = "결과 없음 · 인덱스 {0} · 새로고침으로 갱신";
    public static string SearchStatusIndexAgeSuffix = " · 인덱스 {0}";
    // These three all have to survive a docked sidebar's width, which is far
    // narrower than a normal dialog - full sentences get clipped mid-word
    // there. Written as terse fragments on purpose; the search box's own
    // placeholder already supplies the "type something" instruction, so the
    // idle line spends its width on the matching rules instead, which are the
    // part nobody can guess.
    // Shown instead of a fresh scan's line when the index came off disk. The age
    // is the important half - a file created since that moment simply won't
    // appear, and without the age there's nothing to explain the absence (see
    // SearchIndexCache's note). {0} = a SearchAge* string below.
    public static string SearchStatusCached = "인덱스 {0} · 새로고침으로 갱신";
    public static string SearchAgeJustNow = "방금";
    // {0} = whole minutes / hours / days since the scan.
    public static string SearchAgeMinutes = "{0}분 전";
    public static string SearchAgeHours = "{0}시간 전";
    public static string SearchAgeDays = "{0}일 전";

    // A result whose file is gone - only reachable once an index can outlive
    // the files it names (see SearchIndexCache).
    public static string SearchResultMissing = "삭제되었거나 이동된 파일입니다";

    public static string SearchStatusTooBroad = "너무 광범위함 · 글자나 숫자 포함";
    public static string SearchStatusEmpty = "부분일치 · *? 와일드카드 · ↑↓ 기록";
    public static string SearchStatusScopeMissing = "폴더 없음 · 새로고침하거나 다시 선택";

    // Color settings window
    public static string ColorSettingsTitle = "색상 설정";
    public static string ColorLabelBackground = "탐색기 배경";
    // "폴더 이름"/"파일 이름", not "폴더명"/"파일명": the row menu already says
    // "이름 바꾸기", and one thing called two names reads as two things.
    public static string ColorLabelFolderNameFont = "폴더 이름";
    public static string ColorLabelFolderNameHighlightFont = "폴더 이름 강조";
    public static string ColorLabelFileNameFont = "파일 이름";
    public static string ColorLabelFileNameHighlightFont = "파일 이름 강조";
    public static string ColorLabelSelection = "선택된 항목";
    // 선택된 항목 바로 다음 자리. 둘은 같은 순간을 말하고 하나는 행, 하나는 그
    // 행이 속한 폴더가 차지하는 구간이다.
    public static string ColorLabelSelectionZone = "선택된 폴더 영역";
    // 맨 "패널 배경"이었다. 이 목록 안에 멀티미디어 패널 배경이 같이 있어서 둘이
    // 문맥으로 안 갈렸다 - 이름이 아니라 자리가 문제였고, 그래서 수식을 붙였다
    // (2026-08-17). 이 브러시는 북마크 목록과 검색 화면의 범위 줄을 함께 칠한다
    // (그 줄이 패널 색을 일부러 빌려 쓴다). 저장 키는 HistoryBackgroundColorHex
    // 그대로라 맞춰 둔 색은 초기화되지 않는다.
    public static string ColorLabelHistory = "북마크 패널 배경";
    public static string ColorLabelHoverBackground = "마우스 오버";
    public static string ColorLabelFolderNameHoverFont = "폴더 이름 마우스 오버";
    public static string ColorLabelFileNameHoverFont = "파일 이름 마우스 오버";
    public static string ColorLabelShowMore = "더 보기";
    // 배경은 패널의 것이고 이 셋은 그 안에 적힌 이름의 것이라 "패널"을 빼고 짧게
    // 간다 - 위 배경 행이 이미 어느 패널인지 말했고, 이 목록은 종류로 훑는다.
    public static string ColorLabelPanelNameFont = "북마크 이름";
    public static string ColorLabelPanelNameHighlightFont = "북마크 이름 강조";
    public static string ColorLabelPanelNameHoverFont = "북마크 이름 마우스 오버";
    public static string ColorLabelGuideLine = "들여쓰기 안내선";
    public static string ColorLabelGuideLineActive = "들여쓰기 안내선 강조";
    // 화살표가 아니라 기호 - 사용자가 부르는 이름 그대로.
    public static string ColorLabelExpander = "펼침기호";
    // 켜진 칩의 글자는 두 칩이 공유하는 색 하나라 "필터"를 뺐음 - 붙여 두었더니
    // 제외 칩 글자 행이 안 듣는 것처럼 읽혔음(2026-08-15). 배경은 칩마다 따로다.
    public static string ColorLabelFilterChipChecked = "필터 칩 켜짐";
    public static string ColorLabelFilterChipCheckedFont = "칩 켜짐 글자";
    public static string ColorLabelFilterChipExclude = "제외 칩 꺼짐 글자";
    public static string ColorLabelFilterChipExcludeChecked = "제외 칩 켜짐";
    public static string ColorLabelHeader = "제목 표시줄 배경";
    public static string ColorLabelPanelDivider = "영역 구분선";
    public static string ColorLabelViewerBackground = "멀티미디어 패널 배경";
    // Names both shapes the hidden sidebar can take, because one colour covers
    // both and the option menu already calls them 손잡이 and 막대
    // (MenuAutoHideUseHandle / MenuAutoHideSliverWidth).
    public static string ColorLabelAutoHideHandle = "숨김 시 손잡이/막대";
    public static string ButtonDefaults = "기본값";
    public static string ButtonClose = "닫기";
    // Colours travel between machines; the rest of settings.json does not -
    // hidden folders, bookmarks and last-selected paths all name folders the
    // other PC may not have (2026-08-04).
    public static string ButtonExportColors = "내보내기";
    public static string ButtonImportColors = "불러오기";
    public static string ColorFileFilter = "Edgetree 색상 (*.json)|*.json";
    public static string ColorFileDefaultName = "edgetree-colors.json";
    public static string ColorImportFailedTitle = "색상 불러오기";
    public static string ColorImportFailedBody = "이 파일에는 색상이 없습니다.";
    // 설정이 디스크에 안 써질 때. 한 세션에 한 번만 뜬다 - 저장은 클릭마다 도는
    // 것이라 실패도 클릭마다 나기 때문이다(MainWindow의 구독부).
    //
    // 본문이 말해야 하는 것은 셋이고 그 순서다: 지금 무엇이 안 되고 있는지,
    // 그래서 무엇을 잃게 되는지, 어디를 보면 되는지. 원인을 추측해서 적지 않는다 -
    // 읽기 전용일 수도, 다른 프로그램이 잡고 있을 수도, 동기화 폴더일 수도 있고
    // 앱은 그중 무엇인지 모른다. 경로가 그 자리에서 유일하게 확인 가능한 것이다.
    public static string SettingsSaveFailedTitle = "설정 저장";
    public static string SettingsSaveFailedBody =
        "설정을 저장하지 못했습니다. 지금 변경한 내용은 앱을 닫으면 사라집니다.\n\n{0}";
    // The moon and sun emoji that used to lead these are gone (2026-08-11):
    // they were the only pictures in a window of text buttons, they came from
    // the system's emoji font rather than from the app's own marks, and the
    // words already say which is which.
    // "모드" dropped 2026-08-15: this row gained buttons, and these two were
    // the only ones carrying a word they did not need - the pair is obviously
    // a mode, and the row has clipped its wordy buttons before.
    public static string ColorThemeDarkMode = "다크";
    public static string ColorThemeLightMode = "라이트";
    public static string ButtonRandomColors = "랜덤";
    // The dice's grey cousin: same roll, same floors, colour taken out. Kept
    // beside them rather than in the palette rows because it is a whole-
    // palette action, which is what that half of the row holds.
    public static string ButtonMonoColors = "모노";
    public static string ButtonMonoColorsTip = "회색톤으로 한 번에";
    // On every chainable row's link mark. Says the rule that is not obvious -
    // that BOTH have to be lit - since one lit link on its own does nothing.
    public static string ColorChainTip = "묶기 · 함께 켠 줄끼리 같은 색";
    // Said as what it GIVES, so the pair reads as a choice rather than as one
    // button and its louder twin: this one lands on combinations that go
    // together, the other one on combinations that do not have to.
    public static string ButtonRandomColorsTip = "자연스러운 조합";
    // The second die, beside the first. "원색" says what makes it different in
    // two characters - it starts from a primary hue - where "과감" or "강렬"
    // would be describing a feeling and could mean anything.
    public static string ButtonDaringColors = "원색";
    public static string ButtonDaringColorsTip = "원색에서 출발하는 과감한 조합";
    // "되돌리기" over the more precise "랜덤 전으로", which read oddly
    // (2026-08-09); the button lives beside 랜덤, which carries the
    // context the label dropped.
    //
    // 복원 (2026-08-17). 문체 규칙의 되돌리다 → 취소·복원 중 뒤쪽을 고른 이유는
    // 앞쪽이 쓸 수 없기 때문이다 - 이 창에는 ButtonCancel("취소")이 이미 있고,
    // 나란히 놓이면 "취소"가 창을 닫는 것인지 색을 되돌리는 것인지 갈리지 않는다.
    // 표의 두 후보 중 하나가 다른 라벨과 충돌하면 남은 하나가 답이다.
    public static string ButtonUndoRandom = "복원";
    // Plain (no emoji) versions for use inside a sentence - see
    // ColorResetConfirmBody.
    public static string ColorThemeDarkLabel = "다크 모드";
    public static string ColorThemeLightLabel = "라이트 모드";
    public static string ColorResetConfirmTitle = "색상 초기화";
    public static string ColorResetConfirmBody = "현재 {0}에서 설정한 색상값이 초기화됩니다. 진행하시겠습니까?";

    // About window
    public static string AboutTitle = "정보";
    public static string AboutVersionLabel = "버전";
    public static string AboutAuthorLabel = "제작자";
    public static string AboutDateLabel = "날짜";
    public static string AboutLicenseLabel = "라이선스 요약";
    public static string AboutGithubLabel = "GitHub";
    public static string AboutUpstreamLabel = "상위 프로젝트";
    public static string AboutWebsiteLabel = "웹사이트";
    public static string AboutOtherToolLabel = "같은 개발자의 다른 도구";
    public static string AboutUpdateAvailableFormat = "새 버전 {0} 다운로드";
    public static string AboutAuthorValue = "pjh85336@gmail.com";
    public static string AboutLicenseSummary =
        "MIT 라이선스. 별도의 보증 없이 있는 그대로 제공되며, 사용에 따른 책임은 사용자 본인에게 있습니다.";
    public static string AboutIconLicenseLabel = "아이콘";
    public static string AboutIconLicenseValue =
        "파일·폴더 아이콘: Material Icon Theme (MIT)\n" +
        "화면 글리프: Material Symbols, Google (Apache License 2.0)";
    public static string AboutIconLicenseOpen = "Apache License 2.0 전문 보기";

    // Tray
    public static string TrayOpen = "열기";
    public static string TrayHide = "트레이로";
    public static string TrayAbout = "정보";
    public static string TrayExit = "종료";
    // Only present when there is actually a newer release - no row at all
    // otherwise, and never a balloon. Shared by the tray menu, the tray's
    // tooltip and (2026-08-17) the options menu's top row: one fact, one wording.
    // Renamed off Tray* when the third place arrived.
    public static string UpdateAvailableRow = "새 업데이트 - v{0}";
    // Where that row goes, shown in the gesture column beside it. Not
    // translated - it is the fork release address both languages download from.
    public static string UpdateSiteHost = "github.com/98canon/Edgetree";

    // MessageBox titles/bodies
    public static string PasteFailedTitle = "붙여넣기 실패";
    public static string MoveIntoSelfError = "폴더를 자기 자신이나 그 하위 폴더로 옮길 수 없습니다.";
    public static string CopyIntoSelfError = "폴더를 자기 자신이나 그 하위 폴더로 복사할 수 없습니다.";
    public static string NewFolderFailedTitle = "새 폴더 만들기 실패";
    public static string RenameFailedTitle = "이름 바꾸기 실패";
    // The name a newly created folder gets, and the message when a rename is
    // refused. Both used to be Korean literals sitting in FileOperationService,
    // so an English install still wrote a folder called "새 폴더" onto the
    // user's disk - reported from a German freeware forum, 2026-08-05: "bei
    // einem kurzen Test tauchte trotz englisch noch koreanischer Text auf".
    // Anything a user can SEE belongs here, including text that ends up as a
    // filename.
    public static string NewFolderDefaultName = "새 폴더";
    public static string RenameFailedBody = "이름을 바꿀 수 없습니다.";
    public static string DeleteConfirmTitle = "삭제 확인";
    public static string DeleteConfirmBody = "'{0}'을(를) 휴지통으로 보낼까요?";
    public static string DeleteConfirmBodyMultiple = "선택한 {0}개 항목을 휴지통으로 보낼까요?";
    // Shift+Delete goes through the shell, which asks and reports in its own
    // dialogs. This is only for the case it comes back with a code and no
    // dialog of its own - rare, and worse than saying nothing.
    public static string DeleteFailedShellBody = "삭제하지 못했습니다. (오류 {0})";

    // The only question this app still puts in front of a delete. Names the
    // REASON rather than the risk - "휴지통이 없습니다" is a fact about the
    // place, and it is the fact that makes the rest true.
    public static string DeleteNoRecycleBinTitle = "네트워크 위치에서 삭제";
    public static string DeleteNoRecycleBinBody =
        "'{0}'을(를) 삭제할까요?\n\n네트워크 위치에는 휴지통이 없어 복구할 수 없습니다.";
    public static string DeleteNoRecycleBinBodyMultiple =
        "네트워크 위치의 {0}개 항목을 삭제할까요?\n\n휴지통이 없어 복구할 수 없습니다.";
    // THE SECOND QUESTION, and it exists because the tree cannot show what is
    // about to go (2026-08-26, raised by the author). A folder holding nothing
    // but a hidden folder now says so, but the note only stands where there are
    // no other rows - put one ordinary folder beside the hidden one and the
    // parent reads as almost empty again while the hidden branch is still
    // underneath it. This names the count at the moment it matters instead of
    // parking a row in every folder that has ever had something hidden in it.
    //
    // Same shape as the network question above: the REASON first, because
    // "안 보이는 폴더가 들어 있습니다" is the fact that makes the rest true.
    // WHAT IS ABOUT TO HAPPEN, then what the screen did not show, then ONE
    // question (2026-08-26, author's wording). The network box opens with
    // "삭제할까요?" and closes on the reason; this one is the other way round
    // because the reason is the whole point of asking - and stating the action
    // rather than asking it twice keeps a single question mark in the box.
    public static string DeleteHiddenInsideTitle = "숨긴 폴더가 들어 있음";
    public static string DeleteHiddenInsideBody =
        "'{0}'을(를) 삭제합니다.\n\n안에 숨긴 폴더 {1}개가 있습니다. 그래도 삭제하겠습니까?";
    public static string DeleteHiddenInsideBodyMultiple =
        "선택한 항목을 삭제합니다.\n\n안에 숨긴 폴더 {0}개가 있습니다. 그래도 삭제하겠습니까?";
    public static string DeleteFailedTitle = "삭제 실패";
    public static string CompressFailedTitle = "압축 실패";
    public static string ExtractFailedTitle = "압축 풀기 실패";
    public static string CompressSkippedBody = "{0}개 항목을 읽을 수 없어 건너뛰었습니다.";
    public static string StartWithWindowsFailedTitle = "윈도우 시작 시 실행";
    public static string StartWithWindowsFailedBody = "시작 프로그램 등록에 실패했습니다. 관리자 정책으로 제한되어 있을 수 있습니다.";
    public static string LanguageChangeTitle = "언어 변경";
    public static string LanguageChangeBody = "언어를 변경하려면 앱을 다시 시작해야 합니다. 지금 다시 시작할까요?";
    public static string ImportFailedTitle = "가져오기 실패";
    public static string OverwriteConfirmTitle = "덮어쓰기 확인";
    public static string OverwriteConfirmBody = "'{0}'이(가) 이미 있습니다. 덮어쓸까요?";

    public static string ExportSettingsFailedTitle = "설정 내보내기 실패";
    public static string ImportSettingsFailedTitle = "설정 가져오기 실패";
    public static string SettingsImportedTitle = "설정 가져오기 완료";
    public static string SettingsImportedBody = "설정을 가져왔습니다. 적용하려면 앱을 다시 시작해야 합니다. 지금 다시 시작할까요?";

    public static string ResetSettingsConfirmTitle = "설정 초기화";
    public static string ResetSettingsConfirmBody = "모든 설정과 북마크가 앱 기본 상태로 초기화됩니다. 이 작업은 복구할 수 없습니다.\n\n초기화 후 적용을 위해 앱을 다시 시작합니다. 계속할까요?";

    // Which language the fields below ended up in. Almost nothing needs to ask
    // - the point of this class is that callers just read a string - but a
    // A language-dependent subtitle file has to be told which track to play,
    // and the app's own language is the only answer that isn't a guess (see
    // SubtitleService).
    public static bool IsEnglish { get; private set; }
    public static bool IsSimplifiedChinese { get; private set; }

    public static void Initialize(string language)
    {
        IsEnglish = false;
        IsSimplifiedChinese = false;

        if (language is "zh-CN" or "zh-SG" or "zh-Hans" or "zh-Hans-CN" or "zh-Hans-SG")
        {
            IsSimplifiedChinese = true;
            MenuLanguage = "语言";
            LanguageRestartNote = "（需要重启）";
        MenuBookmark = "书签";
        MenuBookmarkAdd = "添加书签";
        MenuBookmarkRemove = "删除书签";
        MenuBookmarkList = "书签";
        MenuBookmarkListEmpty = "没有书签";
        MenuPresetAdd = "添加预设";
        MenuPresetApply = "应用";
        MenuPresetOverwrite = "覆盖…";
        MenuPresetRename = "重命名…";
        PresetSaveTitle = "保存当前设置";
        PresetRenameTitle = "重命名预设";
        MenuPresetDelete = "删除";
        PresetNameTitle = "预设名称";
        PresetNameHint = "保存位置、大小、停靠、颜色、文件类型和当前文件夹";
        PresetDefaultName = "预设{0}";
        PresetSavedToast = "预设已保存";
        PlaceMissingTitle = "未找到项目";
        PlaceMissingBody = "找不到该项目。\n\n{0}\n\n将其从列表中删除？";
        PresetSlotEmptyTitle = "预设";
        PresetSlotEmptyBody = "尚无预设 {0}。\n要在这里保存当前设置吗？";
        MenuFileFilter = "文件类型";
        FilterChipExecutable = "程序";
        MenuFileFilterAll = "全部";
        MenuFileFilterCode = "代码";
        MenuFileFilterImage = "图片";
        MenuFileFilterDocument = "文档";
        MenuFileFilterMedia = "媒体";
        MenuFileFilterArchive = "压缩包";
        MenuFileFilterExecutable = "程序和快捷方式";
        MenuFileFilterOther = "其他";
        MenuFileFilterCustomEdit = "自定义…";
        FilterCustomTitle = "自定义扩展名";
        FilterCustomHint = "用逗号分隔 · txt、png、.mp3";
        FilterCustomEmptyHint = "留空并确认后，自定义项将被移除";
        MenuFileFilterExcludeEdit = "排除…";
        FilterExcludeTitle = "要排除的扩展名";
        FilterExcludeHint = "用逗号分隔 · 这里写的内容会始终隐藏";
        FilterExcludeEmptyHint = "留空并确认后，排除项将被移除";
        ButtonOk = "确定";
        ButtonCancel = "取消";
        MenuFontWeight = "字体粗细";
        MenuFontWeightNormal = "常规";
        MenuFontWeightBold = "加粗";
        MenuFontWeightFoldersOnly = "仅文件夹加粗";
        MenuFontWeightFilesOnly = "仅文件加粗";
        MenuSidePanel = "书签面板";
        MenuSidePanelShow = "显示";
        MenuHideFolder = "排除";
        MenuHiddenFolderList = "排除的文件夹";
        MenuHiddenFolderListEmpty = "没有排除的文件夹";
        MenuUnhideFolder = "停止排除";
        MenuNetworkLocations = "网络位置";
        MenuNetworkLocationAdd = "添加位置…";
        MenuNetworkLocationsEmpty = "尚未添加位置";
        MenuNetworkLocationRemove = "从列表中移除";
        NetworkLocationPromptTitle = "添加网络位置";
        NetworkLocationPromptHint = "输入 \\\\server\\share 或文件夹路径。映射到驱动器号的内容已经在列表中。";
        NetworkLocationUnreachableTitle = "无法连接";
        NetworkLocationUnreachableBody = "{0}\n\n没有回应。仍然将其添加到列表中吗？";
        NetworkLocationDuplicateTitle = "已在列表中";
        MenuListRowRemove = "移除";
        HiddenClearAllConfirmTitle = "清除所有排除项";
        HiddenClearAllConfirmBody = "要再次显示所有排除的文件夹吗？ ({0})";
        MenuBookmarkClearAll = "全部清除";
        BookmarkClearAllConfirmTitle = "清除所有书签";
        BookmarkClearAllConfirmBody = "要清除所有书签吗？ ({0})";
        BookmarkShortcutNext = "下一个书签";
        BookmarkShortcutPrev = "上一个书签";
        MenuNewFolder = "新建文件夹";
        MenuRefresh = "刷新";
        MenuPathBarAtTop = "在顶部显示路径栏";
        MenuExpandOnSingleClick = "单击即可展开";
        MenuAutoCollapse = "自动折叠文件夹";
        MenuCollapseAllExpanded = "折叠所有文件夹";
        CollapseAllConfirmTitle = "折叠所有文件夹";
        CollapseAllConfirmBody = "折叠所有展开的文件夹？\n\n扩展的内容不会被记住，因此无法撤消。";
        MenuOpen = "在默认应用程序中打开";
        MenuExpandFolder = "扩张";
        MenuCollapseFolder = "坍塌";
        MenuViewHere = "看法";
        MenuPlayHere = "玩";
        MenuOpenWith = "打开方式";
        MenuCut = "切";
        MenuCopy = "复制";
        MenuPaste = "粘贴";
        MenuSelectAll = "选择全部";
        MenuCompress = "压缩";
        MenuExtract = "提炼";
        MenuRename = "重命名";
        MenuDelete = "删除";
        MenuCopyPath = "复制路径";
        MenuMultiSelectionInfo = "已选择 {0} 项";
        MenuOpenTerminal = "在终端中打开";
        MenuOpenWithCode = "用 Code 打开";
        MenuRevealInExplorer = "在资源管理器中显示";
        MenuRevealInTree = "在树中显示";
        GestureDoubleClick = "双击";
        MenuCreateShortcut = "创建快捷方式";
        MenuProperties = "特性";
        MenuAlwaysOnTop = "始终置顶";
        MenuGeneralSettings = "常规设置";
        MenuStartWithWindows = "随 Windows 启动";
        MenuAlwaysShowTrayIcon = "始终显示托盘图标";
        MenuShowHiddenItems = "显示隐藏和系统项";
        MenuShowFolderIcons = "显示文件夹图标";
        MenuShowFileIcons = "显示文件图标";
        MenuShowDriveIcons = "显示驱动器图标";
        MenuTitleBarTitle = "标题栏文字";
        MenuTitleBarMyComputerIcon = "显示“此电脑”图标";
        MenuDragMoves = "拖动时移动（按住 Ctrl 可复制）";
        MenuShowPanelDividers = "显示面板分隔线";
        MenuSidePanelAtBottom = "在底部显示";
        ButtonEdgeShades = "阴影";
        ButtonEdgeShadesTip = "为列表顶部和底部添加阴影";
        MenuViewerSideSwapped = "交换两侧";
        MenuDockOnRight = "固定到右侧边缘";
        MenuReserveWorkArea = "最大化时让出位置";
        MenuShortcuts = "快捷键";
        ShortcutSettingsTitle = "快捷键";
        ShortcutUnbound = "无";
        ShortcutPress = "请按下新快捷键…";
        ShortcutClear = "清除";
        ShortcutReset = "恢复默认";
        ShortcutHint = "点一行，再按下你想用的键。资源管理器那套 F2、Delete、Ctrl+C 保持不变。";
        ShortcutSearch = "搜索";
        ShortcutHelp = "帮助";
        ShortcutClosePanel = "关闭预览面板";
        ShortcutCollapseAll = "折叠全部文件夹";
        ShortcutNextItem = "下一个文件";
        ShortcutHistoryBack = "上一个文件夹";
        ShortcutHistoryForward = "下一个文件夹";
        ShortcutSlideshow = "幻灯片";
        ShortcutClock = "时钟";
        ShortcutSavePreset = "保存当前预设";
        MenuAutoDock = "自动停靠到屏幕边缘";
        MenuAutoHideCloseOnLeave = "鼠标离开时关闭";
        MenuAutoHideUseHandle = "使用把手而不是整条边缘";
        MenuAutoHideSlide = "滑动动画";
        MenuAutoHideSliverWidth = "自动隐藏宽度";
        MenuColorSettings = "颜色设置";
        ColorSwatchTooltip = "单击：颜色选择器 · 右键单击：输入颜色代码";
        ColorHexInputHint = "#RRGGBB · 回车应用，Esc 取消";
        MenuRestart = "重新启动";
        MenuHelp = "帮助";
        MenuAbout = "关于";
        HelpTitle = "Edgetree 帮助";
        MenuIconStyle = "图标样式";
        MenuIconStyleDefault = "默认";
        MenuIconStyleShell = "Windows 资源管理器";
        MenuDefaultSort = "默认排序";
        MenuSort = "排序方式";
        MenuSortByName = "名称";
        MenuSortByDate = "修改日期";
        MenuSortByType = "类型";
        MenuSortBySize = "大小";
        MenuSortAscending = "升序";
        MenuSortDescending = "降序";
        MenuFollowParentSort = "继承排序";
        MenuSearchInFolder = "在当前文件夹中搜索";
        SortTooltipFormat = "按 {0} 排序";
        SortModeFollowGlobal = "继承的排序";
        SortModeFolderGroup = "按文件夹分组";
        SortModeNameAsc = "名称升序";
        SortModeNameDesc = "名称降序";
        SortModeDateAsc = "日期升序";
        SortModeDateDesc = "日期降序";
        MenuFontSize = "字体大小（Ctrl +/-）";
        MenuMaxItemsPerFolder = "每个文件夹的项目数";
        MenuMaxItemsAll = "显示全部";
        MenuTabSpacing = "缩进宽度";
        MenuRowSpacing = "行间距";
        MenuScrollBarThickness = "滚动条宽度";
        MenuExportSettings = "导出设置…";
        MenuImportSettings = "导入设置…";
        MenuResetSettings = "重置所有设置…";
        ToolTipPinLeft = "固定到左侧";
        ToolTipPinRight = "固定到右侧";
        ToolTipPinAutoHide = "自动隐藏";
        ToolTipPinStayOpen = "固定并保持打开";
        ToolTipCollapseAll = "折叠所有文件夹（Shift+单击：不恢复）";
        ToolTipRestoreExpanded = "恢复展开的文件夹（Shift+单击：折叠，不恢复）";
        ToolTipOptions = "选项";
        ToolTipUpdateAvailable = "版本 {0} 可供下载";
        ToolTipMinimize = "最小化到托盘";
        ToolTipPutAway = "把应用程序收起来";
        ToolTipClose = "出口";
        RootPathLabel = "此电脑";
        MenuThumbnailMaxSize = "缩略图最大大小";
        ShowMoreFormat = "… 显示更多 {0} 个";
        ShowLessFormat = "… 隐藏 {0} 个";
        FilterHiddenFormat = "… 被筛选隐藏：{0}";
        HiddenFolderNoticeFormat = "… 排除的文件夹：{0}";
        FilterAndHiddenFormat = "… 被筛选隐藏：{0} · 排除的文件夹：{1}";
        FolderEmptyLabel = "… 空文件夹";
        ToolTipSearch = "搜索（Ctrl+F）";
        ToolTipViewer = "多媒体面板";
        ViewerZoomFit = "适合";
        ViewerZoomActual = "1:1";
        ViewerZoomFill = "填充";
        ViewerNavigator = "导航器";
        ViewerClose = "关闭多媒体面板";
        FooterNowPlayingOpen = "在多媒体面板中打开";
        FooterHeldFilmResume = "继续播放";
        ViewerNowPlayingLabel = "正在播放";
        ViewerBackToPlaying = "返回正在播放的曲目";
        MenuSetWallpaper = "设为桌面背景";
        TreeHistoryBack = "后退（Ctrl+←）";
        TreeHistoryForward = "前进（Ctrl+→）";
        TreeHistoryList = "你去过的文件夹（右键单击后退/前进）";
        ViewerPrevImage = "上一张图片";
        ViewerNextImage = "下一张图片";
        ViewerFilmstrip = "缩略图条";
        MenuFilmstripGrid = "缩略图网格";
        ViewerFullscreen = "全屏";
        DocumentPreview = "预览";
        DocumentEdit = "编辑";
        DocumentPopOut = "弹出查看";
        DocumentTooLarge = "文件太大，无法在这里打开。请用默认程序打开。";
        DocumentLoadFailed = "无法打开此文件。";
        DocumentSaveFailed = "未能保存。";
        DocumentPickFile = "在树中点选一个文件";
        DocumentNewTab = "新标签";
        GestureWheelClick = "滚轮单击";
        ViewerFullDesktop = "填满桌面";
        ViewerFullDesktopHint = "保持任务栏可见";
        MenuImageViewer = "多媒体面板";
        MenuPrecacheThumbnails = "预加载图片缩略图";
        MenuOpenMediaInViewer = "双击打开";
        MenuViewerFollowsSelection = "随选择展开";
        MenuSlideshow = "幻灯片放映";
        MenuSlideshowSeconds = "每张图片的秒数";
        MenuViewerClock = "时钟和日期";
        MenuViewerClockSize = "时钟大小";
        ViewerClockTimeFormat = "h:mm";
        ViewerClockDateFormat = "dddd, MMMM d";
        MenuClearThumbnailCache = "清理缩略图文件";
        MenuClearThumbnailCacheSized = "清理缩略图文件（{0}）";
        ViewerPrecaching = "正在预加载 {0}";
        ViewerMarkedCount = "已选中 {0} 个";
        ViewerMarkAdd = "视频书签";
        ViewerMarkList = "视频书签";
        ViewerRewind = "回到开头";
        ViewerPrevTrack = "上一曲";
        ViewerNextTrack = "下一曲";
        ViewerPrevVideo = "上一个视频";
        ViewerNextVideo = "下一个视频";
        ViewerZoom = "视频大小";
        ViewerZoomPicture = "图片大小";
        ViewerFitWindow = "将窗口适配到视频";
        ViewerSubtitles = "字幕";
        ViewerSubtitleSize = "字幕大小";
        ViewerSubtitlePosition = "字幕位置";
        ViewerSubtitleSync = "字幕同步（秒）";
        ViewerMarkClear = "清除全部";
        ViewerHdrToneMap = "HDR 色彩校正";
        ViewerHdrBrightness = "  亮度";
        ViewerHdrSaturation = "  饱和度";
        ViewerHdrContrast = "  对比度";
        ViewerMediaOpening = "正在打开…";
        ViewerMediaOpeningSlow = "正在打开… 这需要一点时间";
        ViewerMediaOpenGaveUp = "没有响应 — 已取消播放";
        ViewerMediaStalled = "正在等待文件…";
        ViewerPlay = "播放";
        ViewerPause = "暂停";
        ViewerStop = "停止";
        ViewerMute = "静音";
        ViewerRepeatOff = "不继续播放";
        ViewerRepeatAll = "循环当前文件夹";
        ViewerRepeatOne = "单曲循环";
        ViewerRepeatShuffle = "随机循环";
        ViewerRepeatHint = "右键单击可更改";
        ViewerBackgroundPlay = "后台播放 · 在其他文件夹中继续播放";
        ViewerFolderItemCount = "这里可以显示 {0} 个";
        ViewerFolderPlayAll = "播放此文件夹";
        ViewerPlaybackUnsupported = "此格式无法在此处播放 · 按 Enter 用默认应用程序打开它";
        ViewerPlaybackInterrupted = "播放已停止 · 按播放继续，按 Enter 用默认应用程序打开它";
        ViewerNoAudio = "没有声音";
        ViewerOpenExternally = "在默认应用程序中打开";
        ToolTipExitSearch = "返回资源管理器（Ctrl+E）";
        SearchTooltipBrowseFolder = "选择要搜索的文件夹";
        SearchTooltipRefresh = "重新索引";
        SearchTooltipRefreshStale = "重新索引 · 此文件夹已更改";
        SearchTooltipHistory = "最近搜索";
        SearchHistoryDeleteTooltip = "移除此搜索";
        SearchBrowseFolderDialogTitle = "选择要搜索的文件夹";
        SearchScopeNone = "选择要搜索的文件夹 →";
        SearchBoxPlaceholder = "搜索";
        PathBarPlaceholder = "输入路径，按 Enter";
        SearchStatusScanning = "正在建立索引… ({0}) · 现在可以搜索";
        SearchStatusResults = "找到 {0} 项";
        SearchStatusResultsCapped = "显示 {0} / {1}";
        SearchStatusNoResults = "没有结果";
        SearchStatusNoResultsCached = "没有结果 · 索引来自 {0} · 刷新以更新";
        SearchStatusIndexAgeSuffix = " · 索引来自 {0}";
        SearchStatusCached = "索引来自 {0} · 刷新以更新";
        SearchAgeJustNow = "刚刚";
        SearchAgeMinutes = "{0} 分钟前";
        SearchAgeHours = "{0} 小时前";
        SearchAgeDays = "{0}天前";
        SearchResultMissing = "该文件已删除或移动";
        SearchStatusTooBroad = "范围太宽 · 添加一个字母或数字";
        SearchStatusEmpty = "子字符串 · * ? 通配符 · ↑↓ 历史";
        SearchStatusScopeMissing = "文件夹缺失 · 刷新或重新选择";
        ColorSettingsTitle = "颜色设置";
        ColorLabelBackground = "资源管理器背景";
        ColorLabelFolderNameFont = "文件夹名称";
        ColorLabelFolderNameHighlightFont = "文件夹名称高亮";
        ColorLabelFileNameFont = "文件名";
        ColorLabelFileNameHighlightFont = "文件名高亮";
        ColorLabelSelection = "选中项";
        ColorLabelSelectionZone = "选中文件夹区域";
        ColorLabelHistory = "书签面板背景";
        ColorLabelHoverBackground = "鼠标悬停";
        ColorLabelFolderNameHoverFont = "文件夹名称悬停";
        ColorLabelFileNameHoverFont = "文件名悬停";
        ColorLabelShowMore = "显示更多";
        ColorLabelPanelNameFont = "书签名称";
        ColorLabelPanelNameHighlightFont = "书签名称高亮";
        ColorLabelPanelNameHoverFont = "书签名称悬停";
        ColorLabelGuideLine = "引导线";
        ColorLabelGuideLineActive = "引导线高亮";
        ColorLabelExpander = "展开箭头";
        ColorLabelFilterChipChecked = "筛选芯片开启";
        ColorLabelFilterChipCheckedFont = "芯片开启文字";
        ColorLabelFilterChipExclude = "排除芯片关闭文字";
        ColorLabelFilterChipExcludeChecked = "排除芯片开启";
        ColorLabelHeader = "标题栏背景";
        ColorLabelPanelDivider = "面板分隔线";
        ColorLabelViewerBackground = "多媒体面板背景";
        ColorLabelAutoHideHandle = "自动隐藏把手/边条";
        ButtonDefaults = "默认值";
        ButtonClose = "关闭";
        ButtonExportColors = "导出";
        ButtonImportColors = "导入";
        ColorFileFilter = "Edgetree 颜色 (*.json)|*.json";
        ColorFileDefaultName = "edgetree-colors.json";
        ColorImportFailedTitle = "导入颜色失败";
        ColorImportFailedBody = "该文件不包含颜色。";
        SettingsSaveFailedTitle = "设置保存失败";
        SettingsSaveFailedBody = "您的设置无法保存。您所做的更改将在应用关闭时丢失。\n\n{0}";
        ColorThemeDarkMode = "深色";
        ColorThemeLightMode = "浅色";
        ButtonRandomColors = "随机";
        ButtonMonoColors = "单色";
        ButtonMonoColorsTip = "一键灰阶";
        ColorChainTip = "链 · 一起点亮的行共享一种颜色";
        ButtonRandomColorsTip = "自然搭配的组合";
        ButtonDaringColors = "大胆";
        ButtonDaringColorsTip = "从主色调出发的更大胆组合";
        ButtonUndoRandom = "撤销";
        ColorThemeDarkLabel = "深色模式";
        ColorThemeLightLabel = "浅色模式";
        ColorResetConfirmTitle = "重置颜色";
        ColorResetConfirmBody = "这将重置您在 {0} 中设置的颜色。继续？";
        AboutTitle = "关于";
        AboutVersionLabel = "版本";
        AboutAuthorLabel = "作者";
        AboutDateLabel = "日期";
        AboutLicenseLabel = "许可摘要";
        AboutUpstreamLabel = "上游项目";
        AboutWebsiteLabel = "网站";
        AboutOtherToolLabel = "同一作者的其他工具";
        AboutUpdateAvailableFormat = "下载更新 {0}";
        AboutLicenseSummary = "MIT 许可证。按原样提供，不附带任何担保；是否使用由您自行决定。";
        AboutIconLicenseLabel = "图标";
        AboutIconLicenseValue = "文件和文件夹图标：Material Icon Theme (MIT)\n界面图标：Material Symbols，Google（Apache 许可证 2.0）";
        AboutIconLicenseOpen = "查看 Apache 许可证 2.0";
        TrayOpen = "打开";
        TrayHide = "隐藏到托盘";
        TrayAbout = "关于";
        TrayExit = "退出";
        UpdateAvailableRow = "新更新 - v{0}";
        PasteFailedTitle = "粘贴失败";
        MoveIntoSelfError = "文件夹不能移动到自身或其子文件夹中。";
        CopyIntoSelfError = "文件夹不能复制到自身或其子文件夹中。";
        NewFolderFailedTitle = "创建文件夹失败";
        RenameFailedTitle = "重命名失败";
        NewFolderDefaultName = "新建文件夹";
        RenameFailedBody = "无法重命名此项目。";
        DeleteConfirmTitle = "确认删除";
        DeleteConfirmBody = "将“{0}”移到回收站吗？";
        DeleteConfirmBodyMultiple = "将选中的 {0} 个项目移到回收站吗？";
        DeleteFailedShellBody = "无法删除。（错误 {0}）";
        DeleteNoRecycleBinTitle = "从网络位置删除";
        DeleteNoRecycleBinBody = "删除“{0}”吗？\n\n网络位置没有回收站，因此无法撤销。";
        DeleteNoRecycleBinBodyMultiple = "从网络位置删除 {0} 个项目吗？\n\n网络位置没有回收站，因此无法撤销。";
        DeleteHiddenInsideTitle = "包含排除文件夹";
        DeleteHiddenInsideBody = "“{0}” 将被删除。\n\n其中包含的排除文件夹：{1}\n\n仍要删除吗？";
        DeleteHiddenInsideBodyMultiple = "所选项目将被删除。\n\n其中包含的排除文件夹：{0}\n\n仍要删除吗？";
        DeleteFailedTitle = "删除失败";
        CompressFailedTitle = "压缩失败";
        ExtractFailedTitle = "解压失败";
        CompressSkippedBody = "{0} 个无法读取的项目已跳过。";
        StartWithWindowsFailedTitle = "随 Windows 启动";
        StartWithWindowsFailedBody = "注册为启动程序失败。可能受到管理员策略限制。";
        LanguageChangeTitle = "语言更改";
        LanguageChangeBody = "更改语言需要重新启动应用程序。现在重新启动？";
        ImportFailedTitle = "导入失败";
        OverwriteConfirmTitle = "确认覆盖";
        OverwriteConfirmBody = "“{0}” 已存在。要覆盖吗？";
        ExportSettingsFailedTitle = "导出设置失败";
        ImportSettingsFailedTitle = "导入设置失败";
        SettingsImportedTitle = "设置已导入";
        SettingsImportedBody = "设置已导入。需要重新启动应用程序才能应用它们。现在重新启动？";
        ResetSettingsConfirmTitle = "重置设置";
        ResetSettingsConfirmBody = "所有设置和书签都将重置为应用程序的默认状态。此操作无法撤销。\n\n该应用程序将随后重新启动以应用它。继续？";
        }
        else if (language == "en")
        {
        IsEnglish = true;
            MenuLanguage = "Language";
            LanguageRestartNote = "(Restart required)";
        MenuBookmark = "Bookmark";
        MenuBookmarkAdd = "Add bookmark";
        MenuBookmarkRemove = "Remove bookmark";
        MenuBookmarkList = "Bookmarks";
        MenuBookmarkListEmpty = "No bookmarks";
        MenuPresetAdd = "Add preset";
        MenuPresetApply = "Apply";
        MenuPresetOverwrite = "Overwrite…";
        MenuPresetRename = "Rename…";
        PresetSaveTitle = "Save current setup";
        PresetRenameTitle = "Rename preset";
        MenuPresetDelete = "Delete";
        PresetNameTitle = "Preset name";
        PresetNameHint = "Saves position, size, docking, colors, file types, and the current folder";
        PresetDefaultName = "Preset {0}";
        PresetSavedToast = "Preset saved";
        PlaceMissingTitle = "Item not found";
        PlaceMissingBody = "This item cannot be found.\n\n{0}\n\nRemove it from the list?";
        PresetSlotEmptyTitle = "Presets";
        PresetSlotEmptyBody = "There is no preset {0} yet.\nSave the current setup there?";

        MenuFileFilter = "File types";
        FilterChipExecutable = "Programs";
        MenuFileFilterAll = "All";
        MenuFileFilterCode = "Code";
        MenuFileFilterImage = "Images";
        MenuFileFilterDocument = "Documents";
        MenuFileFilterMedia = "Media";
        MenuFileFilterArchive = "Archives";
        MenuFileFilterExecutable = "Programs & shortcuts";
        MenuFileFilterOther = "Other";
        MenuFileFilterCustomEdit = "Custom…";
        FilterCustomTitle = "Custom extensions";
        FilterCustomHint = "Separate with commas · txt, png, .mp3";
        FilterCustomEmptyHint = "Leaving this empty removes the custom kind";
        MenuFileFilterExcludeEdit = "Exclude…";
        FilterExcludeTitle = "Excluded extensions";
        FilterExcludeHint = "Separate with commas · these are always hidden";
        FilterExcludeEmptyHint = "Leaving this empty removes the exclusion";
        ButtonOk = "OK";
        ButtonCancel = "Cancel";
        MenuFontWeight = "Font weight";
        MenuFontWeightNormal = "Normal";
        MenuFontWeightBold = "Bold";
        MenuFontWeightFoldersOnly = "Bold folders only";
        MenuFontWeightFilesOnly = "Bold files only";
        MenuSidePanel = "Bookmark panel";
        MenuSidePanelShow = "Show";
        MenuHideFolder = "Exclude";
        MenuHiddenFolderList = "Excluded folders";
        MenuHiddenFolderListEmpty = "No excluded folders";
        MenuUnhideFolder = "Stop excluding";
        MenuNetworkLocations = "Network locations";
        MenuNetworkLocationAdd = "Add location…";
        MenuNetworkLocationsEmpty = "No locations added";
        MenuNetworkLocationRemove = "Remove from list";
        NetworkLocationPromptTitle = "Add network location";
        NetworkLocationPromptHint =
            "Enter it as \\\\server\\share, or a folder path. Anything mapped to a drive letter is already in the list.";
        NetworkLocationUnreachableTitle = "Cannot connect";
        NetworkLocationUnreachableBody =
            "{0}\n\nThere was no response. Add it to the list anyway?";
        NetworkLocationDuplicateTitle = "Already in the list";
        MenuListRowRemove = "Remove";
        HiddenClearAllConfirmTitle = "Clear all exclusions";
        HiddenClearAllConfirmBody = "Show every excluded folder again? ({0})";
        MenuBookmarkClearAll = "Clear all";
        BookmarkClearAllConfirmTitle = "Clear all bookmarks";
        BookmarkClearAllConfirmBody = "Clear every bookmark? ({0})";
        BookmarkShortcutNext = "Next bookmark";
        BookmarkShortcutPrev = "Previous bookmark";
        MenuNewFolder = "New folder";
        MenuRefresh = "Refresh";
        MenuPathBarAtTop = "Show path bar at top";
        MenuExpandOnSingleClick = "Expand on a single click";
        MenuAutoCollapse = "Auto-collapse folders";
        MenuCollapseAllExpanded = "Collapse all folders";
        CollapseAllConfirmTitle = "Collapse all folders";
        CollapseAllConfirmBody = "Collapse all expanded folders?\n\nWhat was expanded is not remembered, so this cannot be undone.";
        MenuOpen = "Open in default app";
        MenuExpandFolder = "Expand";
        MenuCollapseFolder = "Collapse";
        MenuViewHere = "View";
        MenuPlayHere = "Play";
        MenuOpenWith = "Open with";
        MenuCut = "Cut";
        MenuCopy = "Copy";
        MenuPaste = "Paste";
        MenuSelectAll = "Select all";
        MenuCompress = "Compress";
        MenuExtract = "Extract";
        MenuRename = "Rename";
        MenuDelete = "Delete";
        MenuCopyPath = "Copy path";
        MenuMultiSelectionInfo = "{0} items selected";
        MenuOpenTerminal = "Open in terminal";
        MenuOpenWithCode = "Open with Code";
        MenuRevealInExplorer = "Reveal in Explorer";
        MenuRevealInTree = "Show in tree";
        GestureDoubleClick = "Double-click";
        MenuCreateShortcut = "Create shortcut";
        MenuProperties = "Properties";

        MenuAlwaysOnTop = "Always on top";
        MenuGeneralSettings = "General";
        MenuStartWithWindows = "Start with Windows";
        MenuAlwaysShowTrayIcon = "Always show tray icon";
        MenuShowHiddenItems = "Show hidden and system items";
        MenuShowFolderIcons = "Show folder icons";
        MenuShowFileIcons = "Show file icons";
        MenuShowDriveIcons = "Show drive icons";
        MenuTitleBarTitle = "Title bar text";
        MenuTitleBarMyComputerIcon = "Show This PC icon";
        MenuDragMoves = "Drag moves (hold Ctrl to copy)";
        MenuShowPanelDividers = "Panel dividers";
        MenuSidePanelAtBottom = "Show at bottom";
        ButtonEdgeShades = "Shading";
        ButtonEdgeShadesTip = "Veils the top and bottom ends of a list";
        MenuViewerSideSwapped = "Swap sides";
        MenuDockOnRight = "Pin to right edge";
        MenuReserveWorkArea = "Make room when others maximize";
        MenuShortcuts = "Keyboard shortcuts";
        ShortcutSettingsTitle = "Keyboard shortcuts";
        ShortcutUnbound = "None";
        ShortcutPress = "Press a new shortcut…";
        ShortcutClear = "Clear";
        ShortcutReset = "Reset";
        ShortcutHint = "Click a row, then press the keys you want. Explorer keys such as F2, Delete and Ctrl+C stay as they are.";
        ShortcutSearch = "Search";
        ShortcutHelp = "Help";
        ShortcutClosePanel = "Close preview panel";
        ShortcutCollapseAll = "Collapse all folders";
        ShortcutNextItem = "Next file";
        ShortcutHistoryBack = "Back";
        ShortcutHistoryForward = "Forward";
        ShortcutSlideshow = "Slideshow";
        ShortcutClock = "Clock";
        ShortcutSavePreset = "Save current preset";
        MenuAutoDock = "Auto-dock to screen edge";
        MenuAutoHideCloseOnLeave = "Close on mouse leave";
        MenuAutoHideUseHandle = "Handle instead of full edge";
        MenuAutoHideSlide = "Slide animation";
        MenuAutoHideSliverWidth = "Auto-hide thickness";
        MenuColorSettings = "Color settings";
        ColorSwatchTooltip = "Click: color picker · Right-click: enter a color code";
        ColorHexInputHint = "#RRGGBB · Enter to apply, Esc to cancel";
        MenuRestart = "Restart";
        MenuHelp = "Help";
        MenuAbout = "About";
        HelpTitle = "Edgetree help";
        MenuIconStyle = "Icon style";
        MenuIconStyleDefault = "Default";
        MenuIconStyleShell = "Windows Explorer";

        MenuDefaultSort = "Default sort";
        MenuSort = "Sort by";
        MenuSortByName = "Name";
        MenuSortByDate = "Date modified";
        MenuSortByType = "Type";
        MenuSortBySize = "Size";
        MenuSortAscending = "Ascending";
        MenuSortDescending = "Descending";
        MenuFollowParentSort = "Inherit sort";
        MenuSearchInFolder = "Search in this folder";
        SortTooltipFormat = "Sorted by {0}";
        SortModeFollowGlobal = "Inherited sort";
        SortModeFolderGroup = "Group by folder";
        SortModeNameAsc = "Name ascending";
        SortModeNameDesc = "Name descending";
        SortModeDateAsc = "Date ascending";
        SortModeDateDesc = "Date descending";
        MenuFontSize = "Font size (Ctrl +/-)";
        MenuMaxItemsPerFolder = "Items per folder";
        MenuMaxItemsAll = "Show all";
        MenuTabSpacing = "Indent width";
        MenuRowSpacing = "Row spacing";
        MenuScrollBarThickness = "Scrollbar width";
        MenuExportSettings = "Export settings...";
        MenuImportSettings = "Import settings...";
        MenuResetSettings = "Reset all settings...";

        ToolTipPinLeft = "Pin to left";
        ToolTipPinRight = "Pin to right";
        ToolTipPinAutoHide = "Auto hide";
        ToolTipPinStayOpen = "Pin open";
        ToolTipCollapseAll = "Collapse all folders (Shift+click: no restore)";
        ToolTipRestoreExpanded = "Restore expanded folders (Shift+click: collapse, no restore)";
        ToolTipOptions = "Options";
        ToolTipUpdateAvailable = "Version {0} available for download";
        ToolTipMinimize = "Minimize to tray";
        ToolTipPutAway = "Put the app away";
        ToolTipClose = "Exit";
        RootPathLabel = "This PC";
        MenuThumbnailMaxSize = "Max thumbnail size";
        ShowMoreFormat = "… Show {0} more";
        ShowLessFormat = "… Show {0} fewer";
        FilterHiddenFormat = "… Hidden by filter: {0}";
        HiddenFolderNoticeFormat = "… Excluded folders: {0}";
        FilterAndHiddenFormat = "… Hidden by filter: {0} · Excluded folders: {1}";
        FolderEmptyLabel = "… Empty";

        ToolTipSearch = "Search (Ctrl+F)";
        ToolTipViewer = "Multimedia panel";
        ViewerZoomFit = "Fit";
        ViewerZoomActual = "1:1";
        ViewerZoomFill = "Fill";
        ViewerNavigator = "Navigator";
        ViewerClose = "Close the multimedia panel";
        FooterNowPlayingOpen = "Open in the multimedia panel";
        FooterHeldFilmResume = "Resume";
        ViewerNowPlayingLabel = "Now playing";
        ViewerBackToPlaying = "Back to the playing track";
        MenuSetWallpaper = "Set as desktop background";
        TreeHistoryBack = "Back  (Ctrl+←)";
        TreeHistoryForward = "Forward  (Ctrl+→)";
        TreeHistoryList = "Folders you have been in  (right-click Back/Forward)";
        ViewerPrevImage = "Previous image";
        ViewerNextImage = "Next image";
        ViewerFilmstrip = "Thumbnail bar";
        MenuFilmstripGrid = "Thumbnail grid";
        ViewerFullscreen = "Full screen";
        DocumentPreview = "Preview";
        DocumentEdit = "Edit";
        DocumentPopOut = "Open in new window";
        DocumentTooLarge = "This file is too large to open here. Use the default app.";
        DocumentLoadFailed = "Could not open this file.";
        DocumentSaveFailed = "Could not save.";
        DocumentPickFile = "Click a file in the tree";
        DocumentNewTab = "New tab";
        GestureWheelClick = "Wheel click";
        ViewerFullDesktop = "Fill desktop";
        ViewerFullDesktopHint = "Keeps the taskbar visible";
        MenuImageViewer = "Multimedia panel";
        MenuPrecacheThumbnails = "Preload image thumbnails";
        MenuOpenMediaInViewer = "Open on double-click";
        MenuViewerFollowsSelection = "Expand on selection";
        MenuSlideshow = "Slideshow";
        MenuSlideshowSeconds = "Seconds per picture";
        MenuViewerClock = "Clock and date";
        MenuViewerClockSize = "Clock size";
        // 12-hour with no meridiem, which is what the lock screen itself
        // shows - the date underneath already says which day it is.
        ViewerClockTimeFormat = "h:mm";
        ViewerClockDateFormat = "dddd, MMMM d";
        MenuClearThumbnailCache = "Clean up thumbnail files";
        MenuClearThumbnailCacheSized = "Clean up thumbnail files ({0})";
        ViewerPrecaching = "Preloading {0}";
        ViewerMarkedCount = "{0} selected";
        ViewerMarkAdd = "Video bookmark";
        ViewerMarkList = "Video bookmarks";
        ViewerRewind = "Back to start";
        ViewerPrevTrack = "Previous track";
        ViewerNextTrack = "Next track";
        ViewerPrevVideo = "Previous video";
        ViewerNextVideo = "Next video";
        ViewerZoom = "Video size";
        ViewerZoomPicture = "Picture size";
        ViewerFitWindow = "Fit window to video";
        ViewerSubtitles = "Subtitles";
        ViewerSubtitleSize = "Subtitle size";
        ViewerSubtitlePosition = "Subtitle position";
        ViewerSubtitleSync = "Subtitle sync (sec)";
        ViewerMarkClear = "Clear all";
        ViewerHdrToneMap = "HDR color correction";
        ViewerHdrBrightness = "  Brightness";
        ViewerHdrSaturation = "  Saturation";
        ViewerHdrContrast = "  Contrast";
        ViewerMediaOpening = "Opening…";
        ViewerMediaOpeningSlow = "Opening… this is taking a while";
        ViewerMediaOpenGaveUp = "No response — playback canceled";
        ViewerMediaStalled = "Waiting for the file…";
        ViewerPlay = "Play";
        ViewerPause = "Pause";
        ViewerStop = "Stop";
        ViewerMute = "Mute";
        ViewerRepeatOff = "Don't continue";
        ViewerRepeatAll = "Repeat folder";
        ViewerRepeatOne = "Repeat one";
        ViewerRepeatShuffle = "Repeat shuffled";
        ViewerRepeatHint = "right-click to change";
        ViewerBackgroundPlay = "Background play · keeps going in other folders";
        ViewerFolderItemCount = "{0} can be shown here";
        ViewerFolderPlayAll = "Play this folder";
        ViewerPlaybackUnsupported = "This format can't be played here · Enter opens it in the default app";
        ViewerPlaybackInterrupted = "Playback stopped · press play to resume, Enter opens it in the default app";
        ViewerNoAudio = "No sound";
        ViewerOpenExternally = "Open in default app";
        ToolTipExitSearch = "Back to Explorer (Ctrl+E)";
        SearchTooltipBrowseFolder = "Choose folder to search";
        SearchTooltipRefresh = "Reindex";
        SearchTooltipRefreshStale = "Reindex · this folder has changed";
        SearchTooltipHistory = "Recent searches";
        SearchHistoryDeleteTooltip = "Remove this search";
        SearchBrowseFolderDialogTitle = "Choose a folder to search";
        SearchScopeNone = "Choose a folder to search →";
        SearchBoxPlaceholder = "Search";
        PathBarPlaceholder = "Type a path, press Enter";
        SearchStatusScanning = "Indexing… ({0}) · you can search now";
        SearchStatusResults = "{0} found";
        SearchStatusResultsCapped = "Showing {0} of {1}";
        SearchStatusNoResults = "No results";
        SearchStatusNoResultsCached = "No results · index from {0} · refresh to update";
        SearchStatusIndexAgeSuffix = " · index from {0}";
        SearchStatusCached = "Index from {0} · refresh to update";
        SearchAgeJustNow = "just now";
        SearchAgeMinutes = "{0} min ago";
        SearchAgeHours = "{0}h ago";
        SearchAgeDays = "{0}d ago";

        SearchResultMissing = "That file was deleted or moved";

        SearchStatusTooBroad = "Too broad · add a letter or digit";
        SearchStatusEmpty = "Substring · * ? wildcards · ↑↓ history";
        SearchStatusScopeMissing = "Folder missing · refresh or re-pick";

        ColorSettingsTitle = "Color settings";
        ColorLabelBackground = "Explorer background";
        ColorLabelFolderNameFont = "Folder name";
        ColorLabelFolderNameHighlightFont = "Folder name highlight";
        ColorLabelFileNameFont = "File name";
        ColorLabelFileNameHighlightFont = "File name highlight";
        ColorLabelSelection = "Selected item";
        ColorLabelSelectionZone = "Selected folder area";
        ColorLabelHistory = "Bookmark panel background";
        ColorLabelHoverBackground = "Mouse hover";
        ColorLabelFolderNameHoverFont = "Folder name mouse hover";
        ColorLabelFileNameHoverFont = "File name mouse hover";
        ColorLabelShowMore = "Show more";
        ColorLabelPanelNameFont = "Bookmark name";
        ColorLabelPanelNameHighlightFont = "Bookmark name highlight";
        ColorLabelPanelNameHoverFont = "Bookmark name mouse hover";
        ColorLabelGuideLine = "Guide line";
        ColorLabelGuideLineActive = "Guide line highlight";
        ColorLabelExpander = "Expand arrow";
        ColorLabelFilterChipChecked = "Filter chip on";
        ColorLabelFilterChipCheckedFont = "Chip on text";
        ColorLabelFilterChipExclude = "Exclude chip off text";
        ColorLabelFilterChipExcludeChecked = "Exclude chip on";
        ColorLabelHeader = "Title bar background";
        ColorLabelPanelDivider = "Panel divider";
        ColorLabelViewerBackground = "Multimedia panel background";
        ColorLabelAutoHideHandle = "Auto-hide handle/bar";
        ButtonDefaults = "Defaults";
        ButtonClose = "Close";
        ButtonExportColors = "Export";
        ButtonImportColors = "Import";
        ColorFileFilter = "Edgetree colors (*.json)|*.json";
        ColorFileDefaultName = "edgetree-colors.json";
        ColorImportFailedTitle = "Import colors";
        ColorImportFailedBody = "That file holds no colors.";
        SettingsSaveFailedTitle = "Settings";
        SettingsSaveFailedBody =
            "Your settings could not be saved. What you have changed will be gone when the app closes.\n\n{0}";
        ColorThemeDarkMode = "Dark";
        ColorThemeLightMode = "Light";
        ButtonRandomColors = "Random";
        ButtonMonoColors = "Mono";
        ButtonMonoColorsTip = "Grayscale in one press";
        // Missing outright until 2026-08-16, so the link marks on all seventeen
        // colour rows carried a Korean tooltip in an English app - a whole
        // feature explained in the wrong language, and the one feature here
        // that explains itself nowhere else.
        ColorChainTip = "Chain · rows lit together share a color";
        ButtonRandomColorsTip = "Combinations that go together";
        ButtonDaringColors = "Bold";
        ButtonDaringColorsTip = "Bolder combinations, starting from a primary hue";
        ButtonUndoRandom = "Undo";
        ColorThemeDarkLabel = "Dark mode";
        ColorThemeLightLabel = "Light mode";
        ColorResetConfirmTitle = "Reset colors";
        ColorResetConfirmBody = "This will reset the colors you've set in {0}. Continue?";

        AboutTitle = "About";
        AboutVersionLabel = "Version";
        AboutAuthorLabel = "Author";
        AboutDateLabel = "Date";
        AboutLicenseLabel = "License summary";
        AboutUpstreamLabel = "Upstream project";
        AboutWebsiteLabel = "Website";
        AboutOtherToolLabel = "Other tools by the same maker";
        AboutUpdateAvailableFormat = "Download update {0}";
        AboutLicenseSummary =
            "MIT License. Provided as-is, without warranty; use is at your own discretion.";
        AboutIconLicenseLabel = "Icons";
        AboutIconLicenseValue =
            "File and folder icons: Material Icon Theme (MIT)\n" +
            "Interface glyphs: Material Symbols, Google (Apache License 2.0)";
        AboutIconLicenseOpen = "Read the Apache License 2.0";

        TrayOpen = "Open";
        TrayHide = "Send to tray";
        TrayAbout = "About";
        TrayExit = "Exit";
        UpdateAvailableRow = "New update - v{0}";

        PasteFailedTitle = "Paste failed";
        MoveIntoSelfError = "A folder can't be moved into itself or into one of its own subfolders.";
        CopyIntoSelfError = "A folder can't be copied into itself or into one of its own subfolders.";
        NewFolderFailedTitle = "Failed to create folder";
        RenameFailedTitle = "Rename failed";
        NewFolderDefaultName = "New folder";
        RenameFailedBody = "Could not rename this item.";
        DeleteConfirmTitle = "Confirm delete";
        DeleteConfirmBody = "Send \"{0}\" to the Recycle Bin?";
        DeleteConfirmBodyMultiple = "Send {0} selected items to the Recycle Bin?";
        DeleteFailedShellBody = "Could not delete. (error {0})";
        DeleteNoRecycleBinTitle = "Delete from a network location";
        DeleteNoRecycleBinBody =
            "Delete \"{0}\"?\n\nA network location has no Recycle Bin, so this cannot be undone.";
        DeleteNoRecycleBinBodyMultiple =
            "Delete {0} items from a network location?\n\nThere is no Recycle Bin, so this cannot be undone.";
        DeleteHiddenInsideTitle = "Excluded folders inside";
        // THREE PARTS, not two sentences: what is going, what is inside it,
        // then the question. English has no plural machinery here, and a count
        // on a label line agrees with any number where "{1} hidden folders"
        // would read "1 hidden folders" (review, 2026-08-27). The label also
        // repeats this box's own title, which ties the two together.
        DeleteHiddenInsideBody =
            "\"{0}\" will be deleted.\n\nExcluded folders inside: {1}\n\nDelete anyway?";
        DeleteHiddenInsideBodyMultiple =
            "The selected items will be deleted.\n\nExcluded folders inside: {0}\n\nDelete anyway?";
        DeleteFailedTitle = "Delete failed";
        CompressFailedTitle = "Compress failed";
        ExtractFailedTitle = "Extract failed";
        CompressSkippedBody = "Skipped {0} that could not be read.";
        StartWithWindowsFailedTitle = "Start with Windows";
        StartWithWindowsFailedBody = "Failed to register as a startup program. It may be restricted by administrator policy.";
        LanguageChangeTitle = "Change language";
        LanguageChangeBody = "Changing the language requires restarting the app. Restart now?";
        ImportFailedTitle = "Import failed";
        OverwriteConfirmTitle = "Confirm overwrite";
        OverwriteConfirmBody = "\"{0}\" already exists. Overwrite it?";

        ExportSettingsFailedTitle = "Failed to export settings";
        ImportSettingsFailedTitle = "Failed to import settings";
        SettingsImportedTitle = "Settings imported";
        SettingsImportedBody = "Settings were imported. Restarting the app is required to apply them. Restart now?";

        ResetSettingsConfirmTitle = "Reset settings";
        ResetSettingsConfirmBody = "All settings and bookmarks will be reset to the app's default state. This cannot be undone.\n\nThe app will restart afterward to apply it. Continue?";
        }

#if INSTRUMENT
        RootPathLabel += " (INSTRUMENTED)";
#elif DEBUG
        RootPathLabel += " (DEBUG)";
#endif
    }
}

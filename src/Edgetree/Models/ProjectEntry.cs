namespace SidebarExplorer.App.Models;

/// <summary>
/// A user-defined project root. Removing or renaming a project never changes
/// anything on disk; the path is only a saved tree entry.
/// </summary>
public sealed class ProjectEntry
{
    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
}

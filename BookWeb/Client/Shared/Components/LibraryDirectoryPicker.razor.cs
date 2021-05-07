using BookWeb.Client.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookWeb.Client.Shared.Components
{
    public partial class LibraryDirectoryPicker
    {
        [Parameter]
        public string Height { get; set; }

        [Parameter]
        public string Width { get; set; }

        [Inject]
        public IFileSystemService FileSystemService { get; set; }

        private TreeItemData _activatedValue;
        private TreeItemData ActivatedValue
        {
            get
            {
                return _activatedValue;
            }

            set
            {
                if (value.IsLibraryFolder)
                    _activatedValue = value;
            }
        }
        private HashSet<TreeItemData> TreeItems { get; set; } = new HashSet<TreeItemData>();

        public class TreeItemData
        {
            public string Path { get; set; }
            public string Name { get; set; }
            public string Icon { get; set; }
            public bool IsLibraryFolder { get; set; }
            public MudBlazor.Color Color { get; set; }

            public HashSet<TreeItemData> TreeItems { get; set; }
            public TreeItemData() { }
            public TreeItemData(string name, string path, string icon, MudBlazor.Color color, bool expandable)
            {
                Name = name;
                Path = path;
                Icon = icon;
                Color = color;
                if (expandable)
                {
                    TreeItems = new HashSet<TreeItemData>();
                    TreeItemData node = new TreeItemData();
                    node.Name = "?";
                    node.Icon = Icons.Filled.HelpOutline;
                    node.Color = MudBlazor.Color.Default;
                    TreeItems.Add(node);
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            List<string> drives = await FileSystemService.GetDrives();
            foreach (string drive in drives)
            {
                TreeItemData node = new TreeItemData(drive, drive, Icons.Filled.Storage, MudBlazor.Color.Default, true);
                TreeItems.Add(node);
            }

        }

        private async Task TreeExpanded(bool expanded, TreeItemData d)
        {
            if (expanded)
            {
                List<string> directories = await FileSystemService.GetDirectories(d.Path, "metadata.db");
                d.TreeItems.Clear();
                foreach (var dir in directories)
                {
                    TreeItemData node;
                    string name = Path.GetFileName(dir);
                    if (dir.EndsWith('*'))
                    {
                        node = new TreeItemData(name.Remove(name.Length - 1), dir.Remove(dir.Length - 1), Icons.Filled.LocalLibrary, Color.Error, false);
                        node.IsLibraryFolder = true;
                    }
                    else
                    {
                        node = new TreeItemData(name, dir, Icons.Filled.Folder, Color.Default, true);
                        node.IsLibraryFolder = false;
                    }
                    d.TreeItems.Add(node);
                }
            }
        }
    }
}

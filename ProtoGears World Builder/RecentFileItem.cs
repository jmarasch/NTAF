using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoGears_World_Builder {
    class RecentFileItem : Fluent.MenuItem {
        public string FilePath { get; set; }
        public string FileName { get { return System.IO.Path.GetFileName(FilePath); } }
        public RecentFileItem(string path) : base() {
            FilePath = path;
            //Header = FileName + Environment.NewLine + path;
            Header = FileName;
            ToolTip = FilePath;
            Icon = "Images/Icons/File.ico";
            }
        }
    }

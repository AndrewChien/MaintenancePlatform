using System.Collections.Generic;
using System.Windows.Markup;

namespace ZNC.Utility.Command
{
    [ContentProperty("Children")]
    public class CommandGroup
    {
        List<CommandBinding> _children = new List<CommandBinding>();
        public List<CommandBinding> Children
        {
            get { return _children; }
        }
    }
}

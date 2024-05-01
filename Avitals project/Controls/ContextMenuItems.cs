using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avitals_project.Controls
{
    public class ContextMenuItems : ObservableCollection<ContextMenuItem>
    {
        public ContextMenuItem this[string text] => FindTextIndex(text);

        private ContextMenuItem FindTextIndex(string text)
        {
            for (int j = 0; j < Items.Count; j++)
            {
                if (Items[j].Text == text)
                {
                    return Items[j];
                }
            }

            throw new ArgumentOutOfRangeException(
                nameof(text),
                $"Item with  text {text} was not present");
        }
    }
}

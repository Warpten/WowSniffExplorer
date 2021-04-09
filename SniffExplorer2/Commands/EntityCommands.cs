using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SniffExplorer.UI.Commands
{
    public static class EntityCommands
    {
        public static RoutedUICommand ShowMovement { get; } = new("View movement", "View movement", typeof(EntityCommands));

    }
}

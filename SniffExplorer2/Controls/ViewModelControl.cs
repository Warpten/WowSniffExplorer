using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SniffExplorer.UI.Controls
{
    public interface IViewModelControl<T>
    {
        public T ViewModel { get; }
    }
}

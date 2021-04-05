using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SniffExplorer.Generators.Utilities
{
    internal partial class DisposableAction : IDisposable
    {
        public DisposableAction(Action action)
        {
            Action = action;
        }

        public Action Action { get; private set; }

        #region IDisposable Members

        public void Dispose()
        {
            Action();
        }

        #endregion
    }
}

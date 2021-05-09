using SniffExplorer.Shared.Attributes.UI;

namespace SniffExplorer.Controls.Models
{
    public partial class FileSelectionModel
    {
        [NotifyingProperty(PropertyName = "FilePath")]
        private string? _filePath;

        [NotifyingProperty(PropertyName = "DiscardUpdateFields")]
        private bool _discardUpdateFields = false;

        [NotifyingProperty(PropertyName = "DiscardUnknownEntities")]
        private bool _discardUnknownEntities = true;
    }
}

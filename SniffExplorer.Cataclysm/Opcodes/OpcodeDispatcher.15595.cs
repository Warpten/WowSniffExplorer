using SniffExplorer.Parsing.Attributes;
using SniffExplorer.Parsing.Helpers.Opcodes;

namespace SniffExplorer.Cataclysm.Opcodes
{
    [OpcodeResolver]
    public partial class OpcodeResolver : BaseOpcodeResolver<OpcodeResolver>
    {
        [OpcodeResolver]
        public void InitializeCataclysm()
        {
            LoadDatabase(Resource.Opcodes_Cataclysm);
        }
    }
}

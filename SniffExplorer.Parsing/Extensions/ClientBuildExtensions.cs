using SniffExplorer.Parsing.Versions;

namespace SniffExplorer.Parsing.Extensions
{
    public static class ClientBuildExtensions
    {
        public static bool Predates(this ClientBuild self, ClientBuild other)
        {
            // Different realm type.
            if (self.ExpansionType != other.ExpansionType)
                return false;

            // Expansion predates
            if (self.Expansion < other.Expansion)
                return true;

            // Within expansion, build predates
            if (self.Expansion == other.Expansion)
                return self.Value < other.Value;

            // Expansion postdates
            return false;
        }
    }
}
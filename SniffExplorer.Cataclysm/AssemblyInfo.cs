using System.Runtime.InteropServices;
using SniffExplorer.Parsing.Attributes;
using SniffExplorer.Parsing.Versions;
using SniffExplorer.Shared.Enums;

// In SDK-style projects such as this one, several assembly attributes that were historically
// defined in this file are now automatically added during build and populated with
// values defined in project properties. For details of which attributes are included
// and how to customise this process see: https://aka.ms/assembly-info-properties


// Setting ComVisible to false makes the types in this assembly not visible to COM
// components.  If you need to access a type in this assembly from COM, set the ComVisible
// attribute to true on that type.

[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM.

[assembly: Guid("1ff5beb7-1146-426e-b5e2-491333ab8c9c")]

// This assembly is in charge of handling Cataclysm era sniffs.
[assembly: Expansion(Expansion = Expansion.Cataclysm, RealmExpansionType = RealmExpansionType.Retail)]
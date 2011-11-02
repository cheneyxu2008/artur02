using System.Collections.Generic;
using Microsoft.Cci;

namespace ClassInspectorWpf.Models
{
    public class AssemblyInspected
    {
        public string Path { get; set; }
        public IEnumerable<INamedTypeDefinition> AllTypes { get; set; }
    }
}

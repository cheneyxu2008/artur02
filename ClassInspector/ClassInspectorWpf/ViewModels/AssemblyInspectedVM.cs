using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using ClassInspectorWpf.Models;
using Microsoft.Cci;

namespace ClassInspectorWpf.ViewModels
{
    public class AssemblyInspectedVM : INotifyPropertyChanged
    {
        private AssemblyInspected _assemblyInspected;

        public AssemblyInspected AssemblyInspected
        {
            get { return _assemblyInspected; }
            set
            {
                _assemblyInspected = value;

                using (var host = new PeReader.DefaultHost())
                {
                    var assembly =
                        host.LoadUnitFrom(
                            _assemblyInspected.Path)
                        as IAssembly;

                    if (assembly != null)
                    {
                        _assemblyInspected.AllTypes = assembly.GetAllTypes();
                    }
                }
            }
        }

        public List<string> TypeNames { 
            get { return new List<string>()
                             {
                                 "aaa",
                                 "bbb"
                             }; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}

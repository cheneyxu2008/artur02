using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassInspectorWpf.Models;
using ClassInspectorWpf.ViewModels;
using Microsoft.Win32;

namespace ClassInspectorWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AssemblyInspectedVM _assemblyVM = new AssemblyInspectedVM();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openAssemblyDialog = new OpenFileDialog()
                                                    {
                                                        CheckFileExists = true,
                                                        Filter = "All files|*.*|DLLs|*.dll|Executables|*.exe"
                                                    };
            if (openAssemblyDialog.ShowDialog(this).GetValueOrDefault())
            {
                var _assemblyInspected = new AssemblyInspected();
                _assemblyInspected.Path = openAssemblyDialog.FileName;
                _assemblyVM.AssemblyInspected = _assemblyInspected;

                listBox1.ItemsSource = _assemblyVM.TypeNames;
            }
        }
    }
}

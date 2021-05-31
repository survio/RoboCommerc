using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RoboCommerc
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public List<Tuple<string, IEnumerable<string>>> testCollection;
        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            string[] assemblies = Directory.GetFiles($"{PathLibraryTextBox.Text}", "*.dll");
            testCollection = new List<Tuple<string, IEnumerable<string>>>();
            Parallel.ForEach(assemblies, assemblyPath =>
            {
                var loadAssembly = Assembly.LoadFile(assemblyPath);
                var classesFromAssembly = loadAssembly.GetTypes().Where(x => x.IsClass);
                foreach (var classType in classesFromAssembly)
                {
                    testCollection.Add(Tuple.Create(classType.Name, classType.GetMethods().Where(x => x.IsPublic || x.IsFamily).Select(x => x.Name)));
                }
            });
            ItemsControl.ItemsSource = testCollection;
        }
    }
}

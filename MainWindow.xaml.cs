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
        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            string[] assemblies = Directory.GetFiles($"{PathLibraryTextBox.Text}", "*.dll");
            ItemsControl.ItemsSource = getTypeMethodFromAssembliesByCondition(assemblies,type => type.IsClass,method => method.IsFamily || method.IsPublic).OrderBy(x=>x.Name);
        }
        private IEnumerable<TypeWrapper> getTypeMethodFromAssembliesByCondition(string[] assemblies, Func<Type,bool> typeSelector, Func<MethodInfo,bool> methodSelector)
        {
            foreach (var path in assemblies)
            {
                foreach (var classType in Assembly.LoadFile(path).GetTypes().Where(typeSelector))
                {
                    yield return new TypeWrapper(classType, methodSelector);
                }
            }
        }
    }
}

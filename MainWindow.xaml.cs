using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

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
            if (!Directory.Exists($"{PathLibraryTextBox.Text}")) return;
            string[] assemblies = Directory.GetFiles($"{PathLibraryTextBox.Text}", "*.dll");
            if (assemblies.Length <= 0) return;
            ItemsControl.ItemsSource = getTypeMethodFromAssembliesByCondition(assemblies,type => type.IsClass,method => method.IsFamily || method.IsPublic).OrderBy(x=>x.Name);
        }
        private IEnumerable<TypeWrapper> getTypeMethodFromAssembliesByCondition(string[] assembliesPaths, Func<Type,bool> typeSelector, Func<MethodInfo,bool> methodSelector)
        {
            foreach (var path in assembliesPaths)
            {
                foreach (var classType in Assembly.LoadFile(path).GetTypes().Where(typeSelector))
                {
                    yield return new TypeWrapper(classType, methodSelector);
                }
            }
        }
    }
}

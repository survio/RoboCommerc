using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
            if(assemblies.Length<=0) return;
            ItemsControl.ItemsSource = getConditionType(assemblies, type => type.IsClass).OrderBy(x=>x.Name);
        }
        private IEnumerable<Type> getConditionType(string[] assembliesPaths, Func<Type, bool> typeCondition)
        {
            foreach (var assemblyPath in assembliesPaths)
            {
                var loadAssembly = Assembly.LoadFile(assemblyPath);
                foreach (var classType in loadAssembly.GetTypes().Where(typeCondition))
                {
                    yield return classType;
                }
            }
        }
    }
}


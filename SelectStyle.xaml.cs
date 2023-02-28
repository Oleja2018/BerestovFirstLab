using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BerestovFirstLab
{
    /// <summary>
    /// Логика взаимодействия для SelectStyle.xaml
    /// </summary>
    public partial class SelectStyle : Window
    {
        public SelectStyle()
        {
            InitializeComponent();
            // Создаем коллекцию 
            List<string> styles = new List<string> { "light", "Dark" };
            // При выборе начинает выполняться процедура
            styleBox.SelectionChanged += ThemeChange;
            // загрузка данных
            styleBox.ItemsSource = styles;
            // первоначально выбранные
            styleBox.SelectedItem = "light";
        }
        void ThemeChange(object sender, SelectionChangedEventArgs e)
        {
            string style = styleBox.SelectedItem as string;
            // определяем путь к файлу ресурсов
            var uri = new Uri(@"ResourceDictionary\" + style + ".xaml", UriKind.Relative);
            // загружаем словарь ресурсов
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            // очищаем коллекцию ресурсов приложения
            Application.Current.Resources.Clear();
            // добавляем загруженный словарь ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }
    }
}
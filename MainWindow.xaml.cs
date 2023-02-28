using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using MenuItem = System.Windows.Controls.MenuItem;
using MessageBox = System.Windows.MessageBox;


namespace BerestovFirstLab
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        // Коллекция объектов изображений(Класс GetImage, файл Image)
        public ObservableCollection<GetImage> GetImages { get; set; }
        // Доп функционал, напрямую не используется
        internal List<string> ListHistory = new List<string>();


        public MainWindow()
        {
            InitializeComponent();
            //Процедура загрузки картинок из ассетов
            LoadDefaultImage();
        }


        /// <summary>
        /// Загрузка стандартных картинок, по уму надо бы переписать процедуру
        /// </summary>
        private void LoadDefaultImage()
        {
            // Получаем путь
            string pathFile = Environment.CurrentDirectory;
            // Приводим к нашему типу(да да можно было просто включить папку в проект)

            string pathFileEditBin = pathFile.Replace(@"bin\Debug", "Assets");
            string pathFileEditRelease = pathFile.Replace(@"bin\Release", "Assets");
            pathFileEditBin = pathFileEditBin + Convert.ToString(@"\Images");
            pathFileEditRelease = pathFileEditRelease + Convert.ToString(@"\Images");
            // проверка существования пути
            if (Directory.Exists(pathFileEditBin))
            {
                @txbPath.Text = pathFileEditBin;
            }
            else {
                @txbPath.Text = pathFileEditRelease;
            }
            // меняем значение текст бокса, вызываем нашу функцию поиска

            GetInfoFile();

        }
        /// <summary>
        /// Выбор пути до нашей папки
        /// </summary>
        private void WayHandlerClick()
        {

            // Выбор папки, это попа
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Проверка на дубликаты, есть возможность просмотра нескольких папок
                txbPath.Text = folderBrowser.SelectedPath;
            }
            //Загрузка истории вызовов
            HistoryChoise(txbPath.Text);
            // Поиск картинок и их загрузка в коллекцию
            GetInfoFile();
        }

        /// <summary>
        /// Поиск файлов в папке
        /// </summary>
        private void GetInfoFile() {

            /* Поиск по каталогу атрубут SearchOption.TopDirectoryOnly обязателен,
            тк нам не нужно искать в подкаталогах */
            if (@txbPath.Text != "Путь файла: ")
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(@txbPath.Text);
                GetImages = new ObservableCollection<GetImage>();
                // Ищем все файлы по нашему условию
                FileInfo[] files = directoryInfo.GetFiles("*.jpg", SearchOption.TopDirectoryOnly);

                // Заполняем GetImage
                foreach (FileInfo file in files)
                {
                    GetImages.Add(new GetImage(file.Name, file.FullName));
                }
                // Переносим данные
                listNameImg.ItemsSource = GetImages;
                // Заполняем наш листбокс

                // Проверка на наличие картинок
                if (GetImages.DefaultIfEmpty() != null) { }
                else { MessageBox.Show("Картинок в формате .jpg не обнаружено"); }
            }

        }
        /// <summary>
        /// Очистка программы от выбранных файлов и изображенний
        /// </summary>
        private void ClearChoise() {
            if (txbPath.Text != "Путь файла: ") {
                ClearImageBox();
                ClearListNameImg();
            }

        }
        // Доп функция очистки листа
        private void ClearListNameImg()
        {
            // Стандартная функция CLEAR не работает, если используется напрямую итем соурс
            listNameImg.ItemsSource = null;
        }

        //Доп функция очистки изображения
        private void ClearImageBox()
        {
            // Стандартная функция CLEAR не работает, если используется напрямую итем соурс
            ImageBox.Source = null;
        }

        /// <summary>
        /// История выбора, доп функционал, напрямую не используется
        /// </summary>
        /// <param name="path"></param>
        private void HistoryChoise(string path) {
            if (path != null) {
                ListHistory.Add(path);
                for (int i = 1; i < ListHistory.Count(); i++)
                {
                    if (ListHistory[i] == ListHistory[i - 1])
                    {
                        ClearChoise();
                    }
                }
            }
        }

        /// <summary>
        /// Логика менюшки с использованием switch case, заменена вызовом событий напрямую, но часть еще осталась
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandlerClick(object sender, RoutedEventArgs e) {

            // Создаем объект класса Меню итем(передаем наш объект)
            MenuItem menuItemFile = (MenuItem)sender;

            // Делаем выборку из нашего меню, используем свойство x:name,
            // по нему проверяем какая кнопка

            switch (menuItemFile.Name.ToString())
            {
                case "Way":
                    WayHandlerClick();
                    break;
                case "Styles":
                    LoadStylesRes();
                    break;
            }

        }
        // Случайно созданная процедура, два раза кликнул по менюшке
        private void MenuItem_Click(object sender, RoutedEventArgs e) { }
        /// <summary>
        /// Реализация логики получения информации об размерах элементов
        /// </summary>
        private void GetInfoElement() {
            MessageBox.Show(Convert.ToString($"ImageBox: Height: {ImageBox.ActualHeight}  Width: {ImageBox.ActualWidth}"));
            MessageBox.Show(Convert.ToString($"listNameImg: Height: {listNameImg.ActualHeight}  Width: {listNameImg.ActualWidth}"));
            MessageBox.Show(Convert.ToString($"txbPath: Height: {txbPath.ActualHeight}  Width: {txbPath.ActualWidth}"));


        }

        /// <summary>
        /// Очистить все, на меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Know_Click(object sender, RoutedEventArgs e)
        {
            ClearChoise();
        }
        // Процедура вызова второго окна
        private void LoadStylesRes() {
            SelectStyle taskWindow = new SelectStyle();
            taskWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            taskWindow.AllowDrop = true;
            taskWindow.Show();
        }
        private void listNameImg_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void listNameImg_GotMouseCapture_1(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }


        /// <summary>
        /// Загрузка нашего изображения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listNameImg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Создать элемент изображения
            GetImage imageView = listNameImg.SelectedItem as GetImage;


            if (imageView != null)
            {
                // Генерируем данные
                BitmapImage bi3 = new BitmapImage();
                // начинаем выполнение
                bi3.BeginInit();
                // Откуда брать данные
                bi3.UriSource = new Uri(imageView.Path, UriKind.Absolute);
                // конец выполнения
                bi3.EndInit();
                // Передаем данные 
                ImageBox.Source = bi3;

            }
        }

        private void GetInfoElements_Click(object sender, RoutedEventArgs e)
        {
            GetInfoElement();
        }

        private void GetInfoSizeScreen_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Convert.ToString($"Height: {BodyScreen.ActualHeight} Width:{BodyScreen.ActualWidth}"));
        }
        /// <summary>
        /// Подобие адаптивности, но я устал
        /// </summary>
        private void MediaQuary() {

            int Height = Convert.ToInt32(BodyScreen.ActualHeight);
            int Width = Convert.ToInt32(BodyScreen.ActualWidth);

            if (Height > 411 & Width > 781) {
                ImageBox.Height = 500;
                ImageBox.Width = 800;
                listNameImg.Height = ImageBox.Height;
                listNameImg.Width = 280;
                txbPath.Width = listNameImg.Width;
                listNameImg.Margin =  new Thickness(21,76,0,50); 
            }
            
        }
        /// <summary>
        /// При изменение разрещшения вызвать изменение элементов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MediaQuary();
        }
    }
}

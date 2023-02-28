using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Microsoft.Win32;
using MenuItem = System.Windows.Controls.MenuItem;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;


namespace BerestovFirstLab
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        public ObservableCollection<GetImage> GetImages { get; set; }
        internal List<string> ListHistory = new List<string>();
        

        public MainWindow()
        {
            InitializeComponent();
            LoadDefaultImage();
        }
     
        private void GetInfoFile() {

            /* Поиск по каталогу атрубут SearchOption.TopDirectoryOnly обязателен,
            тк нам не нужно искать в подкаталогах */
            if (@txbPath.Text != "Путь файла: ") 
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(@txbPath.Text);
                GetImages = new ObservableCollection<GetImage>();
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

        // Выбор изображения, и его загрузка
        private void SetChoise() {
            MessageBox.Show("Появилась картинка");
        }
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

        private void WayHandlerClick() {

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
                case "Main":
                    //MessageBox.Show("Вызван - Main");
                    break;
                case "Know":
                    //MessageBox.Show("Вызван - Know");
                    break;
                case "BackgroundStyle":
                    //MessageBox.Show("Вызван - BackgroundStyle");
                    break;
                case "TextStyle":
                    //MessageBox.Show("Вызван - TextStyle");
                    break;
                case "Styles":
                    //MessageBox.Show("Вызван - Styles");
                    LoadStylesRes();

                    break;
            }

        }
        private void MenuItem_Click(object sender, RoutedEventArgs e) { }
        private void GetInfoElement() {
            MessageBox.Show(Convert.ToString($"ImageBox: Height: {ImageBox.ActualHeight}  Width: {ImageBox.ActualWidth}"));
            MessageBox.Show(Convert.ToString($"listNameImg: Height: {listNameImg.ActualHeight}  Width: {listNameImg.ActualWidth}"));
            MessageBox.Show(Convert.ToString($"txbPath: Height: {txbPath.ActualHeight}  Width: {txbPath.ActualWidth}"));
       

        }

        private void Know_Click(object sender, RoutedEventArgs e)
        {
            ClearChoise();
        }
        private void LoadStylesRes() {
            SelectStyle taskWindow = new SelectStyle();
            taskWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            taskWindow.AllowDrop = true;
            taskWindow.Show();
        }
        private void listNameImg_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SetChoise();
        }

        private void listNameImg_GotMouseCapture_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SetChoise();
        }
        private void LoadDefaultImage()
        {
            string pathFile = Environment.CurrentDirectory;
            string pathFileEdit = pathFile.Replace(@"bin\Debug", "Assets");
            // C:\C#\Berestov\BerestovFirstLab\Assets\Images
            pathFileEdit = pathFileEdit + Convert.ToString(@"\Images");

            @txbPath.Text = pathFileEdit;
            GetInfoFile();

        }

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

        private void MediaQuary() {

            int Height = Convert.ToInt32(BodyScreen.ActualHeight);
            int Width = Convert.ToInt32(BodyScreen.ActualWidth);

            if (Height > 411 & Width > 781) {
                ImageBox.Height = 500;
                ImageBox.Width = 800;
                listNameImg.Height = ImageBox.Height;
                listNameImg.Width = 280;
                txbPath.Width = listNameImg.Width;


            }
            

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MediaQuary();
        }
    }
}

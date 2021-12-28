namespace Chat.DesktopClient.Views
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Logger.Init();
            InitializeComponent();
        }
    }
}

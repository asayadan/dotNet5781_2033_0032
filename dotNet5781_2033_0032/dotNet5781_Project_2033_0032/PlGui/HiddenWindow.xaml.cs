using System;
using System.IO;
using System.Windows;
namespace PlGui
{
    /// <summary>
    /// Interaction logic for HiddenWindow.xaml
    /// </summary>
    public partial class HiddenWindow : Window
    {
        static int num_jokes = 222;
        public HiddenWindow()
        {
            InitializeComponent();
        }

        private void Cool_Button_Click(object sender, RoutedEventArgs e)
        {
            string dir = Directory.GetCurrentDirectory();
            string[] lines = System.IO.File.ReadAllLines(Directory.GetParent(dir) + "/PlGui/jokefile.txt");
            Random rnd = new Random();
            int joke = (rnd.Next()) % num_jokes;
            MessageBox.Show(lines[joke], "AN AMAZING JOKE:", MessageBoxButton.OK, MessageBoxImage.Exclamation);
    
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for MenagmentWindow.xaml
    /// </summary>
    public partial class MenagmentWindow : Window
    {
        string username 
        public MenagmentWindow(string user)
        {
            username = user;
            InitializeComponent();
        }
    }
}

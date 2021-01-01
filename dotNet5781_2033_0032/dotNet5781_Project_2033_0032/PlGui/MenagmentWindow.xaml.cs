using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using BLAPI;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for MenagmentWindow.xaml
    /// </summary>
    public partial class MenagmentWindow : Window
    {

        IBL bl;
        string username; 
        public MenagmentWindow( IBL _bl ,string user)
        {
            username = user;
            bl = _bl;
            InitializeComponent();
        }
    }
}

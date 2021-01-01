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
using System.Windows.Navigation;
using System.Windows.Shapes;

using BLAPI;
namespace PlGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");
        public MainWindow()
        {
            InitializeComponent();

        }

        void OpenWindow(object userName, object password)
        {
            try
            {
                if (bl.GetUserPrivileges((userName as TextBox).Text, (password as TextBox).Text))
                {

                }
            }
            catch (BO.BadUsernameOrPasswordException)
            {

                ;
            }

        
        }

        public  void MouseEnter_new(object sender, EventArgs e) // If the mouse enters the textbox, remove the
        {                                                   // preview text to make it easier for the user
            if ((sender as TextBox).Text == (sender as TextBox).Tag.ToString())
                (sender as TextBox).Text = string.Empty;
        }
        public void MouseLeave_new(object sender, MouseEventArgs e) // If the mouse leaves the textbox, return the
        {                                                        // preview text to make it more clear what to enter here.
            if ((sender as TextBox).Text == "")
                (sender as TextBox).Text = (sender as TextBox).Tag.ToString();
        }
        public void KeyDown_new(object sender, KeyEventArgs e) // If we started writing something, remove the preview text
        {                                                   // to make it easier for the user to write
            if ((sender as TextBox).Text == (sender as TextBox).Tag.ToString())
            {
                (sender as TextBox).Text = "";
            }
        }
    }
}

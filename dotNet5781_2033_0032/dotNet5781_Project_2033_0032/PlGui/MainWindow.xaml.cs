using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;
using BLAPI;
namespace PlGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");
        BackgroundWorker getUser = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();

            getUser.DoWork += OpenWindow;
            //MenagmentWindow menWin = new MenagmentWindow(bl, tb_username.Text);
            //menWin.Show();
            //this.Close();
        }



        void OpenWindow(object sender, DoWorkEventArgs e)
        {
            PlGui.User user = (PlGui.User)e.Argument;
            try
            {
                if (bl.GetUserPrivileges(user.username, user.password))
                {
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        MenagmentWindow menWin = new MenagmentWindow(bl, tb_username.Text);
                        menWin.Show();
                        this.Close();
                    }));
                }
                else
                {
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        MessageBox.Show("section under construction");
                        tb_warnings.Text = "";
                    }));
                }
            }
            catch (BO.BadUsernameOrPasswordException ex)
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    tb_warnings.Text = "Username or password incorrect";
                }));
            }
        }
        public void MouseEnter_new(object sender, EventArgs e) // If the mouse enters the textbox, remove the
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
            if (e.Key == Key.Enter)
                btn_logIn_Click(sender, e);
            
            if ((sender as TextBox).Text == (sender as TextBox).Tag.ToString())
                (sender as TextBox).Text = "";
        }
                

        private void btn_logIn_Click(object sender, RoutedEventArgs e)
        {
            getUser.RunWorkerAsync(new PlGui.User { username = tb_username.Text, password = tb_password.Text });
        }
    }
}

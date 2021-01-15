using BLAPI;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
                        MenagmentWindow menWin = new MenagmentWindow(bl, user.username);
                        tb_username.Text = (string)tb_username.Tag;
                        tb_password.Text = (string)tb_password.Tag;
                        menWin.Closing += OpenWindowafterUsage;
                        menWin.Show();
                        this.Visibility = Visibility.Collapsed;
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
            catch (BO.BadUsernameOrPasswordException)
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    tb_warnings.Text = "Username or password incorrect";
                }));
            }
        }
        void OpenWindowafterUsage(object sender, CancelEventArgs e)
        {
            this.Visibility=Visibility.Visible;
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

        private void bn_singUp_Click(object sender, RoutedEventArgs e)
        {
            SignUp signWin = new SignUp(bl);
            signWin.Closing += OpenWindowafterUsage;
            signWin.Show();
            this.Visibility = Visibility.Collapsed;
        }
    }
}
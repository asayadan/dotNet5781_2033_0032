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
        IBL bl = BLFactory.GetBL("BLImp");
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
                if (bl.RequestUserPrivileges(user.username, user.password))
                {
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        MenagmentWindow menWin = new MenagmentWindow(bl, user.username);
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

        public void KeyDown_new(object sender, KeyEventArgs e) // If we started writing something, remove the preview text
        {                                                   // to make it easier for the user to write
            if (e.Key == Key.Enter)
                btn_logIn_Click(sender, e);

        }
        private void btn_logIn_Click(object sender, RoutedEventArgs e)
        {
            getUser.RunWorkerAsync(new PlGui.User { username = tb_username.Text, password = tb_password.Password });
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
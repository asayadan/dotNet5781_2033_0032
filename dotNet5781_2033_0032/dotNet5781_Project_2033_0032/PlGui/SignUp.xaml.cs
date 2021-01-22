using BLAPI;
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
using System.Windows.Shapes;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        IBL bl;
        BackgroundWorker signWorker=new BackgroundWorker() ;
        public SignUp(IBL _bl)
        {
            bl = _bl;
            signWorker.DoWork += SignUpFunction;
            InitializeComponent();
        }

        void SignUpFunction(object sender, DoWorkEventArgs e)
        {
            PlGui.User user = (PlGui.User)e.Argument;
            try
            {
                bl.CreateUser(user.username, user.password,user.varifyPassword);
                this.Dispatcher.Invoke((Action)(() =>
                {
                    this.Close();
                }));
            }
            catch (BO.BadUsernameOrPasswordException ex)
            {
                if (ex.InnerException!=null)
                {
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        tbl_warnings.Text = "that username is taker try another username";
                    }));
                }
                else
                {
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        tbl_warnings.Text = "the password doesn't match";
                    }));
                }
            }

        }


        private void bn_signUp_Click(object sender, RoutedEventArgs e)
        {

            signWorker.RunWorkerAsync(new PlGui.User { username = tb_username.Text, password = pb_password.Password,varifyPassword=pb_passwordValidation.Password });
        }
    }
}

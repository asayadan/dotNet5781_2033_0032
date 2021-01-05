using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using BLAPI;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for MenagmentWindow.xaml
    /// </summary>
    /// 
    public partial class MenagmentWindow : Window
    {

        IBL bl;
        string username;
        BackgroundWorker busWorker = new BackgroundWorker();

        ObservableCollection<BO.Bus> busCollection;

        BackgroundWorker lineWorker = new BackgroundWorker();

        ObservableCollection<BO.Line> lineCollection;
        public MenagmentWindow( IBL _bl ,string user)
        {
            username = user;
            bl = _bl;
            InitializeComponent();
            SetAllLines();
            cb_lines.ItemsSource = lineCollection;
            cb_lines.DisplayMemberPath = "Code";//show only specific Property of object
            cb_lines.SelectedValuePath = "Id";//selection return only specific Property of object
            cb_lines.SelectedIndex = 0; //index of the object to be selected
            areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Areas));

        }


        void SetAllLines()
        {
            lineCollection = new ObservableCollection<BO.Line>(bl.GetAllLines());
        }

        private void cb_lines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lineWorker.DoWork += bl.;
            //lineWorker.RunWorkerAsync( = (cb_lines.SelectedItem as BO.Line);
            gridOneStudent.DataContext = curStu;

            if (curStu != null)
            {
                //list of courses of selected student
                RefreshAllRegisteredCoursesGrid();
                //list of all courses (that selected student is not registered to it)
                RefreshAllNotRegisteredCoursesGrid();
            }
        }

        private void Worker(object sender, DoWorkEventArgs e)
        {
            
        }

    }
}

﻿using System;
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

namespace dotNet5781_03B_2033_0032
{
    /// <summary>
    /// Interaction logic for Ride.xaml
    /// </summary>
    public partial class Ride : Window
    {
        public Ride()
        {
            InitializeComponent();
        }

        private void tb_km_MouseEnter(object sender, MouseEventArgs e)
        {
            if (tb_km.Text == "Enter the number of kilometers to ride")
            {
                tb_km.Text = "";
            }
        }

        private void tb_km_MouseLeave(object sender, MouseEventArgs e)
        {
            if (tb_km.Text =="")
            {
                tb_km.Text = "Enter the number of kilometers to ride";
            }
        }

        private void tb_km_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MessageBox.Show("new ride");
            }
        }
    }
}

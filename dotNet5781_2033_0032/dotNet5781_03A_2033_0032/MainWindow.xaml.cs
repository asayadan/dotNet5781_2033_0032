﻿using dotNet5781_02_2033_0032;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace dotNet5781_03A_2033_0032
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public const int STATIONS_PER_LINE = 11;
        private LineCollection busLines = new LineCollection();
        private BusLine currentDisplayBusLine;

        public MainWindow()
        {
            InitializeComponent();
            initilize(ref busLines);
            cbBusLines.ItemsSource = busLines;
            cbBusLines.DisplayMemberPath = "_lineNumber";
            cbBusLines.SelectedIndex = 0;
            ShowBusLine((cbBusLines.SelectedValue as BusLine)._lineNumber);

        }

        public static void initilize(ref LineCollection collection)
        {
            List<BusStation> stations = new List<BusStation>();
            int num_stations = 40;
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < num_stations; i++) // Creating 40 lines with random coordinates and random station number.
            {
                int help_station_key = rnd.Next(100000, 999999);
                if (!stations.Exists(x => x.GetBusStationKey == help_station_key))
                {
                    stations.Add(new BusStation(help_station_key));
                }
                else
                    i--;
            }

            float dist;
            for (int i = 0; i < 10; i++)
            {
                int help_bus_key = rnd.Next(1, 999);
                if (!stations.Exists(x => x.GetBusStationKey == help_bus_key)) //Creating 10 lines with random keys
                {
                    BusLine line = new BusLine(help_bus_key, rnd.Next(0, 4));
                    for (int j = 0; j < STATIONS_PER_LINE; j++) // Adding STATIONS_PER_LINE (constant) stations per line.
                    {
                        dist = (float)rnd.NextDouble() + rnd.Next(20);
                        line.addStation(new BusStationLine(stations[(i * STATIONS_PER_LINE + j) % num_stations], dist, dist / 2), j, dist, dist / 2);
                    }

                    collection.addLine(line);
                }
                else
                    i--; // If already exists.
            }

        }

        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = busLines[index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.stations;

        }

        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine)._lineNumber);

        }
    }


}

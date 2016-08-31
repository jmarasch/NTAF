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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Ribbon;

namespace ProtoGears_World_Builder {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow {
        public MainWindow() {
            InitializeComponent();

            }

        private void RibbonWin_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            }

        private void RibbonWin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
            }

        private void RibbonHelpButton_Click(object sender, RoutedEventArgs e) {
            WpfWindowHelper.HideSysMenu(this);
            }
        }
    }
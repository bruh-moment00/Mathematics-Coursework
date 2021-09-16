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

namespace Задание_курсовой
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        char[] emptyArray = {' '};

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataGridA.ItemsSource = FormirationDataGrid.ToDataTable(emptyArray, "A").DefaultView;
            DataGridC.ItemsSource = FormirationDataGrid.ToDataTable(emptyArray, "C").DefaultView;
            DataGridX.ItemsSource = FormirationDataGrid.ToDataTable(emptyArray, "X").DefaultView;
            DataGridY.ItemsSource = FormirationDataGrid.ToDataTable(emptyArray, "Y").DefaultView;
            GridY_Sorted.ItemsSource = FormirationDataGrid.ToDataTable(emptyArray, "Y").DefaultView;

            Dan.m = int.Parse(m_value.Text);
            Dan.h = double.Parse(hValue.Text);
            Dan.Start_A(int.Parse(rnd1.Text), int.Parse(rnd2.Text));
            Dan.Start_C(int.Parse(cRnd1.Text), int.Parse(cRnd2.Text));
            Dan.Start_X(double.Parse(b_value.Text));

            string[] X_string = new string[Dan.X.Length];
            for (int i = 0; i < Dan.X.Length; i++)
            {
                X_string[i] = String.Format("{0:f3}", Dan.X[i]);
            }

            Dan.Start_Y();
            Dan.Y_Sorted = new double[Dan.Y.Length];
            Array.Copy(Dan.Y, Dan.Y_Sorted, Dan.Y.Length);
            Dan.Y_Sorted = Dan.Quick_Sort(Dan.Y_Sorted);
            string[] Y_String = new string[Dan.Y.Length];
            string[] Y_Sorted_String = new string[Dan.Y.Length];
            for (int i = 0; i < Dan.Y.Length; i++)
            {
                Y_String[i] = String.Format("{0:f3}", Dan.Y[i]);
                Y_Sorted_String[i] = String.Format("{0:f3}", Dan.Y_Sorted[i]);
            }
            DataGridA.ItemsSource = FormirationDataGrid.ToDataTable(Dan.A, "A").DefaultView;
            DataGridC.ItemsSource = FormirationDataGrid.ToDataTable(Dan.C, "C").DefaultView;
            DataGridX.ItemsSource = FormirationDataGrid.ToDataTable(Dan.X, "X").DefaultView;
            DataGridY.ItemsSource = FormirationDataGrid.ToDataTable(Dan.Y, "Y").DefaultView;
            GridY_Sorted.ItemsSource = FormirationDataGrid.ToDataTable(Dan.Y_Sorted, "Y").DefaultView;

            ChartButton.IsEnabled = true;
        }

        private void Chart_Open(object sender, RoutedEventArgs e)
        {
            Chart chart = new Chart();
            chart.Show();
            this.IsEnabled = false;
        }


    }
}

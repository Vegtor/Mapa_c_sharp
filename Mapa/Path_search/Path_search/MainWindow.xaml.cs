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
using System.Text.RegularExpressions;
using System.Windows.Controls.Ribbon.Primitives;

namespace Hledani_cesty
{
    public partial class MainWindow : Window
    {
        //Proměnné//
        int rect_size = 60;
        Map map;
        //*******//
        public MainWindow()
        {
            InitializeComponent();

            number_tb_x.Text = "0";
            number_tb_y.Text = "0";

            number_tb_x.TextChanged += number_tb_TextChanged;
            number_tb_y.TextChanged += number_tb_TextChanged;
        }
        //*****************************************************************************************************************//
        // Pomocné metody - private //
        //*****************************************************************************************************************//
        private bool check_text_number(string txt)
        {
            Regex regex = new Regex("[^0-9]+$");
            return regex.IsMatch(txt);
        }
        private void generate_buttons(int n_x, int n_y)
        {
            map = new Map(n_x, n_y);
            for(int i = 0; i < n_y; i++)
            {
                for(int j = 0; j < n_x; j++)
                {
                    Button temp = new Button();
                    temp.Width = rect_size;
                    temp.Height = rect_size;
                    //Testování//
                    //temp.Content = i.ToString() + "," + j.ToString();


                    Grid.SetColumn(temp, j);
                    Grid.SetRow(temp, i);
                    map_grid.Children.Add(temp); //Funguje jako pole
                    map.add_to_map(0, i, j); 
                }
            }
            start_button.IsEnabled = true;
            obst_button.IsEnabled = true;
            end_button.IsEnabled = true;
        }
        private void clear_buttons()
        {
            map_grid.Children.Clear();
            map_grid.RowDefinitions.Clear();
            map_grid.ColumnDefinitions.Clear();
            start_button.IsEnabled = false;
            obst_button.IsEnabled = false;
            end_button.IsEnabled = false;
        }
        //*****************************************************************************************************************//
        // Pomocné metody - KONEC //
        //*****************************************************************************************************************//
        private void number_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (number_tb_x.Text == "" || number_tb_y.Text == "")
            {
                return;
            }
            int n_x = Int32.Parse(number_tb_x.Text);
            int n_y = Int32.Parse(number_tb_y.Text);

            int number_of_columns = map_grid.ColumnDefinitions.Count;
            int number_of_rows = map_grid.RowDefinitions.Count;

            int count_x = n_x - number_of_columns;
            int count_y = n_y - number_of_rows;
            
            if((n_x == 0 || n_y == 0) && (count_x <= 0 && count_y <= 0))
            {
                clear_buttons();
            }
            if (n_x > 0)
            {
                for (int i = 0; i < Math.Abs(count_x); i++)
                {
                    if (count_x > 0)
                    {
                        ColumnDefinition col = new ColumnDefinition();
                        col.Width = new GridLength(rect_size, GridUnitType.Pixel);
                        map_grid.ColumnDefinitions.Add(col);
                    }
                    else if(n_y != 0)
                    {
                        map_grid.Children.Clear();
                    }    
                }
            }
            if (n_y > 0 && n_x != 0)
            {
                for (int i = 0; i < Math.Abs(count_y); i++)
                {
                    if (count_y > 0)
                    {
                        RowDefinition row = new RowDefinition();
                        row.Height = new GridLength(rect_size, GridUnitType.Pixel);
                        map_grid.RowDefinitions.Add(row);
                    }
                    else if (n_x != 0)
                    {
                        map_grid.Children.Clear();
                    }
                }
            }
            if (n_x > 0 && n_y > 0)
            {
                generate_buttons(n_x, n_y);
            }
            num_lbl.Content = map_grid.Children.Count;
        }
        private void number_tb_PreviewText(object sender, TextCompositionEventArgs e)
        {
            e.Handled = check_text_number(e.Text);
        }
    }
}

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
using WpfCinema.Models;

namespace WpfCinema
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (wpfCinemaContext db = new wpfCinemaContext())
            {
                List<Category> categories = db.Categories.ToList();
                gridCategories.ItemsSource = categories;
                ComboBox1.ItemsSource = categories;

                List<Movie> movies = db.Movies.ToList();
                gridMovies.ItemsSource = movies;

                List<Hall> halls = db.Halls.ToList();
                gridHalls.ItemsSource = halls;
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void gridClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

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
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace WpfCinema
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        wpfCinemaContext db = new wpfCinemaContext();
        Regex timeRegex = new Regex("(^\\d{1,2}:\\d{1,2}$)|(^\\d{1,2}:\\d{1,2}:\\d{1,2}$)");
        public MainWindow()
        {
            InitializeComponent();
            {
                UpdateData();
            }
        }

        private void UpdateData()
        {
            List<Category> categories = db.Categories.ToList();
            gridCategories.ItemsSource = categories;
            categorySelector.ItemsSource = categories;

            List<Movie> movies = db.Movies.ToList();
            gridMovies.ItemsSource = movies;
            movieSelector.ItemsSource = movies;

            List<Hall> halls = db.Halls.ToList();
            gridHalls.ItemsSource = halls;
            hallSelector.ItemsSource = halls;

            List<Screening> screenings = db.Screenings.ToList();
            gridScreenings.ItemsSource = screenings;

        }

        private void addCategory_Click(object sender, RoutedEventArgs e)
        {
            if (categoryName.Text != "")
            {
                var newCategory = new Category { Name = categoryName.Text };
                db.Add<Category>(newCategory);
                db.SaveChanges();

                UpdateData();
            }
        }

        private void CategoryRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (gridCategories.SelectedItem == null) return;

            var selectedCategory = gridCategories.SelectedItem as Category;
            db.Categories.Remove(selectedCategory);
            db.SaveChanges();

            UpdateData();
        }

        private void addHall_Click(object sender, RoutedEventArgs e)
        {
            Regex regex = new Regex("^\\d*$");
            if (hallNumber.Text != "" && hallSeats.Text != "" && regex.IsMatch(hallNumber.Text) && regex.IsMatch(hallSeats.Text) && db.Halls.Find(Int32.Parse(hallNumber.Text)) == null)
            {
                var newHall = new Hall { Number = Int32.Parse(hallNumber.Text), NumberOfSeats = Int32.Parse(hallSeats.Text) };
                db.Add<Hall>(newHall);
                db.SaveChanges();

                UpdateData();
            }
        }

        private void HallRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (gridHalls.SelectedItem == null) return;

            var selectedHall = gridHalls.SelectedItem as Hall;
            db.Halls.Remove(selectedHall);
            db.SaveChanges();

            UpdateData();
        }

        private void addMovie_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan duration;
            int categoryId;
            if (categorySelector.SelectedValue != null && movieDuration.Text != "" && timeRegex.IsMatch(movieDuration.Text) && movieTitle.Text != "")
            {
                duration = TimeSpan.Parse(movieDuration.Text);
                categoryId = int.Parse(categorySelector.SelectedValue.ToString());
                var newMovie = new Movie { Title = movieTitle.Text, Duration = duration, Description = movieDesc.Text, Category = categoryId };
                db.Add<Movie>(newMovie);
                db.SaveChanges();

                UpdateData();
            }
        }

        private void MovieRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (gridMovies.SelectedItem == null) return;

            var selectedMovie = gridMovies.SelectedItem as Movie;
            if (db.Screenings.FirstOrDefault(sc => sc.Movie == selectedMovie.Id) != null) 
            {
                MessageBox.Show("Istnieje już seans na ten film", "Istniejąca relacja", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            db.Movies.Remove(selectedMovie);
            db.SaveChanges();

            UpdateData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(movieSelector.SelectedValue != null && hallSelector.SelectedValue != null && screeningTime.Text != "" && timeRegex.IsMatch(screeningTime.Text) && screeningDate.SelectedDate != null) {
                int movieId = int.Parse(movieSelector.SelectedValue.ToString());
                int hallId = int.Parse(hallSelector.SelectedValue.ToString());
                TimeSpan time = TimeSpan.Parse(screeningTime.Text);
                DateTime? dtScreening = screeningDate.SelectedDate + time;
                DateTime? endAt = dtScreening + db.Movies.Find(movieId).Duration;

                var newScreening = new Screening { Movie = movieId, Hall = hallId, StartedAt = dtScreening, EndedAt = endAt};
                db.Add<Screening>(newScreening);
                db.SaveChanges();

                UpdateData();
            }
        }

        private void ScreeningRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (gridScreenings.SelectedItem == null) return;

            var selectedScreening = gridScreenings.SelectedItem as Screening;
            db.Screenings.Remove(selectedScreening);
            db.SaveChanges();

            UpdateData();
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }
    }
}

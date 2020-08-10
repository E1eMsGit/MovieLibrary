using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MovieLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace MovieLibrary.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        public bool MenuToggleButtonIsChecked
        {
            get => _menuToggleButtonIsChecked;
            set => Set(ref _menuToggleButtonIsChecked, value);
        }
        public bool ImdbIsChecked { get; set; }
        public bool KinoPoiskIsChecked { get; set; }
        public List<int> Dates { get; }
        public int BeginYearSelected { get; set; }
        public int EndYearSelected { get; set; }
        public int RatingValue { get; set; }
        public string ErrorText
        {
            get => _errorText;
            set => Set(ref _errorText, value);
        }
        public List<TreeCategory> MenuTreeViewContent { get; }

        private bool _menuToggleButtonIsChecked = false;
        private string _errorText;
        private MainWindow _window;
        private ContentViewModel _contentViewModel;
        private AddNewMovieViewModel _addNewMovieViewModel;
        private bool _isAddMovie;

        #endregion

        #region Constructors

        public MainViewModel(ContentViewModel contentViewModel, AddNewMovieViewModel addNewMovieViewModel)
        {
            _contentViewModel = contentViewModel;
            _addNewMovieViewModel = addNewMovieViewModel;
            _window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            MenuTreeViewContent = new MenuTreeViewContent().Content;
            Dates = new List<int>(Enumerable.Range(1990, DateTime.Now.Year - 1989));
            RatingValue = 5;
            ImdbIsChecked = true;
        }

        #endregion

        #region Commands

        public RelayCommand ApplyParametersCommand => new RelayCommand(ApplyParameters);       
        public RelayCommand ContentViewCommand => new RelayCommand(ContentView);
        public RelayCommand AddNewMovieViewCommand => new RelayCommand(AddNewMovieView);
        public RelayCommand<CancelEventArgs> ClosingProgramCommand => new RelayCommand<CancelEventArgs>(ClosingProgram);

        private void ApplyParameters()
        { 
            List<TreeItem> genreItems = MenuTreeViewContent[0].TreeItems.Where(x => x.IsChecked).ToList();
            List<TreeItem> countryItems = MenuTreeViewContent[1].TreeItems.Where(x => x.IsChecked).ToList();

            if (genreItems.Count == 0)
            {
                ErrorText = "Выбирите жанр";
            }
            else if (BeginYearSelected == 0 && EndYearSelected == 0)
            {
                ErrorText = "Задайте временной период";
            }
            else if (BeginYearSelected == 0)
            {
                ErrorText = "Задайте\nминимальное значение\nвременного периода";
            }
            else if (EndYearSelected == 0)
            {
                ErrorText = "Задайте\nмаксимальное значение\nвременного периода";
            }
            else if (EndYearSelected < BeginYearSelected && EndYearSelected != 0)
            {
                ErrorText = "Минимальное значение\nвыбранного временного\nпериода не может быть\nбольше максимального";
            }
            else
            {
                MenuToggleButtonIsChecked = false;
                ErrorText = string.Empty;
            }
        }
        private void ContentView()
        {
            _window.MainPanel.Children.Clear();
            _window.MainPanel.Children.Add(new View.ContentView() { DataContext = _contentViewModel });
        }
        private void AddNewMovieView()
        {
            if (!_isAddMovie)
            {
                _isAddMovie = true;
                _window.MenuToggleButton.Visibility = Visibility.Hidden;
                _window.MainPanel.Children.Clear();
                _window.MainPanel.Children.Add(new View.AddNewMovieView() { DataContext = _addNewMovieViewModel });
            }
            else
            {
                _isAddMovie = false;
                _window.MenuToggleButton.Visibility = Visibility.Visible;
                ContentView();
            }
        }
        private void ClosingProgram(CancelEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите закрыть программу?", "Выход", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
            else
            {
                e.Cancel = true;
            }
        }

        #endregion

    }
}
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ChmodConverterLib;

namespace ChmodConverterWpf
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _symbolicMode = "";
        private string _numericMode = "";
        private bool _ownerRead, _ownerWrite, _ownerExecute;
        private bool _groupRead, _groupWrite, _groupExecute;
        private bool _othersRead, _othersWrite, _othersExecute;
        private bool _isUpdating;

        public event PropertyChangedEventHandler PropertyChanged;

        public string SymbolicMode
        {
            get => _symbolicMode;
            set
            {
                if (_symbolicMode != value)
                {
                    _symbolicMode = value;
                    OnPropertyChanged(nameof(SymbolicMode));
                    UpdateFromSymbolic();
                }
            }
        }

        public string NumericMode
        {
            get => _numericMode;
            set
            {
                if (_numericMode != value)
                {
                    _numericMode = value;
                    OnPropertyChanged(nameof(NumericMode));
                    UpdateFromNumeric();
                }
            }
        }

        public bool OwnerRead
        {
            get => _ownerRead;
            set { _ownerRead = value; OnPropertyChanged(nameof(OwnerRead)); UpdateFromCheckboxes(); }
        }
        public bool OwnerWrite
        {
            get => _ownerWrite;
            set { _ownerWrite = value; OnPropertyChanged(nameof(OwnerWrite)); UpdateFromCheckboxes(); }
        }
        public bool OwnerExecute
        {
            get => _ownerExecute;
            set { _ownerExecute = value; OnPropertyChanged(nameof(OwnerExecute)); UpdateFromCheckboxes(); }
        }
        public bool GroupRead
        {
            get => _groupRead;
            set { _groupRead = value; OnPropertyChanged(nameof(GroupRead)); UpdateFromCheckboxes(); }
        }
        public bool GroupWrite
        {
            get => _groupWrite;
            set { _groupWrite = value; OnPropertyChanged(nameof(GroupWrite)); UpdateFromCheckboxes(); }
        }
        public bool GroupExecute
        {
            get => _groupExecute;
            set { _groupExecute = value; OnPropertyChanged(nameof(GroupExecute)); UpdateFromCheckboxes(); }
        }
        public bool OthersRead
        {
            get => _othersRead;
            set { _othersRead = value; OnPropertyChanged(nameof(OthersRead)); UpdateFromCheckboxes(); }
        }
        public bool OthersWrite
        {
            get => _othersWrite;
            set { _othersWrite = value; OnPropertyChanged(nameof(OthersWrite)); UpdateFromCheckboxes(); }
        }
        public bool OthersExecute
        {
            get => _othersExecute;
            set { _othersExecute = value; OnPropertyChanged(nameof(OthersExecute)); UpdateFromCheckboxes(); }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void UpdateFromSymbolic()
        {
            if (_isUpdating) return;
            _isUpdating = true;

            try
            {
                SymbolicTextBox.Background = Brushes.White;
                NumericTextBox.Background = Brushes.White;

                if (!string.IsNullOrEmpty(SymbolicMode) && SymbolicMode.Length == 9)
                {
                    string numeric = ChmodConverter.SymbolicToNumeric(SymbolicMode);
                    NumericMode = numeric;
                    UpdateCheckboxesFromSymbolic(SymbolicMode);
                }
                else if (!string.IsNullOrEmpty(SymbolicMode))
                {
                    throw new ArgumentException("Symbolic mode must be 9 characters long (e.g., rwxr-xr-x).");
                }
            }
            catch (ArgumentException ex)
            {
                SymbolicTextBox.Background = Brushes.Red;
            }
            finally
            {
                _isUpdating = false;
            }
        }

        private void UpdateFromNumeric()
        {
            if (_isUpdating) return;
            _isUpdating = true;

            try
            {
                SymbolicTextBox.Background = Brushes.White;
                NumericTextBox.Background = Brushes.White;

                if (!string.IsNullOrEmpty(NumericMode) && NumericMode.Length == 3)
                {
                    string symbolic = ChmodConverter.NumericToSymbolic(NumericMode);
                    SymbolicMode = symbolic;
                    UpdateCheckboxesFromSymbolic(symbolic);
                }
                else if (!string.IsNullOrEmpty(NumericMode))
                {
                    throw new ArgumentException("Numeric mode must be 3 digits long (e.g., 755).");
                }
            }
            catch (ArgumentException ex)
            {
                NumericTextBox.Background = Brushes.Red;
            }
            finally
            {
                _isUpdating = false;
            }
        }

        private void UpdateFromCheckboxes()
        {
            if (_isUpdating) return;
            _isUpdating = true;

            try
            {
                SymbolicTextBox.Background = Brushes.White;
                NumericTextBox.Background = Brushes.White;

                string symbolic =
                    (OwnerRead ? "r" : "-") +
                    (OwnerWrite ? "w" : "-") +
                    (OwnerExecute ? "x" : "-") +
                    (GroupRead ? "r" : "-") +
                    (GroupWrite ? "w" : "-") +
                    (GroupExecute ? "x" : "-") +
                    (OthersRead ? "r" : "-") +
                    (OthersWrite ? "w" : "-") +
                    (OthersExecute ? "x" : "-");

                SymbolicMode = symbolic;
                NumericMode = ChmodConverter.SymbolicToNumeric(symbolic);
            }
            catch (ArgumentException)
            {
                // Немає окремого відображення помилок через чекбокси, оскільки вони завжди валідні
            }
            finally
            {
                _isUpdating = false;
            }
        }

        private void UpdateCheckboxesFromSymbolic(string symbolic)
        {
            if (string.IsNullOrEmpty(symbolic) || symbolic.Length != 9) return;

            OwnerRead = symbolic[0] == 'r';
            OwnerWrite = symbolic[1] == 'w';
            OwnerExecute = symbolic[2] == 'x';
            GroupRead = symbolic[3] == 'r';
            GroupWrite = symbolic[4] == 'w';
            GroupExecute = symbolic[5] == 'x';
            OthersRead = symbolic[6] == 'r';
            OthersWrite = symbolic[7] == 'w';
            OthersExecute = symbolic[8] == 'x';
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
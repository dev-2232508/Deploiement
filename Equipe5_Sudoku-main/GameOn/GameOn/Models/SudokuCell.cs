using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOn.ViewModels
{
    public partial class SudokuCell : ObservableObject
    {
        private int? _value;
        public int? Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value is >= 1 and <= 9 ? value : null;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }
        [ObservableProperty]
        public string _nameCell = "";
        public bool IsReadOnly { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public SudokuCell(string name)
        {
            NameCell = name;
        }

        public SudokuCell(int value, bool isReadOnly, string name)
        {
            Value = value;
            IsReadOnly = isReadOnly;
            NameCell = name;
        }
    }
}

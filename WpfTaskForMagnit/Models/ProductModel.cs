using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfTaskForMagnit.Annotations;

namespace WpfTaskForMagnit.Models
{
    public class ProductModel: INotifyPropertyChanged
    {
        private string _textColumnCell;

        public string TextColumnCell
        {
            get => _textColumnCell;
            set
            {
                _textColumnCell = value;
                OnPropertyChanged();
            }
        }
        private string _textRowCell;

        public string TextRowCell
        {
            get => _textRowCell;
            set
            {
                _textRowCell = value;
                OnPropertyChanged();
            }
        }

        private string _textColumn;

        public string TextColumn
        {
            get => _textColumn;
            set
            {
                _textColumn = value;
                OnPropertyChanged();
            }

        }

        private string _textRow;

        public string TextRow
        {
            get => _textRow;
            set
            {
                _textRow = value;
                OnPropertyChanged();
            }

        }

        private string _fontfamily;

        public string FontFamily
        {
            get => _fontfamily;
            set { _fontfamily = value; OnPropertyChanged(); }
        }

        private string _fontsize;

        public string FontSize
        {
            get => _fontsize;
            set { _fontsize = value; OnPropertyChanged(); }
        }



        private string _chooseBrush;

        public string ChooseBrush
        {
            get => _chooseBrush;
            set { _chooseBrush = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

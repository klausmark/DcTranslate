using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DcTranslate.Model
{
    public class NumberTranslation : INotifyPropertyChanged
    {
        private long _id;
        private string _fromNumber;
        private string _toNumber;
        private string _description;

        public long Id
        {
            get { return _id; }
            set
            {
                _id = value; 
                OnPropertyChanged();
            }
        }

        public string FromNumber
        {
            get { return _fromNumber; }
            set
            {
                _fromNumber = value;
                OnPropertyChanged();
            }
        }

        public string ToNumber
        {
            get { return _toNumber; }
            set
            {
                _toNumber = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

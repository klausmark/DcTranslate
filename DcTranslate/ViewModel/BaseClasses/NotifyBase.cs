using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DcTranslate.ViewModel.BaseClasses
{
    public class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly Dictionary<string, object> _backingFields = new Dictionary<string, object>();

        protected virtual T GetField<T>([CallerMemberName] string propertyName = "")
        {
            return GetBackingField<T>(propertyName);
        }

        protected virtual T GetFieldWhereDefaultValueIs<T>(T defaultValue, [CallerMemberName] string propertyName = "")
        {
            return GetBackingField(propertyName, defaultValue);
        }

        private T GetBackingField<T>(string propertyName, T defaultValue = default(T))
        {
            if (!_backingFields.ContainsKey(propertyName)) return defaultValue;
            return (T)_backingFields[propertyName];
        }

        protected virtual void SetField<T>(T value, [CallerMemberName] string propertyName = "")
        {
            if (_backingFields.ContainsKey(propertyName))
            {
                if (ValueChanged(value, propertyName))
                {
                    SetBackingField(value, propertyName);
                }
            }
            else
            {
                SetBackingField(value, propertyName);
            }
        }

        protected virtual void SetFieldAndInvokeMethod<T>(T value, Action method, [CallerMemberName] string propertyName = "")
        {
            var tmp = GetField<T>(propertyName);
            SetField(value, propertyName);
            try
            {
                method.Invoke();
            }
            catch (Exception)
            {
                SetField(tmp, propertyName);
            }
        }

        private bool ValueChanged<T>(T value, string propertyName)
        {
            var oldValue = _backingFields[propertyName];
            if (oldValue == null && value == null) return false;
            if (oldValue == null) return true;
            return !oldValue.Equals(value);
        }

        private void SetBackingField<T>(T value, string propertyName)
        {
            _backingFields[propertyName] = value;
            OnPropertyChanged(propertyName);
        }
    }
}

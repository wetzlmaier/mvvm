using System.Collections;
using System.ComponentModel;
using FluentValidation;

namespace Mvvm
{
    // This is an example base class for a ViewModel that supports validation using FluentValidation.
    // For CommunityToolkit.Mvvm builtin validation <see cref="ObservableValidator"/> can be used.
    public abstract class ValidatableViewModelBase<T>(IValidator<T> validator) : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errors = [];

        public IEnumerable GetErrors(string? propertyName)
        {
            if (propertyName is null) return Enumerable.Empty<string>();
            return _errors.TryGetValue(propertyName, out var errors) ? errors : Enumerable.Empty<string>();
        }

        public bool HasErrors => _errors.Count != 0;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        protected void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
        }

        protected void ValidateAllProperties(T obj)
        {
            _errors.Clear();
            var results = validator.Validate(obj);

            foreach (var error in results.Errors)
            {
                if (!_errors.ContainsKey(error.PropertyName))
                    _errors[error.PropertyName] = [];

                _errors[error.PropertyName].Add(error.ErrorMessage);
                OnErrorsChanged(new DataErrorsChangedEventArgs(error.PropertyName));
            }
        }

        protected void ValidateProperty(T obj, string propertyName)
        {
            var results = validator.Validate(obj, options => options.IncludeProperties(propertyName));

            if (results.IsValid)
            {
                _errors.Remove(propertyName);
            }
            else
            {
                _errors[propertyName] = [.. results.Errors.Select(e => e.ErrorMessage)];
            }

            OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
        }
    }
}

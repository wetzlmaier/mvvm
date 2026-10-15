using System.ComponentModel.Composition;
using Mvvm;
using UserManagement.Model;
using UserManagement.ViewModel;

namespace WinFormsApp1.View
{
    [Export(typeof(IMainView<DialogResult, int>))]
    public partial class UserForm : Form, IMainView<DialogResult, int>
    {
        public UserForm()
        {
            InitializeComponent();
        }

        public override object? DataContext
        {
            get => base.DataContext;
            set
            {
                base.DataContext = value;
                SetupBindings((IUserViewModel?)value);
            }
        }

        private void SetupBindings(IUserViewModel? viewModel)
        {
            if (viewModel == null)
            {
                return;
            }

            var addUserViewModel = viewModel.AddUserViewModel;
            var showUsersViewModel = viewModel.ShowUsersViewModel;

            // 1. Bind TextBoxes
            // DataSourceUpdateMode.OnPropertyChanged ensures the VM updates on every keystroke
            txtFirstName.DataBindings.Add("Text", addUserViewModel, nameof(addUserViewModel.FirstName), true, DataSourceUpdateMode.OnPropertyChanged);
            txtLastName.DataBindings.Add("Text", addUserViewModel, nameof(addUserViewModel.LastName), true, DataSourceUpdateMode.OnPropertyChanged);

            // 2. Bind ListBox
            var bindingSource = new BindingSource { DataSource = showUsersViewModel.Users };
            lstUsers.DataSource = bindingSource;

            // Format the display text for the ListBox items
            lstUsers.Format += (s, e) =>
            {
                if (e.ListItem is UserModel user)
                {
                    e.Value = $"{user.FirstName} {user.LastName}".Trim();
                }
            };

            // WinForms ListBox doesn't natively listen to ObservableCollection changes. 
            // We must manually tell the BindingSource to reset when the collection changes.
            showUsersViewModel.Users.CollectionChanged += (s, e) =>
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() => bindingSource.ResetBindings(false)));
                }
                else
                {
                    bindingSource.ResetBindings(false);
                }
            };

            addUserViewModel.ErrorsChanged += (s, e) =>
            {
                // Find which control corresponds to the property that has an error
                Control? controlToUpdate = null;

                if (e.PropertyName == nameof(addUserViewModel.FirstName))
                {
                    controlToUpdate = txtFirstName;
                }
                else if (e.PropertyName == nameof(addUserViewModel.LastName))
                {
                    controlToUpdate = txtLastName;
                }

                // If we found a matching control, update the ErrorProvider
                if (controlToUpdate != null)
                {
                    // Get the list of errors for this property from the ViewModel
                    var errors = addUserViewModel.GetErrors(e.PropertyName)?.Cast<string>().ToList();

                    // Grab the first error message, or use an empty string if there are no errors
                    string errorText = (errors != null && errors.Count != 0) ? errors.First() : string.Empty;

                    // SetError displays the icon/tooltip if text exists, and clears it if text is empty
                    errorProvider.SetError(controlToUpdate, errorText);
                }
            };

            // Execute command on click
            btnSave.Click += (s, e) =>
            {
                if (addUserViewModel.SaveUserCommand.CanExecute(null))
                {
                    addUserViewModel.SaveUserCommand.Execute(null);
                }
            };

            this.Load += (s, e) =>
            {
                // Trigger the OnLoadedCommand when the form opens
                if (showUsersViewModel.LoadedCommand.CanExecute(null))
                {
                    showUsersViewModel.LoadedCommand.ExecuteAsync(null);
                }
            };
        }
    }
}

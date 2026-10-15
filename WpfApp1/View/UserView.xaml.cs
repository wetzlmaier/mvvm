using System.ComponentModel.Composition;
using System.Windows;
using Mvvm;

namespace WpfApp1.View
{
    [Export(typeof(IMainView<bool?, double>))]
    public partial class UserView : Window, IMainView<bool?, double>
    {
        public UserView()
        {
            InitializeComponent();
        }
    }
}
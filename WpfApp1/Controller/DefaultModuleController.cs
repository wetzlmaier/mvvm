using System.ComponentModel.Composition;
using Mvvm;

namespace WpfApp1.Controller
{
    [Export(typeof(IModuleController))]

    public class DefaultModuleController : IModuleController
    {
        [ImportingConstructor]
        public DefaultModuleController(IMainView<bool?, double> mainView, IMainViewModel mainViewModel)
        {
            MainView = mainView;
            MainViewModel = mainViewModel;
            MainView.DataContext = mainViewModel;
        }
        
        protected IMainView<bool?, double> MainView { get; }

        protected IMainViewModel MainViewModel { get; }

        public virtual void Initialize()
        {

        }

        public virtual void Run()
        {
            MainView.Show();
        }

        public virtual void Shutdown()
        {
            MainView.Close();
        }
    }
}

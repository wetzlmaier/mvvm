using Mvvm;
using System.ComponentModel.Composition;

namespace WinFormsApp1.Controller
{
    [Export(typeof(IModuleController))]

    public class DefaultModuleController : IModuleController
    {
        [ImportingConstructor]
        public DefaultModuleController(IMainView<DialogResult, int> mainView, IMainViewModel mainViewModel)
        {
            MainView = mainView;
            MainViewModel = mainViewModel;
            MainView.DataContext = mainViewModel;
        }

        protected IMainViewModel MainViewModel { get; }

        protected IMainView<DialogResult, int> MainView { get; }

        public virtual void Initialize()
        {

        }

        public virtual void Run()
        {
            Application.Run((Form)MainView);
        }

        public virtual void Shutdown()
        {
            MainView.Close();
        }
    }
}

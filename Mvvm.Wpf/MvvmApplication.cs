using Common;
using Common.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Versioning;
using System.Windows;
using System.Windows.Threading;

[assembly: SupportedOSPlatform("windows")]

namespace Mvvm.Wpf
{
    public class MvvmApplication(
        string[] moduleAssemblies,
        string[] ignoreAssemblies,
        SearchOption searchOption = SearchOption.TopDirectoryOnly)
        : Application
    {
        protected CompositionContainer? Container { get; private set; }
        protected IModuleController[] ModuleControllers = Array.Empty<IModuleController>();

        protected string[] ModuleAssemblies { get; } = moduleAssemblies;
        protected string[] IgnoreAssemblies { get; } = ignoreAssemblies;

        public MvvmApplication(SearchOption searchOption = SearchOption.TopDirectoryOnly)
            : this( [], searchOption)
        {

        }

        public MvvmApplication(string[] ignoreAssemblies, SearchOption searchOption = SearchOption.TopDirectoryOnly)
            : this( [], ignoreAssemblies, searchOption)
        {

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Don't handle the exceptions in Debug mode because otherwise the Debugger wouldn't
            // jump into the code when an exception occurs.
            DispatcherUnhandledException += AppDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
            Container = CompositionHelper.CreateCompositionContainer(ModuleAssemblies.ToArray(), IgnoreAssemblies.ToArray(), searchOption);

            try
            {
                ModuleControllers = Container.GetExportedValues<IModuleController>().ToArray();
            }
            catch (ReflectionTypeLoadException refEx)
            {
                foreach (var ex in refEx.LoaderExceptions)
                    Console.WriteLine(ex?.Message);

                Current.Shutdown(ExitCodes.Error);
                return;
            }

            if (ModuleControllers.Length == 0)
            {
                MessageBox.Show("No module found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Current.Shutdown(ExitCodes.Error);
                return;
            }

            foreach (IModuleController moduleController in ModuleControllers)
            {
                moduleController.Initialize();
            }

            foreach (IModuleController moduleController in ModuleControllers)
            {
                moduleController.Run();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            foreach (IModuleController moduleController in ModuleControllers.Reverse())
            {
                moduleController.Shutdown();
            }

            if (Container != null)
                Container.Dispose();

            base.OnExit(e);
        }

        private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            HandleException(e.Exception, false);
            e.Handled = true;
        }

        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException((Exception)e.ExceptionObject, e.IsTerminating);
        }

        private static void HandleException(Exception e, bool isTerminating)
        {
            Trace.TraceError(e.ToString());

            if (!isTerminating)
            {
                MessageBox.Show("Es ist ein Fehler aufgetreten.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

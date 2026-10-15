using Common;
using Common.Composition;
using Mvvm;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Versioning;

[assembly: SupportedOSPlatform("windows")]

namespace WinFormsApp1
{
    public static class Program
    {
        private static CompositionContainer? _container;
        private static IModuleController[]? _moduleControllers;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Don't handle the exceptions in Debug mode because otherwise the Debugger wouldn't
            // jump into the code when an exception occurs.
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
            _container = CompositionHelper.CreateCompositionContainer();

            try
            {
                _moduleControllers = [.. _container.GetExportedValues<IModuleController>()];
            }
            catch (ReflectionTypeLoadException refEx)
            {
                foreach (var ex in refEx.LoaderExceptions)
                    Console.WriteLine(ex?.Message);

                return ExitCodes.Error;
            }

            if (_moduleControllers.Length == 0 ) {
                MessageBox.Show("No module found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ExitCodes.Error;
            }

            foreach (IModuleController moduleController in _moduleControllers)
            {
                moduleController.Initialize();
            }

            foreach (IModuleController moduleController in _moduleControllers)
            {
                moduleController.Run();
            }

            return ExitCodes.Ok;
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
                MessageBox.Show("Es ist ein Fehler aufgetreten.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
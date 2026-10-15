using Common.Diagnostics;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Windows;
using FluentValidation;

namespace Mvvm
{
    public abstract class ViewModelBase : ObservableObject, IViewModel
    {
        private static bool? _isInDesignMode;
        private readonly IList<ViewModelBase> _childViewModels = new List<ViewModelBase>();

        public bool IsInDesignMode => IsInDesignModeStatic;

        public static bool IsInDesignModeStatic
        {
            get
            {
                if (!_isInDesignMode.HasValue)
                {
                    _isInDesignMode = (bool)DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement)).Metadata.DefaultValue;
                    if (!_isInDesignMode.Value && Process.GetCurrentProcess().ProcessName.StartsWith("devenv", StringComparison.Ordinal))
                    {
                        _isInDesignMode = true;
                    }
                }

                return _isInDesignMode.Value;
            }
        }

        protected virtual void OnClosed()
        {
            foreach (var childViewModel in _childViewModels)
            {
                childViewModel.OnClosed();
            }
        }

        protected virtual bool OnClosing()
        {
            return _childViewModels.Aggregate(true, (current, childViewModel) => current & childViewModel.OnClosing());
        }

        protected void RegisterChildViewModel(ViewModelBase viewModel)
        {
            Guard.IsNotNull(viewModel);
            ExtendedGuard.NotContains(_childViewModels, viewModel, nameof(viewModel));

            _childViewModels.Add(viewModel);
        }

        protected void UnregisterChildViewModel(ViewModelBase viewModel)
        {
            Guard.IsNotNull(viewModel);
            ExtendedGuard.Contains(_childViewModels, viewModel, nameof(viewModel));

            _childViewModels.Remove(viewModel);
        }
    }
}

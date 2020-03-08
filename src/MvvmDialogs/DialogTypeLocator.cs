using System;
using System.ComponentModel;
using MvvmDialogs.DialogTypeLocators;

namespace Todos.MvvmDialogs
{
    /// <summary>
    /// An implementation of <see cref="IDialogTypeLocator"/> that adapt MVVM Dialogs to the naming
    /// convention of views and view models used in Caliburn.Micro projects.
    /// </summary>
    class DialogTypeLocator : IDialogTypeLocator
    {
        /// <summary>
        /// Locates a dialog type based on the specified view model.
        /// </summary>
        public Type Locate(INotifyPropertyChanged viewModel)
        {
            var viewModelType = viewModel.GetType();

            var dialogFullName = viewModelType.FullName;
            dialogFullName = dialogFullName.Substring(
                0,
                dialogFullName.Length - "Model".Length);

            return viewModelType.Assembly.GetType(dialogFullName);
        }
    }
}

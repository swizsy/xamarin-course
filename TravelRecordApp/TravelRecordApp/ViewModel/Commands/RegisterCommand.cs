using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using static TravelRecordApp.ViewModel.RegisterViewModel;

namespace TravelRecordApp.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        private RegisterViewModel viewModel;

        public event EventHandler CanExecuteChanged;

        public RegisterCommand(RegisterViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            RegisterViewModelData data = (RegisterViewModelData)parameter;
            return data != null && !string.IsNullOrEmpty(data.Email) && !string.IsNullOrEmpty(data.Password) && !string.IsNullOrEmpty(data.ConfirmPassword);
        }

        public void Execute(object parameter)
        {
            viewModel.Register();
        }
    }
}

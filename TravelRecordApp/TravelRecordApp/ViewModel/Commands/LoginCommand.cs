using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TravelRecordApp.Model;

namespace TravelRecordApp.ViewModel.Commands
{
    public class LoginCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private MainViewModel viewModel;

        public LoginCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            User user = (User)parameter;

            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.Login();
        }
    }
}

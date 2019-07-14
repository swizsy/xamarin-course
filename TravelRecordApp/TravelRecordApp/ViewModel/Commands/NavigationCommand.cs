using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TravelRecordApp.ViewModel.Interfaces;

namespace TravelRecordApp.ViewModel.Commands
{
    public class NavigationCommand : ICommand
    {
        private INavigateViewModel viewModel;

        public event EventHandler CanExecuteChanged;

        public NavigationCommand(INavigateViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.Navigate();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TravelRecordApp.ViewModel.Interfaces;

namespace TravelRecordApp.ViewModel.Commands
{
    public class ModificationCommand : ICommand
    {
        private IModifyDataViewModel viewModel;

        public ModificationCommand(IModifyDataViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Modification modification = (Modification)parameter;
            viewModel.Modify(modification);
        }
    }
}

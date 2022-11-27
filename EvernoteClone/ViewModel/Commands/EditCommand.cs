using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class EditCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public NotesVM ViewModel { get; set; }
        public Action Value { get; }

        public EditCommand(NotesVM vm)
        {
            ViewModel = vm;
        }

        public EditCommand(Action value)
        {
            Value = value;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.StartEditing();
        }
    }
}

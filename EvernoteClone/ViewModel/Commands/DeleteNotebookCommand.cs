using EvernoteClone.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class DeleteNotebookCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public NotesVM ViewModel { get; set; }
        public DeleteNotebookCommand(NotesVM vm)
        {
            ViewModel = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Notebook selectedNotebook = parameter as Notebook;

            ViewModel.DeleteNotebook(selectedNotebook.Id);
        }
    }
}

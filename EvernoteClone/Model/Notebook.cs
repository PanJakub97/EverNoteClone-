using EvernoteClone.ViewModel;
using EvernoteClone.ViewModel.Commands;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace EvernoteClone.Model
{
    public class Notebook : HasId, INotifyPropertyChanged
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }

        //private EditCommand startEditing;


        //public EditCommand StartEditing
        //{
        //    get
        //    {
        //        return startEditing ?? (startEditing = new EditCommand(() => { IsEditableNotebook = true; }));
        //    }
        //}

        private bool isEditiableNotebook;
        public bool IsEditableNotebook
        {
            get { return isEditiableNotebook; }
            set
            {
                if (isEditiableNotebook == value) return;

                isEditiableNotebook = value;
                OnPropertyChanged("IsEditableNotebook");
                SelectedNotebookChanged?.Invoke(this, new EventArgs());
            }
        }

        private Visibility isVisibleNotebook;
        public Visibility IsVisibleNotebook
        {
            get { return isVisibleNotebook; }
            set
            {
                isVisibleNotebook = value;
                OnPropertyChanged("IsVisibleNotebook");
                SelectedNotebookChanged?.Invoke(this, new EventArgs());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler SelectedNotebookChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

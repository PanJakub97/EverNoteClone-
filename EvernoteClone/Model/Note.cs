using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace EvernoteClone.Model
{
    public interface HasId
    {
        public string Id { get; set; }
    }

    public class Note : HasId,INotifyPropertyChanged
    {
        public string Id { get; set; }
        public string NotebookId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string FileLocation { get; set; }
        private bool isEditiableNote;
        public bool IsEditableNote
        {
            get { return isEditiableNote; }
            set
            {
                isEditiableNote = value;
                OnPropertyChanged("IsEditableNote");
                SelectedNoteChanged?.Invoke(this, new EventArgs());
            }
        }

        private Visibility isVisibleNote;
        public Visibility IsVisibleNote
        {
            get { return isVisibleNote; }
            set
            {
                isVisibleNote = value;
                OnPropertyChanged("IsVisibleNote");
                SelectedNoteChanged?.Invoke(this, new EventArgs());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler SelectedNoteChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

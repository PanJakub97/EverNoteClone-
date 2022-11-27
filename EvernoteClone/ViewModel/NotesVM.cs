using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace EvernoteClone.ViewModel
{
    public class NotesVM : INotifyPropertyChanged
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }
        public ObservableCollection<Note> Notes { get; set; }
        private Notebook selectedNotebook;
        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            {
                if (selectedNotebook == value)
                    return;

                selectedNotebook = value;

                foreach (var item in Notebooks)
                {
                    item.IsEditableNotebook = false;
                }

                OnPropertyChanged("SelectedNotebook");
                SelectedNotebookChanged?.Invoke(this, new EventArgs());
                GetNotes();
            }
        }

        private Note selectedNote;

        public Note SelectedNote
        {
            get { return selectedNote; }
            set
            {
                selectedNote = value;
                OnPropertyChanged("SelectedNote");
                SelectedNoteChanged?.Invoke(this, new EventArgs());
            }
        }

        private Visibility visibleness;
        public Visibility Visibleness
        {
            get { return visibleness; }
            set
            {
                visibleness = value;
                OnPropertyChanged("Visibleness");
            }
        }

        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
        public EditCommand EditCommand { get; set; }
        public EndEditingCommand EndEditingCommand { get; set; }
        public StopNoteEditingCommand StopNoteEditingCommand { get; set; }
        public DeleteNoteCommand DeleteNoteCommand { get; set; }
        public DeleteNotebookCommand DeleteNotebookCommand { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler SelectedNoteChanged;
        public event EventHandler SelectedNotebookChanged;

        public NotesVM()
        {
            NewNoteCommand = new NewNoteCommand(this);
            NewNotebookCommand = new NewNotebookCommand(this);
            EditCommand = new EditCommand(this);
            EndEditingCommand = new EndEditingCommand(this);
            StopNoteEditingCommand =  new StopNoteEditingCommand(this);
            DeleteNoteCommand = new DeleteNoteCommand(this);
            DeleteNotebookCommand = new DeleteNotebookCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            Visibleness = Visibility.Collapsed;

            GetNotebooks();
        }

        public async void CreateNotebook()
        {
            Notebook newNotebook = new Notebook
            {
                Name = "Notebook",
                UserId = App.UserId
            };

            await DatabaseHelper.Insert(newNotebook);

            GetNotebooks();
        }

        public async void CreateNote(string notebookId)
        {
            Note newNote = new Note
            {
                NotebookId = notebookId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Title = $"Note for {DateTime.Now.ToString()}"
            };

            await DatabaseHelper.Insert(newNote);

            GetNotes();
        }

        public async void GetNotebooks()
        {
            var notebooks = (await DatabaseHelper.Read<Notebook>()).Where(n => n.UserId == App.UserId).ToList();

            Notebooks.Clear();
            foreach(var notebook in notebooks)
            {
                Notebooks.Add(notebook);
            }
        }

        private async void GetNotes()
        {
            if (SelectedNotebook != null)
            {
                var notes = (await DatabaseHelper.Read<Note>()).Where(n => n.NotebookId == SelectedNotebook.Id).ToList();

                Notes.Clear();
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void StartEditing()
        {
            Visibleness = Visibility.Visible;
        }

        public async void StopEditingNotebook(Notebook notebook)
        {
            Visibleness = Visibility.Collapsed;
            //SelectedNotebook.IsVisibleNotebook = Visibility.Collapsed;
            //selectedNote.IsVisibleNote = Visibility.Collapsed;
            await DatabaseHelper.Update(notebook);
            GetNotebooks();
        }

        public async void StopEditingNote (Note note)
        {
            Visibleness = Visibility.Collapsed;
            await DatabaseHelper.Update(note);
            GetNotes();
        }

        public async void DeleteNote(string id)
        {
            try
            {
                Note note = new Note()
                {
                    Id = id
                };
                await DatabaseHelper.Delete(note);

                GetNotes();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public async void DeleteNotebook(string id)
        {
            try
            {
                Notebook notebook = new Notebook()
                {
                    Id= id
                };
                await DatabaseHelper.Delete(notebook);
                GetNotebooks();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}

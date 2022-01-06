//인터페이스

using asp.Models;
using System.Collections.Generic;

namespace asp.DataContext.Repositories
{
    public interface INoteRepository
    {
        void Add(Note note);
        IEnumerable<Note> GetAllNotes();
        Note GetNote(int NoteNo);
        void save();
    }
}
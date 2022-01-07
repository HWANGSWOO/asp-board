using asp.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace asp.DataContext.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly NoteDbcontext _context;

        public NoteRepository(NoteDbcontext context) //NoteDbcontext 를 주입
        {
            _context = context; //context를 필드 내에서도 사용할 수 있도록 필드화 시켜줌
        }
        public void Add(Note note)
        {
            _context.Notes.Add(note);
        }
        public void save()
        {
            _context.SaveChanges(); //데이터베이스에 적용
        }
        public IEnumerable<Note> GetAllNotes()
        {
            var result = _context.Notes.ToList();   //변수를 만들어 그 안에 노트의 데이터를 리스트화 시켜 넘긴다.
            return result;
        }
        public Note GetNote(int NoteNo)
        {
            var result = _context.Notes.Find(NoteNo);
            return result;
        }
    }
}

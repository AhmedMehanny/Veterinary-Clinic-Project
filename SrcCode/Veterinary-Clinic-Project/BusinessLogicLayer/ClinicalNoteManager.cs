using System.Collections.Generic;
using DataAccessLayer;
using Models;

namespace BusinessLogicLayer
{
    public class ClinicalNoteManager
    {
        private readonly ClinicalNoteRepository _repo = new ClinicalNoteRepository();

        public List<ClinicalNote> GetAllNotes() => _repo.GetAll();
        public ClinicalNote GetNoteById(int noteId) => _repo.GetById(noteId);
        public List<ClinicalNote> GetNotesByVisitId(int visitId) => _repo.GetByVisitId(visitId);
        public bool AddNote(ClinicalNote note) => _repo.Insert(note) > 0;
        public bool UpdateNote(ClinicalNote note) => _repo.Update(note) > 0;
        public bool DeleteNote(int noteId) => _repo.Delete(noteId) > 0;
    }
}
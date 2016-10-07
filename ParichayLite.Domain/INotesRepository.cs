using System.Collections.Generic;
using ParichayLite.Domain;
using System;
using System.Linq;

namespace ParichayLite.Domain
{
    public interface INotesRepository
        {
            List<Note> GetAll();
            Note Get(Guid id);
            Guid Add(Note note);
            bool Update(Guid id, Note note);
            bool Delete(Guid id);
        }
}
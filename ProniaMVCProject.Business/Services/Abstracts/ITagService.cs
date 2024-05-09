using ProniaMVCProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaMVCProject.Business.Services.Abstracts
{
    public interface ITagService
    {
        Task AddTag(Tag tag);
        void DeleteTag(int id);
        void UpdateTag(int id, Tag newTag);
        Tag GetTag(Func<Tag, bool>? predicate = null);
        List<Tag> GetAllTags(Func<Tag, bool>? predicate = null);



    }
}

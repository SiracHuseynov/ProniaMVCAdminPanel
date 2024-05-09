using ProniaMVCProject.Business.Exceptions;
using ProniaMVCProject.Business.Services.Abstracts;
using ProniaMVCProject.Core.Models;
using ProniaMVCProject.Core.RepositoryAbstracts;
using ProniaMVCProject.Data.RepositoryConcretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaMVCProject.Business.Services.Concretes
{
    public class TagService : ITagService
    {
        ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public async Task AddTag(Tag tag)
        {
            if (!_tagRepository.GetAll().Any(x => x.Name == tag.Name))
            {
                _tagRepository.AddAsync(tag);
                _tagRepository.CommitAsync();
            }
            else
            {
                throw new DuplicateException("Tag adi eyni ola bilmez!");
            }
        }

        public void DeleteTag(int id)
        {
            Tag tag = _tagRepository.Get(x => x.Id == id);

            if (tag == null) throw new NullReferenceException("Bele bir tag tapilmadi");

            _tagRepository.Delete(tag);
            _tagRepository.Commit();
        }

        public List<Tag> GetAllTags(Func<Tag, bool>? predicate = null)
        {
            return _tagRepository.GetAll(predicate);
        }

        public Tag GetTag(Func<Tag, bool>? predicate = null)
        {
            return _tagRepository.Get(predicate);
        }

        public void UpdateTag(int id, Tag newTag)
        {
            Tag tag = _tagRepository.Get(x => x.Id == id);

            if (tag == null) throw new NullReferenceException("Bele bir tag tapilmadi");

            if (!_tagRepository.GetAll().Any(x => x.Name == tag.Name))
            {
                tag.Name = newTag.Name;
            }

            _tagRepository.Commit();
        }
    }
}

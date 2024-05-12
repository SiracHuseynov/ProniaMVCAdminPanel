using ProniaMVCProject.Business.Exceptions;
using ProniaMVCProject.Business.Services.Abstracts;
using ProniaMVCProject.Core.Models;
using ProniaMVCProject.Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaMVCProject.Business.Services.Concretes
{
    public class FeatureService : IFeatureService
    {
        private readonly IFeatureRepository _featureRepository;

        public FeatureService(IFeatureRepository featureRepository)
        {
            _featureRepository = featureRepository;
        }
        public async Task AddFeature(Feature feature)
        {
            await _featureRepository.AddAsync(feature);
            await _featureRepository.CommitAsync();
        }

        public void DeleteFeature(int id)
        {
            var existFeature = _featureRepository.Get(x => x.Id == id);

            if (existFeature == null) throw new EntityNotFoundException("Feature tapilmadi");

            _featureRepository.Delete(existFeature);
            _featureRepository.Commit();

        }

        public List<Feature> GetAllFeatures(Func<Feature, bool>? predicate = null)
        {
            return _featureRepository.GetAll(predicate);
        }

        public Feature GetFeature(Func<Feature, bool>? predicate = null)
        {
            return _featureRepository.Get(predicate);
        }

        public void UpdateFeature(int id, Feature newFeature)
        {
            var oldFeature = _featureRepository.Get(x => x.Id == id);

            if (oldFeature == null) throw new EntityNotFoundException("Feature tapilmadi");

            oldFeature.Icon = newFeature.Icon;
            oldFeature.Title = newFeature.Title;
            oldFeature.Description = newFeature.Description;
            _featureRepository.Commit();
        }
    }
}

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
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;

        public SliderService(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        public async Task AddSlider(Slider slider)
        {
            if (slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
                throw new ImageContextException("File formati duzgun deyil!");

            if (slider.ImageFile.Length > 2097152)
                throw new ImageSizeException("Seklin olcusu max 2mb ola biler!");

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(slider.ImageFile.FileName);

            fileName = fileName.Length + slider.ImageFile.FileName.Length > 100 ?
                slider.ImageFile.FileName.Substring(0, 55) + fileName : fileName + slider.ImageFile.FileName;

            string path = "C:\\Users\\Sirac Huseynov\\Desktop\\codee's repo\\ProniaMVCAdminPanel\\ProniaMVCProject\\wwwroot\\" + "uploads\\sliders\\" + fileName;

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                slider.ImageFile.CopyTo(fileStream);
            }

            slider.ImageUrl = fileName;

            await _sliderRepository.AddAsync(slider);
            await _sliderRepository.CommitAsync();
        }

        public void DeleteSlider(int id)
        {
            var slider = _sliderRepository.Get(x => x.Id == id);

            if (slider == null) throw new EntityNotFoundException("Bele bir slider yoxdur!");

            _sliderRepository.Delete(slider);
            _sliderRepository.Commit();
        }

        public List<Slider> GetAllSliders(Func<Slider, bool>? predicate = null)
        {
            return _sliderRepository.GetAll(predicate);
        }

        public Slider GetSlider(Func<Slider, bool>? predicate = null)
        {
            return _sliderRepository.Get(predicate);
        }

        public void UpdateSlider(int id, Slider newSlider)
        {
            var oldSlider = _sliderRepository.Get(x => x.Id == id);

            if (oldSlider == null) throw new EntityNotFoundException("Bele bir slider yoxdur!");


            if (newSlider.ImageFile.ContentType != "image/png" && newSlider.ImageFile.ContentType != "image/jpeg")
                throw new ImageContextException("File formati duzgun deyil!");

            if (newSlider.ImageFile.Length > 2097152)
                throw new ImageSizeException("Seklin olcusu max 2mb ola biler!");


            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(newSlider.ImageFile.FileName);

            fileName = fileName.Length + newSlider.ImageFile.FileName.Length > 100 ?
                newSlider.ImageFile.FileName.Substring(0, 55) + fileName : fileName + newSlider.ImageFile.FileName;

            string path = "C:\\Users\\Sirac Huseynov\\Desktop\\codee's repo\\ProniaMVCAdminPanel\\ProniaMVCProject\\wwwroot\\" + "uploads\\sliders\\" + fileName;

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                newSlider.ImageFile.CopyTo(fileStream);
            }


            newSlider.ImageUrl = fileName;

            oldSlider.Title = newSlider.Title;
            oldSlider.Description = newSlider.Description;
            oldSlider.RedirectUrl = newSlider.RedirectUrl;
            oldSlider.ImageUrl = newSlider.ImageUrl;
            oldSlider.ImageFile = newSlider.ImageFile;

            _sliderRepository.Commit();


        }


    }
}


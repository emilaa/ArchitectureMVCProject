using DataAccess.Repositories.Abstract;
using Web.Services.Abstract;
using Web.ViewModels.Home;

namespace Web.Services.Concrete
{
    public class HomeService : IHomeService
    {
        private readonly IHomeMainSliderRepository _homeMainSliderRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMedicalDepartmentRepository _medicalDepartmentRepository;
        private readonly IOurVisionRepository _ourVisionRepository;
        private readonly IWhyChooseUsRepository _whyChooseUsRepository;
        private readonly IAboutRepository _aboutRepository;
        private readonly IAboutPhotoRepository _aboutPhotoRepository;
        private readonly IHomeVideoRepository _homeVideoRepository;
        private readonly INewsRepository _newsRepository;
        private readonly IStatisticRepository _statisticRepository;

        public HomeService(IHomeMainSliderRepository homeMainSliderRepository,
            IDoctorRepository doctorRepository,
            IMedicalDepartmentRepository medicalDepartmentRepository,
            IOurVisionRepository ourVisionRepository,
            IWhyChooseUsRepository whyChooseUsRepository,
            IAboutRepository aboutRepository,
            IAboutPhotoRepository aboutPhotoRepository,
            IHomeVideoRepository homeVideoRepository,
            INewsRepository newsRepository,
            IStatisticRepository statisticRepository)
        {
            _homeMainSliderRepository = homeMainSliderRepository;
            _doctorRepository = doctorRepository;
            _medicalDepartmentRepository = medicalDepartmentRepository;
            _ourVisionRepository = ourVisionRepository;
            _whyChooseUsRepository = whyChooseUsRepository;
            _aboutRepository = aboutRepository;
            _aboutPhotoRepository = aboutPhotoRepository;
            _homeVideoRepository = homeVideoRepository;
            _newsRepository = newsRepository;
            _statisticRepository = statisticRepository;
        }
        public async Task<HomeIndexVM> GetAllAsync()
        {
            var model = new HomeIndexVM
            {
                HomeMainSlider = await _homeMainSliderRepository.GetAllAsync(),
                Doctors = await _doctorRepository.GetHomeDoctorsAsync(),
                MedicalDepartments = await _medicalDepartmentRepository.GetAllAsync(),
                OurVision = await _ourVisionRepository.GetAllAsync(),
                WhyChooseUs = await _whyChooseUsRepository.GetAsync(),
                About = await _aboutRepository.GetAsync(),
                AboutPhoto = await _aboutPhotoRepository.GetAllAsync(),
                HomeVideo = await _homeVideoRepository.GetAsync(),
                LastestNews = await _newsRepository.GetAllAsync(),
                Statistics = await _statisticRepository.GetAllAsync(),
            };

            return model;
        }
    }
}

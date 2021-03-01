using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MockSchool.Web.DataRepositories;
using MockSchool.Web.ViewModels;

namespace MockSchool.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<HomeController> _logger;


        public HomeController(IWebHostEnvironment webHostEnvironment, IStudentRepository studentRepository, ILogger<HomeController> logger)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._studentRepository = studentRepository;
            this._logger = logger;
        }

        public IActionResult Index()
        {
            //_logger.LogTrace("TraceLog");
            //_logger.LogDebug("DebugLog");
            //_logger.LogInformation("InformationLog");
            //_logger.LogWarning("WraningLog");
            //_logger.LogError("ErrorLog");
            //_logger.LogCritical("CriticalLog");

            return View(_studentRepository.GetAllStudents());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniquedFileName = ProcessUploadFile(model);

                var student = new Student
                {
                    Name = model.Name,
                    Email = model.Email,
                    Major = model.Major,
                    PhotoPath = uniquedFileName
                };

                _studentRepository.Insert(student);

                return RedirectToAction("detail", new { id = student.Id });

            }

            return View();
        }


        [HttpPost]
        public IActionResult Edit(StudentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = _studentRepository.GetStudentById(model.Id);

                if (student == null)
                {
                    ViewBag.ErrorMessage = $"学生Id={model.Id}不存在";
                    return View("NotFound");
                }

                student.Major = model.Major;
                student.Name = model.Name;
                student.Email = model.Email;

                var fileName = ProcessUploadFile(model);

                if (fileName != null)
                {
                    student.PhotoPath = fileName;
                }

                var newStudent = _studentRepository.Update(student);

                return View("Detail", new StudentDetailViewModel { Student = newStudent, PageTitle = "学生详情页" });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            var student = _studentRepository.GetStudentById(id);

            if (student == null)
            {
                ViewBag.ErrorMessage = $"学生Id={id}不存在";
                return View("NotFound");
            }

            var studentEditViewModel = new StudentEditViewModel
            {
                Id = student.Id,
                Email = student.Email,
                ExistintPhotoPath = student.PhotoPath,
                Major = student.Major,
                Name = student.Name
            };

            return View(studentEditViewModel);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            if (id == 0)
            {
                throw new NullReferenceException();
            }

            var student = _studentRepository.GetStudentById(id);

            if (student == null)
            {
                ViewBag.ErrorMessage = $"学生Id={id}不存在";
                //Response.StatusCode = 404;

                return View("NotFound");
            }

            var viewModel = new StudentDetailViewModel()
            {
                Student = student,
                PageTitle = "学生详情"
            };

            return View(viewModel);
        }

        public IActionResult Details(int id)
        {
            return View();
        }

        /// <summary>
        /// 处理上传的图片,多图保存
        /// </summary>
        /// <param name="studentCreateViewModel"></param>
        /// <returns></returns>
        private string ProcessUploadFile(StudentCreateViewModel studentCreateViewModel)
        {
            string fileName = null;

            if (studentCreateViewModel.Photos != null && studentCreateViewModel.Photos.Count > 0)
            {
                var fileFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "avatars");

                foreach (var photo in studentCreateViewModel.Photos)
                {
                    fileName = Guid.NewGuid().ToString() + photo.FileName.Substring(photo.FileName.LastIndexOf('.'));

                    var filePath = Path.Combine(fileFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(stream);
                    }
                }
            }

            return fileName;
        }
    }
}

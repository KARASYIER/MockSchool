using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MockSchool.Web.DataRepositories;
using MockSchool.Web.ViewModels;

namespace MockSchool.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStudentRepository _studentRepository;


        public HomeController(IWebHostEnvironment webHostEnvironment, IStudentRepository studentRepository)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._studentRepository = studentRepository;
        }

        public IActionResult Index()
        {
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
                string uniquedFileName = null;

                if (model.Photos != null && model.Photos.Count > 0)
                {
                    var uploadFolder = System.IO.Path.Combine(_webHostEnvironment.WebRootPath, "images", "avatars");

                    foreach (var photo in model.Photos)
                    {
                        uniquedFileName = Guid.NewGuid().ToString() + photo.FileName.Substring(photo.FileName.LastIndexOf('.') + 1);

                        var filePath = System.IO.Path.Combine(uploadFolder, uniquedFileName);

                        photo.CopyTo(new System.IO.FileStream(filePath, System.IO.FileMode.Create));
                    }

                }

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
                    return View("Error");
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
                return View("Error");
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
            var student = _studentRepository.GetStudentById(id);

            if (student == null)
            {
                ViewBag.ErrorMessage = $"学生Id={id}不存在";
                return View("Error");
            }

            var viewModel = new StudentDetailViewModel()
            {
                Student = student,
                PageTitle = "学生详情"
            };

            return View(viewModel);
        }

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

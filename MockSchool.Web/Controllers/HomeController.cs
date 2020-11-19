using System;
using System.Collections.Generic;
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
            return View();
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

                if (model.Photo != null)
                {
                    var uploadFolder = System.IO.Path.Combine(_webHostEnvironment.WebRootPath, "images");

                    uniquedFileName = $"{Guid.NewGuid().ToString()}_{model.Photo.FileName}";

                    var filePath = System.IO.Path.Combine(uploadFolder, uniquedFileName);

                    model.Photo.CopyTo(new System.IO.FileStream(filePath, System.IO.FileMode.Create));
                }

                var student = new Student
                {
                    Name = model.Name,
                    Email = model.Email,
                    Major = model.Major,
                    PhohtPath = uniquedFileName
                };

                _studentRepository.Insert(student);

                return RedirectToAction("detail", new { id = student.Id });

            }

            return View();
        }

        public IActionResult Detail(int id)
        {
            var student = _studentRepository.GetStudentById(id);

            if (student == null)
            {
                ViewBag.ErrorMessage = $"学生Id={id}不存在";
                return View("Not Found");
            }

            var viewModel = new StudentDetailViewModel()
            {
                Student = student,
                PageTitle = "学生详情"
            };



            return View(viewModel);

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApp.Data;
using StudentApp.Models;
using StudentApp.Models.Entities;

namespace StudentApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public StudentController(ApplicationDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }  

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel) 
        {
            // Burada service & implementasyonu
            //          repo & implementasyonu
            //              parametredeki model -> Dto
            //                  mapper ile Entity-Dto birleştirililip Save() metoduna yollanılmalı.
            // Şimdilik tek entity için bu şekilde tamamlandı.

            var saveStudent = new Student();
            saveStudent.Subscribe = viewModel.Subscribe;
            saveStudent.Name = viewModel.Name;  
            saveStudent.Email = viewModel.Email;    
            saveStudent.Phone = viewModel.Phone;

            await dbContext.Students.AddAsync(saveStudent);
            await dbContext.SaveChangesAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List() 
        {
           var studentList = await dbContext.Students.ToListAsync();

            return View(studentList);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var editStudent = await dbContext.Students.FindAsync(id);

            return View(editStudent);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            var updateStudent = await dbContext.Students.FindAsync(student.Id);
            if (updateStudent != null)
            {
                updateStudent.Name = student.Name;
                updateStudent.Email = student.Email;
                updateStudent.Phone = student.Phone;
                updateStudent.Subscribe = student.Subscribe;

                //dbContext.Students.Update(updateStudent);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student student) 
        {
            var deleteStudent = await dbContext.Students.FindAsync(student.Id);

            if(deleteStudent != null) 
            {
                dbContext.Students.Remove(deleteStudent);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Student");
        }


    }
}

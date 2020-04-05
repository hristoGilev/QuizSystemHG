﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizSystem.Services.Data;
using QuizSystem.Web.ViewModels.ExamUses;

namespace QuizSystem.Web.Controllers
{
    public class ExamUsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IExamUsersService examUsersService;

        public ExamUsersController(IUsersService usersService, IExamUsersService examUsersService)
        {
            this.usersService = usersService;
            this.examUsersService = examUsersService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddUsersToExam(int id)
        {
            var model = new ExamUsersview() { examUsers = this.usersService.GetAll<ExamUserView>(), ExamId = id };
            return this.View(model);

        }

        [Authorize]
        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> AddUsersToExamAsync(string[] username, string idexam)
        {
           await this.examUsersService.AddUserToExamAsync(int.Parse(idexam), username);

           return this.RedirectToAction("Index", "Home");
        }
    }
}

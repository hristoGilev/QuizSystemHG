﻿namespace QuizSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using QuizSystem.Data.Common.Repositories;
    using QuizSystem.Data.Models;
    using QuizSystem.Services.Data;
    using QuizSystem.Web.ViewModels.Exams;

    public class AnswersController : Controller
    {
        private readonly IAnswersSerrvice answersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<ExamUser> repository;
        private readonly IExamsService examsService;

        public AnswersController(
            IAnswersSerrvice answersService,
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<ExamUser> repository,
            IExamsService examsService)
        {
            this.answersService = answersService;
            this.userManager = userManager;
            this.repository = repository;
            this.examsService = examsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync(string id, string examId, string answer)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var exams = this.repository.All().Where(t => t.UserId == user.Id).Select(t => t.ExamId);
            if (!exams.Contains(examId))
            {
                return this.Content("Не сте добавен към изпита!");
            }

            var exam = this.examsService.GetById<ExamViewModel>(int.Parse(examId));
            var questionFromExamId = exam.Questions.Select(t => t.Id).ToList();

            if (exam == null || !questionFromExamId.Contains(int.Parse(id)))
            {
                return this.RedirectToAction("Index", "Home");
            }

            await this.answersService.CreateAsync(id, answer, user.Id);
            return this.RedirectToAction("ById", "Exams", new { id = examId });
        }
    }
}

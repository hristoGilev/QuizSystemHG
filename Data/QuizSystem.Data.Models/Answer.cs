﻿namespace QuizSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using QuizSystem.Data.Common.Models;

    public class Answer : BaseDeletableModel<int>
    {
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}

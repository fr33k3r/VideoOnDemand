﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VideoOnDemand.Data;
using VideoOnDemand.Models;
using VideoOnDemand.Models.DTOModels;

namespace VideoOnDemand.Controllers
{
    public class UserCoursesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserStore<ApplicationUser, IdentityRole, ApplicationDbContext> _userStore;

        public UserCoursesController(UserStore<ApplicationUser, IdentityRole, ApplicationDbContext> userStore)
        {
            _db = userStore.Context;
            _userStore = userStore;
        }

        public IActionResult Index()
        {
            var model = _db.Courses
                .Join(_db.UserCourses, c => c.Id, uc => uc.CourseId, (c, uc) => new { Courses = c, UserCourses = uc })
                .Select(s => new UserCourseDTO { CourseId = s.Courses.Id, CourseTitle = s.Courses.Title, UserId = s.UserCourses.UserId, UserEmail = _userStore.Users.FirstOrDefault(u => u.Id.Equals(s.UserCourses.UserId)).Email });

            return View(model);
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wymieniator.DAL;

namespace Wymieniator.Controllers
{
    public class BookController : Controller
    {
        private WymieniatorContext db = new WymieniatorContext();

        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int? id)
        {
            if(id == 0 || !id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            var book = db.Books.Find(id);
            return View(book);
        }

        public ActionResult ListOfBooksInCategory(string nameOfCategory)
        {
            if(nameOfCategory == null || nameOfCategory.Equals(string.Empty))
            {
                return RedirectToAction("Index", "Home");
            }

            var categories = db.Categories.Include("Books").Where(c => c.Name == nameOfCategory).Single();
            var books = categories.Books.ToList();
            return View(books);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60000)]
        public ActionResult MenuCategory()
        {
            var categories = db.Categories.ToList();
            return PartialView("_MenuCategory", categories);
        }

        public ActionResult BooksPrompt(string term)
        {
            var books = db.Books.Where(a => !a.Hidden && a.Title.ToLower().Contains(term.ToLower())).
                        Take(5).Select(a => new { label = a.Title });
            return Json(books, JsonRequestBehavior.AllowGet);
        }

    }
}
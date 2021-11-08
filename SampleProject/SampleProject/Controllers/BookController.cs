using SampleProject.Entity;
using SampleProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleProject.Controllers
{
    public class BookController : Controller
    {
        // GET: Book

        BookDBEntities db = new BookDBEntities();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Book book)
        {
            //create db book object
            tblBook tblBook = new tblBook();
            //assigning values
            tblBook.BookAuthor = book.BookAuthor;
            tblBook.BookName = book.BookName;

            //adding data to tblbooks
            db.tblBooks.Add(tblBook);

            //save changes to the database
            db.SaveChanges();

            //return back to the view
            return RedirectToAction("BookList");
        }

        public ActionResult BookList()
        {
            List<Book> booklist = new List<Book>();
            var booksdata=db.tblBooks.ToList();
            foreach (var item in booksdata)
            {
                booklist.Add(new Book { BookName=item.BookName,BookAuthor=item.BookAuthor});
            }

            return View(booklist);
        }
    }
}
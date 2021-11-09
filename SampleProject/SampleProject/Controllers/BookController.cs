using SampleProject.Entity;
using SampleProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            ViewBag.ButtonName = "Create";
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
                booklist.Add(new Book { BookId=item.ID,BookName=item.BookName,BookAuthor=item.BookAuthor});
            }

            return View(booklist);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var book = db.tblBooks.Where(x => x.ID == id).FirstOrDefault();
            Book objbook = new Book();
            objbook.BookId = book.ID;
            objbook.BookAuthor = book.BookAuthor;
            objbook.BookName = book.BookName;
            ViewBag.ButtonName = "Update";
            return View("Index", objbook);
        }
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            var Objbook = db.tblBooks.Where(x => x.ID == book.BookId).FirstOrDefault();
            Objbook.BookAuthor = book.BookAuthor;
            Objbook.BookName = book.BookName;
            db.Entry(Objbook).State= EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("BookList");
        }
    }
}
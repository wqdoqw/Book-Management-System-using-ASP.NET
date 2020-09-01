using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.IO;

namespace WebApplication2.Controllers
{
    public class BooksController : Controller
    {

        
        ServiceReference1.WebService1SoapClient webService = new ServiceReference1.WebService1SoapClient();

        private s4340813 db = new s4340813();

        // GET: Books
        public ActionResult Index()
        {
            

            return View(db.Books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

     

        public ActionResult showAll()
        {
            foreach (Book b in db.Books)
            {
                db.Books.Remove(b);

            }
            db.SaveChanges();
            String file = @"C:\Users\USER\Downloads\WebApplication2\WebApplication2\WebApplication2\books.txt";
            List<String> books = webService.readBooks(file);
            foreach (String line in books)
            {
                String[] tokens = line.Split(',');

               
                   Book b = new Book
                   { 
                    
                    BookID = Convert.ToInt32(tokens[0]),
                    Name = tokens[1],
                    Author = tokens[2],
                    Year = tokens[3],
                    Price = Convert.ToDecimal(tokens[4].Substring(1)),
                    Stock = Convert.ToInt32(tokens[5])

                };
                db.Books.Add(b);
                db.SaveChanges();
            }
           // redirectToIndex();
            return View(db.Books.ToList());


            
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addBook([Bind(Include = "ID,BookID,Name,Author,Year,Price,Stock")] Book book)
        {
            // var book = new Book { BookID = BookID, Name = Name, Author = Author, Year = Year, Price = price, Stock = stock };
            //  String file = @"C:\Users\s4340813\Downloads\Prac2\Prac2\books.txt";

            //  List<String> books = webService.readBooks(file);

            // List<String> books = webService.writeBooks(line,file);
            
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(book);

        }
        public ActionResult addBook()
        {
            return View();
        }


        public ActionResult redirectToIndex()
        {
           
            return RedirectToAction("index");
        }
      
        
        public ActionResult showResults(String UserInput, String UserSelect)
        {
            List<Book> books = new List<Book>();
            if (UserInput == "")
            {
                return Content("<script>alert('Error please check your input')</script>");
                
            }
            else if (UserSelect == "ID")
            {
                int bookID = Convert.ToInt32(UserInput);
                foreach (Book b in db.Books)
                {
                    if (b.BookID == bookID)
                    {
                        books.Add(b);
                    }

                }
            }

            else if (UserSelect == "Name")
            {
                string bookName = UserInput;
                foreach (Book b in db.Books)
                {
                    if (b.Name == bookName)
                    {
                        books.Add(b);
                    }

                }
            }
            else if (UserSelect == "Author")
            {
                string authorName = UserInput;
                foreach (Book b in db.Books)
                {
                    if (b.Author == authorName)
                    {
                        books.Add(b);
                    }

                }
            }
            else if (UserSelect == "Year")
            {
                string year = UserInput;
                foreach (Book b in db.Books)
                {
                    if (b.Year == year)
                    {
                        books.Add(b);
                    }

                }
            }
         
                return View(books);
        }
   
        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BookID,Name,Author,Year,Price,Stock")] Book book)
        {
           // var book = new Book { BookID = BookID, Name = Name, Author = Author, Year = Year, Price = price, Stock = stock };

            String file = @"C:\Users\USER\Downloads\WebApplication2\WebApplication2\WebApplication2\books.txt";
            String line = book.BookID.ToString() + "," + book.Name + "," + book.Author + "," + book.Year + ",$" + book.Price.ToString() + "," + book.Stock.ToString();

            if (ModelState.IsValid)
            {
              webService.writeBooks(line,file);
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("index");
            }

            return View(book);
        }
        
        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BookID,Name,Author,Year,Price,Stock")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
      
        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Book book = db.Books.Find(id);
           

            db.Books.Remove(book);
                db.SaveChanges();
                return RedirectToAction("index");
            }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

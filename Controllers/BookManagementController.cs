using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookManagementApp.Models;
using Microsoft.AspNetCore.Http;

namespace BookManagementApp.Controllers
{
    /// <summary>
    /// 書籍管理に関する処理を行うコントローラー
    /// </summary>
    public class BookManagementController : Controller
    {
        /// <summary>
        /// DBコンテキスト
        /// </summary>
        private readonly BookManagementAppContext _context;

        /// <summary>
        /// DBコンテキストであるBookManagementAppContextを代入し、いつでもDBコンテキストを使用可能
        /// </summary>
        /// <param name="context">DBコンテキスト</param>
        public BookManagementController(BookManagementAppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// ListのGET処理にリダイレクト
        /// </summary>
        /// <returns>ListメソッドのGET処理</returns>
        public IActionResult Index()
        {
            return RedirectToAction(nameof(List));
        }

        /// <summary>
        /// データベースにあるすべての書籍情報を取得後、Views>BookManagement>List.cshtmlに移行
        /// </summary>
        /// <returns>書籍一覧画面</returns>
        public async Task<IActionResult> List()
        {
            var bookManagementAppContext = _context.Book.Include(b => b.Person);
            return View(await bookManagementAppContext.ToListAsync());
        }

        /// <summary>
        /// 入力されたデータをもとに書籍の検索を行い、その結果をViews>BookManagement>List.cshtmlに返す
        /// </summary>
        /// <param name="booktitle">書籍タイトル</param>
        /// <param name="author">著者名</param>
        /// <param name="publisher">出版社名</param>
        /// <returns>書籍一覧画面</returns>
        [HttpPost]
        public async Task<IActionResult> List(string booktitle, string author, string publisher)
        {
            string DepName = HttpContext.Session.GetString("DepartmentName");

            if (string.IsNullOrWhiteSpace(booktitle) && string.IsNullOrWhiteSpace(author) && string.IsNullOrWhiteSpace(publisher) && string.IsNullOrWhiteSpace(Request.Form["flag"]))
            {
                var bookManagementAppContext = _context.Book.Include(b => b.Person);
                return View(await bookManagementAppContext.ToListAsync());
            }
            else if (string.IsNullOrWhiteSpace(booktitle) && string.IsNullOrWhiteSpace(author) && string.IsNullOrWhiteSpace(publisher))
            {
                return View(await _context.Book.Where(m => m.ManagementDepartment.Contains(DepName)).ToListAsync());
            }
            else if (string.IsNullOrWhiteSpace(booktitle) && string.IsNullOrWhiteSpace(author) && string.IsNullOrWhiteSpace(Request.Form["flag"]))
            {
                return View(await _context.Book.Where(m => m.Company.Contains(publisher)).ToListAsync());
            }
            else if (string.IsNullOrWhiteSpace(booktitle) && string.IsNullOrWhiteSpace(publisher) && string.IsNullOrWhiteSpace(Request.Form["flag"]))
            {
                return View(await _context.Book.Where(m => m.Writer.Contains(author)).ToListAsync());
            }
            else if (string.IsNullOrWhiteSpace(author) && string.IsNullOrWhiteSpace(publisher) && string.IsNullOrWhiteSpace(Request.Form["flag"]))
            {
                return View(await _context.Book.Where(m => m.Title.Contains(booktitle)).ToListAsync());
            }
            else if (string.IsNullOrWhiteSpace(publisher) && string.IsNullOrWhiteSpace(Request.Form["flag"]))
            {
                return View(await _context.Book.Where(m => m.Title.Contains(booktitle) && m.Writer.Contains(author)).ToListAsync());
            }
            else if (string.IsNullOrWhiteSpace(author) && string.IsNullOrWhiteSpace(Request.Form["flag"]))
            {
                return View(await _context.Book.Where(m => m.Title.Contains(booktitle) && m.Company.Contains(publisher)).ToListAsync());
            }
            else if (string.IsNullOrWhiteSpace(booktitle) && string.IsNullOrWhiteSpace(Request.Form["flag"]))
            {
                return View(await _context.Book.Where(m => m.Writer.Contains(author) && m.Company.Contains(publisher)).ToListAsync());
            }
            else if (string.IsNullOrWhiteSpace(author) && string.IsNullOrWhiteSpace(publisher))
            {
                return View(await _context.Book.Where(m => m.Title.Contains(booktitle) && m.ManagementDepartment.Contains(DepName)).ToListAsync());
            }
            else if (string.IsNullOrWhiteSpace(booktitle) && string.IsNullOrWhiteSpace(author))
            {
                return View(await _context.Book.Where(m => m.Company.Contains(publisher) && m.ManagementDepartment.Contains(DepName)).ToListAsync());
            }
            else if (string.IsNullOrWhiteSpace(booktitle) && string.IsNullOrWhiteSpace(publisher))
            {
                return View(await _context.Book.Where(m => m.Writer.Contains(author) && m.ManagementDepartment.Contains(DepName)).ToListAsync());
            }
            else if (string.IsNullOrWhiteSpace(booktitle))
            {
                return View(await _context.Book.Where(m => m.Writer.Contains(author) && m.Company.Contains(publisher) && m.ManagementDepartment.Contains(DepName)).ToListAsync());
            }
            else if (string.IsNullOrWhiteSpace(author))
            {
                return View(await _context.Book.Where(m => m.Title.Contains(booktitle) && m.Company.Contains(publisher) && m.ManagementDepartment.Contains(DepName)).ToListAsync());
            }
            else if (string.IsNullOrWhiteSpace(publisher))
            {
                return View(await _context.Book.Where(m => m.Title.Contains(booktitle) && m.Writer.Contains(author) && m.ManagementDepartment.Contains(DepName)).ToListAsync());
            }
            else if (string.IsNullOrWhiteSpace(Request.Form["flag"]))
            {
                return View(await _context.Book.Where(m => m.Title.Contains(booktitle) && m.Writer.Contains(author) && m.Company.Contains(publisher)).ToListAsync());
            }
            else
            {
                return View(await _context.Book.Where(m => m.Title.Contains(booktitle) && m.Writer.Contains(author) && m.Company.Contains(publisher) && m.ManagementDepartment.Contains(DepName)).ToListAsync());
            }
        }

        /// <summary>
        /// 必要なデータをViewDataに保管した後、Views>BookManagement>Register.cshtmlに移行
        /// </summary>
        /// <returns>書籍登録画面</returns>
        public IActionResult Register()
        {
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "PersonId");
            ViewData["UserId"] = HttpContext.Session.GetString("UserId");
            ViewData["DepartmentName"] = HttpContext.Session.GetString("DepartmentName");
            ViewData["Now"] = DateTime.Now.ToString("yyyy/MM/dd");
            return View();
        }

        /// <summary>
        /// 入力されたデータの検証を行い、問題がなければ書籍の登録を行う
        /// 入力されたデータに不備がある場合入力データを保持したまま元のViewに戻る
        /// 書籍の登録が完了した場合、Indexメソッドにリダイレクトする
        /// </summary>
        /// <param name="book">フォームに入力されたデータがまとめられたBookデータ</param>
        /// <returns>Indexメソッドにリダイレクト</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("BookId,Title,Writer,Company,Price,Buydate,ManagementDepartment")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.Updatedate = DateTime.Now;
                book.PersonId = (int)HttpContext.Session.GetInt32("PersonId");
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "PersonId", book.PersonId);
            return View(book);
        }

        /// <summary>
        /// データベースに存在するidか検証を行い、存在するデータであれば検索した書籍のデータを持ってViews>BookManagement>Detail.cshtmlに移行する
        /// 存在しないデータの場合404を表示する
        /// </summary>
        /// <param name="id">BookId</param>
        /// <returns>書籍詳細画面</returns>
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Person)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            ViewData["BuyDate"] = book.Buydate?.ToString("yyyy/MM/dd");
            ViewData["UpdateDate"] = book.Updatedate.ToString("yyyy/MM/dd");

            return View(book);
        }

        /// <summary>
        /// データベースに存在するidか検証を行い、存在するデータであれば検索した書籍のデータを持ってViews>BookManagement>Edit.cshtmlに遷移する
        /// idが存在しない場合は404に遷移
        /// /// </summary>
        /// <param name="id">BookId</param>
        /// <returns>編集画面</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "PersonId", book.PersonId);
            ViewData["UserId"] = HttpContext.Session.GetString("UserId");
            ViewData["DepartmentName"] = HttpContext.Session.GetString("DepartmentName");
            ViewData["Now"] = DateTime.Now.ToString("yyyy/MM/dd");
            return View(book);
        }

        /// <summary>
        /// データベースに存在するidかつ正しい値が入力されているか検証を行い、問題がなければ更新を行いIndexメソッドにリダイレクトする
        /// 入力データに問題があった場合は、入力したデータを保持したまま元の書籍編集画面に戻る
        /// </summary>
        /// <param name="id">BookId</param>
        /// <param name="book">フォームに入力されたデータがまとめられたBookデータ</param>
        /// <returns>Indexメソッドにリダイレクト</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,Writer,Company,Price,Buydate,ManagementDepartment")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    book.Updatedate = DateTime.Now;
                    book.PersonId = (int)HttpContext.Session.GetInt32("PersonId");
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "PersonId", book.PersonId);
            return View(book);
        }

        /// <summary>
        /// 指定されたidの検索を行い、そのデータを削除する
        /// 削除後はIndexメソッドにリダイレクト
        /// </summary>
        /// <param name="id">BookId</param>
        /// <returns>Indexメソッドにリダイレクト</returns>
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 指定されたIDが存在するか検索を行うメソッド
        /// </summary>
        /// <param name="id">BookId</param>
        /// <returns>bool値</returns>
        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.BookId == id);
        }

    }
}

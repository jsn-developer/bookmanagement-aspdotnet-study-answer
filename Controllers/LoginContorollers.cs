using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BookManagementApp.Models;
using Microsoft.AspNetCore.Http;

namespace BookManagementApp.Controllers
{
    /// <summary>
    /// ログイン処理に関するコントローラー
    /// </summary>
    public class LoginController : Controller
    {
        /// <summary>
        /// DBコンテキスト
        /// </summary>
        private readonly BookManagementAppContext _context;

        /// <summary>
        /// DBコンテキストであるBookManagementAppContextを代入し、いつでもDBコンテキストを使用可能
        /// </summary>
        /// <param name="context">DBコンテキスト</param>
        public LoginController(BookManagementAppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// アクセス時にMainアクションメソッドにリダイレクト
        /// </summary>
        /// <returns>Mainアクションメソッドにリダイレクト</returns>
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Main));
        }

        /// <summary>
        /// アクセス時にViews>Login>Index.cshtmlに移行
        /// </summary>
        /// <returns>ログイン画面</returns>
        public IActionResult Main()
        {
            return View();
        }

        /// <summary>
        /// データが入力されているか検証を行い、問題なければ入力されたデータと一致するユーザーデータがあるか検索を行う
        /// 該当データがない場合は元のログイン画面に戻り、存在する場合はBookManagementControllerのIndexにリダイレクトする
        /// </summary>
        /// <param name="person">フォームに入力されたPersonデータ</param>
        /// <returns>BookManagementControllerのIndexメソッドにリダイレクト</returns>
        [HttpPost]
        public async Task<IActionResult> Form([Bind("UserId,Password")] Person person)
        {
            if (String.IsNullOrWhiteSpace(person.UserId) || String.IsNullOrWhiteSpace(person.Password))
            {
                ViewData["Message"] = "どちらかが空もしくは入力されていません";
                return View("Main");
            }

            var user = await _context.Person.FirstOrDefaultAsync(m => m.UserId == person.UserId && m.Password == person.Password);
            if (user == null)
            {
                ViewData["Message"] = "該当者なし";
                return View("Main");
            }
            HttpContext.Session.SetString("UserId", person.UserId);
            HttpContext.Session.SetString("Password", person.Password);
            HttpContext.Session.SetString("DepartmentName", user.DepartmentName);
            HttpContext.Session.SetInt32("PersonId", user.PersonId);
            return RedirectToRoute("Default", new { Controller = "bookManagement", Action = "Index" });
        }
    }
}

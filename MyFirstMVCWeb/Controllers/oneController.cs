﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFirstMVCWeb.Models;

namespace MyFirstMVCWeb.Controllers
{    //HomeController理解為 控制器名稱叫home, 
    //控制器後面必須加上單詞Controller,這是一個規定
    public class oneController : Controller
    {
        // GET: one
        userEntities19 db = new userEntities19();

        public ActionResult LogOff()
        {//登出
            int w = 0;

            Session.Clear();
            return RedirectToAction("register");
        }

        //獲取用戶登入資料
        [HttpPost]
        public ActionResult login(string stud, string Pwd)
        {
            //根據用戶輸入的數據查詢,如果不符合提醒用戶並重新輸入
            //如果ok下一
            string time = Request.QueryString["cratetime"];
            string status = Request.QueryString["status"];
            firstTable_2 u = db.firstTable_2.FirstOrDefault(t => t.user == stud);
            if (u != null)
            {
                if (u.password == Pwd)
                {
                    if (u.power == "teacher")
                    {
                        Session["UserID"] = u.user;
                        Session["name"] = u.name;

                        return RedirectToAction("home", "data");
                    }
                    else
                    {
                        Session["UserID"] = u.user;
                        Session["name"] = u.name;
                        return RedirectToAction("student", "student");
                    }

                }
                else
                {
                    TempData["Error"] = "帳密有誤,請重新輸入";
                    ViewData["Error"] = "帳密有誤,請重新輸入";
                    return RedirectToAction("register");
                }
            }
            else
            {
                TempData["Error"] = "不存在的帳密";
                ViewData["Error"] = "不存在的帳密";
                return RedirectToAction("register");
            }
        }
        public ActionResult register(string wq)
        {
            if (wq == "1")
            {
                ViewData["Error"] = "請重新登入";
            }

            string Date = DateTime.Now.ToString("yyyy-MM-dd");
            List<allrollcallTable_1> ew = db.allrollcallTable_1.ToList();
            semesterTable_1 we = db.semesterTable_1.FirstOrDefault(t => t.data == Date);
            if (we != null)
            {
                foreach (var w in ew)
                {
                    if (w.semester == null)
                    {
                        w.semester = we.id;
                        db.SaveChanges();
                    }
                }
            }
            return View();
        }
    }
}
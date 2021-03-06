﻿using ModernMarketResearch.Areas.Admin.Models.DAL;
using ModernMarketResearch.Areas.Admin.Models.ViewModel;
using ModernMarketResearch.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModernMarketResearch.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {

            private NewsRepository _ObjNewsRepository;
        public NewsController()
        {
            _ObjNewsRepository = new NewsRepository();
        }


        //NewsRepository ObjNewsRepository = new NewsRepository();
        ModernMarketResearchEntities db = new ModernMarketResearchEntities();
       // [CustomAuthorization("ReportUploader,ReportCreater", "Create,Delete")]
        public ActionResult NewsIndex()
        {
            return View(_ObjNewsRepository.GetNews());
        }
        [HttpGet]
      //  [CustomAuthorization("ReportUploader,ReportCreater", "Create,Delete")]
        public ActionResult NewsCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
      //  [CustomAuthorization("ReportUploader,ReportCreater", "Create,Delete")]
        public ActionResult NewsCreate(NewsVM news)
        {
            var newstitle = db.NewsMasters.Where(x => x.NewsTitle == news.NewsTitle).Select(x => x.NewsTitle).FirstOrDefault();

            if (newstitle == null)
            {
                _ObjNewsRepository.InsertNews(news);
                return RedirectToAction("NewsIndex");
            }
            else
            {
                ViewBag.DuplicateNewsTitle = "Duplicate NewsTitle !...";
                return View(news);
            }
        }
        [HttpGet]
     //   [CustomAuthorization("ReportUploader,ReportCreater", "Create,Delete")]
        public ActionResult NewsEdit(int id)
        {
            return View(_ObjNewsRepository.EditNews(id));
        }

        [HttpPost]
        [ValidateInput(false)]
       // [CustomAuthorization("ReportUploader,ReportCreater", "Create,Delete")]
        public ActionResult NewsEdit(NewsVM news)
        {
            var newsTitlewiseid = db.NewsMasters.Where(x => x.NewsTitle == news.NewsTitle).Select(x => x.NewsId).FirstOrDefault();

            if (newsTitlewiseid == news.NewsId || newsTitlewiseid == 0)
            {
                if (news.NewsURL == null)
                {
                    var newsurl =ModernMarketResearch.Areas.Admin.Models.Common.GenerateSlug(news.NewsTitle);
                    news.NewsURL = newsurl;
                }
                var newsUrlwiseid = db.NewsMasters.Where(x => x.NewsUrl == news.NewsURL).Select(x => x.NewsId).FirstOrDefault();

                if (newsUrlwiseid == news.NewsId || newsUrlwiseid == 0)
                {
                    _ObjNewsRepository.EditpostNews(news);
                    return RedirectToAction("NewsIndex");
                }
                else
                {
                    ViewBag.NewsURL = "Duplicate News Url ....";
                    return View(news);
                }
            }
            else
            {
                ViewBag.DuplicateNewsTitle = "Duplicate News Title ....";
                return View(news);
            }
        }
     //   [CustomAuthorization("ReportUploader,ReportCreater", "Create,Delete")]
        public ActionResult NewsDetails(int id)
        {
            
            return View(_ObjNewsRepository.GetNewsById(id));

        }
        [HttpGet]
      //  [CustomAuthorization("ReportUploader,ReportCreater", "Create,Delete")]
        public ActionResult NewsDelete(int id)
        {
            return View(_ObjNewsRepository.GetNewsById(id));
        }
        [HttpPost]
        [ActionName(name: "NewsDelete")]
    //    [CustomAuthorization("ReportUploader,ReportCreater", "Create,Delete")]
        public ActionResult NewsDelete1(int id)
        {
            var n = db.NewsMasters.Where(x => x.NewsId == id).Select(x => x).FirstOrDefault();
            n.IsActive = false;
            db.Entry(n).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("NewsIndex");
        }
    }
}

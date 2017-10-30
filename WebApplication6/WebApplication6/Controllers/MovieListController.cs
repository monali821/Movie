using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class MovieListController : Controller
    {
        // GET: MovieList
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult MovieList()
        {
            ViewBag.Message = "";
            mdlMovieListcs obj = new mdlMovieListcs();
            return View(obj);
        }

        [HttpPost]
        public ActionResult MovieList(mdlMovieListcs objSave, String Command)
        {
            if (Command == "Save")
            {
                objSave.Save();
                ViewBag.Message = "Record Saved";
            }
            else
                return RedirectToAction("MovieList");
            return View(objSave);
        }
        [HttpGet]
        public ActionResult MyMovieList()
        {
            mdlMovieListTrans objTemp = new mdlMovieListTrans();
            objTemp.setUserList();
            return View(objTemp);
        }
        [HttpGet]
        public ActionResult ManageMovieList(String id)
        {
            mdlMovieListcs obj = new mdlMovieListcs();
            obj.MovieListId = id;
            obj.Search();
            return View(obj);

        }

        [HttpGet]
        public ActionResult AddMovieInList()
        {
            ViewBag.Message = "";
            mdlMovieListcs obj = new mdlMovieListcs();
            return View(obj);
        }
        //[HttpGet]
        //public ActionResult DeleteMovieList(mdlMovieListcs objDelete, String id)
        //{
        // objDelete.delete();


        //        return RedirectToAction("MovieList");

        //}
       
    }
    }
    
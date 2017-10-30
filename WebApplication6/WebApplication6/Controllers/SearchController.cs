using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Search(String id,String SearchTitle)
        {

            mdlMovieListTrans objModel = new mdlMovieListTrans();
            DataSet ds = new DataSet();
            ds = objModel.SearchMovieListMovies(id);
            List<mdlSearchResult> objReturn = (from drRow in ds.Tables[0].AsEnumerable()
                                               select new mdlSearchResult()
                                               {
                                                   mListId = drRow["mListId"].ToString(),
                                                   MoviceId = drRow["MoviceId"].ToString(),
                                                   MoviceListId = drRow["MoviceListId"].ToString(),
                                                   YearofRelease = drRow["YearofRelease"].ToString(),
                                                   Title = drRow["Title"].ToString(),
                                                   MoviePoster = drRow["MoviePoster"].ToString(),
                                                   Rating = drRow["rating"].ToString() 
                                               }).ToList();
            if(SearchTitle.ToString().Length>0)
            {
                objReturn = objReturn.Where(x => x.Title.Contains(SearchTitle)).ToList();
            }
            return Json(objReturn, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SearchMovie(String idd)
        {
            mdlMovieListTrans objModel = new mdlMovieListTrans();
            DataSet ds = new DataSet();
            ds = objModel.SearchMovie(idd);
            List<MovieEnt> objReturn = (from drRow in ds.Tables[0].AsEnumerable()
                                               select new MovieEnt()
                                               {
                                                   title = drRow["title"].ToString(),
                                                   Movie_id = drRow["Movie_id"].ToString(),
                                               }).ToList();
            if(objReturn.Count==0)
            {
                objReturn.Add(new MovieEnt() { Movie_id = "-1", title = "Not Exists" });
            }
            return Json(objReturn, JsonRequestBehavior.AllowGet);
        }

        public HttpResponseMessage AddMovie(String ListId, String MovieId)
        {
            mdlMovieListTrans obj = new mdlMovieListTrans();
            obj.AddMovie(MovieId, ListId);
            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("Movie Added.")
            };
        }
        public HttpResponseMessage DeleteMoive(String ListId)
        {
            mdlMovieListTrans obj = new mdlMovieListTrans();
            obj.DeleteMovieTrans( ListId);
            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("Movie Deleted.")
            };
        }

        public HttpResponseMessage ratedown(String MovieId)
        {
            mdlMovieListTrans obj = new mdlMovieListTrans();
            obj.RateUp(MovieId, "A", -1);
            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("Movie Deleted.")
            };
        }

        public HttpResponseMessage rateup(String MovieId)
        {
            mdlMovieListTrans obj = new mdlMovieListTrans();
            obj.RateUp(MovieId, "A", 1);
            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("Movie Deleted.")
            };
        }
    }



    public class mdlSearchResult
    {
        public String mListId { get; set; }
        public String MoviceId { get; set; }
        public String MoviceListId { get; set; }
        public String Movie_Name { get; set; }
        public String YearofRelease { get; set; }
        public String MoviePoster { get; set; }
        public String Title { get; set; }
        public string Rating { get; set; }

    }
    public class MovieEnt
    {
        public String Movie_id { get; set; }
        public String title { get; set; }
    }
}
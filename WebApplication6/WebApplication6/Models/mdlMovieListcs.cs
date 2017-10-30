using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace WebApplication6.Models
{
    public class mdlMovieListcs
    {
        public String MovieListId { get; set; }
        public String MovieListName { get; set; }
        public String UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public int rating { get; set; }

        public Boolean Save()
        {
            try
            {
                String cnString = System.Configuration.ConfigurationManager.AppSettings["con"].ToString();
                SqlConnection cn = new SqlConnection(cnString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Insert into Movie_List values(@p1,@p2,@p3,@p4)";
                cmd.Parameters.Add(new SqlParameter("@p1", Guid.NewGuid().ToString()));
                cmd.Parameters.Add(new SqlParameter("@p2", MovieListName));
                cmd.Parameters.Add(new SqlParameter("@p3", "A"));
                cmd.Parameters.Add(new SqlParameter("@p4", DateTime.Now.ToString()));
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }


        public Boolean Search()
        {
            DataSet ds = new DataSet();
            String cnString = System.Configuration.ConfigurationManager.AppSettings["con"].ToString();
            SqlConnection cn = new SqlConnection(cnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.Connection = cn;
            cmd.CommandText = "select  *  from Movie_List where MovieListId='" + MovieListId + "'";
            adp.SelectCommand = cmd;
            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                MovieListName = ds.Tables[0].Rows[0]["MovieListName"].ToString();
            }
            return true;
        }
    }


    public class mdlMovieListTrans
    {
        public List<mdlMovieListcs> objMyList = new List<mdlMovieListcs>();

        public Boolean setUserList()
        {
            objMyList = new List<mdlMovieListcs>();
            DataSet ds = new DataSet();
            String cnString = System.Configuration.ConfigurationManager.AppSettings["con"].ToString();
            SqlConnection cn = new SqlConnection(cnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.Connection = cn;
            cmd.CommandText = "select MovieListId,MovieListName,rating =( select  isnull(avg(rating),0) from Movie_Rating where Movie_Rating.Movielist_Id= Movie_List.MovieListId or  Movie_Rating.Movie_Id in(select MovieInList.MoviceId from MovieInList where MovieInList.MoviceListId=Movie_List.MovieListId )) from Movie_List where UserId='A'";
            adp.SelectCommand = cmd;
            adp.Fill(ds);
            foreach (DataRow drRow in ds.Tables[0].Rows)
            {
                mdlMovieListcs obj = new mdlMovieListcs();
                obj.MovieListId = drRow["MovieListId"].ToString();
                obj.MovieListName = drRow["MovieListName"].ToString();
                obj.rating = Convert.ToInt16(drRow["rating"].ToString());
                objMyList.Add(obj);
            }
            return true;
        }
        public DataSet SearchMovieListMovies(String MListID)
        {
            DataSet ds = new DataSet();
            String cnString = System.Configuration.ConfigurationManager.AppSettings["con"].ToString();
            SqlConnection cn = new SqlConnection(cnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.Connection = cn;
            cmd.CommandText = "Select *,rating=(select AVG(rating) from Movie_Rating where Movie_Rating.Movie_id=MovieInList.MoviceId)  from MovieInList left join Movie on Movie.movie_id=MovieInList.MoviceId Where MovieInList.MoviceListId='" + MListID + "'";
            adp.SelectCommand = cmd;
            adp.Fill(ds);
            return ds;
        }


        public Boolean DeleteMovieTrans(String TranId)
        {
            try
            {
                String cnString = System.Configuration.ConfigurationManager.AppSettings["con"].ToString();
                SqlConnection cn = new SqlConnection(cnString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Delete from MovieInList where mListId='" + TranId + "'";
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public DataSet SearchMovie(String MListID)
        {
            DataSet ds = new DataSet();
            String cnString = System.Configuration.ConfigurationManager.AppSettings["con"].ToString();
            SqlConnection cn = new SqlConnection(cnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.Connection = cn;
            cmd.CommandText = "Select title,Movie_id  from Movie where Movie_Id not in(Select MoviceId from MovieInList where MovieInList.MoviceListId='" + MListID + "')";
            adp.SelectCommand = cmd;
            adp.Fill(ds);
            return ds;
        }

        public Boolean AddMovie(String Movieid,String ListId)
        {
            try
            {
                String cnString = System.Configuration.ConfigurationManager.AppSettings["con"].ToString();
                SqlConnection cn = new SqlConnection(cnString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Insert into MovieInList  values('" + Guid.NewGuid().ToString() + "','" + Movieid + "','" + ListId + "')";
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }


        public Boolean RateUp(String Movieid,String userid,int rating)
        {
            try
            {
                String cnString = System.Configuration.ConfigurationManager.AppSettings["con"].ToString();
                SqlConnection cn = new SqlConnection(cnString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_movie_rating";
                cmd.Parameters.Add(new SqlParameter("@userid", userid));
                cmd.Parameters.Add(new SqlParameter("@Movie_Id", Movieid));
                cmd.Parameters.Add(new SqlParameter("@r", rating));
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
    }
}

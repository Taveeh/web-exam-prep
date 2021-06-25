using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using template.Data;
using template.Models;

namespace template.Controllers {
    public class MainController : Controller {
        private DataAbstractLayer _dataAbstractLayer = new DataAbstractLayer();
        // GET
        public ActionResult Index() {
            return View();
        }

        public ActionResult Login(string username) {
            HttpContext.Session["username"] = username;
            return Redirect("/Main/All");
        }

        public ActionResult All() {
            return View("App");
        }

        public string AddDocument(string name, string contents)
        {
            _dataAbstractLayer.AddDocument(name, contents);
            return JsonConvert.SerializeObject("success");
        }

        public string GetMoviesFromAuthor()
        {
            var name = (string) HttpContext.Session["username"];
            var movieIds = _dataAbstractLayer.GetAuthorByName(name).MovieList;
            List<Movie> movies = new List<Movie>();
            string[] ids = movieIds.Split(',');
            foreach (var id in ids)
            {
                var movie = _dataAbstractLayer.GetMovieById(Int32.Parse(id));
                if (movie != null)
                {
                    movies.Add(movie);
                }
            }
            return JsonConvert.SerializeObject(movies);
        }
        
        public string GetDocumentsFromAuthor()
        {
            var name = (string) HttpContext.Session["username"];
            var documentList = _dataAbstractLayer.GetAuthorByName(name).DocumentList;
            List<Document> documents = new List<Document>();
            string[] ids = documentList.Split(',');
            foreach (var id in ids)
            {
                var document = _dataAbstractLayer.GetDocumentById(Int32.Parse(id));
                if (document != null)
                {
                    documents.Add(document);
                }
            }
            return JsonConvert.SerializeObject(documents);
        }

        public string GetDocumentWithMostAuthors()
        {
            var authors = _dataAbstractLayer.GetAllAuthors();
            var map = new SortedDictionary<int, int>();
            foreach (var author in authors)
            {
                string[] ids = author.DocumentList.Split(',');
                foreach (var id in ids)
                {
                    if (!map.ContainsKey(Int32.Parse(id)))
                    {
                        map.Add(Int32.Parse(id), 1);
                    }
                    else
                    {
                        map[Int32.Parse(id)] += 1;
                    }
                }
            }
            return JsonConvert.SerializeObject(_dataAbstractLayer.GetDocumentById(
                map.Aggregate((x, y) => x.Value > y.Value ? x : y).Key));
        }

        // public string GetAllParents() {
        //     List<Author> parents = _dataAbstractLayer.GetAllParents();
        //     return JsonConvert.SerializeObject(parents);
        // }
        //
        // public string GetAllChildren() {
        //     List<Child> children = _dataAbstractLayer.GetAllChildren();
        //     return JsonConvert.SerializeObject(children);
        // }
    }
}
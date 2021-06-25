using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ex2_2020.Data;
using Ex2_2020.Models;
using Newtonsoft.Json;

namespace Ex2_2020.Controllers {
    public class MainController : Controller {
        private DataAbstractLayer _dataAbstractLayer = new DataAbstractLayer();
        private string _lastUser = "";

        private string _lastAdded = "";
        // GET
        public ActionResult Index() {
            return View();
        }

        public ActionResult Login(string username) {
            HttpContext.Session["username"] = username;
            return Redirect("/Main/All");
        }

        public ActionResult All() {
            return View("userPage");
        }

        public string GetAll() {
            List<Posts> postsList = _dataAbstractLayer.getAllPosts();
            return JsonConvert.SerializeObject(postsList);
        }

        public void UpdateTopic(int id, string text) {
            string user = (string) HttpContext.Session["username"];
            _dataAbstractLayer.UpdatePost(
                user,
                new DateTime().Millisecond,
                id,
                text
            );
        }

        public void AddTopic(string text, string topicname) {
            
            string user = (string) HttpContext.Session["username"];
            _lastAdded = text;
            _lastUser = user;
            int id = _dataAbstractLayer.GetTopicIdWithName(topicname);
            if (id == -1) {
                _dataAbstractLayer.AddTopic(topicname);
                id = _dataAbstractLayer.GetTopicIdWithName(topicname);
            }
            _dataAbstractLayer.AddPost(new Random().Next(), text, user, new DateTime().Month, id);
            
        }

        public string CheckAdded() {
            string user = (string) HttpContext.Session["username"];
            Console.WriteLine(_lastAdded);
            Console.WriteLine(_lastUser);
            if (_lastUser != "" && _lastUser != user) {
                string toReturn = _lastAdded;
                _lastUser = "";
                _lastAdded = "";
                return JsonConvert.SerializeObject(toReturn);
            }
            return JsonConvert.SerializeObject("");
        }
    }
}
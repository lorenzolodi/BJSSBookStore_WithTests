using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Configuration;

namespace BookServiceQA.Support_classes
{
    public class TestData
    {
        private IRestResponse _response;
        private RestRequest accessRequest;
        private JArray responseObjectArray;
        public string token;
        public string highestAuthorId;
        public string highestBookId;

        public TestData()
        {
            token = ConfigurationManager.AppSettings["Token"];
        }

        public void DeleteAllBooks()
        {
            GetAllBooks();
            if (responseObjectArray.Count > 0)
            {
                foreach (JObject book in responseObjectArray)
                {
                    var client = new RestClient(Browser.testURL);
                    accessRequest = new RestRequest("/api/Books/" + book["Id"].ToString(), Method.DELETE);
                    accessRequest.AddHeader("x-user-token", token);
                    Browser.highestBookId = book["Id"].ToString(); //save and share the highest id before deleting 
                    IRestResponse response = client.Execute(accessRequest);
                }
            }  
        }

        public void DeleteAllAuthors()
        {
            GetAllAuthors();
            if (responseObjectArray.Count > 0)
            {
                foreach (JObject author in responseObjectArray)
                {
                    var client = new RestClient(Browser.testURL);
                    accessRequest = new RestRequest("/api/Authors/" + author["Id"].ToString(), Method.DELETE);
                    accessRequest.AddHeader("x-user-token", token);
                    Browser.highestAuthorId = author["Id"].ToString(); //save and share the highest id before deleting 
                    IRestResponse response = client.Execute(accessRequest);
                }
            }
        }

        public void GetAllBooks()
        {
            var client = new RestClient(Browser.testURL);
            accessRequest = new RestRequest("/api/books", Method.GET);
            accessRequest.AddHeader("x-user-token", token);
            _response = client.Execute(accessRequest);
            responseObjectArray = JArray.Parse(_response.Content);
        }

        public void GetAllAuthors()
        {
            var client = new RestClient(Browser.testURL);
            accessRequest = new RestRequest("/api/authors", Method.GET);
            accessRequest.AddHeader("x-user-token", token);
            _response = client.Execute(accessRequest);
            responseObjectArray = JArray.Parse(_response.Content);
        }

        public void CreateAuthor(int id, string name)
        {
            var client = new RestClient(Browser.testURL);
            accessRequest = new RestRequest("/api/authors", Method.POST);
            accessRequest.AddHeader("x-user-token", token);
            accessRequest.RequestFormat = DataFormat.Json;
            accessRequest.AddParameter("Id", id);
            accessRequest.AddParameter("Name", name);
            _response = client.Execute(accessRequest);
        }

        public void CreateBook(int id, string title, int year, double price, string genre, int authorId)
        {
            var client = new RestClient(Browser.testURL);
            accessRequest = new RestRequest("/api/books", Method.POST);
            accessRequest.AddHeader("x-user-token", token);
            accessRequest.RequestFormat = DataFormat.Json;
            accessRequest.AddParameter("Id", id);
            accessRequest.AddParameter("Title", title);
            accessRequest.AddParameter("Year", year);
            accessRequest.AddParameter("Price", price);
            accessRequest.AddParameter("Genre", genre);
            accessRequest.AddParameter("AuthorId", authorId);
            _response = client.Execute(accessRequest);
        }

        public void PopulateAuthors()
        {
            CreateAuthor(1, "Alessandro Manzoni");
            CreateAuthor(2, "Dante Alighieri");
            CreateAuthor(3, "Giovanni Boccaccio");
        }

        public void PopulateBooks()
        {
            CreateBook(1, "Promessi Sposi", 1827, 80.99, "Novel", Convert.ToInt32(Browser.highestAuthorId) +1);
            CreateBook(2, "Divinia Commedia", 1307, 220.49, "Poem", Convert.ToInt32(Browser.highestAuthorId) + 2);
            CreateBook(3, "Decameron", 1351, 180.53, "Poems", Convert.ToInt32(Browser.highestAuthorId) + 3);
        }
    }
}

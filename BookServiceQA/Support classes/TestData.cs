using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;

namespace BookServiceQA.Support_classes
{
    public class TestData
    {
        private IRestResponse _response;
        private int authorsCount;
        private int booksCount;
        private RestClient client;
        private RestRequest accessRequest;
        private string bookId;
        private string authorId;
        private JObject responseObjectList;
        private JArray responseObjectArray;
        private string firstAuthorId;
        private string lastAuthorId;
        private string firstBookId;
        private string lastBookId;
        public string token;
        public string highestAuthorId;

        public TestData()
        {
            token = "0c6a36ba-10e4-438f-ba86-0d5b68a2bb15";
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
                    highestAuthorId = author["Id"].ToString(); //save the highest author id before deleting
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
            CreateBook(1, "Promessi Sposi", 1827, 80.99, "Novel", Convert.ToInt32(highestAuthorId)+1);
            CreateBook(2, "Divinia Commedia", 1307, 220.49, "Poem", Convert.ToInt32(highestAuthorId) + 2);
            CreateBook(3, "Decameron", 1351, 180.53, "Poems", Convert.ToInt32(highestAuthorId) + 3);
        }
    }
}

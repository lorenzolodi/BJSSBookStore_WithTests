using System;
using TechTalk.SpecFlow;
using RestSharp;
using Newtonsoft.Json.Linq;
using NUnit;
using NUnit.Framework;
using System.Configuration;
using BookServiceQA.Support_classes;

namespace BookServiceQA.StepDefinitions
{
    [Binding]
    public class APITestSteps
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
        public string token = ConfigurationManager.AppSettings["Token"];
        private int nextAuthorId = Int32.Parse(Browser.highestAuthorId) + 1;    //HighestAuthorId is the last id of the deleted authors
        private int nextBookId = Int32.Parse(Browser.highestBookId) + 1;        //HighestBookId is the last id of the deleted books

        public IRestResponse CallAPI(string apiURL, RestSharp.Method apiMethod, params string[] args)
        {
            var client = new RestClient(Browser.testURL);

            if (apiMethod.ToString() == Method.POST.ToString())
            {
                if (apiURL == "/api/authors/")
                {
                    accessRequest = new RestRequest(apiURL, apiMethod);
                    accessRequest.AddParameter("Id", args[0]);
                    accessRequest.AddParameter("Name", args[1]);
                }
                if (apiURL == "/api/books/")
                {
                    accessRequest = new RestRequest(apiURL, apiMethod);
                    accessRequest.AddParameter("Id", args[0]);
                    accessRequest.AddParameter("Title", args[1]);
                    accessRequest.AddParameter("Year", args[2]);
                    accessRequest.AddParameter("Price", args[3]);
                    accessRequest.AddParameter("Genre", args[4]);
                    accessRequest.AddParameter("AuthorId", args[5]);
                }
            }

            if (apiMethod.ToString() == Method.PUT.ToString())
            {
                if (apiURL == "/api/authors/")
                {
                    apiURL += args[0];
                    accessRequest = new RestRequest(apiURL, apiMethod);
                    accessRequest.AddParameter("Id", args[0]);
                    accessRequest.AddParameter("Name", args[1]);
                }
                if (apiURL == "/api/books/")
                {
                    apiURL += args[0];
                    accessRequest = new RestRequest(apiURL, apiMethod);
                    accessRequest.AddParameter("Id", args[0]);
                    accessRequest.AddParameter("Title", args[1]);
                    accessRequest.AddParameter("Year", args[2]);
                    accessRequest.AddParameter("Price", args[3]);
                    accessRequest.AddParameter("Genre", args[4]);
                    accessRequest.AddParameter("AuthorId", args[5]);
                }
            }

            if (apiMethod.ToString() == Method.GET.ToString() || apiMethod.ToString() == Method.DELETE.ToString())
            {
                accessRequest = new RestRequest(apiURL, apiMethod);
            }

            accessRequest.AddHeader("x-user-token", token);
            return client.Execute(accessRequest);
        }

        [Given]
        public void GivenIHaveThreeBooksInMyBookStore()
        {

        }
        
        [When]
        public void WhenIAccessTheListOfBooks()
        {
            _response = CallAPI("/api/books", Method.GET);
            responseObjectArray = JArray.Parse(_response.Content);
        }

        [Then]
        public void ThenAllBooksAreReturned()
        {
            responseObjectArray = JArray.Parse(_response.Content);
            Assert.That(responseObjectArray.Count, Is.EqualTo(3));
        }

        [When]
        public void WhenIAccessABookById()
        {
            _response = CallAPI("/api/books/"+nextBookId, Method.GET);
        }

        [Then]
        public void ThenTheCorrespondingBookIsReturned()
        {
            responseObjectList = JObject.Parse(_response.Content);
            Assert.That(responseObjectList["Id"].ToString(), Is.EqualTo(nextBookId.ToString()));
            Assert.That(responseObjectList.Property("Title").Value.ToString, Is.EqualTo("Promessi Sposi"));
            Assert.That(responseObjectList["AuthorName"].ToString(), Is.EqualTo("Alessandro Manzoni"));
        }

        [Given]
        public void GivenIHaveThreeAuthorsInMyBookStore()
        {

        }

        [When]
        public void WhenIAccessAllAuthors()
        {
            _response = CallAPI("/api/authors", Method.GET);
        }

        [Then]
        public void ThenAllAuthorsAreReturned()
        {
            responseObjectArray = JArray.Parse(_response.Content);
            Assert.That(responseObjectArray.Count, Is.EqualTo(3));
            //var firstBook = responseObjectArray[0];
            //Assert.That(firstBook["Name"].ToString(), Is.EqualTo("Jane Austen"));
        }

        [When]
        public void WhenIAccessAnAuthorById()
        {
            _response = CallAPI("/api/authors/"+nextAuthorId, Method.GET);
        }

        [Then]
        public void ThenTheCorrespondingAuthorIsReturned()
        {
            responseObjectList = JObject.Parse(_response.Content);
            Assert.That(responseObjectList["Id"].ToString(), Is.EqualTo(nextAuthorId.ToString()));
            Assert.That(responseObjectList.Property("Name").Value.ToString(), Is.EqualTo("Alessandro Manzoni"));
        }

        [When]
        public void WhenIAddANewAuthor()
        {
            nextAuthorId += 3;
            string[] paramArray = new string[] { nextAuthorId.ToString(), "Virgilio" };
            _response = CallAPI("/api/authors/", Method.POST, paramArray);
        }

        [Then]
        public void ThenIHaveOneMoreAuthor()
        {
            _response = _response = CallAPI("/api/authors", Method.GET);
            responseObjectArray = JArray.Parse(_response.Content);
            Assert.That(responseObjectArray.Count, Is.EqualTo(4));
        }

        [When]
        public void WhenIAddANewBook()
        {   
            nextBookId += 3;
            string[] paramArray = new string[] { nextBookId.ToString(), "Il Conte di Carmagnola", "1819", "200.1", "Tragedy", nextAuthorId.ToString() };
            _response = CallAPI("/api/books/", Method.POST, paramArray);
        }

        [Then]
        public void ThenIHaveOneMoreBook()
        {
            _response = _response = CallAPI("/api/books", Method.GET);
            responseObjectArray = JArray.Parse(_response.Content);
            Assert.That(responseObjectArray.Count, Is.EqualTo(4));
        }

        [When]
        public void WhenIUpdateAnAuthor()
        {
            string[] paramArray = new string[] { nextAuthorId.ToString(), "Luigi Pirandello" };
            _response = CallAPI("/api/authors/", Method.PUT, paramArray);
        }

        [Then]
        public void ThenTheAuthorIsCorrectlyUpdated()
        {
            _response = CallAPI("/api/authors/" + nextAuthorId, Method.GET);
            responseObjectList = JObject.Parse(_response.Content);
            Assert.That(responseObjectList.Property("Name").Value.ToString(), Is.EqualTo("Luigi Pirandello"));
        }

        [When]
        public void WhenIUpdateABook()
        {
            int newAuthor = nextAuthorId +1;
            string[] paramArray = new string[] { nextBookId.ToString(), "Convivio", "1307", "516.2", "Trattato", newAuthor.ToString() };
            _response = CallAPI("/api/books/", Method.PUT, paramArray);
        }

        [Then]
        public void ThenTheBookIsCorrectlyUpdated()
        {
            int newAuthor = nextAuthorId + 1;
            _response = CallAPI("/api/books/" + nextBookId, Method.GET);
            responseObjectList = JObject.Parse(_response.Content);
            Assert.That(responseObjectList["Id"].ToString(), Is.EqualTo(nextBookId.ToString()));
            Assert.That(responseObjectList.Property("Title").Value.ToString, Is.EqualTo("Convivio"));
            Assert.That(responseObjectList["Year"].ToString(), Is.EqualTo("1307"));
            Assert.That(responseObjectList.Property("Price").Value.ToString, Is.EqualTo("516.2"));
            Assert.That(responseObjectList["Genre"].ToString(), Is.EqualTo("Trattato"));
            Assert.That(responseObjectList.Property("AuthorName").Value.ToString, Is.EqualTo("Dante Alighieri"));
        }

        [Then]
        public void ThenTheAuthorDataIsMatching()
        {
            _response = CallAPI("/api/authors/" + nextAuthorId, Method.GET);
            responseObjectList = JObject.Parse(_response.Content);
            Assert.That(responseObjectList.Property("Name").Value.ToString(), Is.EqualTo("Virgilio"));
        }

        [Then]
        public void ThenTheBookDataIsMatching()
        {
            _response = CallAPI("/api/books/" + nextBookId, Method.GET);
            responseObjectList = JObject.Parse(_response.Content);
            Assert.That(responseObjectList.Property("Title").Value.ToString(), Is.EqualTo("Il Conte di Carmagnola"));
            Assert.That(responseObjectList.Property("Year").Value.ToString(), Is.EqualTo("1819"));
            Assert.That(responseObjectList.Property("Price").Value.ToString(), Is.EqualTo("200.1"));
            Assert.That(responseObjectList.Property("Genre").Value.ToString(), Is.EqualTo("Tragedy"));
            Assert.That(responseObjectList.Property("AuthorName").Value.ToString(), Is.EqualTo("Alessandro Manzoni"));
        }

        [When]
        public void WhenIDeleteAnAuthor()
        {
            _response = CallAPI("/api/Authors/" + nextAuthorId, Method.DELETE);
        }

        [Then]
        public void ThenTheAuthorNoLongerExists()
        {
            _response = CallAPI("/api/authors", Method.GET);
            responseObjectArray = JArray.Parse(_response.Content);
            Assert.That(responseObjectArray.Count, Is.EqualTo(2));
            
            _response=CallAPI("/api/authors/" + nextAuthorId, Method.GET);
            responseObjectList = JObject.Parse(_response.Content);
            Assert.That(responseObjectList["ExceptionMessage"].ToString(), Is.EqualTo("Sequence contains no elements"));
        }

        [When]
        public void WhenIDeleteABook()
        {
            _response = CallAPI("/api/Books/" + nextBookId, Method.DELETE);
        }

        [Then]
        public void ThenTheBookNoLongerExists()
        {
            _response = CallAPI("/api/books", Method.GET);
            responseObjectArray = JArray.Parse(_response.Content);
            Assert.That(responseObjectArray.Count, Is.EqualTo(2));

            _response = CallAPI("/api/books/" + nextBookId, Method.GET);
            Assert.That(_response.StatusDescription.ToString(), Is.EqualTo("Not Found"));
        }

        [Given]
        public void Given_I_have_an_author_YES_NO_a_corresponding_book(string yes_no)
        {
            if (yes_no.Equals("without"))
            {
                _response = CallAPI("/api/Books/" + nextBookId, Method.DELETE);
            }
        }

        [Then]
        public void Then_the_author_still_exists()
        {
            _response = CallAPI("/api/authors/" + nextAuthorId, Method.GET);
            responseObjectList = JObject.Parse(_response.Content);
            Assert.That(responseObjectList["Id"].ToString(), Is.EqualTo(nextAuthorId.ToString()));
            Assert.That(responseObjectList.Property("Name").Value.ToString(), Is.EqualTo("Alessandro Manzoni"));
        }


    }
}

//jObject for list
//jArray for array
//JToken 
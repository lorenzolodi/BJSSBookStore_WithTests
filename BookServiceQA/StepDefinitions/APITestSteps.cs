using System;
using TechTalk.SpecFlow;
using RestSharp;
using Newtonsoft.Json.Linq;
using NUnit;
using NUnit.Framework;

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
        public string token = "0c6a36ba-10e4-438f-ba86-0d5b68a2bb15";

        [Given]
        public void GivenIHaveAtLeastOneBook()
        {
            client = new RestClient(Browser.testURL);
            accessRequest = new RestRequest("/api/books", Method.GET);
            accessRequest.AddHeader("x-user-token", token);
            _response = client.Execute(accessRequest);
            responseObjectArray = JArray.Parse(_response.Content);
            booksCount = responseObjectArray.Count;
            firstBookId = responseObjectArray[0]["Id"].ToString();
            lastBookId = responseObjectArray[booksCount-1]["Id"].ToString();
            Assert.That(booksCount, Is.GreaterThan(0));
        }
        
        [When]
        public void WhenIAccessTheListOfBooks()
        {
            accessRequest = new RestRequest("/api/books", Method.GET);
            accessRequest.AddHeader("x-user-token", "0f4f1173-6f4d-4b98-838d-435905bcc8ee");
            _response = client.Execute(accessRequest);
        }

        [Then]
        public void ThenAllBooksAreReturned()
        {
            responseObjectArray = JArray.Parse(_response.Content);
            Assert.That(responseObjectArray.Count, Is.GreaterThan(0));
            //var firstBook = responseObjectArray[0];
            //Assert.That(firstBook["Title"].ToString(), Is.EqualTo("Pride and Prejudice"));
        }

        [When]
        public void WhenIAccessABookById()
        {
            client = new RestClient(Browser.testURL);
            accessRequest = new RestRequest("/api/books/1", Method.GET);
            accessRequest.AddHeader("x-user-token", "0f4f1173-6f4d-4b98-838d-435905bcc8ee");
            _response = client.Execute(accessRequest);
        }

        [Then]
        public void ThenTheCorrespondingBookIsReturned()
        {
            responseObjectList = JObject.Parse(_response.Content);
            Assert.That(responseObjectList["Id"].ToString(), Is.EqualTo("1"));
            Assert.That(responseObjectList.Property("Title").Value.ToString, Is.EqualTo("Pride and Prejudice"));
            Assert.That(responseObjectList["AuthorName"].ToString(), Is.EqualTo(responseObjectList.Property("AuthorName").Value.ToString()));
        }

        [Given]
        public void GivenIHaveAtLeastOneAuthor()
        {
            client = new RestClient(Browser.testURL);
            accessRequest = new RestRequest("/api/authors", Method.GET);
            accessRequest.AddHeader("x-user-token", "0f4f1173-6f4d-4b98-838d-435905bcc8ee");
            _response = client.Execute(accessRequest);
            responseObjectArray = JArray.Parse(_response.Content);
            authorsCount = responseObjectArray.Count;
            firstAuthorId = responseObjectArray[0]["Id"].ToString();
            lastAuthorId = responseObjectArray[authorsCount-1]["Id"].ToString();
            Assert.That(authorsCount, Is.GreaterThan(0));
        }

        [When]
        public void WhenIAccessAllAuthors()
        {
            client = new RestClient(Browser.testURL);
            accessRequest = new RestRequest("/api/authors", Method.GET);
            accessRequest.AddHeader("x-user-token", "0f4f1173-6f4d-4b98-838d-435905bcc8ee");
            _response = client.Execute(accessRequest);
        }

        [Then]
        public void ThenAllAuthorsAreReturned()
        {
            responseObjectArray = JArray.Parse(_response.Content);
            Assert.That(responseObjectArray.Count, Is.GreaterThan(0));
            //var firstBook = responseObjectArray[0];
            //Assert.That(firstBook["Name"].ToString(), Is.EqualTo("Jane Austen"));
        }

        [When]
        public void WhenIAccessAnAuthorById()
        {
            client = new RestClient(Browser.testURL);
            accessRequest = new RestRequest("/api/authors/1", Method.GET);
            accessRequest.AddHeader("x-user-token", "0f4f1173-6f4d-4b98-838d-435905bcc8ee");
            _response = client.Execute(accessRequest);
        }

        [Then]
        public void ThenTheCorrespondingAuthorIsReturned()
        {
            responseObjectList = JObject.Parse(_response.Content);
            Assert.That(responseObjectList["Id"].ToString(), Is.EqualTo("1"));
            Assert.That(responseObjectList.Property("Name").Value.ToString(), Is.EqualTo("Jane Austen"));
            Assert.That(responseObjectList["Name"].ToString(), Is.EqualTo(responseObjectList.Property("Name").Value.ToString()));
        }

        [When]
        public void WhenIAddANewAuthor()
        {
            accessRequest = new RestRequest("/api/authors", Method.POST);
            accessRequest.AddHeader("x-user-token", "0f4f1173-6f4d-4b98-838d-435905bcc8ee");
            accessRequest.RequestFormat = DataFormat.Json;
            accessRequest.AddParameter("Id", 4);
            accessRequest.AddParameter("Name", "Manzoni");
            _response = client.Execute(accessRequest);
        }

        [Then]
        public void ThenIHaveOneMoreAuthor()
        {
            accessRequest = new RestRequest("/api/authors", Method.GET);
            accessRequest.AddHeader("x-user-token", "0f4f1173-6f4d-4b98-838d-435905bcc8ee");
            _response = client.Execute(accessRequest);
            responseObjectArray = JArray.Parse(_response.Content);
            Assert.That(responseObjectArray.Count, Is.EqualTo(authorsCount + 1));
        }

        [When]
        public void WhenIAddANewBook()
        {   
            accessRequest = new RestRequest("/api/authors", Method.POST);
            accessRequest.AddHeader("x-user-token", "0f4f1173-6f4d-4b98-838d-435905bcc8ee");
            accessRequest.RequestFormat = DataFormat.Json;
            accessRequest.AddParameter("Id", 1);
            accessRequest.AddParameter("Title", "Decameron");
            accessRequest.AddParameter("Year", 1350);
            accessRequest.AddParameter("Price", 200);
            accessRequest.AddParameter("Genre", "Novel");
            accessRequest.AddParameter("AuthorId", 3);
            _response = client.Execute(accessRequest);
        }

        [Then]
        public void ThenIHaveOneMoreBook()
        {
            accessRequest = new RestRequest("/api/books", Method.GET);
            accessRequest.AddHeader("x-user-token", "0f4f1173-6f4d-4b98-838d-435905bcc8ee");
            _response = client.Execute(accessRequest);
            responseObjectArray = JArray.Parse(_response.Content);
            Assert.That(responseObjectArray.Count, Is.EqualTo(booksCount + 1));
        }

        [When]
        public void WhenIUpdateTheAuthor()
        {
            accessRequest = new RestRequest("/api/authors/1", Method.PUT);
            accessRequest.AddHeader("x-user-token", "0f4f1173-6f4d-4b98-838d-435905bcc8ee");
            accessRequest.RequestFormat = DataFormat.Json;
            accessRequest.AddParameter("Id", 1);
            accessRequest.AddParameter("Name", "Semproni");
            _response = client.Execute(accessRequest);
        }

        [Then]
        public void ThenTheAuthorIsCorrectlyUpdated()
        {
            client = new RestClient(Browser.testURL);
            accessRequest = new RestRequest("/api/authors/1", Method.GET);
            accessRequest.AddHeader("x-user-token", "0f4f1173-6f4d-4b98-838d-435905bcc8ee");
            _response = client.Execute(accessRequest);
            responseObjectList = JObject.Parse(_response.Content);
            Assert.That(responseObjectList.Property("Name").Value.ToString(), Is.EqualTo("Semproni"));
        }

        [When]
        public void WhenIUpdateTheBook()
        {
            accessRequest = new RestRequest("/api/books/1", Method.POST);
            accessRequest.AddHeader("x-user-token", "0f4f1173-6f4d-4b98-838d-435905bcc8ee");
            accessRequest.RequestFormat = DataFormat.Json;
            accessRequest.AddParameter("Id", 1);
            accessRequest.AddParameter("Title", "Decameron");
            accessRequest.AddParameter("Year", 1350);
            accessRequest.AddParameter("Price", 200);
            accessRequest.AddParameter("Genre", "Novel");
            accessRequest.AddParameter("AuthorId", 3);
            _response = client.Execute(accessRequest);
        }

        [Then]
        public void ThenTheBookIsCorrectlyUpdated()
        {
            client = new RestClient(Browser.testURL);
            accessRequest = new RestRequest("/api/books/1", Method.GET);
            accessRequest.AddHeader("x-user-token", "0f4f1173-6f4d-4b98-838d-435905bcc8ee");
            _response = client.Execute(accessRequest);
            responseObjectList = JObject.Parse(_response.Content);
            Assert.That(responseObjectList["Id"].ToString(), Is.EqualTo("1"));
            Assert.That(responseObjectList.Property("Title").Value.ToString, Is.EqualTo("Decameron"));
            Assert.That(responseObjectList["AuthorName"].ToString(), Is.EqualTo(1350));
            Assert.That(responseObjectList.Property("Price").Value.ToString, Is.EqualTo(200));
            Assert.That(responseObjectList["Genre"].ToString(), Is.EqualTo("Novel"));
            Assert.That(responseObjectList.Property("AuthrId").Value.ToString, Is.EqualTo(3));
        }

        [Then]
        public void ThenTheAuthorDataIsMatching()
        {
            client = new RestClient(Browser.testURL);
            accessRequest = new RestRequest("/api/authors/"+(Int32.Parse(lastAuthorId)+1).ToString(), Method.GET);
            accessRequest.AddHeader("x-user-token", "0f4f1173-6f4d-4b98-838d-435905bcc8ee");
            _response = client.Execute(accessRequest);
            responseObjectList = JObject.Parse(_response.Content);
            Assert.That(responseObjectList.Property("Name").Value.ToString(), Is.EqualTo("Manzoni"));
        }

        [Then]
        public void ThenTheBookDataIsMatching()
        {
            client = new RestClient(Browser.testURL);
            accessRequest = new RestRequest("/api/books/" + (Int32.Parse(lastBookId) + 1).ToString(), Method.GET);
            accessRequest.AddHeader("x-user-token", "0f4f1173-6f4d-4b98-838d-435905bcc8ee");
            _response = client.Execute(accessRequest);
            responseObjectList = JObject.Parse(_response.Content);
            Assert.That(responseObjectList.Property("Title").Value.ToString(), Is.EqualTo("Decameron"));
            Assert.That(responseObjectList.Property("Year").Value.ToString(), Is.EqualTo(1350));
            Assert.That(responseObjectList.Property("Price").Value.ToString(), Is.EqualTo(200));
            Assert.That(responseObjectList.Property("Genre").Value.ToString(), Is.EqualTo("Novel"));
            Assert.That(responseObjectList.Property("AuthorId").Value.ToString(), Is.EqualTo(18));
        }

        [When]
        public void WhenIDeleteAnAuthor()
        {
            accessRequest = new RestRequest("/api/Authors/" + authorId, Method.DELETE);
            accessRequest.AddHeader("x-user-token", "0f4f1173-6f4d-4b98-838d-435905bcc8ee");
            IRestResponse response = client.Execute(accessRequest);
        }

        [Then]
        public void ThenTheAuthorNoLongerExists()
        {
            accessRequest = new RestRequest("/api/authors", Method.GET);
            accessRequest.AddHeader("x-user-token", "0f4f1173-6f4d-4b98-838d-435905bcc8ee");
            _response = client.Execute(accessRequest);
            responseObjectArray = JArray.Parse(_response.Content);
            Assert.That(responseObjectArray.Count, Is.EqualTo(authorsCount - 1));

            client = new RestClient(Browser.testURL);
            accessRequest = new RestRequest("/api/authors/" + authorId, Method.GET);
            accessRequest.AddHeader("x-user-token", "0f4f1173-6f4d-4b98-838d-435905bcc8ee");
            _response = client.Execute(accessRequest);
            responseObjectList = JObject.Parse(_response.Content);
            Assert.That(responseObjectList["ExceptionMessage"].ToString(), Is.EqualTo("Sequence contains no elements"));
        }

        [When]
        public void WhenIDeleteABook()
        {
            accessRequest = new RestRequest("/api/Books/"+bookId, Method.DELETE);
            accessRequest.AddHeader("x-user-token", "0f4f1173-6f4d-4b98-838d-435905bcc8ee");
            IRestResponse response = client.Execute(accessRequest);
        }

        [Then]
        public void ThenTheBookNoLongerExists()
        {
            accessRequest = new RestRequest("/api/books", Method.GET);
            accessRequest.AddHeader("x-user-token", "0f4f1173-6f4d-4b98-838d-435905bcc8ee");
            _response = client.Execute(accessRequest);
            responseObjectArray = JArray.Parse(_response.Content);
            Assert.That(responseObjectArray.Count, Is.EqualTo(booksCount - 1));

            client = new RestClient(Browser.testURL);
            accessRequest = new RestRequest("/api/books/" + bookId, Method.GET);
            accessRequest.AddHeader("x-user-token", "0f4f1173-6f4d-4b98-838d-435905bcc8ee");
            _response = client.Execute(accessRequest);
            Assert.That(_response.StatusDescription.ToString(), Is.EqualTo("Not Found"));
        }

    }
}

//jObject for list
//jArray for array
//JToken 
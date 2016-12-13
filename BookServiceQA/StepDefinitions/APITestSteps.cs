using System;
using TechTalk.SpecFlow;
using RestSharp;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Configuration;
using BookServiceQA.Support_classes;

namespace BookServiceQA.StepDefinitions
{
    [Binding]
    public class APITestSteps
    {
        private IRestResponse response;
        private RestRequest accessRequest;
        private JObject responseObjectList;
        private JArray responseObjectArray;
        public string Token = ConfigurationManager.AppSettings["Token"];
        private int nextAuthorId;
        private int nextBookId;

        public string FirstAuthorId()
        {
            response = CallAPI("/api/authors", Method.GET);
            responseObjectArray = JArray.Parse(response.Content);
            return responseObjectArray[0]["Id"].ToString();
        }

        public string FirstBookId()
        {
            response = CallAPI("/api/books", Method.GET);
            responseObjectArray = JArray.Parse(response.Content);
            return responseObjectArray[0]["Id"].ToString();
        }

        public IRestResponse CallAPI(string apiURL, RestSharp.Method apiMethod, params string[] args)
        {
            var client = new RestClient(Browser.TestURL);

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

            accessRequest.AddHeader("x-user-token", Token);
            return client.Execute(accessRequest);
        }

        [Given]
        public void GivenIHaveThreeBooksInMyBookStore()
        {

        }
        
        [When]
        public void WhenIAccessTheListOfBooks()
        {
            response = CallAPI("/api/books", Method.GET);
            responseObjectArray = JArray.Parse(response.Content);
        }

        [Then]
        public void ThenAllBooksAreReturned()
        {
            responseObjectArray = JArray.Parse(response.Content);
            Assert.That(responseObjectArray.Count, Is.EqualTo(3));
        }

        [When]
        public void WhenIAccessABookById()
        {
            response = CallAPI("/api/books/"+nextBookId, Method.GET);
        }

        [Then]
        public void ThenTheCorrespondingBookIsReturned()
        {
            responseObjectList = JObject.Parse(response.Content);
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
            response = CallAPI("/api/authors", Method.GET);
        }

        [Then]
        public void ThenAllAuthorsAreReturned()
        {
            responseObjectArray = JArray.Parse(response.Content);
            Assert.That(responseObjectArray.Count, Is.EqualTo(3));
        }

        [When]
        public void WhenIAccessAnAuthorById()
        {
            response = CallAPI("/api/authors/"+nextAuthorId, Method.GET);
        }

        [Then]
        public void ThenTheCorrespondingAuthorIsReturned()
        {
            responseObjectList = JObject.Parse(response.Content);
            Assert.That(responseObjectList["Id"].ToString(), Is.EqualTo(nextAuthorId.ToString()));
            Assert.That(responseObjectList.Property("Name").Value.ToString(), Is.EqualTo("Alessandro Manzoni"));
        }

        [When]
        public void WhenIAddANewAuthor()
        {
            nextAuthorId += 3;
            string[] paramArray = new string[] { nextAuthorId.ToString(), "Virgilio" };
            response = CallAPI("/api/authors/", Method.POST, paramArray);
        }

        [Then]
        public void ThenIHaveOneMoreAuthor()
        {
            response = response = CallAPI("/api/authors", Method.GET);
            responseObjectArray = JArray.Parse(response.Content);
            Assert.That(responseObjectArray.Count, Is.EqualTo(4));
        }

        [When]
        public void WhenIAddANewBook()
        {   
            nextBookId += 3;
            string[] paramArray = new string[] { nextBookId.ToString(), "Il Conte di Carmagnola", "1819", "200.1", "Tragedy", nextAuthorId.ToString() };
            response = CallAPI("/api/books/", Method.POST, paramArray);
        }

        [Then]
        public void ThenIHaveOneMoreBook()
        {
            response = response = CallAPI("/api/books", Method.GET);
            responseObjectArray = JArray.Parse(response.Content);
            Assert.That(responseObjectArray.Count, Is.EqualTo(4));
        }

        [When]
        public void WhenIUpdateAnAuthor()
        {
            string[] paramArray = new string[] { nextAuthorId.ToString(), "Luigi Pirandello" };
            response = CallAPI("/api/authors/", Method.PUT, paramArray);
        }

        [Then]
        public void ThenTheAuthorIsCorrectlyUpdated()
        {
            response = CallAPI("/api/authors/" + nextAuthorId, Method.GET);
            responseObjectList = JObject.Parse(response.Content);
            Assert.That(responseObjectList.Property("Name").Value.ToString(), Is.EqualTo("Luigi Pirandello"));
        }

        [When]
        public void WhenIUpdateABook()
        {
            int newAuthor = nextAuthorId +1;
            string[] paramArray = new string[] { nextBookId.ToString(), "Convivio", "1307", "516.2", "Trattato", newAuthor.ToString() };
            response = CallAPI("/api/books/", Method.PUT, paramArray);
        }

        [Then]
        public void ThenTheBookIsCorrectlyUpdated()
        {
            int newAuthor = nextAuthorId + 1;
            response = CallAPI("/api/books/" + nextBookId, Method.GET);
            responseObjectList = JObject.Parse(response.Content);
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
            response = CallAPI("/api/authors/" + nextAuthorId, Method.GET);
            responseObjectList = JObject.Parse(response.Content);
            Assert.That(responseObjectList.Property("Name").Value.ToString(), Is.EqualTo("Virgilio"));
        }

        [Then]
        public void ThenTheBookDataIsMatching()
        {
            response = CallAPI("/api/books/" + nextBookId, Method.GET);
            responseObjectList = JObject.Parse(response.Content);
            Assert.That(responseObjectList.Property("Title").Value.ToString(), Is.EqualTo("Il Conte di Carmagnola"));
            Assert.That(responseObjectList.Property("Year").Value.ToString(), Is.EqualTo("1819"));
            Assert.That(responseObjectList.Property("Price").Value.ToString(), Is.EqualTo("200.1"));
            Assert.That(responseObjectList.Property("Genre").Value.ToString(), Is.EqualTo("Tragedy"));
            Assert.That(responseObjectList.Property("AuthorName").Value.ToString(), Is.EqualTo("Alessandro Manzoni"));
        }

        [When]
        public void WhenIDeleteAnAuthor()
        {
            response = CallAPI("/api/Authors/" + nextAuthorId, Method.DELETE);
        }

        [Then]
        public void ThenTheAuthorNoLongerExists()
        {
            response = CallAPI("/api/authors", Method.GET);
            responseObjectArray = JArray.Parse(response.Content);
            Assert.That(responseObjectArray.Count, Is.EqualTo(2));
            
            response=CallAPI("/api/authors/" + nextAuthorId, Method.GET);
            responseObjectList = JObject.Parse(response.Content);
            Assert.That(responseObjectList["ExceptionMessage"].ToString(), Is.EqualTo("Sequence contains no elements"));
        }

        [When]
        public void WhenIDeleteABook()
        {
            response = CallAPI("/api/Books/" + nextBookId, Method.DELETE);
        }

        [Then]
        public void ThenTheBookNoLongerExists()
        {
            response = CallAPI("/api/books", Method.GET);
            responseObjectArray = JArray.Parse(response.Content);
            Assert.That(responseObjectArray.Count, Is.EqualTo(2));

            response = CallAPI("/api/books/" + nextBookId, Method.GET);
            Assert.That(response.StatusDescription.ToString(), Is.EqualTo("Not Found"));
        }

        [Given]
        public void Given_I_have_an_author_YES_NO_a_corresponding_book(string yes_no)
        {
            if (yes_no.Equals("without"))
            {
                response = CallAPI("/api/Books/" + nextBookId, Method.DELETE);
            }
        }

        [Then]
        public void Then_the_author_still_exists()
        {
            response = CallAPI("/api/authors/" + nextAuthorId, Method.GET);
            responseObjectList = JObject.Parse(response.Content);
            Assert.That(responseObjectList["Id"].ToString(), Is.EqualTo(nextAuthorId.ToString()));
            Assert.That(responseObjectList.Property("Name").Value.ToString(), Is.EqualTo("Alessandro Manzoni"));
        }

        [Given]
        public void Given_I_have_three_authors_and_three_books_in_my_book_store()
        {
            TestData dataBuilder = new TestData();
            dataBuilder.DeleteAllBooks();
            dataBuilder.DeleteAllAuthors();
            dataBuilder.PopulateAuthors();
            dataBuilder.PopulateBooks();

            nextAuthorId = Int32.Parse(FirstAuthorId());
            nextBookId = Int32.Parse(FirstBookId());
        }

    }
}

//jObject for list
//jArray for array
//JToken 
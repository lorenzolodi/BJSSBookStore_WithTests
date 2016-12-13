Feature: BookList
	As a generic user
	I want to be able to view the entire book list
	In order to select a book

Background: Fill up book store
	Given I have three authors and three books in my book store

@SmokeTest @ViewBookList @Declarative
Scenario: View book list
	Given I have accessed the BJSS book store
#	And at least one test environment exist
	When I select an environment
	Then book list is displayed
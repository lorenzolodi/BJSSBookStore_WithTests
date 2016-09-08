Feature: BookList
	In order to select a book
	As a generic user
	I want to be able to view the entire book list

@SmokeTest @ViewBookList
Scenario: View book list
	Given I have accessed the BJSS book store
	And at least one test environment exist
	When I click on the Details link
	Then book list is displayed
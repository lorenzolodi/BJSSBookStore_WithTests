Feature: BookList
	As a generic user
	In order to select a book
	I want to be able to view the entire book list

@SmokeTest @ViewBookList
Scenario: View book list
	Given I have accessed the BJSS book store
#	And at least one test environment exist
	When I select an environment
	Then book list is displayed
Feature: BookDetails
	As a librarian
	In order to manage the book store
	I want to be able to view a full list of the available books

@SmokeTest @BookDetails
Scenario: View book details
	Given I am on the Book list screen
#	And at least one book exist in the system
	And I select a book
	Then the book details are _ displayed
#	And the Author field is displayed
	And the Book List Author matches with the Detail Author 
#	And the Title field is displayed
	And the Book List Title matches with the Detail Title
#	And the Year field is displayed
#	And the Genre field is displayed
#	And the Price field is displayed

	@BookDetails
Scenario: Home link
	Given I am on the Book list screen
#	And at least one book exist in the system
	And I select a book
	When I click on the Home link
#	Then the environment list screen is displayed
	Then the book details are not displayed
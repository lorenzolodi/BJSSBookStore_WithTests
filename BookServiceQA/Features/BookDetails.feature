Feature: BookDetails
	As a librarian
	In order to manage the book store
	I want to be able to view a full list of the available books

@BookDetails @Imperative
Scenario: View book details (Imperative)
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

@BookDetails @Declarative
Scenario: View book details (Declarative)
	Given a list of books
	When I select a book
	Then I can see its details
	And I can see the matching Author and Title

@BookDetails @Declarative
Scenario: Home link
	Given I am on the Book list screen
#	And at least one book exist in the system
	And I select a book
	When I click on the Home link
#	Then the environment list screen is displayed
	Then the book details are not displayed
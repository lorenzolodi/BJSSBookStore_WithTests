Feature: APITest
	As an API 
	In order to avoid silly mistakes
	I want to be told the sum of two numbers

@API
Scenario: Get all the books
	Given I have at least one book
	When I access the list of books
	Then all books are returned

@API
Scenario: Get a book by id
	Given I have at least one book
	When I access a book by id
	Then the corresponding book is returned

@API
Scenario: Get all the authors
	Given I have at least one author
	When I access all authors
	Then all authors are returned

@API
Scenario: Get an author by id
	Given I have at least one author
	When I access an author by id
	Then the corresponding author is returned

@API
Scenario: Post an author
	Given I have at least one author
	When I add a new author
	Then I have one more author
	And the author data is matching

@API
Scenario: Post a book
	Given I have at least one author
	And I have at least one book
	When I add a new book
	Then I have one more book
	And the book data is matching

@API
Scenario: Update an author
	Given I have at least one author
	When I update the author
	Then the author is correctly updated

@API
Scenario: Update a book
	Given I have at least one book
	When I update the book
	Then the book is correctly updated

@API
Scenario: Delete an author
	Given I have at least one author
	When I delete an author
	Then the author no longer exists

@API
Scenario: Delete a book
	Given I have at least one book
	When I delete a book
	Then the book no longer exists
Feature: APITest
	As an API 
	In order to avoid silly mistakes
	I want to be told the sum of two numbers

@API @Declarative
Scenario: Get all the books
	Given I have three books in my book store	
	When I access the list of books
	Then all books are returned

@API @Declarative
Scenario: Get a book by id
	Given I have three books in my book store
	When I access a book by id
	Then the corresponding book is returned

@API @Declarative
Scenario: Get all the authors
	Given I have three authors in my book store
	When I access all authors
	Then all authors are returned

@API @Declarative
Scenario: Get an author by id
	Given I have three authors in my book store
	When I access an author by id
	Then the corresponding author is returned

@API @Declarative
Scenario: Post an author
	Given I have three authors in my book store
	When I add a new author
	Then I have one more author
	And the author data is matching

@API @Declarative
Scenario: Post a book
	Given I have three authors in my book store
	And I have three books in my book store
	When I add a new book
	Then I have one more book
	And the book data is matching

@API @Declarative
Scenario: Update an author
	Given I have three authors in my book store
	When I update an author
	Then the author is correctly updated

@API @Declarative
Scenario: Update a book
	Given I have three books in my book store
	When I update a book
	Then the book is correctly updated

@API @Declarative
Scenario: Cannot delete an author that has a book
	Given I have an author with a corresponding book
	When I delete an author
	Then the author still exists

@API @Declarative
Scenario: Delete an author
	Given I have an author without a corresponding book
	When I delete an author
	Then the author no longer exists

@API @Declarative
Scenario: Delete a book
	Given I have three books in my book store
	When I delete a book
	Then the book no longer exists
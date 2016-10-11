Feature: NewBook
	As a librarian
	In order to keep the book store interesting
	I want to be able to add a new book


@SmokeTest @NewBook
Scenario: Add a new book
	Given I am on the Book list page
	And I have entered the following values on the Add Book form
	| Author          | Title        | Year | Genre | Price |
	| Charles Dickens | Oliver Twist | 1838 | Novel | 9.99  |
	When I press Submit
	Then the new book is displayed in the book list

@NewBook
Scenario: Decimal values are not allowed in the Year field
	Given I am on the Book list page
	And I have entered the following values on the Add Book form
	| Author          | Title        | Year | Genre | Price |
	| Charles Dickens | Oliver Zest | 1838.33 | Horror | 9.99  |
	When I press Submit
	Then I will not be able to add the book
#	And an error message will be displayed		--> the tooltip is a browser feature that cannot be testes

@NewBook
Scenario Outline: Mandatory fields
	Given I am on the Book list page
	And I have entered <Author>, <Title>, <Year>, <Genre> and <Price>
	When I press Submit
	Then I will not be able to add the book
	And an error message will be displayed
Examples: 
	| Author              | Title       | Year | Genre   | Price |
	| Miguel de Cervantes | n/a         | 1500 | Fiction | 10    |
	| Charles Dickens     | Oliver Nest | n/a  | Fiction | 10    |		
	| Charles Dickens     | Oliver Nest | 1500 | Fiction | n/a   |		

#	Non-null validation for year and price is triggered ONLY after moving to other fields and then deleting the initial value
#	Otherwise the default value is set
	
	
	
	
	


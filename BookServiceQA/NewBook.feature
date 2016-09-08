Feature: NewBook
	In order to keep the book store interesting
	As a librarian
	I want to be able to add a new book


@SmokeTest
Scenario: Add a new book
	Given I am on the Book list screen
	And I have entered the following values on the Add Book form
	| Author          | Title        | Year | Genre | Price |
	| Charles Dickens | Oliver Twist | 1838 | Novel | 9.99  |
#	When I press Submit
#	Then the new book is displayed in the book list

Scenario: Decimal values are not allowed in the Year field
	Given I am on the Book list screen
	And I have entered the following values on the Add Book form
	| Author          | Title        | Year | Genre | Price |
	| Charles Dickens | Oliver Zest | 1838.33 | Novel | 9.99  |
	When I press Submit
	Then I will not be able to add the book
	And an error message will be displayed

Scenario Outline: Mandatory fields
	Given I am on the Book list screen
	And I have entered vAuthor, vTitle, vYear, vGenre, vPrice
	When I press Submit
	Then I will not be able to add the book
	And an error message will be displayed

	Examples: 
	| vAuthor         | vTitle      | vYear | vGenre  | vPrice |
	| Charles Dickens |             | 1500  | Fiction | 10     |
	| Charles Dickens | Oliver Nest |       | Fiction | 10     |
	| Charles Dickens | Oliver Nest | 1500  | Fiction |        |


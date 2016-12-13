Feature: NewBook
	As a librarian
	I want to be able to add new books
	In order to keep the book store interesting

Background: Fill up book store
	Given I have three authors and three books in my book store

@NewBook @Imperative
Scenario: Add a new book
	Given I am on the Book list page
	And I have entered the following values on the Add Book form
	| Author          | Title        | Year | Genre | Price  |
	| Dante Alighieri | Inferno		 | 1304 | Novel | 459.99 |
	When I press Submit
	Then the new book is displayed in the book list

@NewBook @Imperative
Scenario: Decimal values are not allowed in the Year field
	Given I am on the Book list page
	And I have entered the following values on the Add Book form
	| Author			 | Title						  | Year	| Genre	  | Price |
	| Giovanni Boccaccio | Comedia delle ninfe fiorentine | 1342.33 | Classic | 9.99  |
	When I press Submit
	Then I will not be able to add the book
#	And an error message will be displayed		--> the tooltip is a browser feature that cannot be tested

@NewBook @Imperative
Scenario Outline: Mandatory fields
	Given I am on the Book list page
	And I have entered <Author>, <Title>, <Year>, <Genre> and <Price>
	When I press Submit
	Then I will not be able to add the book
	And an error message will be displayed
Examples: 
	| Author             | Title     | Year | Genre       | Price |
	| Giovanni Boccaccio | n/a       | 1335 | Poem        | 55    |
	| Dante Alighieri    | Vita Nova | n/a  | Prosimetrum | 99    |
	| Alessandro Manzoni | Adelchi   | 1822 | Tragedy     | n/a   |

#	Non-null validation for year and price is triggered ONLY after moving to other fields and then deleting the initial value
#	Otherwise the default value is set
	
	
	
	
	


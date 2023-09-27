Feature: Delete A Customer
	As a system manager 
	I want to delete a default customers
	So I can inspect the customer

Scenario: Delete A Customer
	When I delete a default customer
	Then the following customer deleted record should be recorded:
		| FirstName | LastName | PhoneNumber | Email             | BankAccountNumber | DateOfBirth | IsDeleted |
		| alireza   | davari   | 09120556987 | alireza@email.com | IR123456789       | 2000-01-01  | true      |
	And the following customer deleted record should be recorded in event store:
		| FirstName | LastName | PhoneNumber | Email             | BankAccountNumber | DateOfBirth | IsDeleted |
		| alireza   | davari   | 09120556987 | alireza@email.com | IR123456789       | 2000-01-01  | true      |

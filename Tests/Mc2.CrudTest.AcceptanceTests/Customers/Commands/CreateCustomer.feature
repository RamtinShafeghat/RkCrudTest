Feature: Create a Customer
	As a system manager
	I want to create a customer
	To record a customers transaction

Scenario: Create a Customer
	Given the following customer info:
		| FirstName | LastName | PhoneNumber | Email           | BankAccountNumber | DateOfBirth |
		| reza      | salari   | 09125874132 | email@gamil.com | 65987874          | 2001/02/03  |
	When I create a customer
	Then the following customers record should be recorded:
		| FirstName | LastName | PhoneNumber | Email            | BankAccountNumber | DateOfBirth |
		| reza      | salari   | 09125874132 | email@gamil.com | 65987874          | 2001-02-03  |
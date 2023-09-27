Feature: Get A Customer
	As a system manager 
	I want to get a default customers
	So I can inspect the customer

Scenario: Get A Customer
	When I request a default customer
	Then the following customer should be returned:
		| FirstName | LastName | PhoneNumber | Email             | BankAccountNumber | DateOfBirth |
		| alireza   | davari   | 09120556987 | alireza@email.com | IR123456789       | 2000-01-01  |

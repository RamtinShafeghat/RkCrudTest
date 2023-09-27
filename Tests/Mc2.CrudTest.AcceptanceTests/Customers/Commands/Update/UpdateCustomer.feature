Feature: Update a Customer
	As a system manager
	I want to Update a customer
	To record a customers transaction

Scenario: Update a Customer
	Given the following customer update info:
		| FirstName | LastName | PhoneNumber | Email               | BankAccountNumber | DateOfBirth |
		| alirezaa  | davarii  | 09120556999 | alireza@emailll.com | IR123456777       | 2003/02/01  |
	When I Update a customer
	Then the following customer updated record should be recorded:
		| FirstName | LastName | PhoneNumber | Email               | BankAccountNumber | DateOfBirth |
		| alirezaa  | davarii  | 09120556999 | alireza@emailll.com | IR123456777       | 2003/02/01  |
	And the following customer updated record should be recorded in event store:
		| FirstName | LastName | PhoneNumber | Email               | BankAccountNumber | DateOfBirth |
		| alirezaa  | davarii  | 09120556999 | alireza@emailll.com | IR123456777       | 2003/02/01  |
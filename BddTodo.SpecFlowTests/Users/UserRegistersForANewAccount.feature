Feature: User registers for a new account
	As an unauthenticated user
	I want to register
	so that i can manage my todo lists

Scenario: Unauthenticated user fails to register
 Given an unauthenticated user
 And i provide all of the required data for the registration page
 And i forget to provide my first name
 When i submit the registration form
 Then i will be told I need to provide my first name
 
Scenario: Unauthenticated user registers
 Given an unauthenticated user
 And i provide all of the required data for the registration page
 When i submit the registration form
 Then i will be added to the application database
 And i will be logged in
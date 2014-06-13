Feature: Search
	In order to find zendesk items by criteria
	As an api consumer
	I want to be told the sum of two numbers


	Scenario: I can search for organizations by custom field
	Given an organization in Zendesk with the name 'Coffee Break' and the custom field 'restaurantid' and value '321'
	When I search for organizations with the custom field 'restaurantid' and value '321'
	Then I am returned the organization ''Coffee Break'
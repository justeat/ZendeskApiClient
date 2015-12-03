Feature: Search
	In order to find zendesk items by criteria
	As an api consumer
	I want to be told the sum of two numbers

Scenario: I can search for organizations by custom field
	Given an organization in Zendesk with the name 'Coffee Break' and the custom field 'org_restaurant_id' and value '321'
	When I search for organizations with the custom field 'org_restaurant_id' and value '321'
	Then I am returned the organization 'Coffee Break'

Scenario: I can page for organizations
	Given an organization in Zendesk with the name 'Coffee Break' and the custom field 'org_restaurant_id' and value '321'
	And an organization in Zendesk with the name 'Cupcake Cafe' and the custom field 'org_restaurant_id' and value '654'
	When I search for organizations with the page size '1' and page number '1'
	And I search again for organizations with the page size '1' and page number '2'
	Then I am returned differnt results containing '1' item each

Scenario: I can search by fields
	Given an organization in Zendesk with the name 'Coffee Break' and the custom field 'org_restaurant_id' and value '321'
	And an organization in Zendesk with the name 'Cupcake Cafe' and the custom field 'org_restaurant_id' and value '654'
	When I search for the second organization by name
	Then I am returned the organization 'Cupcake Cafe'

Scenario: I can find a user by email address
	Given the email address of a user for whom I wish to search
	When I search for a user by their email address
	Then I am returned the correct user

Scenario: I can search using a greater than opperator
	Given an organization in Zendesk named 'Coffee Break'
	When I search for organisations created today
	Then I am returned only organisations with a created date of today
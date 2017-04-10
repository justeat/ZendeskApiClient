Feature: Organization
	In order to manage organizations
	As an api comsumer
	I want to be able to get, getall, put, post and delete 

Scenario: When I can post and get an organisation
	Given an organization in Zendesk with the name 'The Cheese Factory'
	When I call get by id on the ZendeskApiClient
	Then I get an Organization from Zendesk with the name 'The Cheese Factory'

Scenario: When I call Put I am able to update an organisations
	Given an organization in Zendesk with the name 'Coffee Hour'
	When I update the organization with the name 'The Cheese Factory'
	And I call get by id on the ZendeskApiClient
	Then I get an Organization from Zendesk with the name 'The Cheese Factory'

Scenario: When I call Delete the organisations is deleted from zendesk
	Given an organization in Zendesk with the name 'The Cheese Factory'
	When I call delete by id on the ZendeskApiClient
	Then the Organization is no longer in zendesk

Scenario: When I can post and search for an organisation by externalId
	Given an organization in Zendesk with the name 'The Cheese Factory'
	When I call search by external ids on the ZendeskApiClient
	Then I get an Organization from Zendesk with the name 'The Cheese Factory'
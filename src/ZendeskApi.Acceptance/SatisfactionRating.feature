Feature: Satisfaction Rating
	In order to manage customer satisfaction
	As an api consumer
	I want to be able to get and post satisfaction ratings

Scenario: When I call Get by id, I get the given request by Id
	Given a ticket in Zendesk with the subject 'I would like a new laptop' and description 'This old one is slow'
	And a satisfaction rating with the score 'Good'
	When I call get satisfaction rating by id on the ZendeskApiClient
	Then I get a satisfaction rating with a score of good

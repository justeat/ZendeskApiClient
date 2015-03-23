Feature: Requests
	In order to manage customer requests
	As an api consumer
	I want to be able to get, getall, put, post and delete requests

Scenario: When I call Get by id, I get the given request by Id
	Given a request in Zendesk with the subject 'The coffee machiene is broken' and comment 'I can't work in these conditions!'
	When I call get by id on the ZendeskApiClient
	Then I get a request from Zendesk with the subject 'The coffee machiene is broken' and description 'I can't work in these conditions!'

Scenario: When I call Post I am able to add a request
	Given a request in Zendesk with the subject 'I've swallowed my mouse cable' and comment 'It's a bit of a problem'
	When I call get by id on the ZendeskApiClient
	Then I get a request from Zendesk with the subject 'I've swallowed my mouse cable' and description 'It's a bit of a problem'

Scenario: When I call Delete the request is deleted from zendesk
	Given a request in Zendesk with the subject 'The coffee machiene is broken' and comment 'I can't work in these conditions!'
	When I call delete by id on the ZendeskApiClient
	Then the request is no longer in zendesk

Scenario: When I call Get by id, I get told it was created via the api
	Given a request in Zendesk with the subject 'The coffee machiene is broken' and comment 'I can't work in these conditions!'
	When I call get by id on the ZendeskApiClient
	Then I get a request from Zendesk which is via the api

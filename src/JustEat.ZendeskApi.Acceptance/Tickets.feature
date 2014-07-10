Feature: Tickets
	In order to manage issues
	As an api comsumer
	I want to be able to get, getall, put, post and delete tickets

Scenario: When I call Get by id, I get the given ticket by Id
	Given a ticket in Zendesk with the subject 'The coffee machiene is broken' and description 'I can't work in these conditions!'
	When I call get by id on the ZendeskApiClient
	Then I get a ticket from Zendesk with the subject 'The coffee machiene is broken' and description 'I can't work in these conditions!'

Scenario: When I call GetAll, I am returned a list of tickets
	Given the following tickets in Zendesk
		| Subject                       | Description                       |
		| I've swallowed my mouse cable | It's a bit of a problem           |
		| The coffee machiene is broken | I can't work in these conditions! |
	When I call getall by id on the ZendeskApiClient
	Then I get a ticket from Zendesk with the following values
		| Subject                       | Description                       |
		| I've swallowed my mouse cable | It's a bit of a problem           |
		| The coffee machiene is broken | I can't work in these conditions! |

Scenario: When I call Post I am able to add a ticket
	Given a ticket in Zendesk with the subject 'I've swallowed my mouse cable' and description 'It's a bit of a problem'
	When I call get by id on the ZendeskApiClient
	Then I get a ticket from Zendesk with the subject 'I've swallowed my mouse cable' and description 'It's a bit of a problem'

Scenario: When I call Post I am able to add a task
	Given a task in Zendesk with the subject 'I've swallowed my mouse cable' and description 'It's a bit of a problem' and type 'task'
	When I call get by id on the ZendeskApiClient
	Then I get a task from Zendesk with the subject 'I've swallowed my mouse cable' and description 'It's a bit of a problem' and type 'task'

Scenario: When I call Put I am able to update a ticket
	Given a ticket in Zendesk with the subject 'I've swallowed my mouse cable' and description 'It's a bit of a problem'
	When I update the ticket with the status 'closed'
	And I call get by id on the ZendeskApiClient
	Then I get a ticket from Zendesk with the status 'closed'

Scenario: When I call Delete the ticket is deleted from zendesk
	Given a ticket in Zendesk with the subject 'The coffee machiene is broken' and description 'I can't work in these conditions!'
	When I call delete by id on the ZendeskApiClient
	Then the ticket is no longer in zendesk


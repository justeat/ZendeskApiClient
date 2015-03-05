Feature: TicketComments
	In order to manage issues
	As an api comsumer
	I want to be able to get, getall, put, post and delete ticketcomments

Scenario: When I add a comment it is created
	Given a ticket in Zendesk with the subject 'The coffee machine is broken' and description 'I can't work in these conditions!'
	And I add the comment 'The biscuits have all gone too!'
	And I add the comment 'Do you have an ETA fix date?'
	When I call get all comments for that ticket
	Then I am returned a comment with the body 'The biscuits have all gone too!'
	Then I am returned a comment with the body 'Do you have an ETA fix date?'

Scenario: I can attach files to a ticket comment
	Given a ticket in Zendesk with the subject 'The coffee machine is broken' and description 'I can't work in these conditions!'
	And I upload a file to attach to that comment
	When I add the comment 'The biscuits have all gone too!' with the upload attached
	And I call get all comments for that ticket
	Then I am returned a comment with the body 'The biscuits have all gone too!' with that attachment
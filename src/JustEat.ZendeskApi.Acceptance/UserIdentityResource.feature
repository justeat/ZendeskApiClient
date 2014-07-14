Feature: UserIdentityResource
	In order to manage User Identity in Zendesk (Email and Phone Number)
	As an api comsumer
	I want to be able to get, update & insert user identity info via Api calls

Scenario: I can get User Identities by User Id
	Given Zendesk User with an email
	When I call UserIdentityResource GetAll for this User
	Then I should get list of user identities

Scenario: I can update the identities of a User
	Given Zendesk User with an email
	When I call UserIdentityResource GetAll for this User
	And I change the email
	Then it should be changed


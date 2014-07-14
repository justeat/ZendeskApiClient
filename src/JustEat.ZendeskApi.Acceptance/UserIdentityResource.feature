Feature: UserIdentityResource
	In order to manage User Identity in Zendesk (Email and Phone Number)
	As an api comsumer
	I want to be able to get, update & insert user identity info via Api calls

Scenario: I can get User Identities by User Id
	Given Zendesk User
	When I call UserIdentityResource GetAll for this User
	Then I should get list of user identities

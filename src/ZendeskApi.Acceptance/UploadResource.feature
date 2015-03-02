Feature: UploadResource
	In order to create Attachments in Zendesk
	As an api comsumer
	I want to be able to upload files via Api calls

Scenario: I can upload a file
	Given I have a file to upload
	When I call UploadResource Post
	Then I should get a token back

Scenario: I can delete an upload
	Given I have a valid token
	When I delete an uploaded resource
	Then I should get an OK response

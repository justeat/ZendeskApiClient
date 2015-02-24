Feature: UploadResource
	In order to create Attachments in Zendesk
	As an api comsumer
	I want to be able to upload files via Api calls

Scenario: 1. I can upload file
	Given I have a file to upload
	When I call UploadResource Post
	Then I should get a token back

Scenario: 2. I can delete upload
	Given I have a valid token
	When I call UploadResource Delete
	Then I should get OK response

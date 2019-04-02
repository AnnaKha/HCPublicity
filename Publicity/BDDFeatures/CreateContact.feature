Feature: Create New Contact
	As a User I want to
	add new contacts to the system,
	avoiding duplicate creation

@OpenApplication
@CloseBrowser
Scenario: New contact with all information can be created
	Given I have opened Create new Contact
	And entered all information for contact
	When I press save 
	Then check created contact is available in search results

@OpenApplication
@CloseBrowser
Scenario: New contact with only required information can be created
	Given I have opened Create new Contact
	And entered only required information for contact
	When I press save 
	Then check created contact is available in search results

@OpenApplication
@CloseBrowser
Scenario: Edit contact from search by adding additional address
	Given I have opened Create new Contact
	And entered only required information for contact
	And I press save
	And click edit icon for created contact
	And add additional address
	And I press save 
	When click edit icon for created contact
	Then check edited contact contains additional address

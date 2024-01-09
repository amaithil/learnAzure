Feature: Google -RemoteDriver

@myTag
Scenario Outline: Can find search results on remote
  Given I am on the remote google page
		| Browsername |
		| <Browsername> |
  When I search for "TestingBot" on remote
  Then I should see title "TestingBot - Google Search" on remote
  
  Examples:
		| Browsername |
		| chrome      |
		| firefox     |
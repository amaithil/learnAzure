Feature: Google -RemoteDriver - Firefox

Scenario Outline: Can find search results on remote
  Given I am on the remote google page
		| Browsername |
		| <Browsername> |
  When I search for "TestingBot" on remote
  Then I should see title "TestingBot - Google Search" on remote
  
  Examples:
		| Browsername	|
		| firefox		|
  

Scenario Outline: Can find different search results on remote
  Given I am on the remote google page
		| Browsername |
		| <Browsername> |
  When I search for "Math" on remote
  Then I should see title "Math - Google Search" on remote
  
  Examples:
		| Browsername	|
		| chrome		|
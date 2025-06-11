Feature: echo

This feature aims to validate that API is running.

Scenario: Api is running and return its version when echo is called
	Given Cdb.Yield.Simulator.API is available
	When a request is sent to "api/echo" route
	Then it should return success with status code 200
		And it should also return its name and version
			| name                | version |
			| CDB Yield Simulator | 1.0.0   |

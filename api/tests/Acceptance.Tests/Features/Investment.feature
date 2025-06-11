Feature: Investment

This feature aims to validate the investment simulation in a CDB

Scenario: When investment duration is less than 2 - Should return Bad Request
	Given Cdb.Yield.Simulator.API is available
		And the following request message
			| Duration | Amount |
			| 1        | 1000   |
			| 0        | 1000   |
			| -10      | 1000   |
	When a POST is sent to "api/investment/simulate" route
	Then it should return status code 400
		And it should return error message
			| content                          | code   | type | source                               |
			| Duration must be greater than 1. | BR-002 | 4    | InvestmentSimulationCommandValidator |


Scenario: When investment amount is not positive - Should return Bad Request
	Given Cdb.Yield.Simulator.API is available
		And the following request message
			| Duration | Amount |
			| 10       | 0      |
			| 10       | -1000  |
	When a POST is sent to "api/investment/simulate" route
	Then it should return status code 400
		And it should return error message
			| content                           | code   | type | source                               |
			| Amount must be greater than zero. | BR-001 | 4    | InvestmentSimulationCommandValidator |

Scenario: when the contract rules are satisfied - Should return success
	Given Cdb.Yield.Simulator.API is available
		And the following request message
			| Duration | Amount |
			| 12       | 1000   |
	When a POST is sent to "api/investment/simulate" route
	Then it should return success
		And it should return status code 200
		And it should have no messages
		And it should return following data
			| initialAmount | grossAmount | netAmount | taxAmount | taxRate | duration | monthlyRate | totalInterest |
			| 1000          | 1123.08     | 1098.47   | 24.62     | 0.20    | 12       | 0.0097      | 123.08        |
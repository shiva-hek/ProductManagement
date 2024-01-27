Feature: CreateProduct

As an authenticated user,
I want to add a new product to the system

Background:
	Given Given the database contains the following users:
		| Firstname | Lastname | Id                                   | Email            |
		| John      | Doe      | C8F01166-025B-493D-85E8-2573B9D509E3 | john.d@gmail.com |
		| Jane      | Smith    | 6142E620-DBDE-4940-8920-5CEB6CD88508 | jane.s@gmail.com |
	And the following products are created:
		| Name        | ProduceDate         | ManufacturePhone                   | ManufactureEmail   | IsAvailable | CreatorId                            |
		| iphone-12-1 | 2024-01-01 12:00:00 | 223366                             | iphone12@gmail.com | true        | C8F01166-025B-493D-85E8-2573B9D509E3 |
		| iphone-13-1 | 2024-01-01 12:00:00 | 552266-DBDE-4940-8920-5CEB6CD88508 | iphone13@gmail.com | true        | C8F01166-025B-493D-85E8-2573B9D509E3 |

@Accpetance
Scenario: Create a new product successfully
	When a user with the Id "C8F01166-025B-493D-85E8-2573B9D509E3" attempts to create a products with the following attributes:
		| Key              | Value               |
		| Name             | iphone14-1          |
		| ProduceDate      | 2024-01-01 12:00:00 |
		| ManufacturePhone | 335587              |
		| ManufactureEmail | m1@gmail.com        |
		| IsAvailable      | true                |
	Then the user should receive the following result:
		| Key     | Value |
		| Success | true  |

@BusinessRule
Scenario Outline: The products's ProduceDate and ManufactureEmail must be unique
	Given a user with the Id "C8F01166-025B-493D-85E8-2573B9D509E3" attempts to create a duplicate product with the following attributes:
		| Key              | Value               |
		| Name             | iphone-12-1         |
		| ProduceDate      | 2024-01-01 12:00:00 |
		| ManufacturePhone | 335587              |
		| ManufactureEmail | m1@gmail.com        |
		| IsAvailable      | true                |
	Then the user should receive the following result:
		| Key     | Value |
		| Success | false |
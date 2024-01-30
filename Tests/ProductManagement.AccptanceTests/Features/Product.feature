Feature: Product

As a registered user,
I want to be able to manage products within the system.

Background:
	Given the database contains the following users:
		| Id                                   | Firstname | Lastname | UserName | Password    | Email           | PhoneNumber |
		| ab8e8e54-d7ca-4028-90d2-b7b3bba033ba | john      | doe      | john     | 12345678910 | john@gmail.com  | 445566      |
		| a149eff6-f08d-4b14-b8b3-16c25fd0e255 | julie     | lerman   | julie    | 12345678910 | julie@gmail.com | 112233      |
	And the following products are created:
		| Id                                   | Name        | ProduceDate         | ManufacturePhone | ManufactureEmail   | IsAvailable | CreatorId                            |
		| 1111CA4B-E86A-4C69-8C5A-AE4D0E77056A | iphone-12-1 | 2024-01-01 12:00:00 | 223366           | iphone12@gmail.com | true        | ab8e8e54-d7ca-4028-90d2-b7b3bba033ba |
		| FCFCABE6-CD91-4407-BBAE-6A00D16CDC1D | iphone-13-1 | 2024-01-02 12:00:00 | 885522           | iphone13@gmail.com | true        | ab8e8e54-d7ca-4028-90d2-b7b3bba033ba |
		| FFFCB089-2EC7-4E96-ACA5-EE2B3DE98BED | iphone-10-1 | 2024-01-05 12:00:00 | 885522           | iphone10@gmail.com | true        | a149eff6-f08d-4b14-b8b3-16c25fd0e255 |

@Accpetance
Scenario: Create a new product successfully
	When an authorized user with the Id "C8F01166-025B-493D-85E8-2573B9D509E3" attempts to create a products with the following attributes:
		| Key              | Value               |
		| Name             | iphone14-1          |
		| ProduceDate      | 2024-01-01 12:00:00 |
		| ManufacturePhone | 335587              |
		| ManufactureEmail | m1@gmail.com        |
		| IsAvailable      | true                |
	Then the product should be created successfully

@BusinessRule
Scenario Outline: The products's ProduceDate and ManufactureEmail must be unique
	When an authorized user with the Id "C8F01166-025B-493D-85E8-2573B9D509E3" attempts to create a duplicate product with the following attributes:
		| Key              | Value               |
		| Name             | iphone-12-1         |
		| ProduceDate      | 2024-01-01 12:00:00 |
		| ManufacturePhone | 335587              |
		| ManufactureEmail | iphone12@gmail.com  |
		| IsAvailable      | true                |
	Then the instantiation should fail

@Accpetance
Scenario: Update product details successfully
	When an authorized user with the Id "ab8e8e54-d7ca-4028-90d2-b7b3bba033ba" has created a product and attempts to update it as below
		| Key              | Value                                |
		| Id               | 1111CA4B-E86A-4C69-8C5A-AE4D0E77056A |
		| Name             | iphone-12-pro                        |
		| ProduceDate      | 2024-01-01 12:00:00                  |
		| ManufacturePhone | 888555                               |
		| ManufactureEmail | iphone12@gmail.com                   |
		| IsAvailable      | true                                 |
	Then the product's name should be changed to "iphone-12-pro"
	And the nanufacturePhone should be changed to "888555"


@BusinessRule
Scenario: Invalid attempt to update product details by a non-creator user
	When an authorized user with Id "a149eff6-f08d-4b14-b8b3-16c25fd0e255" that is not the creator of a product attempts to update it as below
		| Key              | Value                                |
		| Id               | 1111CA4B-E86A-4C69-8C5A-AE4D0E77056A |
		| Name             | iphone-12-pro                        |
		| ProduceDate      | 2024-01-01 12:00:00                  |
		| ManufacturePhone | 888555                               |
		| ManufactureEmail | iphone12@gmail.com                   |
		| IsAvailable      | true                                 |
	Then the system should reject the update attempt with an UnauthorizedAccessException


@Accpetance
Scenario: Delete product successfully
	When an authorized user with the Id "ab8e8e54-d7ca-4028-90d2-b7b3bba033ba" attempts to delete the follwing product that he has created before
		| Key              | Value                                |
		| Id               | 1111CA4B-E86A-4C69-8C5A-AE4D0E77056A |
		| Name             | iphone-12-1                          |
		| ProduceDate      | 2024-01-01 12:00:00                  |
		| ManufacturePhone | 22336                                |
		| ManufactureEmail | iphone12@gmail.com                   |
		| IsAvailable      | true                                 |
	Then the product repository should not contain any product with Id "1111CA4B-E86A-4C69-8C5A-AE4D0E77056A"


@BusinessRule
Scenario: Invalid attempt to Delete product
	When an authorized user with the Id "a149eff6-f08d-4b14-b8b3-16c25fd0e255" attempts to delete the follwing product that is not creator of it
		| Key              | Value                                |
		| Id               | 1111CA4B-E86A-4C69-8C5A-AE4D0E77056A |
		| Name             | iphone-12-1                          |
		| ProduceDate      | 2024-01-01 12:00:00                  |
		| ManufacturePhone | 22336                                |
		| ManufactureEmail | iphone12@gmail.com                   |
		| IsAvailable      | true                                 |
	Then the system should reject the update attempt with an UnauthorizedAccessException


@Acceptance
Scenario: Filter products successfully
	When a user attempts to filter products with the name of the creator containing "john"
	Then the user should receive the following list of products: 
		| Name        | ProduceDate         | ManufacturePhone | ManufactureEmail   | IsAvailable | CreatorName |
		| iphone-12-1 | 2024-01-01 12:00:00 | 223366           | iphone12@gmail.com | true        | john doe    |
		| iphone-13-1 | 2024-01-02 12:00:00 | 885522           | iphone13@gmail.com | true        | john doe    |

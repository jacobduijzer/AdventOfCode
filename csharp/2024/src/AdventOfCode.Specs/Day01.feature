Feature: Historian Hysteria

Scenario: Part 1, parsing the test data
    Given the list the Historians have
"""
3   4
4   3
2   5
1   3
3   9
3   3
"""
    When you fix the list
    Then there should be to arrays with the values
        | LeftArray | RightArray |
        | 1, 2, 3, 3, 3, 4 | 3, 3, 3, 4, 5, 9 |

Scenario: Part 1, calculating the distance
    Given the list the Historians have
    """
    3   4
    4   3
    2   5
    1   3
    3   9
    3   3
    """
    When you fix the list
    And calculate the total distances
    Then the total distance should be 11 

Scenario: Part 1, with real input from file
    Given the list the Historians have, called 'day01.txt'
    When you fix the list
    And calculate the total distances
    Then the total distance should be 2430334 
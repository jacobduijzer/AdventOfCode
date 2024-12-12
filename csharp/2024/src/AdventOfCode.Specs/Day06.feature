Feature: Day 06: Guard Gallivant

    Scenario: Part 1, loading the map and finding the guard with the test data
        Given a map of the guard situation
        """
        ....#.....
        .........#
        ..........
        ..#.......
        .......#..
        ..........
        .#..^.....
        ........#.
        #.........
        ......#...
        """
        When you load the grid
        Then the location of the guard should be 4,6

    Scenario: Part 1, counting the distinc positions the guard visits with the test data
        Given a map of the guard situation
        """
        ....#.....
        .........#
        ..........
        ..#.......
        .......#..
        ..........
        .#..^.....
        ........#.
        #.........
        ......#...
        """
        When you load the grid
        And you count the number of distinct positions the guard visits
        Then the number of distinct locations the guard visits should be 41

    Scenario: Part 1, loading the map and finding the guard 
        Given the map of the guard situation called 'Day06.txt'
        When you load the grid
        Then the location of the guard should be 60,70

    Scenario: Part 1, counting the distinc positions the guard visits 
        Given the map of the guard situation called 'Day06.txt'
        When you load the grid
        And you count the number of distinct positions the guard visits
        Then the number of distinct locations the guard visits should be 4967
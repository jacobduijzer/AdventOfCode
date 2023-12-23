use hashbrown::HashSet;
use std::collections::VecDeque;

fn map_input_to_grid(input: &str) -> ((usize, usize), Vec<Vec<char>>) {
    let mut starting_point = (0, 0);
    let map: Vec<Vec<char>> = input
        .lines()
        .enumerate()
        .map(|(y, l)| {
            l.chars()
                .enumerate()
                .map(|(x, char)| {
                    if char == 'S' {
                        starting_point = (y, x);
                        '.'
                    } else {
                        char
                    }
                })
                .collect()
        })
        .collect();

    (starting_point, map)
}

fn get_positions_after_steps(map: &Vec<Vec<char>>,
                             start: (usize, usize),
                             steps: usize) ->  HashSet<(usize, usize)> {
    let mut positions: HashSet<(usize, usize)> = HashSet::new();
    positions.insert(start);

    for _ in 0..steps {
        let mut new_positions: HashSet<(usize, usize)> = HashSet::new();
        for position in positions {
            let (y, x) = position;
            if y > 0 && map[y - 1][x] == '.' {
                new_positions.insert((y - 1, x));
            }
            if y < map.len() - 1 && map[y + 1][x] == '.' {
                new_positions.insert((y + 1, x));
            }
            if x > 0 && map[y][x - 1] == '.' {
                new_positions.insert((y, x - 1));
            }
            if x < map[y].len() - 1 && map[y][x + 1] == '.' {
                new_positions.insert((y, x + 1));
            }
        }
        positions = new_positions;
    }
    positions
}


pub fn solve_part1(input: &str) -> usize {
    let (start, grid) = map_input_to_grid(input);
    get_positions_after_steps(&grid, start, 64)
        .len()
}

#[cfg(test)]
mod tests {
    use super::*;

    const TEST_DATA_1: &str = "...........
.....###.#.
.###.##..#.
..#.#...#..
....#.#....
.##..S####.
.##..#...#.
.......##..
.##.#.####.
.##..##.##.
...........";

    const TEST_DATA_2: &str = "...###.#.
..#.#....
#..S####.
#..#...#.
#..##.##.
.........";

    // #[test]
    // fn test_map_input_to_grid() {
    //     let grid = map_input_to_grid(TEST_DATA_1);
    //
    //     assert_eq!(grid.len(), 11);
    //     assert_eq!(grid[0].len(), 11);
    //     assert_eq!(grid[0].chars().nth(0).unwrap(), '.');
    //     assert_eq!(grid[0].chars().nth(9).unwrap(), '.');
    //     assert_eq!(grid[9].chars().nth(0).unwrap(), '.');
    //     assert_eq!(grid[9].chars().nth(9).unwrap(), '#');
    //     assert_eq!(grid[5].chars().nth(5).unwrap(), 'S');
    // }
    //
    // #[test]
    // fn find_test_coordinate_in_test_data() {
    //     let grid = map_input_to_grid(TEST_DATA_1);
    //
    //     let (row, col) = find_start_coordinate(&grid);
    //
    //     assert_eq!(5, row);
    //     assert_eq!(5, col);
    // }
    //
    // #[test]
    // fn find_test_coordinate_in_test_data_2() {
    //     let grid = map_input_to_grid(TEST_DATA_2);
    //
    //     let (row, col) = find_start_coordinate(grid);
    //
    //     assert_eq!(2, row);
    //     assert_eq!(3, col);
    // }

    #[test]
    fn solve_part1_with_test_data() {
        // let result = solve_part1(TEST_DATA_1);

        let (start, grid) = map_input_to_grid(TEST_DATA_1);
        let result = get_positions_after_steps(&grid, start, 6)
            .len();

        assert_eq!(16, result);
    }

}

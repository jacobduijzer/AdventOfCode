use hashbrown::HashMap;

type Grid = Vec<Vec<char>>;

fn parse_input_to_grid(input: &str) -> Grid {
    input
        .lines()
        .collect::<Vec<_>>()
        .iter()
        .map(|line| line.trim().chars().collect())
        .collect()
}


fn slide_all_rocks(grid: Grid) -> Grid {
    let mut slided_grid = grid.clone();

    let mut done = false;
    while !done {
        done = true;
        for r in 0..slided_grid.len() - 1 {
            for c in 0..slided_grid[0].len() {
                if slided_grid[r+1][c] == 'O' && slided_grid[r][c] == '.' {
                    slided_grid[r][c] = 'O';
                    slided_grid[r+1][c] = '.';
                    done = false;
                }
            }
        }
    }

    slided_grid
}

fn rotate_grid(grid: Grid) -> Grid {
    let mut rotated: Grid = vec![vec!['.'; grid.len()]; grid[0].len()];
    for col in 0..grid[0].len() {
        for row in 0..grid.len() {
            rotated[col][grid[0].len() - 1 - row] = grid[row][col];
        }
    }
    rotated
}

fn calculate_load(grid: Grid) -> usize {
    let mut total: usize = 0;
    for col in 0..grid[0].len() {
        let mut next = 0;
        for row in 0..grid.len() {
            match grid[row][col] {
                'O' => {
                    total += grid.len() as usize - next;
                    next += 1;
                }
                '#' => next = row as usize + 1,
                _ => {}
            }
        }
    }
    total
}

fn calculate_load_for_part_2(grid: &Vec<Vec<char>>) -> i64 {
    let mut total: i64 = 0;
    for col in 0..grid[0].len() {
        for row in 0..grid.len() {
            match grid[row][col] {
                'O' => {
                    total += grid.len() as i64 - row as i64;
                }
                _ => {}
            }
        }
    }
    total
}

fn rotate_and_slide(mut grid: Grid) -> Grid {
    grid = slide_all_rocks(grid);
    grid = rotate_grid(grid);
    grid = slide_all_rocks(grid);
    grid = rotate_grid(grid);
    grid = slide_all_rocks(grid);
    grid = rotate_grid(grid);
    grid = slide_all_rocks(grid);
    rotate_grid(grid)
}

pub fn solve_part1(input: &str) -> usize {
    let mut grid = parse_input_to_grid(input);
    grid = slide_all_rocks(grid);
    calculate_load(grid)
}

pub fn solve_part2(input: &str) -> usize {
    let mut grid = parse_input_to_grid(input);
    let mut store: HashMap<String, usize> = HashMap::new();
    let mut cycle_found_at = 0;


    for j in 0..1000 {
        grid = rotate_and_slide(grid);

        let joined = grid
            .iter()
            .map(|row| row.iter().collect::<String>())
            .collect::<Vec<String>>()
            .join("");

        let found = store.contains_key(&joined);
        if found {
            if cycle_found_at == 0 {
                cycle_found_at = j;
                store.clear();
                store.insert(joined, j);
                continue;
            }

            let mut remaining = 1000000000 - j - 1;
            remaining %= j - cycle_found_at;
            for _ in 0..remaining {
                grid = rotate_and_slide(grid);
            }

            return calculate_load_for_part_2(&grid).try_into().unwrap();
        }

        store.insert(joined, j);
    }

    0
}

#[cfg(test)]
mod tests {
    use rstest::*;
    use super::*;
    use crate::common::point::Point;

    const TEST_DATA_1: &str = "O....#....
O.OO#....#
.....##...
OO.#O....O
.O.....O#.
O.#..O.#.#
..O..#O..O
.......O..
#....###..
#OO..#....";

    #[test]
    fn parse_input_with_test_data_1() {
        let grid = parse_input_to_grid(TEST_DATA_1);

        assert_eq!(10, grid.len());
        assert_eq!(10, grid[0].len());

        assert_eq!('O', grid[0][0]);
        assert_eq!('.', grid[0][1]);
    }

    #[test]
    fn slide_all_rocks_with_test_input() {
        let grid = parse_input_to_grid(TEST_DATA_1);
        let slided_grid = slide_all_rocks(grid);

        assert_eq!(slided_grid[0][0], 'O');
        assert_eq!(slided_grid[1][0], 'O');
        assert_eq!(slided_grid[2][0], 'O');
        assert_eq!(slided_grid[3][0], 'O');
        assert_eq!(slided_grid[4][0], '.');
        assert_eq!(slided_grid[8][0], '#');
        assert_eq!(slided_grid[9][0], '#');
    }

    #[test]
    fn calculate_load_for_part_1_with_test_input() {
        let result = solve_part1(TEST_DATA_1);

        assert_eq!(136, result);
    }

    #[test]
    fn rotate_grid_with_test_input() {
        let grid = parse_input_to_grid(TEST_DATA_1);
        let slided_grid = slide_all_rocks(grid);
        let rotated = rotate_grid(slided_grid);

        assert_eq!(rotated[0][0], '#');
    }

    #[test]
    fn calculate_load_for_part_2_with_test_input() {
        let result = solve_part2(TEST_DATA_1);

        assert_eq!(64, result);
    }
}
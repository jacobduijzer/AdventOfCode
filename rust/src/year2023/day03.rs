use regex::Regex;
use std::cmp::{max, min};
use std::collections::HashMap;

fn map_input_to_grid(input: &str) -> Vec<String> {
   input
        .lines()
        .map(|s| s.to_string())
        .collect()
}

fn look_around_for_symbols(map: &Vec<String>, row: usize, col: usize, radius: usize) -> bool {
    let rows = map.len();
    let cols = if rows > 0 { map[0].len() } else { 0 };

    for i in (row.saturating_sub(radius))..=(row + radius).min(rows - 1) {
        for j in (col.saturating_sub(radius))..=(col + radius).min(cols - 1) {
            if i != row || j != col {
                if let ch = map.get(i).and_then(|row| row.chars().nth(j)).unwrap() {
                    if !ch.is_ascii_digit() && ch != '.' {
                        return true;
                    }
                }
            }
        }
    }

    false
}

pub fn solve_part1(input: &str) -> i32 {
    let matrix = map_input_to_grid(input);
    let number_re = Regex::new("\\d+").unwrap();
    let mut sum = 0;

    for i in 0..matrix.len() {
        let row = &matrix[i];
        for number in number_re.find_iter(row) {
            let mut found: bool = false;
            for j in number.start()..number.end() {
                if look_around_for_symbols(&matrix, i, j, 1) && !found {
                    sum += number.as_str().parse::<i32>().unwrap();
                    found = true;
                }
            }
        }
    }

    sum
}

pub fn solve_part2(input: &str) -> i32 {
    let matrix = map_input_to_grid(input);
    let number_re = Regex::new(r"\d+").unwrap();
    let mut gears: HashMap<(usize, usize), Vec<i32>> = HashMap::new();

    for i in 0..matrix.len() {
        let row = &matrix[i];

        for number in number_re.find_iter(row) {
            if i > 0 {
                let above_row = &matrix[i - 1].as_bytes();

                for j in number.start().saturating_sub(1)..min(above_row.len(), number.end() + 1) {
                    if above_row[j] == b'*' {
                        gears.entry((i - 1, j)).or_default().push(number.as_str().parse::<i32>().unwrap());
                    }
                }
            }

            if i < matrix.len() - 1 {
                let below_row = &matrix[i + 1].as_bytes();

                for j in number.start().saturating_sub(1)..min(below_row.len(), number.end() + 1) {
                    if below_row[j] == b'*' {
                        gears.entry((i + 1, j)).or_default().push(number.as_str().parse::<i32>().unwrap());
                    }
                }
            }

            if number.start() > 0 {
                let left = row.as_bytes()[number.start() - 1];

                if left == b'*' {
                    gears.entry((i, number.start() - 1)).or_default().push(number.as_str().parse::<i32>().unwrap());
                }
            }

            if number.end() < row.len() {
                let right = row.as_bytes()[number.end()];

                if right == b'*' {
                    gears.entry((i, number.end())).or_default().push(number.as_str().parse::<i32>().unwrap());
                }
            }
        }
    }

    let mut sum = 0;
    gears
        .values()
        .filter(|vec| vec.len() == 2)
        .for_each(|vec| sum += vec[0] * vec[1]);

    sum
}

#[cfg(test)]
mod tests {
    use itertools::assert_equal;
    use super::*;

    const test_input: &str = "467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..";

    #[test]
    fn load_game_data() {
        let engine_map = map_input_to_grid(test_input);

        assert_eq!(engine_map.len(), 10);
        assert_eq!(engine_map[0].len(), 10);
        assert_eq!(engine_map[0].chars().nth(0).unwrap(), '4');
        assert_eq!(engine_map[0].chars().nth(9).unwrap(), '.');
        assert_eq!(engine_map[9].chars().nth(0).unwrap(), '.');
        assert_eq!(engine_map[9].chars().nth(9).unwrap(), '.');
    }

    #[test]
    fn find_part_numbers_01() {
        let test_data = "467..114..
...*......";

        let sum_part_numbers = solve_part1(test_data);

        assert_eq!(sum_part_numbers, 467);
    }

    #[test]
    fn find_part_numbers() {
        let sum_part_numbers = solve_part1(test_input);

        assert_eq!(sum_part_numbers, 4361);
    }

    #[test]
    fn find_gear_ratio()
    {
        let gear_ratio = solve_part2(test_input);
        assert_eq!(gear_ratio, 467835)
    }
}
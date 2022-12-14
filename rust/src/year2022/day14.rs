use std::collections::HashSet;
use regex::Regex;

struct Bounds {
    max_y: i32,
    min_x: i32,
    max_x: i32
}

impl Bounds {
    fn from(max_y: i32, min_x: i32, max_x: i32) -> Bounds {
        Bounds { max_y, min_x, max_x }
    }
}

fn parse_grid(input: &str) -> (Bounds, HashSet<(i32, i32)>) {
    let mut grid   = HashSet::new();
    let mut max_y  = 0;
    let mut min_x  = 0;
    let mut max_x  = 0;
    let parser = Regex::new(r"(\d+),(\d+)").expect("Should pe in the format (x, y)");
    for line in input.lines() {
        let mut cursor = (0, 0);

        for pos in parser.captures_iter(&line) {
            let x = pos[1].parse::<i32>().unwrap();
            let y = pos[2].parse::<i32>().unwrap();

            if cursor == (0, 0) {
                cursor = (x, y);
            }
            else {
                if x == cursor.0 {
                    if y < cursor.1 {
                        for y in y..=cursor.1 {
                            grid.insert((x, y));
                        }
                    }
                    else {
                        for y in cursor.1..=y {
                            grid.insert((x, y));
                        }
                    }
                }
                else {
                    if x < cursor.0 {
                        for x in x..=cursor.0 {
                            grid.insert((x, y));
                        }
                    }
                    else {
                        for x in cursor.0..=x {
                            grid.insert((x, y));
                        }
                    }
                }
                cursor = (x, y);
            }
            max_y  = max_y.max(y);
            min_x  = min_x.min(x);
            max_x  = max_x.max(x);
        }
    }
    (Bounds::from(max_y, min_x, max_x), grid)
}

fn start_flowing_sand(bounds: Bounds, grid: &mut HashSet<(i32, i32)>) -> usize {
    let max_y = bounds.max_y;
    let mut count = 0;
    let mut sand  = (500, 0);

    while sand.1 < max_y {
        if !grid.contains(&(sand.0, sand.1 + 1)) {
            sand.1 += 1;
        }
        else if !grid.contains(&(sand.0 - 1, sand.1 + 1)) {
            sand.0 -= 1;
            sand.1 += 1;
        }
        else if !grid.contains(&(sand.0 + 1, sand.1 + 1)) {
            sand.0 += 1;
            sand.1 += 1;
        }
        else {
            count += 1;
            grid.insert(sand);
            if sand.1 == 0 {
                break;
            }
            sand = (500, 0);
        }
    }
    count
}

fn add_floor(bounds: Bounds, grid: &mut HashSet<(i32, i32)>) -> Bounds
{
    let max_y = bounds.max_y + 2;
    let min_x = bounds.min_x - max_y;
    let max_x = bounds.max_x + max_y;

    for x in min_x..=max_x {
        grid.insert((x, max_y));
    }

    Bounds::from(max_y, min_x, max_x)
}

pub fn solve_part1(input: &str) -> usize {
    let (took, (bounds, mut grid)) = took::took(|| parse_grid(input));
    println!("Time spent parsing grid: {}", took);
    let (took, result) = took::took(|| start_flowing_sand(bounds, &mut grid));
    println!("Time spent flowing sand: {}", took);
    result
}

pub fn solve_part2(input: &str) -> usize {
    let (took, (bounds, mut grid)) = took::took(|| parse_grid(input));
    println!("Time spent parsing grid: {}", took);
    let (took, bounds) = took::took(|| add_floor(bounds, &mut grid));
    println!("Time spent adding floor: {}", took);
    let (took, result) = took::took(|| start_flowing_sand(bounds, &mut grid));
    println!("Time spent flowing sand: {}", took);
    result
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = crate::common::input::read_file(2022, 14, "testinput");
        assert_eq!(solve_part1(&input), 24);
    }

    #[test]
    fn part_one()
    {
        let input = crate::common::input::read_file(2022, 14, "input");
        assert_eq!(solve_part1(&input), 888);
    }

    #[test]
    fn test_part_two() {
        let input = crate::common::input::read_file(2022, 14, "testinput");
        assert_eq!(solve_part2(&input), 93);
    }

    #[test]
    fn part_two() {
        let input = crate::common::input::read_file(2022, 14, "input");
        let (took, result) = took::took(|| solve_part2(&input));
        println!("Time spent full part: {}", took);
        assert_eq!(result, 26461);
    }


}
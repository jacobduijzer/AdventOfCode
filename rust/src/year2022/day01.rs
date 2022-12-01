use crate::common;
use std::collections::BTreeSet;
use std::ops::Sub;

pub fn solve_part1(input: &str) -> i32 {
    let mut strongest_elve = 0;
    let mut current = 0;
    for line in input.lines() {
       if line.is_empty() {
           if current > strongest_elve {
               strongest_elve = curr;
           }
           current = 0;
       } else {
           current += line.parse::<i32>().unwrap();
       }
    }
    strongest_elve
}

pub fn solve_part2(input: &str) -> u32 {
    let mut elves: Vec<u32> = Vec::new();
    let mut current: u32 = 0;
    for line in input.lines() {
        if line.is_empty() {
            elves.push(current);
            current = 0;
        } else {
            let value = line.parse::<u32>().unwrap();
            current += value;
        }
    }
    if current != 0 {
        elves.push(current);
    }
    elves.sort_by(|a, b| b.cmp(a));
    elves[0] + elves[1] + elves[2]
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = common::input::read_file(2022, 1, "testinput");
        assert_eq!(solve_part1(&input), 24000);
    }

    #[test]
    fn test_part_two() {
        let input = common::input::read_file(2022, 1, "testinput");
        assert_eq!(solve_part2(&input), 45000);
    }

}
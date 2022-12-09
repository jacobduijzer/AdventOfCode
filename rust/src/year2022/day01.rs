use itertools::Itertools;
use std::cmp::Reverse;
use std::collections::BinaryHeap;

type Input = Vec<u32>;

fn get_elves(input: &str) -> Input {
    input
        .split("\n\n")
        .map(|elf| elf.lines().filter_map(|cal|  cal.parse::<u32>().ok()).sum())
        .collect()
}

pub fn solve_part1(input: &str) -> u32 {
    get_elves(input)
        .iter()
        .max()
        .copied()
        .unwrap()
}

pub fn solve_part2(input: &str) -> u32 {
    get_elves(input)
        .iter()
        .sorted_by_key(|x| Reverse(*x))
        .take(3)
        .sum()
}

pub fn solve_part1_first(input: &str) -> i32 {
    let mut strongest_elve = 0;
    let mut current = 0;

    for line in input.lines() {
       if line.is_empty() {
           if current > strongest_elve {
               strongest_elve = current;
           }
           current = 0;
       } else {
           current += line.parse::<i32>().unwrap();
       }
    }
    strongest_elve
}

pub fn solve_part2_first(input: &str) -> u32 {
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

pub fn solve_part2_second(input: &str) -> u32 {
    let mut elves = BinaryHeap::new();
    //let mut elves: Vec<u32> = Vec::new();
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

    elves.pop().unwrap() + elves.pop().unwrap() + elves.pop().unwrap()
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = crate::common::input::read_file(2022, 1, "testinput");
        assert_eq!(solve_part1(&input), 24000);
    }

    #[test]
    fn test_part_two() {
        let input = crate::common::input::read_file(2022, 1, "testinput");
        assert_eq!(solve_part2(&input), 45000);
    }

    #[test]
    fn test_part_one_first() {
        let input = crate::common::input::read_file(2022, 1, "testinput");
        assert_eq!(solve_part1_first(&input), 24000);
    }

    #[test]
    fn test_part_two_first() {
        let input = crate::common::input::read_file(2022, 1, "testinput");
        assert_eq!(solve_part2_first(&input), 45000);
    }

}
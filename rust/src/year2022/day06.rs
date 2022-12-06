use crate::common;
use std::collections::HashSet;

fn solve(input: &str, length: usize) -> usize {
    let data = input.chars().collect::<Vec<_>>();

    for i in length..input.len() {
        let message: HashSet<&char> = HashSet::from_iter(data[(i - length)..i].iter());
        if message.len() == length {
            return i;
        }
    }
    0
}

pub fn solve_part1(input: &str) -> usize {
   solve(input, 4)
}

pub fn solve_part2(input: &str) -> usize {
   solve(input, 14)
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = common::input::read_file(2022, 6, "testinput");
        assert_eq!(solve_part1(&input), 7);
    }

    #[test]
    fn test_part_one_a() {
        let input = "bvwbjplbgvbhsrlpgdmjqwftvncz";
        assert_eq!(solve_part1(&input), 5);
    }

    #[test]
    fn test_part_one_b() {
        let input = "nppdvjthqldpwncqszvftbrmjlhg";
        assert_eq!(solve_part1(&input), 6);
    }

    #[test]
    fn test_part_two_a() {
        let input = "mjqjpqmgbljsphdztnvjfqwrcgsmlb";
        assert_eq!(solve_part2(&input), 19);
    }

    #[test]
    fn test_part_two_b() {
        let input = "bvwbjplbgvbhsrlpgdmjqwftvncz";
        assert_eq!(solve_part2(&input), 23);
    }
}
use crate::common;
use std::str::FromStr;
use std::collections::HashSet;

pub fn solve_part1(input: &str) -> usize {
    unimplemented!()
}

pub fn solve_part2(input: &str) -> uzise {
 unimplemented!()
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = common::input::read_file(2022, 9, "testinput");
        assert_eq!(solve_part1(&input), "CMZ");
    }

    #[test]
    fn test_part_two() {
        let input = common::input::read_file(2022, 5, "testinput");
        assert_eq!(solve_part2(&input), "MCD");
    }
}
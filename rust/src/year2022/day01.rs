use crate::common;

pub fn solve_part1(input: &str) -> u32 {
    unimplemented!("TODO");
}

pub fn solve_part2(input: &str) -> u32 {
    unimplemented!("TODO");
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = common::input::read_file(2022, 1, "testinput");
        assert_eq!(solve_part1(&input));
    }

    #[test]
    fn test_part_two() {
        let input = common::input::read_file(2022, 1, "testinput");
        assert_eq!(solve_part2(&input), 5);
    }

}
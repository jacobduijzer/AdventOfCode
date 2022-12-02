use crate::common;

pub fn solve_part1(input: &str) -> u32 {
    input
        .lines()
        .map(|l| l.split_once(" ").unwrap())
        .fold(0u32, |score, (opp, me)| {
            match (opp, me) {
                ("A", "Z") => score + 3,
                ("B", "X") => score + 1,
                ("C", "Y") => score + 2,
                ("C", "X") => score + 6 + 1,
                ("A", "Y") => score + 6 + 2,
                ("B", "Z") => score + 6 + 3,
                (_, "X") => score + 3 + 1,
                (_, "Y") => score + 3 + 2,
                (_, "Z") => score + 3 + 3,
                _ => unreachable!(),
            }
        })
        .into()
}

pub fn solve_part2(input: &str) -> u32 {
    input
        .lines()
        .map(|l| l.split_once(" ").unwrap())
        .fold(0u32, |score, (opp, me)| {
            match (opp, me) {
                ("A", "X") => score + 3,
                ("B", "X") => score + 1,
                ("C", "X") => score + 2,
                ("A", "Y") => score + 4,
                ("B", "Y") => score + 5,
                ("C", "Y") => score + 6,
                ("A", "Z") => score + 8,
                ("B", "Z") => score + 9,
                ("C", "Z") => score + 7,
                _ => unreachable!(),
            }
        })
        .into()
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = common::input::read_file(2022, 2, "testinput");
        assert_eq!(solve_part1(&input), 15);
    }

    #[test]
    fn test_part_two() {
        let input = common::input::read_file(2022, 2, "testinput");
        assert_eq!(solve_part2(&input), 12);
    }
}
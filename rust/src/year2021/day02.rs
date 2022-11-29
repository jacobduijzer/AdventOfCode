use crate::common;

pub fn solve_part1(input: &str) -> i32 {
    let (f, d) = input
        .lines()
        .map(|l| l.split_once(" ").unwrap())
        .fold((0, 0), |(f, d), (k, v)| {
            match (k, v.parse::<i32>().unwrap()) {
                ("forward", v) => (f + v, d),
                ("down", v) => (f, d + v),
                ("up", v) => (f, d - v),
                _ => unreachable!(),
            }
        });
    return f * d;
}

pub fn solve_part2(input: &str) -> i32 {
    let (f, d , _) = input
        .lines()
        .map(|l| l.split_once(" ").unwrap())
        .fold((0, 0, 0), |(f, d, a), (k, v)| {
            match (k, v.parse::<i32>().unwrap()) {
                ("forward", v) => (f + v, d + a * v,  a),
                ("down", v) => (f, d, a + v),
                ("up", v) => (f, d,  a - v),
                _ => unreachable!(),
            }
        });
    return f * d;
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = common::input::read_file(2021, 2, "testinput");
        assert_eq!(solve_part1(&input), 150);
    }

    #[test]
    fn test_part_two() {
        let input = common::input::read_file(2021, 2, "testinput");
        assert_eq!(solve_part2(&input), 900);
    }
}
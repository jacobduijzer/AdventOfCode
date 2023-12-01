use regex::{Regex, RegexSet};

const WORDVALUES: [(&str, i32); 9] = [("one", 1), ("two", 2), ("three", 3), ("four", 4), ("five", 5), ("six", 6), ("seven", 7),("eight", 8), ("nine", 9)];

fn first_number(line: &str, part2: bool) -> i32 {
     if let Some(v) = line.chars().next().unwrap_or('z').to_digit(10) {
        v as i32
    } else {
        let word_result = WORDVALUES
            .iter().map(|(w, v)| if part2 && line.starts_with(w) {v} else{&0}).sum::<i32>();
        if word_result > 0 {
            word_result
        } else {
            first_number(&line[1..], part2)
        }
    }
}

fn last_number(s : &str, part2: bool) -> i32 {
    if let Some(v) = s.chars().last().unwrap_or('z').to_digit(10) {
        v as i32
    } else {
        let word_result = WORDVALUES
            .iter().map(|(w, v)| if part2 && s.ends_with(w) {v} else{&0}).sum::<i32>();
        if word_result > 0 {
            word_result
        } else {
            last_number(&s[..s.len()-1], part2)
        }
    }
}

fn solve(input: &str, part2: bool) -> i32 {
    input
        .lines()
        .map(|line| first_number(line, part2) * 10 + last_number(line, part2))
        .sum()
}

pub fn solve_part1(input: &str) -> i32 {
    solve(input, false)
}

pub fn solve_part2(input: &str) -> i32 {
   solve(input, true)
}


#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_first_number_part1() {
        assert_eq!(first_number("1abc2", false), 1);
        assert_eq!(first_number("pqr3stu8vwx",false), 3);
        assert_eq!(first_number("a1b2c3d4e5f", false), 1);
        assert_eq!(first_number("treb7uchet", false), 7);
    }

    #[test]
    fn test_last_number_part1() {
        assert_eq!(last_number("1abc2", false), 2);
        assert_eq!(last_number("pqr3stu8vwx", false), 8);
        assert_eq!(last_number("a1b2c3d4e5f", false), 5);
        assert_eq!(last_number("treb7uchet", false), 7);
    }

    #[test]
    fn test_first_number_part2() {
        assert_eq!(first_number("twoone", true), 2);
        assert_eq!(first_number("eightwo", true), 8);
        assert_eq!(first_number("two1nine", true), 2);
        assert_eq!(first_number("eightwothree", true), 8);
        assert_eq!(first_number("abcone2threexyz", true), 1);
        assert_eq!(first_number("abcone2threexyz", true), 1);
        assert_eq!(first_number("abcone2threexyz", true), 1);
        assert_eq!(first_number("xtwone3four", true), 2);
        assert_eq!(first_number("abcone2threexyz", true), 1);
        assert_eq!(first_number("4nineeightseven2", true), 4);
        assert_eq!(first_number("abcone2threexyz", true), 1);
        assert_eq!(first_number("abcone2threexyz", true), 1);
        assert_eq!(first_number("zoneight234", true), 1);
        assert_eq!(first_number("abcone2threexyz", true), 1);
        assert_eq!(first_number("7pqrstsixteen", true), 7);
    }

    #[test]
    fn test_last_number_part2() {
        assert_eq!(last_number("twoone", true), 1);
        assert_eq!(last_number("eightwo", true), 2);
    }

    #[test]
    fn test_part1() {
        let data: &str = "1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet
";
        let part1_res = solve_part1(data);
        assert_eq!(part1_res, 142);
    }

    #[test]
    fn test_part2() {
        let data: &str = "two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen
";
        let part2_res = solve_part2(data);
        assert_eq!(part2_res, 281);
    }

}
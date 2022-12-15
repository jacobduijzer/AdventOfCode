use std::collections::HashMap;

#[derive(PartialEq)]
pub struct Instruction {
    cycles: i8,
    steps: i64
}

impl From<&str> for Instruction {
    fn from(s: &str) -> Self {
        match s.split_ascii_whitespace().collect::<Vec<&str>>().as_slice() {
            ["noop"] => Instruction { cycles: 1, steps: 0_i64 },
            ["addx", v] => Instruction { cycles: 2, steps: v.parse::<i64>().unwrap_or(0_i64) },
            _ => unreachable!(),
        }
    }
}

fn parse_data(input: &str) -> Vec<Instruction> {
    input
        .lines()
        .map(|line| Instruction::from(line))
        .collect()
}

fn calculate_state(input: &[Instruction]) -> HashMap<usize, i64> {
    let mut x: i64 = 1;
    let mut state: HashMap<usize, i64> = HashMap::new();
    for (cycle, instruction) in input.iter().enumerate() {
        x += instruction.steps;
        state.insert(cycle + 1, x);
    }
    state
}

fn part_one(instructions: Vec<Instruction>) -> i64 {
    let cycles = vec![20, 60, 100, 140, 180, 220];
    let mut registry = 1;
    let mut cycle = 0;
    let mut signal_strengths: Vec<i64> = vec![];

    for instruction in instructions {
        for _ in 0..instruction.cycles {
            cycle += 1;
            if cycles.contains(&cycle) {
                signal_strengths.push(registry * cycle);
            }
        }
        if cycle > 220 {
            break;
        }
        registry += instruction.steps;
    };

    signal_strengths.iter().sum::<i64>()
}

pub fn part_two(instructions: Vec<Instruction>) -> String {
    let mut result: String = String::new();
    let mut crt: Vec<Vec<String>> = vec![vec![]; 6];
    let mut pixel: i64 = 0;
    let mut registry: i64 = 1;
    let mut cycle = 0;
    let mut row;

    for signal in instructions {
        for _ in 0..signal.cycles {
            row = cycle / 40;
            cycle += 1;
            if (registry - pixel).abs() <= 1 {
                crt[row].push("#".to_owned());
            } else {
                crt[row].push(".".to_owned());
            }
            pixel += 1;
            pixel %= 40;
        }
        registry += signal.steps;
    };

    for row in crt {
        result.push_str(row.join("").as_str());
        result.push_str("\n");
    }

    result
}

pub fn solve_part1(input: &str) -> i64 {
    let (took, parse_result) = took::took(|| parse_data(input));
    println!("Time spent parsing: {}", took);

    let (took, result) = took::took(|| part_one(parse_result));
    println!("Result part one: {result}");
    println!("Time spent: {}", took);

    result
}

pub fn solve_part2(input: &str) -> String  {
    let (took, parse_result) = took::took(|| parse_data(input));
    println!("Time spent parsing: {}", took);
    let (took, result) = took::took(|| part_two(parse_result));
    //println!("Result part two:");
    //println!("{result}");
    println!("Time spent: {}", took);
    result
}

#[cfg(test)]
mod tests {
    use super::*;

    const TEST_DATA_1: &str = "noop
addx 3
addx -5";

    #[test]
    fn parse_simple_input() {
        let instructions = parse_data(TEST_DATA_1);
        assert_eq!(instructions.len(), 3);
    }

    #[test]
    fn test_part_one() {
        let input = crate::common::input::read_file(2022, 10, "testinput");
        assert_eq!(solve_part1(&input), 13140);
    }

    const TEST_DATA_2: &str = "##..##..##..##..##..##..##..##..##..##..
###...###...###...###...###...###...###.
####....####....####....####....####....
#####.....#####.....#####.....#####.....
######......######......######......####
#######.......#######.......#######.....
";

    #[test]
    fn test_part_two() {
        let input = crate::common::input::read_file(2022, 10, "testinput");
        assert_eq!(solve_part2(&input), TEST_DATA_2);
    }
}
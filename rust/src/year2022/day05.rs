use crate::common;
use std::str::FromStr;
use regex::Regex;

struct Ship {
    number_of_stacks: u8,

}
struct Rearrangement {
    number_of_items: usize,
    start_position: usize,
    target_position: usize
}

fn parse_ship(input: &str) -> Vec<Vec<char>> {
    let num_stacks = input.lines().last().unwrap().split_whitespace().collect::<Vec<_>>().len();
    let mut stacks: Vec<_> = (0 .. num_stacks).map(|_| Vec::<char>::new()).collect();
    input
        .lines()
        .rev()
        .into_iter()
        .skip(1)
        .for_each(|line| {
            for n in 0 .. num_stacks {
                let index = n * 4 + 1;
                if index < line.len() && line.chars().nth(index).unwrap() != ' ' {
                    stacks[n].push(line.chars().nth(index).unwrap());
                }
            }
        });

    stacks
}

fn parse_rearrangement(input: &str) -> Vec<Rearrangement> {
    input
        .lines()
        .into_iter()
        .map(|line| {
            let test: Vec<&str> = line.split_whitespace().collect();
            return Rearrangement {
                number_of_items: usize::from_str(test[1]).unwrap(),
                start_position: usize::from_str(test[3]).unwrap(),
                target_position: usize::from_str(test[5]).unwrap()
            };
    }).collect()
}

fn parse(input: &str) -> (Vec<Vec<char>>, Vec<Rearrangement>) {
    let data: Vec<&str> = input.split("\n\n").collect();
    let ship = parse_ship(&data[0]);
    let rearrangements = parse_rearrangement(&data[1]);
    (ship, rearrangements)
}

pub fn solve_part1(input: &str) -> String {
    let mut slice: &[_] = &['a', 'b', 'c', 'd'];
    println!("Starting");
    let (mut ship, arrangements) = parse(input);
    println!("Data parsed");
    let mut counter: u32 = 0;
    let mut moved_crates = vec![];
    arrangements
        .iter()
        .for_each(|arr| {
            //println!("({}/{}) move {} from {} to {}", counter, arrangements.len(), arr.number_of_items, arr.start_position, arr.target_position);
            let new_len = ship[arr.start_position - 1].len() - arr.number_of_items;
            moved_crates.extend(ship[arr.start_position - 1].drain(new_len..));
            moved_crates.reverse();
            ship[arr.target_position - 1].append(&mut moved_crates);
            counter += 1;
        });
    let result = ship
        .iter()
        .map(|line| line.last().unwrap())
        .collect::<String>();

    result
}

pub fn solve_part2(input: &str) -> String {
    println!("Starting");
    let (mut ship, arrangements) = parse(input);
    println!("Data parsed");
    let mut counter: u32 = 0;
    let mut moved_crates = vec![];
    arrangements
        .iter()
        .for_each(|arr| {
            //println!("({}/{}) move {} from {} to {}", counter, arrangements.len(), arr.number_of_items, arr.start_position, arr.target_position);
            let new_len = ship[arr.start_position - 1].len() - arr.number_of_items;
            moved_crates.extend(ship[arr.start_position - 1].drain(new_len..));
            ship[arr.target_position - 1].append(&mut moved_crates);
            counter += 1;
        });
    let result = ship
        .iter()
        .map(|line| line.last().unwrap())
        .collect::<String>();

    result
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = common::input::read_file(2022, 5, "testinput");
        assert_eq!(solve_part1(&input), "CMZ");
    }

    #[test]
    fn test_part_two() {
        let input = common::input::read_file(2022, 5, "testinput");
        assert_eq!(solve_part2(&input), "MCD");
    }
}

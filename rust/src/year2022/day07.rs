use std::str::FromStr;

fn parse_listing(input: &str) -> Vec<i32> {
    let lines: Vec<&str> = input.lines().collect();

    let mut current: Vec<i32> = vec![];
    let mut done: Vec<i32> = vec![];

    for line in lines {
        if line.starts_with("$ cd") {
            if line.contains("..") {
                done.push(current.pop().unwrap());
            }
            else {
                current.push(0);
            }
        }
        else if !line.starts_with("$ ls") && !line.starts_with("dir") {
            let num = i32::from_str(line.split_once(' ').expect("Line should have a space").0).unwrap();
            for i in &mut current {
                *i += num;
            }
        }
    }
    done.extend(&current);
    done
}

pub fn solve_part1(input: &str) -> usize {
    let listing: Vec<i32> = parse_listing(input);
    listing.iter().filter(|n| **n <= 100000).sum::<i32>() as usize
}

pub fn solve_part2(input: &str) -> i32 {
    let listing: Vec<i32> = parse_listing(input);
    let max = listing.iter().max().unwrap();
    let unused = 70000000 - max;
    let required = 30000000 - unused;
    listing.iter().filter(|n| **n >= required).min().copied().unwrap()
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = crate::common::input::read_file(2022, 7, "testinput");
        assert_eq!(solve_part1(&input), 95437);
    }

    #[test]
    fn test_part_two() {
        let input = crate::common::input::read_file(2022, 7, "testinput");
        assert_eq!(solve_part2(&input), 24933642);
    }
}
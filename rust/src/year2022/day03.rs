fn get_shared_item_from_pair(a: &str, b: &str) -> Option<char> {
    for character in a.chars() {
        if b.contains(character) {
            return Some(character);
        }
    }
    None
}

fn get_shared_item_from_trio(a: &str, b: &str, c: &str) -> Option<char> {
    for character in a.chars() {
        if b.contains(character) && c.contains(character) {
            return Some(character);
        }
    }
    None
}

fn priority(c: char) -> u8 {
    if c.is_ascii_uppercase() {
        (c as u8) - ('A' as u8) + 27
    } else if c.is_ascii_lowercase() {
        (c as u8) - ('a' as u8) + 1
    } else {
        0
    }
}

pub fn solve_part1(input: &str) -> u32 {
    input
        .lines()
        .map(|line| line.split_at(line.len() / 2))
        .fold(0, |total: u32, (compartment_a, compartment_b)| {
            let shared_item = get_shared_item_from_pair(compartment_a, compartment_b).unwrap();
            total + priority(shared_item) as u32
        })
}

pub fn solve_part2(input: &str) -> u32 {
    let elves: Vec<&str> = input.lines().collect();
    let total = elves
        .chunks(3)
        .fold(0, |total, elve_trio| {
            let shared_item = get_shared_item_from_trio(elve_trio[0], elve_trio[1], elve_trio[2]).unwrap();
            total + priority(shared_item) as u32
        });
    total
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = crate::common::input::read_file(2022, 3, "testinput");
        assert_eq!(solve_part1(&input), 157);
    }

    #[test]
    fn test_part_two() {
        let input = "vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw";
        assert_eq!(solve_part2(&input), 70);
    }
}
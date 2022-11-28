use crate::common;

pub fn solve_part1(folder: &str, day: u8) -> u32 {
    let input = common::input::read_file(folder, day);
    let count: u32 = input
        .lines()
        .map(|n| n.parse().unwrap())
        .collect::<Vec<u32>>()
        .windows(2)
        .filter(|w| w[1] > w[0])
        .count() as u32;
    count
}

pub fn solve_part2(folder: &str, day: u8) -> u32 {
    let input = common::input::read_file(folder, day);
    let count: u32 = input
        .lines()
        .map(|n| n.parse().unwrap())
        .collect::<Vec<u32>>()
        .windows(4)
        .filter(|w| w[3] > w[0])
        .count() as u32;
    count
}

#[cfg(test)]
mod tests { 
    use super::*;
    
    #[test]
    fn test_part_one() {
        assert_eq!(solve_part1("year2021/testinput/", 1), 7);
    }

    #[test]
    fn test_part_two() {
        assert_eq!(solve_part2("year2021/testinput/", 1), 5);
    }

}

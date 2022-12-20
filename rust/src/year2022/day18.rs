//use hashbrown::HashSet;

use std::collections::HashSet;
use itertools::Itertools;

fn sides((x,y,z): (i32,i32,i32)) -> [(i32,i32,i32); 6] {
    [(x-1,y,z),(x+1,y,z),(x,y-1,z),(x,y+1,z),(x,y,z-1),(x,y,z+1)]
}

fn solve(input: &str) -> (usize, usize) {
    let drops = input.lines()
        .filter_map(|l| l.split(',').map(|x| x.parse().unwrap()).collect_tuple())
        .collect::<HashSet<_>>();
    let max = drops.iter().flat_map(|&(x,y,z)| [x,y,z]).max().unwrap() + 1;
    let (mut seen, mut stack) = (HashSet::new(), vec![(0,0,0)]);
    while let Some(p) = stack.pop() {
        for (x,y,z) in sides(p) {
            if !drops.contains(&(x,y,z)) && !seen.contains(&(x,y,z)) && [x,y,z].iter().all(|&i| -1 <= i && i <= max) {
                seen.insert((x,y,z));
                stack.push((x,y,z));
            }
        }
    }

    let part1 = drops.iter().flat_map(|&p| sides(p)).filter(|s| !drops.contains(s)).count();
    let part2 = drops.iter().flat_map(|&p| sides(p)).filter(|s| seen.contains(s)).count();
    (part1, part2)
}

pub fn solve_part1(input: &str) -> usize {
    solve(input).0
}

pub fn solve_part2(input: &str) -> usize {
    solve(input).1
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = crate::common::input::read_file(2022, 18, "testinput");
        assert_eq!(solve_part1(&input), 64);
    }

    #[test]
    fn part_one() {
        let input = crate::common::input::read_file(2022, 18, "input");
        assert_eq!(solve_part1(&input), 3550);
    }

    #[test]
    fn test_part_two() {
        let input = crate::common::input::read_file(2022, 18, "testinput");
        assert_eq!(solve_part2(&input), 58);
    }

    #[test]
    fn part_two() {
        let input = crate::common::input::read_file(2022, 18, "input");
        assert_eq!(solve_part2(&input), 2028);
    }
}
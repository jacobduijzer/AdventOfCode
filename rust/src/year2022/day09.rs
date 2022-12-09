use std::collections::HashSet;

#[derive(Debug, Default, Clone, Copy, PartialEq, Eq, Hash)]
struct Coordinate {
    x: i32,
    y: i32
}

fn follow(lead: &Coordinate, t: &mut Coordinate) {
    let dx = lead.x - t.x;
    let dy = lead.y - t.y;
    if (dx.abs() + dy.abs() > 1) && (dx.abs() > 1 || dy.abs() > 1) {
        t.x += dx.signum();
        t.y += dy.signum()
    }
}

fn solve(input: &str, rope_length: usize) -> usize {
    let mut visited: HashSet<Coordinate> = HashSet::with_capacity(10_000);
    let mut head = Coordinate::default();
    let mut tails = vec![Coordinate::default(); rope_length];

    for instruction in input.lines() {
        let (direction, amount) = instruction.split_once(' ').unwrap();
        for _ in 0..amount.parse().unwrap() {
            match direction {
                "U" => head.y += 1,
                "D" => head.y -= 1,
                "L" => head.x -= 1,
                "R" => head.x += 1,
                _ => panic!("Should be unreachable!"),
            }
            follow(&head, &mut tails[0]);

            if rope_length == 2 {
                visited.insert(tails[0]);
            } else {
                for i in 1..rope_length {
                    let (left, right) = tails.split_at_mut(i);
                    follow(&left[i - 1], &mut right[0]);
                }
                visited.insert(tails.last().unwrap().clone());
            }
        }
    }
    visited.len()
}

pub fn solve_part1(input: &str) -> usize {
    solve(input, 2)
}

pub fn solve_part2(input: &str) -> usize {
    solve(input, 9)
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = crate::common::input::read_file(2022, 9, "testinput");
        assert_eq!(solve_part1(&input), 13);
    }

    const DATA_PART_2: &str = "R 5
U 8
L 8
D 3
R 17
D 10
L 25
U 20
";
    #[test]
    fn test_part_two() {
        let input = DATA_PART_2;
        assert_eq!(solve_part2(&input), 36);
    }
}
use hashbrown::HashMap;
use itertools::Itertools;

#[derive(PartialEq, Debug)]
struct Instruction {
    direction: (i64, i64),
    steps: i64
}

fn map_instructions(input: &str) -> Vec<Instruction> {

    let deltas: HashMap<char, (i64, i64)> =
            HashMap::from([('R', (1, 0)), ('L', (-1, 0)), ('D', (0, 1)), ('U', (0, -1))]);

    input
        .lines()
        .map(|line| {
            let values: Vec<&str> = line.split(' ').collect();
            let delta = deltas
                    .get(&values.get(0).unwrap().chars().nth(0).unwrap())
                    .unwrap();
            let steps = values.get(1).unwrap().parse::<i64>().unwrap();
            Instruction { direction: (delta.0, delta.1), steps }
        })
        .collect()
}

fn shoe_lace(poly: Vec<(i64, i64)>) -> i64 {
    let mut ret: i64 = 0;
    for i in 0..poly.len() {
        let current_point = poly.get(i).unwrap();
        let target_point = poly.get(if i == 0 { poly.len() - 1} else { i - 1}).unwrap();

        ret += target_point.0 * current_point.1 - target_point.1 * current_point.0;
    }
    ret / 2 as i64
}

pub fn solve_part1(input: &str) -> i64 {
    let mut poly: Vec<(i64, i64)> = vec![(0, 0)];
    let mut point_count: i64 = 0;

    let map = map_instructions(input);

    for line in map {
        let last_point = poly.last().unwrap();
        point_count += line.steps as i64;
        poly.push((last_point.0 + line.direction.0 * line.steps, last_point.1 + line.direction.1 * line.steps));
    }

    shoe_lace(poly) + point_count / 2 + 1
}

#[cfg(test)]
mod tests {
    use super::*;

    const TEST_DATA_1: &str = "R 6 (#70c710)
D 5 (#0dc571)
L 2 (#5713f0)
D 2 (#d2c081)
R 2 (#59c680)
D 2 (#411b91)
L 5 (#8ceee2)
U 2 (#caa173)
L 1 (#1b58a2)
U 2 (#caa171)
R 2 (#7807d2)
U 3 (#a77fa3)
L 2 (#015232)
U 2 (#7a21e3)";

    #[test]
    fn map_test_input() {
        let map = map_instructions(TEST_DATA_1);

        assert_eq!(14, map.len());
        assert_eq!((1, 0), map[0].direction);
        assert_eq!(6, map[0].steps);
        assert_eq!((0, 1), map[1].direction);
        assert_eq!(5, map[1].steps);
        assert_eq!((-1, 0), map[2].direction);
        assert_eq!(2, map[2].steps);
        assert_eq!((0, -1), map[13].direction);
        assert_eq!(2, map[13].steps);
    }

    #[test]
    fn solve_part_1_with_test_data() {
        let result = solve_part1(TEST_DATA_1);

        assert_eq!(62, result);
    }
}
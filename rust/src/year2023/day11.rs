use std::collections::HashSet;

#[derive(PartialEq, Debug)]
struct Galaxy {
    x: usize,
    y: usize,
}

impl Galaxy {
    fn distance(
        &self,
        other: &Galaxy,
        verticals: &HashSet<usize>,
        horizontals: &HashSet<usize>,
        age_bonus: usize,
    ) -> usize {
        let mut result = self.x.abs_diff(other.x) + self.y.abs_diff(other.y);
        for i in self.x.min(other.x) + 1..self.x.max(other.x) {
            if !verticals.contains(&i) {
                result += age_bonus;
            }
        }
        for i in self.y.min(other.y) + 1..self.y.max(other.y) {
            if !horizontals.contains(&i) {
                result += age_bonus;
            }
        }
        result
    }
}

fn get_galaxies(input: &str) -> (Vec<Galaxy>, HashSet<usize>, HashSet<usize>) {
    let mut verticals = HashSet::new();
    let mut horizontals = HashSet::new();
    let mut galaxies = vec![];
    for (i, line) in input.lines().enumerate() {
        let line = line.trim();
        for (j, ch) in line.chars().enumerate() {
            if ch == '#' {
                verticals.insert(j);
                horizontals.insert(i);
                galaxies.push(Galaxy { x: j, y: i });
            }
        }
    }
    (galaxies, verticals, horizontals)
}

fn process(input: &str, age_bonus: usize) -> usize {
    let mut result = 0;
    let (galaxies, verticals, horizontals) = get_galaxies(input);
    for i in 0..galaxies.len() {
        for j in i + 1..galaxies.len() {
            result += galaxies[i].distance(&galaxies[j], &verticals, &horizontals, age_bonus);
        }
    }
    result
}

pub fn solve_part1(input: &str) -> usize {
    process(input, 1)
}

pub fn solve_part2(input: &str) -> usize {
    process(input, 1000000 - 1)
}

#[cfg(test)]
mod tests {
    use itertools::PadUsing;
    use super::*;

    const TEST_DATA_1: &str = "...#......
.......#..
#.........
..........
......#...
.#........
.........#
..........
.......#..
#...#.....";

    #[test]
    fn get_galaxies_from_test_input() {
        let (galaxies, verticals, horizontals) = get_galaxies(TEST_DATA_1);

        assert_eq!(9, galaxies.len());

        assert_eq!(galaxies[0], Galaxy { x: 3, y: 0});
        assert_eq!(galaxies[8], Galaxy { x: 4, y: 9});

        assert_eq!(7, verticals.len());
        assert_eq!(8, horizontals.len());
    }

    #[test]
    fn part_one_with_test_data() {
        let result = solve_part1(TEST_DATA_1);

        assert_eq!(374, result)
    }

    #[test]
    fn part_two_with_smaller_examples()
    {
        assert_eq!(1030, process(TEST_DATA_1, 9));
        assert_eq!(8410, process(TEST_DATA_1, 99));
    }

    #[test]
    fn part_two_with_test_data() {
        let result = solve_part2(TEST_DATA_1);

        assert_eq!(82000210, result);
    }
}
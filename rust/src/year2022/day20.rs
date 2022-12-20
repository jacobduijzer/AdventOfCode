fn mix_and_decrypt(coordinates: Vec<i64>, mix: usize) -> i64 {
    let mut to_sort = (0..coordinates.len()).collect::<Vec<_>>();
    for _ in 0..mix {
        let mut i = 0;
        while i < to_sort.len() {
            let sorted_index = to_sort[i];
            let item = coordinates[i];
            let new_position = (sorted_index as i64 + item).rem_euclid(coordinates.len() as i64 - 1);
            for sortable in to_sort.iter_mut() {
                if *sortable > sorted_index {
                    *sortable -= 1;
                }
                if *sortable >= new_position as usize {
                    *sortable += 1;
                }
            }
            to_sort[i] = new_position as usize;
            i += 1;
        }
    }

    let ordered_zero_position = to_sort[coordinates.iter().position(|&x| x == 0).unwrap()];
    let first_index = (ordered_zero_position + 1000).rem_euclid(coordinates.len());
    let second_index = (ordered_zero_position + 2000).rem_euclid(coordinates.len());
    let third_index = (ordered_zero_position + 3000).rem_euclid(coordinates.len());

    let first = coordinates[to_sort.iter().position(|&x| x == first_index).unwrap()];
    let second = coordinates[to_sort.iter().position(|&x| x == second_index).unwrap()];
    let third = coordinates[to_sort.iter().position(|&x| x == third_index).unwrap()];

    (first + second + third)
}

pub fn solve_part1(input: &str) -> i64 {

    let coordinates = input.lines()
        .map(|line| line.parse::<i64>().unwrap())
        .collect::<Vec<i64>>();

    mix_and_decrypt(coordinates, 1)
}

pub fn solve_part2(input: &str) -> i64 {

    let coordinates = input.lines()
        .map(|line| line.parse::<i64>().unwrap() * 811589153)
        .collect::<Vec<i64>>();

    mix_and_decrypt(coordinates, 10)
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = crate::common::input::read_file(2022, 20, "testinput");
        assert_eq!(solve_part1(&input), 3);
    }

    #[test]
    fn part_one() {
        let input = crate::common::input::read_file(2022, 20, "input");
        assert_eq!(solve_part1(&input), 9866);
    }

    #[test]
    fn test_part_two() {
        let input = crate::common::input::read_file(2022, 20, "testinput");
        assert_eq!(solve_part2(&input), 1623178306);
    }

    #[test]
    fn part_two() {
        let input = crate::common::input::read_file(2022, 20, "input");
        assert_eq!(solve_part2(&input), 12374299815791);
    }
}
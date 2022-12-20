use std::borrow::Borrow;

const WIDTH: usize = 7;

pub fn solve_part1(input: &str, mut amount: usize) -> usize {
    let mut directions = input.trim().chars().cycle();
    let blocks = [
        vec![(0, 0), (0, 1), (0, 2), (0, 3)],
        vec![(0, 1), (1, 0), (1, 1), (1, 2), (2, 1)],
        vec![(0, 0), (0, 1), (0, 2), (1, 2), (2, 2)],
        vec![(0, 0), (1, 0), (2, 0), (3, 0)],
        vec![(0, 0), (0, 1), (1, 0), (1, 1)],
    ];

    let mut map = vec![[false; WIDTH]; 0];
    let mut resting = 0;
    let mut block = 0;
    let mut shifting = ((3isize, 2isize));
    while resting < amount {
        let direction = directions.next();
        let new_x = match direction {
            Some('<') => shifting.1 - 1,
            _ => shifting.1 + 1
        };

        if new_x >= 0 && blocks[block].iter().all(|&(y, x)| {
            let (abs_y, abs_x) = (shifting.0 as usize + y, new_x as usize + x);
            abs_x < WIDTH && (abs_y >= map.len() || !map[abs_y][abs_x])
        }) {
            shifting.1 = new_x;
        }

        let new_y = shifting.0 - 1;
        if new_y < 0 || blocks[block].iter().any(|&(y, x)| {
            let (abs_y, abs_x) = (new_y as usize + y, shifting.1 as usize + x);
            abs_y < map.len() && map[abs_y][abs_x]
        }) {
            for &(y, x) in &blocks[block] {
                let (abs_y, abs_x) = (shifting.0 as usize + y, shifting.1 as usize + x);
                while abs_y >= map.len() {
                    map.push([false; WIDTH]);
                }
                map[abs_y][abs_x] = true;
            }

            shifting = (map.len() as isize + 3, 2);
            block = (block + 1) % blocks.len();
            resting += 1;
        } else {
            shifting.0 = new_y;
        }
    }
    map.len()
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = crate::common::input::read_file(2022, 17, "testinput");
        assert_eq!(solve_part1(&input, 2022), 3068);
    }

    #[test]
    fn part_one() {
        let input = crate::common::input::read_file(2022, 17, "input");
        assert_eq!(solve_part1(&input, 2022), 3055);
    }

    //#[test]
    //fn test_part_two() {
    //    let input = crate::common::input::read_file(2022, 17, "testinput");
    //    //assert_eq!(solve_part1(&input, 1000000000000), 10);
    //}

    //#[test]
    //fn part_two() {
    //    //let input = crate::common::input::read_file(2022, 17, "input");
    //    //assert_eq!(solve_part1(&input, 1000000000000), 13615843289729);
    //}
}

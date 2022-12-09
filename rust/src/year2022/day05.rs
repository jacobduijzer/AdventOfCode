use std::str::FromStr;

struct Rearrangement {
    number_of_items: usize,
    start_position: usize,
    target_position: usize
}

fn parse_ship(input: &str) -> Vec<Vec<char>> {
    let num_stacks = input.lines().last().unwrap().split_whitespace().collect::<Vec<_>>().len();
    //let mut stacks: Vec<_> = (0 .. num_stacks).map(|_| Vec::<char>::new()).collect();
    let mut stacks = vec![vec![]; num_stacks];
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
    println!("Starting");
    let (mut ship, arrangements) = parse(input);
    println!("Data parsed");
    let mut counter: u32 = 0;
    let mut moved_crates = vec![];
    arrangements
        .iter()
        .for_each(|arr| {
            println!("({}/{}) move {} from {} to {}", counter, arrangements.len(), arr.number_of_items, arr.start_position, arr.target_position);
            let new_len = ship[arr.start_position - 1].len() - arr.number_of_items;
            moved_crates = ship[arr.start_position - 1]
                .drain(new_len..)
                .rev()
                .collect();
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
            println!("({}/{}) move {} from {} to {}", counter, arrangements.len(), arr.number_of_items, arr.start_position, arr.target_position);
            let new_len = ship[arr.start_position - 1].len() - arr.number_of_items;
            moved_crates = ship[arr.start_position - 1].drain(new_len..).collect();
            ship[arr.target_position - 1].append(&mut moved_crates);
            counter += 1;
        });
    let result = ship
        .iter()
        .map(|line| line.last().unwrap())
        .collect::<String>();

    result
}

//pub fn faster(input: &str) {
//    let (mut ship, arrangements) = parse(input);
//    unimplemented!()
//    //  If it's on the SRC, increase offset
////  If it's on the DST, descrease offset
////    If the offset becomes negative, move to SRC: [src, offset + count]
//    //let stepFast (stacks: (int * int) array) move =
//    //let updatePos (curStack, curOffset) =
//    //    if curStack = move.Src then (curStack, curOffset + move.Count)
//    //elif curStack = move.Dst then
//    //let newOffset = curOffset - move.Count
//    //if newOffset >= 0 then (curStack, newOffset)
//    //else (move.Src, newOffset + move.Count)
//    //else (curStack, curOffset)
//    //for i = 0 to (Array.length stacks) - 1 do
//    //Array.set stacks i (updatePos stacks[i])
//    //stacks
//}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = crate::common::input::read_file(2022, 5, "testinput");
        assert_eq!(solve_part1(&input), "CMZ");
    }

    #[test]
    fn test_part_two() {
        let input = crate::common::input::read_file(2022, 5, "testinput");
        assert_eq!(solve_part2(&input), "MCD");
    }
}

fn get_hash(word: &str) -> u32 {
    let mut current_value = 0;

    for letter in word.chars() {
        current_value += letter as u32;
        current_value *= 17;
        current_value %= 256;
    }

    return current_value;
}

pub fn solve_part1(input: &str) -> u32 {
    let mut result: u32 = 0;
    let trimmed = input.trim_end_matches('\n');
    trimmed.split(',').for_each(|s| {
        result += get_hash(s);
    });
    result
}

pub fn solve_part2(input: &str) -> u32 {
    let mut result: u32 = 0;
    let mut boxes: Vec<Vec<(&str, u32)>> = vec![];
    for _ in 0..256 {
        boxes.push(vec![]);
    }
    let trimmed = input.trim_end_matches('\n');
    trimmed.split(',')
        .for_each(|s| {
            if s.contains('=') {
                if let Some((label, focal_str)) = s.split_once('=') {
                    let focal: u32 = focal_str.parse::<u32>().unwrap();
                    let hash = get_hash(label) as usize;
                    if boxes[hash].iter().any(|entry| entry.0 == label) {
                        let pos = boxes[hash]
                            .iter()
                            .position(|entry| entry.0 == label)
                            .unwrap();
                        let elem = boxes[hash].get_mut(pos).unwrap();
                        *elem = (label, focal);
                    } else {
                        boxes[hash].push((label, focal));
                    }
                }
            } else if s.contains('-') {
                let label = &s[..s.len() - 1];
                let hash = get_hash(label) as usize;
                if boxes[hash].iter().any(|entry| entry.0 == label) {
                    let pos = boxes[hash]
                        .iter()
                        .position(|entry| entry.0 == label)
                        .unwrap();
                    boxes[hash].remove(pos);
                }
            }
        });

    for box_num in 0..256 {
        for slot_num in 0..boxes[box_num].len() {
            result += (box_num as u32 + 1) * (slot_num as u32 + 1) * boxes[box_num][slot_num].1;
        }
    }
    result
}

#[cfg(test)]
mod tests {
    use rstest::*;
    use super::*;
    use crate::common::point::Point;

    const TEST_DATA_1: &str = "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7";

    #[test]
    fn test_algorithm_on_single_word() {
        let decoded = get_hash("HASH");

        assert_eq!(52, decoded);
    }

    #[rstest]
    #[case("HASH",  52)]
    #[case("rn=1",  30)]
    #[case("cm-",   253)]
    #[case("qp=3",  97)]
    #[case("cm=2",  47)]
    #[case("qp-",   14)]
    #[case("pc=4",  180)]
    #[case("ot=9",  9)]
    #[case("ab=5",  197)]
    #[case("pc-",   48)]
    #[case("pc=6",  214)]
    #[case("ot=7",  231)]
    fn test_single_hashes(#[case] input: &str, #[case] expected: u32) {
        let result = get_hash(input);

        assert_eq!(expected, result)
    }

    #[test]
    fn solve_part_1_with_test_data() {
        let result = solve_part1(TEST_DATA_1);

        assert_eq!(1320, result);
    }

    #[test]
    fn solve_part_2_with_test_data() {
        let result = solve_part2(TEST_DATA_1);

        assert_eq!(145, result);
    }
}
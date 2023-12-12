use itertools::Itertools;

fn parse_spring_arrangements(input: &str) -> Vec<(&str, Vec<usize>)> {
    input
        .lines()
        .map(|line| {
            let mut parts = line.split_whitespace();
            let pattern = parts.next().unwrap();
            let counts = parts
                .next()
                .unwrap()
                .split(',')
                .map(|x| x.parse::<usize>().unwrap())
                .collect();
            (pattern, counts)
        })
        .collect()
}

fn count_arrangements(line: &str, counts: &[usize]) -> usize {
    let line = line.as_bytes();
    let line_length = line.len();
    let count_length = counts.len();
    let mut arrangement_counts = vec![vec![vec![0; line_length + 1]; count_length + 1]; line_length + 1];

    arrangement_counts[line_length][count_length][0] = 1;
    arrangement_counts[line_length][count_length - 1][counts[count_length - 1]] = 1;

    for pos in (0..line_length).rev() {
        for (group, &max_count) in counts.iter().enumerate() {
            for count in 0..=max_count {
                for &c in &[b'.', b'#'] {
                    if line[pos] == c || line[pos] == b'?' {
                        if c == b'.' && count == 0 {
                            arrangement_counts[pos][group][count] += arrangement_counts[pos + 1][group][0];
                        } else if c == b'.' && group < count_length && counts[group] == count {
                            arrangement_counts[pos][group][count] += arrangement_counts[pos + 1][group + 1][0];
                        } else if c == b'#' {
                            arrangement_counts[pos][group][count] += arrangement_counts[pos + 1][group][count + 1];
                        }
                    }
                }
            }

        }
        if matches!(line[pos], b'.' | b'?') {
            arrangement_counts[pos][count_length][0] += arrangement_counts[pos + 1][count_length][0];
        }

    }

    arrangement_counts[0][0][0]
}

pub fn solve_part1(input: &str) -> usize {
    let data = parse_spring_arrangements(input);

    data
        .iter()
        .map(|(pattern, counts)| count_arrangements(pattern, counts))
        .sum::<usize>()
}

pub fn solve_part2(input: &str) -> usize {
    let data = parse_spring_arrangements(input);

    data
        .iter()
        .map(|(pattern, counts)| {
            let pattern = [*pattern; 5].join("?");
            let counts = counts.repeat(5);
            count_arrangements(&pattern, &counts)
        })
        .sum::<usize>()
}

#[cfg(test)]
mod tests {
    use rstest::*;
    use super::*;

    const TEST_DATA_1: &str = "???.### 1,1,3
.??..??...?##. 1,1,3
?#?#?#?#?#?#?#? 1,3,1,6
????.#...#... 4,1,1
????.######..#####. 1,6,5
?###???????? 3,2,1";

    #[test]
    fn parse_input_with_test_data_1() {
        let spring_arrangements = parse_spring_arrangements(TEST_DATA_1);

        assert_eq!("???.###", spring_arrangements.first().unwrap().0);
        assert_eq!(vec![1, 1, 3], spring_arrangements.first().unwrap().1);

        assert_eq!("?###????????", spring_arrangements.last().unwrap().0);
        assert_eq!(vec![3, 2, 1], spring_arrangements.last().unwrap().1);
    }

    #[rstest]
    #[case("???.###", &[1,1,3], 1)]
    #[case(".??..??...?##.", &[1,1,3], 4)]
    #[case("?###????????", &[3, 2, 1], 10)]
    #[case("???.###????.###????.###????.###????.###", &[1,1,3,1,1,3,1,1,3,1,1,3,1,1,3], 1)]
    #[case("?###??????????###??????????###??????????###??????????###????????", &[3, 2, 1, 3, 2, 1, 3, 2, 1, 3, 2, 1, 3, 2, 1], 506250)]
    fn count_arrangement_for_single_line(#[case] arrangement: &str, #[case] counts: &[usize], #[case] expected: usize) {
        let result = count_arrangements(arrangement, counts);

        assert_eq!(expected, result)
    }

    #[test]
    fn solve_part1_with_test_data_1() {
        let result = solve_part1(TEST_DATA_1);

        assert_eq!(result, 21);
    }

    #[test]
    fn solve_part2_with_test_data_1() {
        let result = solve_part2(TEST_DATA_1);

        assert_eq!(result, 525152);
    }

}
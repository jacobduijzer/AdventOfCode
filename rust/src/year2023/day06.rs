fn get_race_results(input: &str) -> Vec<(u128, u128)> {
    let (time, dist) = input.split_once('\n').unwrap();
    parse_vec(time).zip(parse_vec(dist)).collect()
}

fn parse_vec<'s>(line: &'s str) -> impl Iterator<Item=u128> + 's
{
    line.split_whitespace()
        .skip(1)
        .filter_map(|s| s.parse().ok())
}

fn calculate_wins(races: Vec<(u128, u128)>) -> u128 {
    let mut numbers : Vec<u128> = vec![];
    for race in races.iter() {
        let mut count = 0;
        for i in 1..race.0 {
            if i * (race.0 - i) > race.1 {
                count = count + 1;
            }
        }
        numbers.push(count);
    }

    numbers.iter().product()
}

pub fn solve_part1(input: &str) -> u128 {
    let race_results = get_race_results(input);
    calculate_wins(race_results)
}

#[cfg(test)]
mod tests {
    use super::*;

    const TEST_DATA: &str = "Time:      7  15   30
Distance:  9  40  200";

    const TEST_DATA2: &str = "Time:      71530
Distance:  940200";

    const REAL_DATA: &str = "Time:        44     70     70     80
Distance:   283   1134   1134   1491";

    const REAL_DATA2: &str = "Time:        44707080
Distance:   283113411341491";

    #[test]
    fn parse_race_results_with_test_data() {
        let race_results = get_race_results(TEST_DATA);

        assert_eq!(7, race_results[0].0);
        assert_eq!(9, race_results[0].1);
        assert_eq!(15, race_results[1].0);
        assert_eq!(40, race_results[1].1);
    }

    #[test]
    fn parse_race_results_with_real_data() {
        let race_results = get_race_results(REAL_DATA);

        assert_eq!(44, race_results[0].0);
        assert_eq!(283, race_results[0].1);
        assert_eq!(80, race_results[3].0);
        assert_eq!(1491, race_results[3].1);
    }

    #[test]
    fn get_result_for_part1_with_test_data() {
        let result = solve_part1(TEST_DATA);

        assert_eq!(288, result)
    }

    #[test]
    fn get_result_for_part1_with_real_data() {
        let result = solve_part1(TEST_DATA2);

        assert_eq!(71503, result)
    }

    #[test]
    fn get_result_for_part2_with_test_data() {
        let result = solve_part1(TEST_DATA);

        assert_eq!(288, result)
    }

    #[test]
    fn get_result_for_part2_with_real_data() {
        let result = solve_part1(REAL_DATA2);

        assert_eq!(29432455, result)
    }
}

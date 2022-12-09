use std::str::FromStr;

struct SectionAssignment {
    lower1: usize,
    upper1: usize,
    lower2: usize,
    upper2: usize
}

fn parse_assignments(input: &str) -> Vec<SectionAssignment> {
     input
        .lines()
        .map(|line| {
            let (range1, range2) = line.split_once(',').expect("Each line should have a comma");
            let (range1_low, range1_high) = range1.split_once('-').expect("Each range should have a dash");
            let (range2_low, range2_high) = range2.split_once('-').expect("Each range should have a dash");
            return SectionAssignment {
                lower1: usize::from_str(range1_low).unwrap(),
                upper1: usize::from_str(range1_high).unwrap(),
                lower2: usize::from_str(range2_low).unwrap(),
                upper2: usize::from_str(range2_high).unwrap()
            }})
        .collect()
}

pub fn solve_part1(input: &str) -> u32 {
    let section_assignments = parse_assignments(input);
    let overlapping_pairs = section_assignments
        .into_iter()
        .fold(0, |total: u32, assignment: SectionAssignment| {
            if assignment.lower1 >= assignment.lower2 && assignment.upper1 <= assignment.upper2 {
                return total + 1;
            }
            else if assignment.lower2 >= assignment.lower1 && assignment.upper2 <= assignment.upper1 {
                return total + 1;
            }
            total
        });
    overlapping_pairs
}

pub fn solve_part2(input: &str) -> u32 {
    let section_assignments = parse_assignments(input);
    let overlapping_pairs = section_assignments
        .into_iter()
        .fold(0, |total: u32, assignment: SectionAssignment| {
            if assignment.lower1 <= assignment.lower2 && assignment.upper1 >= assignment.lower2 {
                return total + 1;
            }
            else if assignment.lower2 <= assignment.lower1 && assignment.upper2 >= assignment.lower1 {
                return total + 1;
            }
            total
        });
    overlapping_pairs
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = crate::common::input::read_file(2022, 4, "testinput");
        assert_eq!(solve_part1(&input), 2);
    }

    #[test]
    fn test_part_two() {
        let input = crate::common::input::read_file(2022, 4, "testinput");
        assert_eq!(solve_part2(&input), 4);
    }
}
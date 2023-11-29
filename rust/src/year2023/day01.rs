pub fn solve_part1(input: &str) -> &str {
    return "Hello, World!";
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = crate::common::input::read_file(2023, 1, "testinput");
        assert_eq!(solve_part1(&input), "Hello, World!");
    }
}
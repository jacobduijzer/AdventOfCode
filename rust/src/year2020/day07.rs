use std::collections::HashMap;
use regex::Regex;

fn generate_map(input: &str) -> HashMap<String, Vec<(usize, String)>> {
    let mut map = HashMap::new();
    let re = Regex::new(r"(?P<number>[0-9]+) (?P<color>.+?) bag").unwrap();
    for line in input.lines() {
        let mut iter = line.split(" bags contain ");
        let color = iter.next().unwrap().to_string();
        let mut contains: Vec<(usize, String)> = Vec::new();
        for captures in re.captures_iter(line) {
            contains.push(((&captures["number"]).parse().unwrap(), (&captures["color"]).to_string()));
        }
        map.insert(color, contains);
    }
    map
}

fn can_hold(map: &HashMap<String, Vec<(usize, String)>>, hold_map: &mut HashMap<String, bool>, current_color: &str) -> bool {
    if let Some(&can_hold) = hold_map.get(current_color) {
        return can_hold;
    };
    let vec = map.get(current_color).unwrap();
    for (_, color) in vec.iter() {
        if color == "shiny gold" || can_hold(map, hold_map, color) {
            hold_map.insert(current_color.to_string(), true);
            return true;
        }
    }
    hold_map.insert(current_color.to_string(), false);
    return false;
}

pub fn solve_part1(input: &str) -> i32 {
    let mut count = 0;
    let mut hold_map: HashMap<String, bool> = HashMap::new();

    let map = generate_map(input);

    for (color, _) in map.iter() {
        if can_hold(&map, &mut hold_map, color) {
            count += 1;
        }
    }
    count
}

#[cfg(test)]
mod tests {
    use super::*;

    const TEST_DATA: &str = "light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.";

    #[test]
    fn map_test_input() {
        let map = generate_map(TEST_DATA);

        assert_eq!(9, map.len());
    }

    #[test]
    fn solve_part1_with_test_data() {
        let result = solve_part1(TEST_DATA);

        assert_eq!(4, result);
    }
}
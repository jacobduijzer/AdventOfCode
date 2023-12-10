use std::collections::HashMap;
use regex::Regex;
use num::integer::lcm;

struct Map<'a> {
    instructions: Vec<char>,
    nodes: HashMap<&'a str, (&'a str, &'a str)>
}

fn get_instructions(line: &str) -> Vec<char> {
    line
        .chars()
        .collect::<Vec<char>>()
}

fn get_node(line: &str) -> (&str, &str, &str) {
    let pattern = Regex::new(r"(\w+) = \((\w+), (\w+)\)").unwrap();
    if let Some(captures) = pattern.captures(line) {
        let key = captures.get(1).map_or("", |m| m.as_str());
        let left = captures.get(2).map_or("", |m| m.as_str());
        let right = captures.get(3).map_or("", |m| m.as_str());

        return (key, left, right)
    }

    panic!("Error: {}", "Cannot match line")
}

fn get_map(input: &str) -> Map {
    let instructions = get_instructions(input.lines().next().unwrap());
    let mut data_map: HashMap<&str, (&str, &str)> = HashMap::new();
    input
        .lines()
        .skip(2)
        .for_each(|line| {
            let (index, left, right) = get_node(line);
            data_map.insert(index, (left, right));
        });

    Map { instructions, nodes: data_map }
}

pub fn solve_part1(input: &str) -> usize {
    let map = get_map(input);

    let mut curr_node = "AAA";
    let mut curr_steps = 0;

    loop {
        if curr_node == "ZZZ" {
            break;
        }

        let side = map.instructions[curr_steps % map.instructions.len()];
        curr_node = if side == 'L' {
            map.nodes.get(curr_node).map_or_else(|| "", |tuple| tuple.0)
        } else {
            map.nodes.get(curr_node).map_or_else(|| "", |tuple| tuple.1)
        };

        curr_steps += 1;
    }

    curr_steps
}

pub fn solve_part2(input: &str) -> usize {
    let map = get_map(input);

    let start_nodes: Vec<_> = map.nodes.keys().filter(|k| k.ends_with('A')).collect();

    let lcm = start_nodes
        .iter()
        .map(|&node| {
            let mut curr_node = node;
            let mut curr_steps = 0;

            loop {
                if curr_node.ends_with('Z') {
                    break;
                }

                let side = map.instructions[curr_steps % map.instructions.len()];
                curr_node = if side == 'L' {
                    &map.nodes.get(curr_node).unwrap().0
                } else {
                    &map.nodes.get(curr_node).unwrap().1
                };

                curr_steps += 1;
            }

            curr_steps as u64
        })
        .fold(1, lcm);

    lcm.try_into().unwrap()
}



#[cfg(test)]
mod tests {
    use super::*;

    const TEST_DATA_1: &str = "RL

AAA = (BBB, CCC)
BBB = (DDD, EEE)
CCC = (ZZZ, GGG)
DDD = (DDD, DDD)
EEE = (EEE, EEE)
GGG = (GGG, GGG)
ZZZ = (ZZZ, ZZZ)";

    const TEST_DATA_2: &str = "LLR

AAA = (BBB, BBB)
BBB = (AAA, ZZZ)
ZZZ = (ZZZ, ZZZ)";

    const TEST_DATA_3: &str = "LRLRLLRLLRRRLRLLRRLRLRRLRRLLLLRRLLRLRRLRRLRLRLRRLRLLRLRLRLRRRLLRLRLRLRRLRRLRRRLRRLRRLRRLRRLRRRLRRLRLLRLLRRRLRLRLLRRRLLRRLLLLRLRRRLLRLRLRRLRRRLRLRRLRLRRLLRLRRLLRLLRRLRLLRLLRRLRRRLLRRLRLRLRRLRRLRRRLRRLRRRLLRRLRLRRRLRRRLRLRRRLRRLRRLRRLRRRLRRLRLRRRLRLRRLLRRLRRRLRLRRRLLRLRRRLRRRLRLRLRRRLLRRLLRLRRRLRRLRRRLLLRRRR

FTX = (VVM, VVM)
LNR = (DQG, CMF)
NXS = (TKM, FPB)
FQF = (HDC, NFB)
SPH = (MQB, XFB)
FDL = (CTR, NXS)
DMF = (VHG, LJV)
JBP = (CKR, VBF)
MMK = (JVC, NSS)
LLT = (MVP, QFC)
VVN = (SHF, XMN)";

    const TEST_DATA_4: &str = "LR

11A = (11B, XXX)
11B = (XXX, 11Z)
11Z = (11B, XXX)
22A = (22B, XXX)
22B = (22C, 22C)
22C = (22Z, 22Z)
22Z = (22B, 22B)
XXX = (XXX, XXX)";

    #[test]
    fn parse_hands_with_test_data_1() {
        let map = get_map(TEST_DATA_1);

        assert_eq!(2, map.instructions.len());
    }

    #[test]
    fn parse_hands_with_test_data_2() {
        let map = get_map(TEST_DATA_2);

        assert_eq!(3, map.instructions.len());
    }

    #[test]
    fn parse_hands_with_test_data_3() {
        let map = get_map(TEST_DATA_3);

        assert_eq!(307, map.instructions.len());
    }

    #[test]
    fn get_node_with_single_line() {
        let (first, second, third) = get_node("AAA = (BBB, CCC)");

        assert_eq!("AAA", first);
        assert_eq!("BBB", second);
        assert_eq!("CCC", third);
    }

    #[test]
    fn get_map_with_test_data_1() {
        let map = get_map(TEST_DATA_1);

        assert_eq!(2, map.instructions.len());
        assert_eq!(7, map.nodes.len());
        assert_eq!("BBB", map.nodes.get("AAA").map_or_else(|| "", |tuple| tuple.0));
        assert_eq!("CCC", map.nodes.get("AAA").map_or_else(|| "", |tuple| tuple.1))
    }

    #[test]
    fn get_map_with_test_data_2() {
        let map = get_map(TEST_DATA_2);

        assert_eq!(3, map.instructions.len());
        assert_eq!(3, map.nodes.len());
    }

    #[test]
    fn solve_part_1_with_test_data_1() {
        let result = solve_part1(TEST_DATA_1);

        assert_eq!(2, result);
    }

    #[test]
    fn solve_part_1_with_test_data_2() {
        let result = solve_part1(TEST_DATA_2);

        assert_eq!(6, result);
    }

    #[test]
    fn solve_part_2_with_test_data_4() {
        let result = solve_part2(TEST_DATA_4);

        assert_eq!(6, result);
    }
}
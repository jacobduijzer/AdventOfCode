use regex::Regex;

#[derive(Debug, Clone)]
struct Workflow {
    name: String,
    rules: Vec<Rule>,
}

#[derive(Debug, Clone)]
enum Rule {
    Evaluation(Condition, Destination),
    Fallthrough(Destination),
}

#[derive(Debug, Copy, Clone)]
enum Condition {
    LessThan(char, isize),
    GreaterThan(char, isize),
    LessThanEqual(char, isize),
    GreaterThanEqual(char, isize),
}

#[derive(Debug, Clone, PartialEq, Eq)]
enum Destination {
    Accept,
    Reject,
    Workflow(String),
}

#[derive(Debug, Clone, PartialEq)]
struct Part {
    x: isize,
    m: isize,
    a: isize,
    s: isize,
}

fn parse_coordinate_line(coord_line: &str) -> Part {
   let re = Regex::new(r"\{x=(\d+),m=(\d+),a=(\d+),s=(\d+)\}").unwrap();

    let Some(numbers) = re.captures(coord_line) else {
        println!("no match!");
        panic!();
    };

    Part {
        x: numbers.get(1).unwrap().as_str().parse().unwrap(),
        m: numbers.get(2).unwrap().as_str().parse().unwrap(),
        a: numbers.get(3).unwrap().as_str().parse().unwrap(),
        s: numbers.get(4).unwrap().as_str().parse().unwrap()
    }
}

// fn parse_coordinates(input: &str) -> Vec<Part> {
//     let (rules, inputs) = input
//         .split_once("\n\n")
//         .unwrap();
//     // inputs
//     //     .into_iter()
//     //     .map(|line| parse_coordinate_line(line))
//     //     .collect()
// }



#[cfg(test)]
mod tests {
    use super::*;

    const TEST_DATA_1: &str = "px{a<2006:qkq,m>2090:A,rfg}
pv{a>1716:R,A}
lnx{m>1548:A,A}
rfg{s<537:gd,x>2440:R,A}
qs{s>3448:A,lnx}
qkq{x<1416:A,crn}
crn{x>2662:A,R}
in{s<1351:px,qqz}
qqz{s>2770:qs,m<1801:hdj,R}
gd{a>3333:R,R}
hdj{m>838:A,pv}

{x=787,m=2655,a=1222,s=2876}
{x=1679,m=44,a=2067,s=496}
{x=2036,m=264,a=79,s=2244}
{x=2461,m=1339,a=466,s=291}
{x=2127,m=1623,a=2188,s=1013}";

    #[test]
    fn parse_single_coordinate_line() {
        let parsed = parse_coordinate_line("{x=787,m=2655,a=1222,s=2876}");

        assert_eq!(Part { x: 787, m: 2655, a: 1222, s: 2876 }, parsed);
    }

    // #[test]
    // fn parse_coord_info() {
    //     let coord_info = parse_coordinates(TEST_DATA_1);
    //
    //     assert_eq(5, coord_info.len());
    // }
}
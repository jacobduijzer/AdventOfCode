use std::cmp::Ordering;

#[derive(Debug, PartialEq, Eq, Clone)]
enum Packet {
    Number(usize),
    List(Vec<Packet>),
}

impl Packet {
    fn new(input: &str) -> Self {
        if input.starts_with('[') {
            let mut values = Vec::new();
            let input = input.strip_prefix('[').unwrap().strip_suffix(']').unwrap();
            let mut buf = String::new();
            let mut level = 0;
            for c in input.chars() {
                match c {
                    '[' => {
                        level += 1;
                        buf.push('[');
                    }
                    ']' => {
                        level -= 1;
                        buf.push(']');
                    }
                    ',' => {
                        if level == 0 {
                            values.push(Packet::new(&buf));
                            buf.clear();
                        } else {
                            buf.push(',');
                        }
                    }
                    c => buf.push(c),
                }
            }
            if !buf.is_empty() {
                values.push(Packet::new(&buf));
            }
            Self::List(values)
        } else {
            // single
            Self::Number(input.parse().unwrap())
        }
    }

    fn extract_list(int: usize) -> Self {
        Self::List(vec![Self::Number(int)])
    }
}

impl PartialOrd for Packet {
    fn partial_cmp(&self, other: &Self) -> Option<Ordering> {
        Some(self.cmp(other))
    }
}

impl Ord for Packet {
    fn cmp(&self, other: &Self) -> Ordering {
        match (self, other) {
            (Packet::Number(left), Packet::Number(right)) => left.cmp(right),
            (Packet::Number(left), right) => Packet::extract_list(*left).cmp(right),
            (left, Packet::Number(right)) => left.cmp(&Packet::extract_list(*right)),
            (Packet::List(left), Packet::List(right)) => {
                for i in 0..left.len().max(right.len()) {
                    match (left.get(i), right.get(i)) {
                        (Some(left), Some(right)) => match left.cmp(right) {
                            Ordering::Less => return Ordering::Less, // in order
                            Ordering::Equal => (),
                            Ordering::Greater => return Ordering::Greater, // not in order
                        },
                        (Some(_), None) => return Ordering::Greater, // right ran out first -> not in order
                        (None, Some(_)) => return Ordering::Less, // left ran out first -> in order
                        (None, None) => unreachable!(),
                    }
                }
                Ordering::Equal
            }
        }
    }
}

pub fn solve_part1(input: &str) -> usize {
    input
        .split("\n\n")
        .enumerate()
        .map(|(index, pair)| {
            let (left, right) = pair.split_once('\n').unwrap();
            let left = Packet::new(left);
            let right = Packet::new(right);
            if left.cmp(&right) == Ordering::Less {
                index + 1
            } else {
                0
            }
        })
        .sum::<usize>()
}

pub fn solve_part2(input: &str) -> usize {
    let one = Packet::new("[[2]]");
    let two = Packet::new("[[6]]");
    let mut values: Vec<Packet> = input
        .lines()
        .filter_map(|s| {
            if s.is_empty() {
                None
            } else {
                Some(Packet::new(s))
            }
        })
        .chain(vec![one.clone(), two.clone()])
        .collect();
    values.sort();
    values
        .into_iter()
        .enumerate()
        .filter(|(_, v)| v == &one || *v == two)
        .map(|(i, _)| i + 1)
        .product::<usize>()
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = crate::common::input::read_file(2022, 13, "testinput");
        assert_eq!(solve_part1(&input), 13);
    }

    #[test]
    fn test_part_two() {
        let input = crate::common::input::read_file(2022, 13, "testinput");
        assert_eq!(solve_part2(&input), 140);
    }
}
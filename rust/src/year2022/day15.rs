use regex::Regex;

struct Sensor {
    sensor: (isize, isize),
    closest_beacon: (isize, isize),
    distance_to_closest_beacon: isize,
}

fn manhattan_dist(a: &(isize, isize), b: &(isize, isize)) -> isize {
    (a.0 - b.0).abs() + (a.1 - b.1).abs()
}

fn tuning_frequency(sensor: (isize, isize)) -> isize {
    sensor.0 * 4_000_000 + sensor.1
}

fn parse_beacons(input: &str) -> Vec<Sensor>  {
    let regex = Regex::new(r"Sensor at x=(-?-?\d*\d+), y=(-?\d*\d+): closest beacon is at x=(-?\d*\d+), y=(-?\d*\d+)").unwrap();
    input
        .lines()
        .map(|line| {
            let matches = regex.captures(line).unwrap();
            let x1 = matches[1].parse::<isize>().unwrap();
            let y1 = matches[2].parse::<isize>().unwrap();
            let x2 = matches[3].parse::<isize>().unwrap();
            let y2 = matches[4].parse::<isize>().unwrap();
            Sensor {
                sensor: (x1, y1),
                closest_beacon: (x2, y2),
                distance_to_closest_beacon:  manhattan_dist(&(x1, y1), &(x2, y2))
            }
        }).collect()
}

fn part1(sensors: Vec<Sensor>, y: isize) -> usize {
    let min_x = sensors.iter().map(|s| s.sensor.0 - s.distance_to_closest_beacon).min().unwrap();
    let max_x = sensors.iter().map(|s| s.sensor.0 + s.distance_to_closest_beacon).max().unwrap();

    (min_x..=max_x)
        .filter(|x| {
            let cursor = (*x, y);
            sensors
                .iter()
                .any(|s| manhattan_dist(&cursor, &s.sensor) <= s.distance_to_closest_beacon) &&
                sensors.iter().all(|s| cursor != s.closest_beacon)
        })
        .count()
}

pub fn solve_part1(input: &str, y: isize) -> usize {
    let (took, sensors) = took::took(|| parse_beacons(input));
    println!("Time spent parsing sensors: {}", took);
    let (took, result) = took::took(|| part1(sensors, y ));
    println!("Time spent calculating result: {}", took);
    result
}

struct Action {
    stop_position: (isize, isize),
    action: (isize, isize)
}

fn part2(sensors: Vec<Sensor>, y: isize) -> isize {
    let range = 0..=y;
    for s in &sensors {
        let border_dist = s.distance_to_closest_beacon + 1;

        let actions: Vec<Action> = vec![
            Action { stop_position: (s.sensor.0 + border_dist, s.sensor.1), action: (1, -1)},
            Action { stop_position: (s.sensor.0, s.sensor.1 - border_dist), action: (-1, -1)},
            Action { stop_position: (s.sensor.0 - border_dist,  s.sensor.1), action: (-1, 1)},
            Action { stop_position: (s.sensor.0,  s.sensor.1 + border_dist), action: (1, 1) }];

        let mut cursor = actions.last().unwrap().stop_position;

        for action in actions.iter() {
            while cursor != action.stop_position {
                if range.contains(&cursor.0)
                    && range.contains(&cursor.1)
                    && !sensors.iter().any(|s| manhattan_dist(&cursor, &s.sensor) <= s.distance_to_closest_beacon)
                {
                    return tuning_frequency(cursor);
                }
                cursor.0 += action.action.0;
                cursor.1 += action.action.1;
            }
        }
    }

    unreachable!()
}

pub fn solve_part2(input: &str, y: isize) -> isize {
    let (took, sensors) = took::took(|| parse_beacons(input));
    println!("Time spent parsing sensors: {}", took);
    let (took, result) = took::took(|| part2(sensors, y));
    println!("Time spent calculating result: {}", took);
    result
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_parse_beacons() {
        let input = crate::common::input::read_file(2022, 15, "testinput");
        let beacons = parse_beacons(&input);
        assert_eq!(beacons.len(), 14);
        assert_eq!(beacons[0].sensor.0, 2);
        assert_eq!(beacons[0].sensor.1, 18);
        assert_eq!(beacons[0].closest_beacon.0, -2);
        assert_eq!(beacons[0].closest_beacon.1, 15);
        assert_eq!(beacons.last().unwrap().sensor.0, 20);
        assert_eq!(beacons.last().unwrap().sensor.1, 1);
        assert_eq!(beacons.last().unwrap().closest_beacon.0, 15);
        assert_eq!(beacons.last().unwrap().closest_beacon.1, 3);
    }

    #[test]
    fn test_part_one() {
        let input = crate::common::input::read_file(2022, 15, "testinput");
        assert_eq!(solve_part1(&input, 10), 26);
    }

    #[test]
    fn part_one() {
        let input = crate::common::input::read_file(2022, 15, "input");
        assert_eq!(solve_part1(&input, 2000000), 5299855);
    }

    #[test]
    fn test_part_two() {
        let input = crate::common::input::read_file(2022, 15, "testinput");
        assert_eq!(solve_part2(&input, 20), 56000011);
    }

    #[test]
    fn part_two() {
        let input = crate::common::input::read_file(2022, 15, "input");
        assert_eq!(solve_part2(&input, 4000000), 13615843289729);
    }
}
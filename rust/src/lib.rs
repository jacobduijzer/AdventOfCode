mod common;
mod year2021;
mod year2022;

pub fn solve(
    year: u16,
    day: u8,
    part: u8) {

    let input = common::input::read_file(year, day, "input");

    match (year, day, part) {
        // 2021
        (2021, 1, 1) => println!("Part 1: {}", year2021::day01::solve_part1(&input)),
        (2021, 1, 2) => println!("Part 2: {}", year2021::day01::solve_part2(&input)),
        (2021, 2, 1) => println!("Part 1: {}", year2021::day02::solve_part1(&input)),
        (2021, 2, 2) => println!("Part 2: {}", year2021::day02::solve_part2(&input)),

        // 2022
        (2022, 1, 1) => println!("Part 1: {}", year2022::day01::solve_part1(&input)),
        (2022, 1, 2) => println!("Part 2: {}", year2022::day01::solve_part2_second(&input)),
        (2022, 2, 1) => println!("Part 1: {}", year2022::day02::solve_part1(&input)),
        (2022, 2, 2) => println!("Part 2: {}", year2022::day02::solve_part2(&input)),
        (2022, 5, 1) => println!("Part 1: {}", year2022::day05::solve_part1(&input)),
        (2022, 5, 2) => println!("Part 2: {}", year2022::day05::solve_part2(&input)),
        (2022, 6, 1) => println!("Part 1: {}", year2022::day06::solve_part1(&input)),
        (2022, 6, 2) => println!("Part 2: {}", year2022::day06::solve_part2(&input)),

        _ => println!("No match for year: {}, day: {}, part: {}", year, day, part)
    };
}

pub fn solve_raw(
    year: &str,
    day: &str,
    part: &str) {
    let year = year.parse::<u16>().unwrap();
    let day = day.parse::<u8>().unwrap();
    let part = part.parse::<u8>().unwrap();
    solve(year, day, part);
}

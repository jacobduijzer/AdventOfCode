extern crate core;

use stopwatch::{Stopwatch};
mod common;
mod year2021;
mod year2022;
mod year2023;

pub const ANSI_ITALIC: &str = "\x1b[3m";
pub const ANSI_BOLD: &str = "\x1b[1m";
pub const ANSI_RESET: &str = "\x1b[0m";

pub fn solve(year: u16, day: u8, part: u8, file: &str) {

    let sw = Stopwatch::start_new();
    let input = common::input::read_file_by_name(year, file, "input");
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
        (2022, 3, 1) => println!("Part 1: {}", year2022::day03::solve_part1(&input)),
        (2022, 3, 2) => println!("Part 2: {}", year2022::day03::solve_part2(&input)),
        (2022, 4, 1) => println!("Part 1: {}", year2022::day04::solve_part1(&input)),
        (2022, 4, 2) => println!("Part 2: {}", year2022::day04::solve_part2(&input)),
        (2022, 5, 1) => println!("Part 1: {}", year2022::day05::solve_part1(&input)),
        (2022, 5, 2) => println!("Part 2: {}", year2022::day05::solve_part2(&input)),
        (2022, 6, 1) => println!("Part 1: {}", year2022::day06::solve_part1(&input)),
        (2022, 6, 2) => println!("Part 2: {}", year2022::day06::solve_part2(&input)),
        (2022, 7, 1) => println!("Part 1: {}", year2022::day07::solve_part1(&input)),
        (2022, 7, 2) => println!("Part 2: {}", year2022::day07::solve_part2(&input)),
        (2022, 8, 1) => println!("Part 1: {}", year2022::day08::solve_part1(&input)),
        (2022, 8, 2) => println!("Part 2: {}", year2022::day08::solve_part2(&input)),
        (2022, 9, 1) => println!("Part 1: {}", year2022::day09::solve_part1(&input)),
        (2022, 9, 2) => println!("Part 2: {}", year2022::day09::solve_part2(&input)),
        (2022, 10, 1) => println!("Part 1: {}", year2022::day10::solve_part1(&input)),
        (2022, 10, 2) => println!("Part 2: {}", year2022::day10::solve_part2(&input)),
        (2022, 12, 1) => println!("Part 1: {}", year2022::day12::solve_part1(&input)),
        (2022, 13, 1) => println!("Part 1: {}", year2022::day13::solve_part1(&input)),
        (2022, 13, 2) => println!("Part 2: {}", year2022::day13::solve_part2(&input)),
        (2022, 14, 1) => println!("Part 1: {}", year2022::day14::solve_part1(&input)),
        (2022, 14, 2) => println!("Part 2: {}", year2022::day14::solve_part2(&input)),
        (2022, 15, 1) => println!("Part 1: {}", year2022::day15::solve_part1(&input, 2000000)),
        (2022, 15, 2) => println!("Part 2: {}", year2022::day15::solve_part2(&input, 4000000)),
        (2022, 17, 1) => println!("Part 1: {}", year2022::day17::solve_part1(&input, 2022)),
        (2022, 17, 2) => println!("Part 2: {}", year2022::day17::solve_part1(&input, 1000000000000)),
        (2022, 18, 1) => println!("Part 1: {}", year2022::day18::solve_part1(&input)),
        (2022, 18, 2) => println!("Part 2: {}", year2022::day18::solve_part2(&input)),
        (2022, 20, 1) => println!("Part 1: {}", year2022::day20::solve_part1(&input)),
        (2022, 20, 2) => println!("Part 2: {}", year2022::day20::solve_part2(&input)),

        // 2023
        (2023, 1, 1) => println!("Part 1: {}", year2023::day01::solve_part1(&input)),
        (2023, 1, 2) => println!("Part 2: {}", year2023::day01::solve_part2(&input)),
        (2023, 2, 1) => println!("Part 1: {}", year2023::day02::solve_part1(&input)),
        (2023, 2, 2) => println!("Part 2: {}", year2023::day02::solve_part2(&input)),
        (2023, 3, 1) => println!("Part 1: {}", year2023::day03::solve_part1(&input)),
        (2023, 3, 2) => println!("Part 2: {}", year2023::day03::solve_part2(&input)),
        (2023, 4, 1) => println!("Part 1: {}", year2023::day04::solve_part1(&input)),
        (2023, 4, 2) => println!("Part 2: {}", year2023::day04::solve_part2(&input)),
        (2023, 5, 1) => println!("Part 1: {}", year2023::day05::solve_part1(&input)),
        (2023, 5, 2) => println!("Part 2: {}", year2023::day05::solve_part2(&input)),
        (2023, 6, 1) => println!("Part 1: {}", year2023::day06::solve_part1(&input)),
        (2023, 7, 1) => println!("Part 1: {}", year2023::day07::solve_part1(&input)),
        (2023, 7, 2) => println!("Part 2: {}", year2023::day07::solve_part2(&input)),
        (2023, 8, 1) => println!("Part 1: {}", year2023::day08::solve_part1(&input)),
        (2023, 8, 2) => println!("Part 2: {}", year2023::day08::solve_part2(&input)),
        (2023, 9, 1) => println!("Part 1: {}", year2023::day09::solve_part1(&input)),
        (2023, 9, 2) => println!("Part 2: {}", year2023::day09::solve_part2(&input)),

        (2023, 11, 1) => println!("Part 1: {}", year2023::day11::solve_part1(&input)),
        (2023, 11, 2) => println!("Part 2: {}", year2023::day11::solve_part2(&input)),

        (2023, 12, 1) => println!("Part 1: {}", year2023::day12::solve_part1(&input)),
        (2023, 12, 2) => println!("Part 2: {}", year2023::day12::solve_part2(&input)),
        _ => println!("No match for year: {}, day: {}, part: {}", year, day, part)
    };

    println!(
        "{}Total:{} {}{:.2}ms{}",
        ANSI_BOLD, ANSI_RESET, ANSI_ITALIC, sw.elapsed_ms(), ANSI_RESET
    );
}

pub fn solve_raw(year: &str, day: &str, part: &str, file: &str) {
    let year = year.parse::<u16>().unwrap();
    let day = day.parse::<u8>().unwrap();
    let part = part.parse::<u8>().unwrap();
    solve(year, day, part, file);
}

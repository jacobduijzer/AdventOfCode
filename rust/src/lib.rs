mod common;
mod year2021;

pub fn solve(
    year: u16,
    day: u8,
    part: u8) {

    match (year, day, part) {
        (2021, 1, 1) => println!("Part 1: {}", year2021::day01::solve_part1("year2021/input/", 1)),
        (2021, 1, 2) => println!("Part 2: {}", year2021::day01::solve_part2("year2021/input/", 1)),
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

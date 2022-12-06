use std::env;

fn main() -> Result<(), String> {
    let usage = || -> ! {
        eprintln!("Arguments: year day part filename");
        eprintln!("    where day is 1-25 and part is 1-2");
        std::process::exit(1);
    };

    let args: Vec<String> = env::args().collect();

    if args.len() == 5 {
        let year = &args[1];
        let day = &args[2];
        let part = &args[3];
        let file = &args[4];

        eprintln!("Running part {} of day {} of year {} with file {}", part, day, year, file);
        aoc::solve_raw(year, day, part, file);
    } else {
        usage();
    }

    Ok(())
}

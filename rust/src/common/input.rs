use std::env;
use std::fs;

pub fn read_file(year: u16, day: u8, folder: &str) -> String {
    let cwd = env::current_dir().unwrap();

    let filepath = cwd
        .join("src")
        .join(format!("year{}", year))
        .join(format!("{}", folder))
        .join(format!("{:02}.txt", day));
    //println!("path: {}", filepath.to_str().unwrap());
    let f = fs::read_to_string(filepath);
    f.expect("could not open input file")
}

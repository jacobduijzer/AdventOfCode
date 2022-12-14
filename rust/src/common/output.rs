pub const ANSI_ITALIC: &str = "\x1b[3m";
pub const ANSI_BOLD: &str = "\x1b[1m";
pub const ANSI_RESET: &str = "\x1b[0m";

pub fn print_time() {
    println!("{}Total:{} {}{:.2}ms{}", ANSI_BOLD, ANSI_RESET, ANSI_ITALIC, sw.elapsed_ms(), ANSI_RESET);
}
use std::collections::{HashMap, HashSet, VecDeque};

#[derive(Clone, Copy, Debug, Eq, Hash, PartialEq)]
struct Position {
    x: u32,
    y: u32,
}

fn build_grid(input: &str) -> (HashMap<Position, char>, Position) {
    let mut grid: HashMap<Position, char> = HashMap::new();
    let mut start: Position = Position { x: 0, y: 0 };
    for (row, line) in input.lines().enumerate() {
        for (col, ch) in line.chars().enumerate() {
            grid.insert(Position { x: row as u32, y: col as u32 }, ch);
            if ch == 'S' {
                start = Position { x: row as u32, y: col as u32 };
            }
        }
    }
    (grid, start)
}

#[cfg(test)]
mod tests {
    use itertools::PadUsing;
    use super::*;

    const TEST_DATA_1: &str = ".....
.S-7.
.|.|.
.L-J.
.....";

    const TEST_DATA_2: &str = "..F7.
.FJ|.
SJ.L7
|F--J
LJ...";

    #[test]
    fn build_grid_with_test_data_1() {
        let (grid, start) = build_grid(TEST_DATA_1);

        assert_eq!(start, Position { x: 1, y: 1});
        assert_eq!(25, grid.len());
    }

    #[test]
    fn build_grid_with_test_data_2() {
        let (grid, start) = build_grid(TEST_DATA_2);

        assert_eq!(start, Position { x: 2, y: 0});
        assert_eq!(25, grid.len());
    }
}
use common::point::Point;
use crate::common;

#[derive(Clone, PartialEq, Eq, Hash, Debug)]
pub struct Grid<T> {
    pub width: i32,
    pub height: i32,
    pub bytes: Vec<T>,
}

impl Grid<u8> {

    pub fn new(width: i32, height: i32) -> Grid<u8> {
        let mut bytes = Vec::with_capacity((width * height) as usize);
        Grid { width, height, bytes }
    }

    pub fn with_bytes(width: i32, height: i32, bytes: Vec<u8>) -> Grid<u8> {
        let mut bytes = Vec::with_capacity((width * height) as usize);
        Grid { width, height, bytes }
    }

    pub fn parse(input: &str) -> Self {
        let raw: Vec<_> = input.lines().map(str::as_bytes).collect();
        let width = raw[0].len() as i32;
        let height = raw.len() as i32;
        let mut bytes = Vec::with_capacity((width * height) as usize);
        raw.iter().for_each(|slice| bytes.extend_from_slice(slice));
        Grid { width, height, bytes }
    }

    pub fn rotate(&self) -> Grid<u8> {
        // let mut newmap = Grid::new(self.height, self.width);
        let mut bytes: Vec<u8> = Vec::with_capacity((self.width * self.height) as usize);
        // println!("old width: {}, old height: {}", self.width, self.height);
        // println!("new width: {}, new height: {}", newmap.width, newmap.height);
        //If horizontal axis is x, vertical is y, and chunk size is n, then clockwise rotation can be performed as follows:
        //(x2, y2) = (n - 1 - y1, x1)
        for h in 0..self.height {
            for w in 0..self.width {
                // newmap[Point::new(h, w)] = self[Point::new(h, w)];
                // print!("{}", self[Point::new(w, h)]);
                // newmap[Point::new(self.width - 1 -  w, h)] = self[Point::new(w, h)];
                println!("pos 1: ({}, {}), pos 2: ({}, {}), val: {}", w, h, self.width - 1 - w, h, self[Point::new(w, h)]);
                // newmap[Point::new(self.width - 1 - w, h)] = self[Point::new(w, h)];
                // bytes[self.width - 1 - w *  h] = self[Point::new(w, h)];
                bytes[0] = b'0';
            }
            println!("");
        }

        Grid::with_bytes(self.height, self.width, bytes)
    }
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::common::point::Point;

    const TEST_DATA_1: &str = "..........
0000000000
##########
**********";

    const TEST_DATA_2: &str = "1234
    5678
    7890";

    #[test]
    fn parse_lines_of_strings_into_grid() {
        let grid = Grid::parse(TEST_DATA_1);

        assert_eq!(4, grid.height);
        assert_eq!(10, grid.width);
        assert_eq!(grid[Point::new(0, 0)], b'.');
        assert_eq!(grid[Point::new(0, 1)], b'0');
        assert_eq!(grid[Point::new(0, 2)], b'#');
        assert_eq!(grid[Point::new(0, 3)], b'*');
        assert_eq!(grid[Point::new(9, 3)], b'*');
    }

    #[test]
    fn parse_lines_of_strings_into_grid_2() {
        let grid = Grid::parse(TEST_DATA_2);

        assert_eq!(3, grid.height);
        assert_eq!(4, grid.width);
        assert_eq!(grid[Point::new(0, 0)], b'1');
        assert_eq!(grid[Point::new(1, 0)], b'2');
        assert_eq!(grid[Point::new(2, 0)], b'3');
        assert_eq!(grid[Point::new(3, 0)], b'4');
    }

    #[test]
    fn rotate_grid() {
        // let grid = Grid::parse(TEST_DATA_2);
        // let grid2 = grid.rotate();

        //assert_eq!(grid, grid2);

        // assert_eq!(3, grid.height);
        // assert_eq!(4, grid.width);
        // assert_eq!(grid[Point::new(0, 0)], b'1');
        // assert_eq!(grid[Point::new(1, 0)], b'2');
        // assert_eq!(grid[Point::new(2, 0)], b'3');
        // assert_eq!(grid[Point::new(3, 0)], b'4');
    }

}
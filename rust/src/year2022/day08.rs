use std::collections::HashSet;

fn visible_trees(
    grid: &[&[u8]],
    startx: usize,
    starty: usize,
    dx: isize,
    dy: isize,
    seen: &mut HashSet<(usize, usize)>,
) {
    let mut x = startx as isize;
    let mut y = starty as isize;
    let mut max_height = -1;

    while let Some(&tree) = grid.get(y as usize).and_then(|row| row.get(x as usize)) {
        if tree as isize > max_height {
            seen.insert((x as usize, y as usize));
            max_height = tree as isize;
        }
        x += dx;
        y += dy;
    }
}

fn scenic_score(grid: &[&[u8]], x: usize, y: usize) -> usize {
    let width = grid[0].len();
    let height = grid.len();
    let max_tree_height = grid[y][x];

    let range = (0..x).rev().collect::<Vec<usize>>();
    let left = horizontal(max_tree_height, &grid, range, y);

    let range = ((x+1)..width).collect::<Vec<usize>>();
    let right = horizontal(max_tree_height, &grid, range, y);

    let range = (0..y).rev().collect::<Vec<usize>>();
    let up = vertical(max_tree_height, &grid, x, range);

    let range = ((y + 1) .. height).collect::<Vec<usize>>();
    let down = vertical(max_tree_height, &grid, x, range);

    left * right * up * down
}

fn horizontal(max_tree_height: u8, grid: &[&[u8]], x_range: Vec<usize>, y: usize) -> usize {
    let mut blocked = false;
    let amount_of_trees = x_range.into_iter()
        .take_while(|&x| {
            if grid[y][x] < max_tree_height {
                true
            } else {
                blocked = true;
                false
            }
        })
        .count();

    amount_of_trees + 1 * blocked as usize
}

fn vertical(max_tree_height: u8, grid: &[&[u8]], x: usize, y_range: Vec<usize>) -> usize {
    let mut blocked = false;
    let amount_of_trees = y_range
        .into_iter()
        .take_while(|&y| {
            if grid[y][x] < max_tree_height {
                true
            } else {
                blocked = true;
                false
            }
        })
        .count();
    amount_of_trees + 1 * blocked as usize
}

pub fn solve_part1(input: &str) -> usize {
    let mut seen = HashSet::new();
    let grid: Vec<_> = input
        .lines()
        .map(|line| line.as_bytes())
        .collect();

    let width = grid[0].len();
    let height = grid.len();

    for x in 0..width {
        visible_trees(&grid, x, 0, 0, 1, &mut seen);
        visible_trees(&grid, x, height - 1, 0, -1, &mut seen);
    }
    for y in 0..height {
        visible_trees(&grid, 0, y, 1, 0, &mut seen);
        visible_trees(&grid, width - 1, y, -1, 0, &mut seen);
    }

    seen.len()
}

pub fn solve_part2(input: &str) -> usize {
    let grid: Vec<_> = input
        .lines()
        .map(|line| line.as_bytes())
        .collect();

    let width = grid[0].len();
    let height = grid.len();

    let mut max_score = 0;
    for x in 0..width {
        for y in 0..height {
            let score = scenic_score(&grid, x, y);
            if score > max_score {
                max_score = score;
            }
        }
    }

    max_score
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = crate::common::input::read_file(2022, 8, "testinput");
        assert_eq!(solve_part1(&input), 21);
    }

    #[test]
    fn test_part_two() {
        let input = crate::common::input::read_file(2022, 8, "testinput");
        assert_eq!(solve_part2(&input), 8);
    }
}
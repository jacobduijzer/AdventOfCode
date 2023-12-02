pub struct SnowIslandGameHistory {
    pub games: Vec<Game>,
}

impl SnowIslandGameHistory {
    pub fn new(game_data: &str) -> SnowIslandGameHistory {
        let games = game_data
            .lines()
            .map(Game::new)
            .collect::<Vec<_>>();
        SnowIslandGameHistory { games }
    }

    pub fn sum_game_ids_with_cubes(&self, red: u32, green: u32, blue: u32) -> u32 {
        self.games
            .iter()
            .filter(|game| game.has_enough_cubes(red, green, blue))
            .map(|game| game.id)
            .sum()
    }

    pub fn sum_minimum_needed_cubes(&self) -> u32 {
        self.games.iter().map(Game::min_cubes_possible).sum()
    }
}

#[derive(Debug, Clone, Copy, Default, Eq, PartialEq)]
pub struct MinCubes {
    pub red: u32,
    pub green: u32,
    pub blue: u32,
}

pub struct Game {
    pub id: u32,
    pub draws: Vec<Draw>,
}

impl Game {
    pub fn new(log: &str) -> Game {
        let split = log.split(':').collect::<Vec<_>>();
        let id = Game::get_id(split[0]);

        let draws = split[1]
            .split(';')
            .map(Draw::new)
            .collect::<Vec<_>>();

        Game { id, draws }
    }

    fn get_id(id_split: &str) -> u32 {
        let id = id_split[5..].parse::<u32>();
        id.unwrap()
    }

    pub fn has_enough_cubes(&self, red: u32, green: u32, blue: u32) -> bool {
        self.draws
            .iter()
            .all(|draw| draw.has_enough_cubes(red, green, blue))
    }

    pub fn min_cubes_possible(&self) -> u32 {
        let min_cubes = self.minimum_cubes();
        min_cubes.red * min_cubes.green * min_cubes.blue
    }

    pub fn minimum_cubes(&self) -> MinCubes {
        self.draws
            .iter()
            .fold(MinCubes::default(), |mut min_cubes, draw| {
                if draw.red > min_cubes.red {
                    min_cubes.red = draw.red;
                }
                if draw.green > min_cubes.green {
                    min_cubes.green = draw.green;
                }
                if draw.blue > min_cubes.blue {
                    min_cubes.blue = draw.blue;
                }
                min_cubes
            })
    }
}

#[derive(Debug, Clone, Copy)]
pub struct Draw {
    pub red: u32,
    pub green: u32,
    pub blue: u32,
}

impl Draw {
    pub fn new(draw_split: &str) -> Draw {
        let split = draw_split.split(',').collect::<Vec<_>>();
        let mut red = 0;
        let mut blue = 0;
        let mut green = 0;

        split
            .into_iter()
            .map(Draw::get_color)
            .for_each(|(color, amount)| match color {
                "red"   => red += amount,
                "green" => green += amount,
                "blue"  => blue += amount,
                _ => panic!("invalid color: {}", color)
            });

        Draw { red, green, blue }
    }

    fn get_color(color: &str) -> (&str, u32) {
        let split = color.split(' ').collect::<Vec<_>>();

        let amount = split[1].parse::<u32>().unwrap();

        (split[2], amount)
    }

    pub fn has_enough_cubes(&self, red: u32, green: u32, blue: u32) -> bool {
        self.red <= red && self.green <= green && self.blue <= blue
    }
}

pub fn solve_part1(input: &str) -> u32 {
    let snow_island_game = SnowIslandGameHistory::new(input);

    snow_island_game.sum_game_ids_with_cubes(12, 13, 14)
}

pub fn solve_part2(input: &str) -> u32 {
    let snow_island_game = SnowIslandGameHistory::new(input);

    snow_island_game.sum_minimum_needed_cubes()
}

#[cfg(test)]
mod tests {
    use super::*;

    fn load_game_data() {
        let game = Game::new("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green");

        assert_eq!(game.id, 1);
    }

    #[test]
    fn load_game_history() {
        let game_history = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";
        let snow_island_game = SnowIslandGameHistory::new(game_history);

        let sum = snow_island_game.sum_game_ids_with_cubes(12, 13, 14);

        assert_eq!(sum, 8);
    }

    #[test]
    fn calculate_fewest_number_of_cubes() {
        let game_history = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";
        let snow_island_game = SnowIslandGameHistory::new(game_history);

        let sum = snow_island_game.sum_minimum_needed_cubes();

        assert_eq!(sum, 2286);
    }

}
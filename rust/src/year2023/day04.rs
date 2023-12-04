use hashbrown::HashSet;

fn parse_card(card: &str) -> (Vec<u32>, Vec<u32>){
    let card_info : Vec<_> = card.split(':').collect();
    let all_numbers : Vec<_> = card_info[1].split('|').collect();
    let numbers : Vec<u32> = all_numbers[0]
        .split_whitespace()
        .map(|s| s.parse().expect("parse error"))
        .collect();

    let winning_numbers : Vec<u32> = all_numbers[1]
        .split_whitespace()
        .map(|s| s.parse().expect("parse error"))
        .collect();

    (numbers, winning_numbers)
}

fn find_winning_numbers(numbers: Vec<u32>, winning_numbers: Vec<u32>) -> Vec<u32>
{
    let mut intersect_result: Vec<u32> = winning_numbers.clone();

    let unique_a: HashSet<u32> = numbers.into_iter().collect();
    unique_a
        .intersection(&intersect_result.into_iter().collect())
        .map(|i| *i)
        .collect::<Vec<_>>()
}

fn score_card(winning_numbers: Vec<u32>) -> usize {
    u32::pow(2, (winning_numbers.len() - 1).try_into().unwrap()).try_into().unwrap()
}

fn get_winners_count(numbers: &Vec<u32>, mut winning_numbers: &Vec<u32>) -> u32 {
    let mut winners = 0;
    for number in numbers.iter() {
        if winning_numbers.contains(&number) {
            winners += 1;
        }
    }
    return winners;
}

fn calculate_lottery_ticket_amount(cards: Vec<(Vec<u32>, Vec<u32>)>) -> u32 {
    let mut amount: Vec<u32> = vec![1; cards.len()];

    for (index, card) in cards.iter().enumerate() {
        let current_amount = amount[index];
        let winning_numbers = get_winners_count(&cards[index].0, &cards[index].1);

        for offset in 1..winning_numbers + 1 {
            let new_index = index + offset as usize;
            if new_index >= amount.len() {
                break;
            }
            amount[index + offset as usize] += current_amount;
        }
    }

    let total_amount: u32 = amount.iter().sum();
    total_amount
}

pub fn solve_part1(input: &str) -> usize {
    input
        .lines()
        .map(|line| {
           let card = parse_card(line);
            let duplicates = find_winning_numbers(card.0, card.1);
            if duplicates.len() > 0 {
                score_card(duplicates)
            }
            else {
                0
            }
        })
        .sum()
}

pub fn solve_part2(input: &str) -> u32 {
    let cards: Vec<(Vec<u32>, Vec<u32>)> = input
        .lines()
        .map(|line |parse_card(line))
        .collect();

    calculate_lottery_ticket_amount(cards)
}

#[cfg(test)]
mod tests {
    use super::*;

    const test_data: &str = "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11";

    #[test]
    fn parse_single_card() {
        let scratchcard = parse_card("Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53");

        // numbers
        assert_eq!(41, scratchcard.0[0]);
        assert_eq!(17, scratchcard.0[4]);

        // winning numbers
        assert_eq!(83, scratchcard.1[0]);
        assert_eq!(53, scratchcard.1[7]);
    }

    #[test]
    fn get_dumplicates_from_card() {
        let scratchcard = parse_card("Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53");

        let duplicates = find_winning_numbers(scratchcard.0, scratchcard.1);

        assert_eq!(4, duplicates.len());
    }

    #[test]
    fn score_single_card() {
        let scratchcard = parse_card("Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53");
        let duplicates = find_winning_numbers(scratchcard.0, scratchcard.1);

        let score = score_card(duplicates);

        assert_eq!(8, score);
    }

    #[test]
    fn run_part1_with_test_data() {
        let result = solve_part1(test_data);

        assert_eq!(13, result);
    }

    #[test]
    fn run_part2_with_test_data() {
        let result = solve_part2(test_data);

        assert_eq!(30, result);
    }

}
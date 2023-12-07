
pub struct Hand {
    cards: [u8; 5],
    bid: usize,
}

fn get_hands(input: &str) -> Vec<Hand> {
    input
        .lines()
        .map(|line| {
            let (prefix, suffix) = line.split_at(5);
            let cards = prefix.as_bytes().try_into().unwrap();
            let bid = suffix.trim().parse().ok().unwrap();
            Hand { cards, bid }
        })
        .collect()
}

fn rank_hands(hands: Vec<Hand>, weak_joker: bool) -> Vec<([usize; 15], [u8; 5], usize)>
{
    let mut ranked_hands: Vec<_> = hands
        .iter()
        .map(|&Hand { cards, bid }| {
            let rank = cards.map(|b| match b {
                b'A' => 14,
                b'K' => 13,
                b'Q' => 12,
                b'J' => if weak_joker { 1 } else { 11 },
                b'T' => 10,
                _ => b.wrapping_sub(b'0')
            });

            let mut freq = [0; 15];
            rank.iter().for_each(|&b| freq[b as usize] += 1);

            if weak_joker {
                let jokers = freq[1];
                freq[1] = 0;
                freq.sort_unstable();
                freq.reverse();
                freq[0] += jokers;
            } else {
                freq.sort_unstable();
                freq.reverse();
            }

            (freq, rank, bid)
        })
        .collect();

    ranked_hands.sort_unstable();
    ranked_hands
}

pub fn solve_part1(input: &str) -> usize {
    let hands = get_hands(input);
    let mut ranked_hands = rank_hands(hands, false);

    ranked_hands
        .iter()
        .enumerate()
        .map(|(i, (_, _, bid))| (i + 1) * bid).sum()
}

pub fn solve_part2(input: &str) -> usize {
    let hands = get_hands(input);
    let mut ranked_hands = rank_hands(hands, true);

    ranked_hands
        .iter()
        .enumerate()
        .map(|(i, (_, _, bid))| (i + 1) * bid).sum()
}

#[cfg(test)]
mod tests {
    use super::*;

    const TEST_DATA: &str = "32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483";

    #[test]
    fn parse_hands_with_test_data() {
        let hands = get_hands(TEST_DATA);

        assert_eq!(5, hands.len());
    }

    #[test]
    fn rank_hands_for_part1_with_test_data() {
        let hands = get_hands(TEST_DATA);
        let ranked_hands = rank_hands(hands, false);

        assert_eq!(5, ranked_hands.len());
        assert_eq!(765, ranked_hands[0].2);
        assert_eq!(220, ranked_hands[1].2);
        assert_eq!(28, ranked_hands[2].2);
        assert_eq!(684, ranked_hands[3].2);
        assert_eq!(483, ranked_hands[4].2);
    }

    #[test]
    fn solve_part1_with_test_data()
    {
        let result = solve_part1(TEST_DATA);

        assert_eq!(6440, result)
    }

    #[test]
    fn rank_hands_for_part2_with_test_data() {
        let hands = get_hands(TEST_DATA);
        let ranked_hands = rank_hands(hands, true);

        assert_eq!(5, ranked_hands.len());
        assert_eq!(765, ranked_hands[0].2);
        assert_eq!(28, ranked_hands[1].2);
        assert_eq!(684, ranked_hands[2].2);
        assert_eq!(483, ranked_hands[3].2);
        assert_eq!(220, ranked_hands[4].2);
    }

    #[test]
    fn solve_part2_with_test_data()
    {
        let result = solve_part2(TEST_DATA);

        assert_eq!(5905, result)
    }
}
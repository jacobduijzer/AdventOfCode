fn predict(mut diffs: Vec<i64>) -> i64
{
    let mut prediction = 0;

    while diffs.iter().any(|&d| d != 0) {
        prediction += update_diffs(&mut diffs);
    }

    prediction
}

fn update_diffs(values: &mut Vec<i64>) -> i64
{
    for i in 0..values.len() - 1 {
        values[i] = values[i + 1] - values[i];
    }

    values.pop().unwrap()
}

fn to_vec_of_vec(input: &str, delim1: &str, delim2: &str) -> Vec<Vec<i64>>
{
    input.split(delim1)
        .map(|e| e.split(delim2)
            .filter_map(|f| f.parse().ok()).collect::<Vec<i64>>())
        .filter(|e| !e.is_empty())
        .collect()
}

pub fn solve_part1(input: &str) -> i64 {
    let history = to_vec_of_vec(input, "\n", " ");

    history
        .iter()
        .map(|v| predict(v.to_vec()))
        .sum()
}

pub fn solve_part2(input: &str) -> i64 {
    let history = to_vec_of_vec(input, "\n", " ");

    history
        .iter()
        .map(|v| predict(v
            .iter()
            .rev()
            .copied()
            .collect()))
        .sum()
}
#[cfg(test)]
mod tests {
    use super::*;

    const TEST_DATA_1: &str = "0 3 6 9 12 15";
}
defmodule Advendofcode.Solutions.Y25.Day02 do
  alias AoC.Input

  def parse(input, _part) do
    Input.read!(input)
    |> String.replace("\n", "")
    |> String.split(",", trim: true)
    |> Enum.map(fn range ->
      [from_str, to_str] =
        range
        |> String.split("-", trim: true)
        |> Enum.map(&String.to_integer/1)
      {from_str, to_str}
    end)
  end

  def invalid?(n) do
    s   = Integer.to_string(n)
    len = String.length(s)

    if rem(len, 2) == 1 do
      false
    else
      {first, second} = String.split_at(s, div(len, 2))
      first == second
    end
  end

  def invalid_part2?(n) do
    s   = Integer.to_string(n)
    len = String.length(s)

    if len < 2 do
      false
    else
      1..div(len, 2)
      |> Enum.any?(fn p ->
        if rem(len, p) != 0 do
          false
        else
          repeats = div(len, p)
          pattern = String.slice(s, 0, p)
          String.duplicate(pattern, repeats) == s
        end
      end)
    end
  end

  def part_one(problem) do
    problem
    |> Enum.flat_map(fn {from, to} -> from..to end)
    |> Enum.filter(&invalid?/1)
    |> Enum.sum()
  end

  def part_two(problem) do
    problem
    |> Enum.flat_map(fn {from, to} -> from..to end)
    |> Enum.filter(&invalid_part2?/1)
    |> Enum.sum()
  end
end

defmodule Advendofcode.Solutions.Y25.Day03 do
  alias AoC.Input

  def parse(input, _part) do
    Input.read!(input)
    |> String.split("\n", trim: true)
  end

  def max_bank_joltage(line) do
    digits =
      line
      |> String.trim()
      |> String.graphemes()
      |> Enum.map(&String.to_integer/1)

    [first | rest] = digits

    {best, _max_prefix} =
      Enum.reduce(rest, {nil, first}, fn d, {best, max_prefix} ->
        candidate = max_prefix * 10 + d
        best = if best == nil or candidate > best, do: candidate, else: best
        max_prefix = if d > max_prefix, do: d, else: max_prefix
        {best, max_prefix}
      end)

    best
  end

  @digits_to_keep 12

  def max_bank_joltage_12(line) do
    digits =
      line
      |> String.trim()
      |> String.graphemes()
      |> Enum.map(&String.to_integer/1)

    k = @digits_to_keep
    to_remove = length(digits) - k

    {stack, _to_remove_left} =
      Enum.reduce(digits, {[], to_remove}, fn d, {stack, to_remove_left} ->
        {stack, to_remove_left} = pop_smaller(stack, d, to_remove_left)
        {[d | stack], to_remove_left}
      end)

    stack
    |> Enum.reverse()
    |> Enum.take(k)
    |> Integer.undigits()
  end

  defp pop_smaller([last | rest] = _, d, to_remove) when to_remove > 0 and last < d do
    pop_smaller(rest, d, to_remove - 1)
  end

  defp pop_smaller(stack, _d, to_remove), do: {stack, to_remove}

  def part_one(problem) do
    problem
    |> Enum.map(&max_bank_joltage/1)
    |> Enum.sum()
  end

  def part_two(problem) do
    problem
    |> Enum.map(&max_bank_joltage_12/1)
    |> Enum.sum()
  end
end

defmodule Advendofcode.Solutions.Y24.Day03 do
  require Logger
  alias AoC.Input

  def parse(input, _part) do
    Input.read!(input)
  end

  def part_one(problem) do
    regex = ~r/mul\((\d+),(\d+)\)/

    Regex.scan(regex, problem)
    |> Enum.map(fn [_, a, b] -> {String.to_integer(a), String.to_integer(b)} end)
    |> Enum.map(fn {a, b} -> a * b end)
    |> Enum.sum()
  end

  def part_two(problem) do
    regex = ~r/(?:don't\(\)|do\(\)|mul\((\d+),(\d+)\))/

    Regex.scan(regex, problem)
    |> Enum.reduce(%{doing: true, sum: 0}, fn
      ["do()"], acc ->
        %{acc | doing: true}

      ["don't()"], acc ->
        %{acc | doing: false}

      ["mul(" <> _ = _full, a, b], %{doing: true, sum: sum} = acc ->
        %{acc | sum: sum + String.to_integer(a) * String.to_integer(b)}

      _other, acc ->
        acc
    end)
    |> Map.get(:sum)
  end
end

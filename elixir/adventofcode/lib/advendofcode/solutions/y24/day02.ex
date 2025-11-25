defmodule Advendofcode.Solutions.Y24.Day02 do
  require Logger
  alias AoC.Input

  def parse(input, _part) do
    Input.read!(input)
    |> String.split("\n", trim: true)
    |> Enum.map(fn line ->
      line
      |> String.split()
      |> Enum.map(&String.to_integer/1)
    end)
  end

  def part_one(levels) do
    safe_reports =
      levels
      |> Enum.filter(&safe?/1)

    length(safe_reports)
  end

  def part_two(levels) do
    safe_reports =
      levels
      |> Enum.filter(&safe_with_dampener?/1)

    length(safe_reports)
  end

  def safe?(levels) do
    diffs =
      levels
      |> Enum.chunk_every(2, 1, :discard)
      |> Enum.map(fn [a, b] -> b - a end)

    increasing? = Enum.all?(diffs, &(&1 in 1..3))
    decreasing? = Enum.all?(diffs, &(&1 in -3..-1))

    increasing? or decreasing?
  end

  def safe_with_dampener?(levels) do
    if safe?(levels) do
      true
    else
      levels
      |> Enum.with_index()
      |> Enum.any?(fn {_val, idx} ->
        levels
        |> List.delete_at(idx)
        |> safe?()
      end)
    end
  end
end

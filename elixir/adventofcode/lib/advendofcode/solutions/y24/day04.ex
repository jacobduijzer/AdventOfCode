defmodule Advendofcode.Solutions.Y24.Day04 do
  alias AoC.Input

  def parse(input, _part) do
    data = Input.read!(input)
    Advent.Grid.new(data)
  end

  def part_one(grid) do
    grid
    |> find_coords("X")
    |> Enum.flat_map(&find_xmas_words(grid, &1))
    |> length()
  end

  def part_two(grid) do
    grid
    |> find_coords("A")
    |> Enum.flat_map(&find_mas_words(grid, &1))
    |> length()
  end

  defp find_coords(grid, letter) do
    grid
    |> Enum.filter(fn {_coord, check} -> letter == check end)
    |> Enum.map(&elem(&1, 0))
  end

  defp directions do
    [{-1, -1}, {-1, 0}, {-1, 1},
     {0, -1}, {0, 1},
     {1, -1}, {1, 0}, {1, 1}]
  end

  defp find_xmas_words(grid, start) do
    directions()
    |> Enum.filter(fn next ->
      matches?(start, next, 1, "M", grid) &&
        matches?(start, next, 2, "A", grid) &&
        matches?(start, next, 3, "S", grid)
    end)
  end

  defp mas_patterns do
    [
      [
        [{1, -1}, {1, 1}],
        [{-1, 1}, {-1, -1}]
      ], [
        [{-1, -1}, {1, -1}],
        [{-1, 1}, {1, 1}]
      ]
    ]
  end

  defp find_mas_words(grid, start) do
    mas_patterns()
    |> Enum.filter(fn [side1, side2] ->
      (Enum.all?(side1, &matches?(start, &1, 1, "M", grid)) &&
         Enum.all?(side2, &matches?(start, &1, 1, "S", grid))) ||
        (Enum.all?(side1, &matches?(start, &1, 1, "S", grid)) &&
           Enum.all?(side2, &matches?(start, &1, 1, "M", grid)))
    end)
  end

  def matches?({row1, col1}, {row2, col2}, offset, letter, grid) do
    Map.get(grid, {row1 + offset * row2, col1 + offset * col2}) == letter
  end



end

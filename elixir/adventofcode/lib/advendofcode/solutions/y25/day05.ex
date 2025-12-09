defmodule Advendofcode.Solutions.Y25.Day05 do
  alias AoC.Input

  def parse(input, _part) do
    [ranges_part, ingredients_part] =
      Input.read!(input)
      |> String.split("\n\n", trim: true)

    ranges =
      for rule <- String.split(ranges_part, "\n"), reduce: MapSet.new() do
        acc ->
          [p1, p2] = String.split(rule, "-")
          MapSet.put(acc, {String.to_integer(p1), String.to_integer(p2)})
      end

    ingredients =
      ingredients_part
      |> String.split("\n", trim: true)
      |> Enum.map(fn line ->
        String.to_integer(line)
      end)

      {ranges, ingredients}
  end

  def part_one({ranges, ingredients}) do
    inside =
      Enum.filter(ingredients, fn n ->
        Enum.any?(ranges, fn {from, to} ->
          n >= from and n <= to
        end)
    end)

    inside |> Enum.count()
  end

  def part_two({ranges, _}) do
    compress_ranges =
      ranges
      |> Enum.to_list()
      |> Enum.sort_by(fn {from, to} -> {from, to} end)
      |> Enum.reduce([], fn {from, to}, acc ->
        case acc do
          [] ->
            [{from, to}]

          [{cur_from, cur_to} | rest] ->
            if from <= cur_to + 1 do
              [{cur_from, max(cur_to, to)} | rest]
            else
              [{from, to} | acc]
          end
        end
      end)
      |> Enum.reverse()

    compress_ranges |> Enum.reduce(0, fn {from, to}, acc ->
      acc + (to - from + 1)
    end)
  end
end

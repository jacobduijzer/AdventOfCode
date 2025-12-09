defmodule Advendofcode.Solutions.Y25.Day04 do
  alias AoC.Input

  @neighbor_offsets for dy <- -1..1,
                        dx <- -1..1,
                        not (dx == 0 and dy == 0),
                        do: {dx, dy}

  def parse(input, _part) do
    Input.read!(input)
    |> String.split("\n", trim: true)
    |> Enum.with_index()
    |> Enum.reduce(MapSet.new(), fn {line, y}, acc ->
      line
      |> String.graphemes()
      |> Enum.with_index()
      |> Enum.reduce(acc, fn
        {"@", x}, acc2 -> MapSet.put(acc2, {x, y})
        {_ch, _x}, acc2 -> acc2
      end)
    end)
  end

  defp accessible?(rolls, {x, y}) do
    neighbor_count =
      @neighbor_offsets
      |> Enum.count(fn {dx, dy} ->
        MapSet.member?(rolls, {x + dx, y + dy})
      end)

    neighbor_count < 4
  end

  defp remove_all_accessible(rolls, removed_so_far) do
    accessible_rolls =
      rolls
      |> Enum.filter(fn coord -> accessible?(rolls, coord) end)

    case accessible_rolls do
      [] ->
        {removed_so_far, rolls}

      _ ->
        rolls_after_removal =
          Enum.reduce(accessible_rolls, rolls, fn coord, acc ->
            MapSet.delete(acc, coord)
          end)

        remove_all_accessible(
          rolls_after_removal,
          removed_so_far + length(accessible_rolls)
        )
    end
  end

  def part_one(problem) do
    problem
    |> Enum.count(fn {x, y} -> accessible?(problem, {x, y}) end)
  end

  def part_two(problem) do
    {removed_count, _remaining} = remove_all_accessible(problem, 0)
    removed_count
  end
end

defmodule Advendofcode.Solutions.Y24.Day05 do
  alias AoC.Input
  require Logger


  def parse(input, _part) do
    [rules_part, pages_part] =
    Input.read!(input)
    |> String.split("\n\n", trim: true)

    rules =
      for rule <- String.split(rules_part, "\n"), reduce: MapSet.new() do
        acc ->
          [p1, p2] = String.split(rule, "|")
          MapSet.put(acc, {String.to_integer(p1), String.to_integer(p2)})
      end

    pages = pages_part
    |> String.split("\n", trim: true)
    |> Enum.map(fn line ->
      line
      |> String.split(",", trim: true)
      |> Enum.map(&String.to_integer/1)
    end)

    {rules, pages}
  end

  def valid?(update, rules) do
    update
    |> Enum.reduce({[], true}, fn
      _page, {prev, false} ->
        {prev, false}

      page, {prev, _valid?} ->
        valid? = not Enum.any?(prev, &MapSet.member?(rules, {page, &1}))
        {[page | prev], valid?}
    end)
    |> elem(1)
  end

  defp fix_update(update, rules) do
    for i <- 1..(Enum.count(update) - 1), j <- 0..(i - 1), reduce: {nil, true} do
      {indexes, false} ->
        {indexes, false}

      {nil, true} ->
        {first, second} = {Map.get(update, j), Map.get(update, i)}

        if MapSet.member?(rules, {second, first}) do
          {{i, j}, false}
        else
          {nil, true}
        end
    end
    |> then(fn
      {nil, true} -> update
      {{i, j}, false} -> update |> swap(i, j) |> fix_update(rules)
    end)
  end

  defp swap(update, i, j) do
    update |> Map.put(i, Map.get(update, j)) |> Map.put(j, Map.get(update, i))
  end

  def part_one({rules, pages}) do
    pages
    |> Enum.filter(&valid?(&1, rules))
    |> Enum.map(&Enum.at(&1, &1 |> length |> div(2)))
    |> Enum.sum()
  end

  def part_two({rules, pages}) do
    pages
    |> Enum.reject(&valid?(&1, rules))
    |> Enum.map(&for {p, i} <- Enum.with_index(&1), into: %{}, do: {i, p})
    |> Enum.map(&fix_update(&1, rules))
    |> Enum.map(&Map.get(&1, &1 |> Enum.count() |> div(2)))
    |> Enum.sum()
  end
end

defmodule Advendofcode.Solutions.Y25.Day06 do
  alias AoC.Input

  def parse(input, _part) do
    Input.read!(input)
  end

  defp parse_part_one(input) do
    lines =
      input
      |> String.split("\n", trim: true)

    numbers =
      lines
      |> Enum.drop(-1)
      |> Enum.map(&String.split(&1))
      |> Enum.zip()
      |> Enum.map(fn tuple ->
        tuple
        |> Tuple.to_list()
        |> Enum.map(&String.to_integer/1)
      end)

    ops =
      lines
      |> List.last()
      |> String.split()

    columns =
      Enum.zip(numbers, ops)
      |> Enum.map(fn {values, op} -> %{values: values, op: op} end)

    columns
  end

  defp apply_op("+", args), do: Enum.sum(args)
  defp apply_op("*", args), do: Enum.product(args)

  def part_one(problem) do
    problem = parse_part_one(problem)

    Enum.map(problem, fn %{values: cols, op: op} ->
      apply_op(op, cols)
    end)
    |> Enum.sum()
  end

  def part_two(problem) do
    rows = problem |> String.split("\n", trim: true)
    {ops, rows} = List.pop_at(rows, -1)
    rows = Enum.map(rows, &String.graphemes/1)
    ops = String.split(ops, " ", trim: true)

    rows
    |> Stream.unfold(fn
      [[] | _] ->
        nil

      rows ->
        {rows, col} = Enum.map_reduce(rows, [], fn [h | t], acc -> {t, [h | acc]} end)
        {:lists.reverse(col), rows}
    end)
    |> Stream.map(fn str_digits ->
      case str_digits |> Enum.join("") |> String.trim() do
        "" -> nil
        str_num -> String.to_integer(str_num)
      end
    end)
    |> Stream.chunk_by(&is_integer/1)
    |> Stream.filter(&(&1 != [nil]))
    |> Stream.zip(ops)
    |> Enum.sum_by(fn {numbers, op} -> apply_op(op, numbers) end)
  end
end

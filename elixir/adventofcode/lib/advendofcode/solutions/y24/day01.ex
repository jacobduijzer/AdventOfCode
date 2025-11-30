defmodule Advendofcode.Solutions.Y24.Day01 do
  alias AoC.Input

  def parse(input, _part) do
    Input.stream!(input)
    |> Enum.reject(&(&1 == ""))
    |> Enum.map(fn line ->
      [l, r] =
        line
        |> String.split()
        |> Enum.map(&String.to_integer/1)

      {l, r}
    end)
    |> Enum.unzip()
  end

  def part_one({lefts, rights}) do
    lefts
    |> Enum.sort()
    |> Enum.zip(Enum.sort(rights))
    |> Enum.map(fn {l, r} -> abs(l - r) end)
    |> Enum.sum()
  end

  def part_two({lefts, rights}) do
    freqs = Enum.frequencies(rights)

    Enum.reduce(lefts, 0, fn l, acc ->
      acc + l * Map.get(freqs, l, 0)
    end)
  end
end

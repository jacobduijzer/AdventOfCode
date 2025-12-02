defmodule Advendofcode.Solutions.Y25.Day01 do
  alias AoC.Input

  # it is a gimmick, but visualizing the dial is fun. Does not seem to be correctly working yet though.
  @use_vis false

  @start 50
  @dial_size 100

  def parse(input, _part) do
    Input.read!(input)
    |> String.split("\n", trim: true)
    |> Enum.map(fn <<dir::binary-size(1), rest::binary>> ->
      {dir, String.to_integer(rest)}
    end)
  end

  def part_one(problem) do
    {_final_pos, zero_hits} =
      problem
      |> Enum.map(fn
        {"R", n} -> n
        {"L", n} -> -n
      end)
      |> Enum.reduce({@start, 0}, fn step, {pos, hits} ->
        new_pos = rem(pos + step, @dial_size)
        new_pos = if new_pos < 0, do: new_pos + @dial_size, else: new_pos

        hits = if new_pos == 0, do: hits + 1, else: hits

        if @use_vis do
          for step <- pos..rem(pos + step, @dial_size) do
            loop(step, hits)
          end
        end

        {new_pos, hits}
      end)

    zero_hits
  end

  defp loop(pos, hits) do
    IO.write(IO.ANSI.clear())
    IO.write(IO.ANSI.home())

    Dial.render(pos, hits)

    Process.sleep(10)
  end

  def part_two(problem) do
    {_final_pos, zero_hits} =
      Enum.map(problem, fn
        {"R", n} -> n
        {"L", n} -> -n
      end)
      |> Enum.reduce({@start, 0}, fn step, {pos, hits} ->
        raw_from = pos
        raw_to = pos + step

        new_hits =
          abs(:math.floor(raw_to / @dial_size) - :math.floor(raw_from / @dial_size))

        new_pos = rem(raw_to, @dial_size)
        new_pos = if new_pos < 0, do: new_pos + @dial_size, else: new_pos

        {new_pos, hits + new_hits}
      end)

    zero_hits
  end
end

defmodule Dial do
  @width 31
  @height 15

  @radius 7.0
  @thickness 0.6

  @size 100

  def size, do: @size

  def render(pos, hits) when is_integer(pos) do
    norm_pos = Integer.mod(pos, @size)
    pointer_coords = pointer_coords(norm_pos)

    frame_lines =
      for row <- 0..(@height - 1) do
        for col <- 0..(@width - 1) do
          char_for(row, col, pointer_coords)
        end
        |> Enum.join("")
      end

    IO.puts(Enum.join(frame_lines, "\n"))
    IO.puts("")
    IO.puts("pos: #{pos} (wrapped: #{norm_pos}) / #{@size - 1} hits at 0: #{hits}")
  end

  defp char_for(row, col, pointer_coords) do
    cond do
      MapSet.member?(pointer_coords, {row, col}) ->
        "^"

      on_circle?(row, col) ->
        "o"

      true ->
        " "
    end
  end

  defp on_circle?(row, col) do
    {cx, cy} = center()
    x = col - cx
    y = row - cy

    dist = :math.sqrt(x * x + y * y)

    abs(dist - @radius) < @thickness
  end

  defp pointer_coords(pos) do
    pos = Integer.mod(pos, @size)

    {cx, cy} = center()

    angle = 2.0 * :math.pi() * pos / @size
    theta = angle - :math.pi() / 2.0

    pointer_len = @radius - 1.0

    ex = :math.cos(theta) * pointer_len
    ey = :math.sin(theta) * pointer_len

    end_col = round(cx + ex)
    end_row = round(cy + ey)

    dx = end_col - cx
    dy = end_row - cy

    steps = max(abs(dx), abs(dy))

    coords =
      if steps == 0 do
        [{cy, cx}]
      else
        for i <- 0..steps do
          t = i / steps
          col = round(cx + dx * t)
          row = round(cy + dy * t)
          {row, col}
        end
      end

    MapSet.new(coords)
  end

  defp center do
    {div(@width, 2), div(@height, 2)}
  end
end

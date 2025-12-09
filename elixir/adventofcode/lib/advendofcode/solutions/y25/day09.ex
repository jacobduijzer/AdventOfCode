defmodule Advendofcode.Solutions.Y25.Day09 do
  alias AoC.Input

  def parse(input, _part) do
    Input.read!(input)
    |> String.split("\n", trim: true)
    |> Enum.map(fn line ->
      [x_str, y_str] = String.split(line, ",")
      {String.to_integer(x_str), String.to_integer(y_str)}
    end)
  end

  def max_area_with_points(points) do
    for {x1, y1} = p1 <- points,
        {x2, y2} = p2 <- points,
        p1 != p2,
        reduce: {0, nil, nil} do
      {best_area, best_p1, best_p2} ->
        width = abs(x2 - x1) + 1
        height = abs(y2 - y1) + 1
        area = width * height

        if area > best_area do
          {area, p1, p2}
        else
          {best_area, best_p1, best_p2}
        end
    end
  end

  def part_one(problem) do
    {area, _, _} = max_area_with_points(problem)
    area
  end

  # def part_two(problem) do
  #   problem
  # end
end

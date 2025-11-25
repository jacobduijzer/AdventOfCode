defmodule Advendofcode.Solutions.Y24.Day02Test do
  alias AoC.Input, warn: false
  alias Advendofcode.Solutions.Y24.Day02, as: Solution, warn: false
  use ExUnit.Case, async: true

  # To run the test, run one of the following commands:
  #
  #     mix AoC.test --year 2024 --day 2
  #
  #     mix test test/2024/day02_test.exs
  #
  # To run the solution
  #
  #     mix AoC.run --year 2024 --day 2 --part 1
  #
  # Use sample input file:
  #
  #     # returns {:ok, "priv/input/2024/day-02-mysuffix.inp"}
  #     {:ok, path} = Input.resolve(2024, 2, "mysuffix")
  #
  # Good luck!

  defp solve(input, part) do
    problem =
      input
      |> Input.as_file()
      |> Solution.parse(part)

    apply(Solution, part, [problem])
  end

  test "part one example" do
    input = ~S"""
    7 6 4 2 1
    1 2 7 8 9
    9 7 6 2 1
    1 3 2 4 5
    8 6 4 4 1
    1 3 6 7 9
    """

    assert 2 == solve(input, :part_one)
  end

  # Once your part one was successfully sumbitted, you may uncomment this test
  # to ensure your implementation was not altered when you implement part two.

  # @part_one_solution CHANGE_ME
  #
  # test "part one solution" do
  #   assert {:ok, @part_one_solution} == AoC.run(2024, 2, :part_one)
  # end

  test "part two example" do
    input = ~S"""
    7 6 4 2 1
    1 2 7 8 9
    9 7 6 2 1
    1 3 2 4 5
    8 6 4 4 1
    1 3 6 7 9
    """

    assert 4 == solve(input, :part_two)
  end

  # You may also implement a test to validate the part two to ensure that you
  # did not broke your shared modules when implementing another problem.

  # @part_two_solution CHANGE_ME
  #
  # test "part two solution" do
  #   assert {:ok, @part_two_solution} == AoC.run(2024, 2, :part_two)
  # end
end

defmodule Adventofcode.MixProject do
  use Mix.Project

  def project do
    [
      app: :adventofcode,
      version: "0.1.0",
      elixir: "~> 1.19",
      start_permanent: Mix.env() == :prod,
      modkit: [
        mount: [
          {Mix.Tasks, "lib/mix/tasks", flavor: :mix_task},
          {Advendofcode, "lib/advendofcode", flavor: :elixir}
        ]
      ],
      deps: deps()
    ]
  end

  # Run "mix help compile.app" to learn about applications.
  def application do
    [
      extra_applications: [:logger]
    ]
  end

  # Run "mix help deps" to learn about dependencies.
  defp deps do
    [
      # {:dep_from_hexpm, "~> 0.3.0"},
      # {:dep_from_git, git: "https://github.com/elixir-lang/my_dep.git", tag: "0.1.0"}
      {:aoc, "~> 0.16"}
    ]
  end

  def cli do
    [
      preferred_envs: ["aoc.test": :test]
    ]
  end
end

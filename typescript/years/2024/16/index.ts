import _ from "lodash";
import * as util from "../../../util/util";
import * as test from "../../../util/test";
import chalk from "chalk";
import { log, logSolution, trace } from "../../../util/log";
import { performance } from "perf_hooks";
import { normalizeTestCases } from "../../../util/test";
import { Cell, Grid, Dir,CardinalDirection } from "../../../util/grid";
// import { MinPriorityQueue } from "@datastructures-js/priority-queue";

const YEAR = 2024;
const DAY = 16;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\16\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\16\data.txt
// problem url  : https://adventofcode.com/2024/day/16

class PriorityQueue<T> {
	private heap: { value: T; priority: number }[] = [];
	push(value: T, priority: number) {
		this.heap.push({ value, priority });
		this.heap.sort((a, b) => a.priority - b.priority);
	}
	pop(): T | undefined {
		return this.heap.shift()?.value;
	}
	isEmpty(): boolean {
		return this.heap.length === 0;
	}
}

function rotateDirectionClockwise(direction: CardinalDirection): CardinalDirection {
	const directions: CardinalDirection[] = ["north", "east", "south", "west"];
	const index = directions.indexOf(direction);
	return directions[(index + 1) % 4];
}

function rotateDirectionCounterClockwise(direction: CardinalDirection): CardinalDirection {
	const directions: CardinalDirection[] = ["north", "east", "south", "west"];
	const index = directions.indexOf(direction);
	return directions[(index + 3) % 4];
}

function getMovement(direction: CardinalDirection): [number, number] {
	switch (direction) {
		case "north": return Dir.N;
		case "east": return Dir.E;
		case "south": return Dir.S;
		case "west": return Dir.W;
		default: throw new Error("Invalid direction");
	}
}

async function p2024day16_part1(input: string, ...params: any[]): Promise<number> {
	const grid = new Grid({ serialized: input });
	const start = grid.getCell("S");
	const end = grid.getCell("E");

	if (!start || !end)
		throw new Error("Start or end cell not found");

	const queue = new PriorityQueue<{ cell: Cell, direction: CardinalDirection, score: number }>();
	queue.push({ cell: start, direction: "east", score: 0 }, 0);

	const visited = new Set<string>();

	while (!queue.isEmpty()) {
		const { cell: current, direction, score } = queue.pop()!;
		const posKey = `${current.position[0]},${current.position[1]},${direction}`;

		if (visited.has(posKey))
			continue;

		visited.add(posKey);

		if (current.position[0] === end.position[0] && current.position[1] === end.position[1])
			return score;

		const [dx, dy] = getMovement(direction);
		const nextCell = grid.getCell([current.position[0] + dx, current.position[1] + dy]);
		if (nextCell && nextCell.value !== "#")
			queue.push({ cell: nextCell, direction, score: score + 1 }, score + 1);


		const newDirectionClockwise = rotateDirectionClockwise(direction);
		queue.push({ cell: current, direction: newDirectionClockwise, score: score + 1000 }, score + 1000);

		const newDirectionCounterClockwise = rotateDirectionCounterClockwise(direction);
		queue.push({ cell: current, direction: newDirectionCounterClockwise, score: score + 1000 }, score + 1000);
	}

	return -1;
}

async function p2024day16_part2(input: string, ...params: any[]) {
	return "Not implemented";
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `###############
#.......#....E#
#.#.###.#.###.#
#.....#.#...#.#
#.###.#####.#.#
#.#.#.......#.#
#.#.#####.###.#
#...........#.#
###.#.#####.#.#
#...#.....#.#.#
#.#.#.###.#.#.#
#.....#...#.#.#
#.###.#.#.#.#.#
#S..#.....#...#
###############`,
		extraArgs: [],
		expected: `7036`
	},{
		input: `#################
#...#...#...#..E#
#.#.#.#.#.#.#.#.#
#.#.#.#...#...#.#
#.#.#.#.###.#.#.#
#...#.#.#.....#.#
#.#.#.#.#.#####.#
#.#...#.#.#.....#
#.#.#####.#.###.#
#.#.#.......#...#
#.#.###.#####.###
#.#.#...#.....#.#
#.#.#.#####.###.#
#.#.#.........#.#
#.#.#.#########.#
#S#.............#
#################`,
		extraArgs: [],
		expected: `11048`
	}];
	const part2tests: TestCase[] = [];

	const [p1testsNormalized, p2testsNormalized] = normalizeTestCases(part1tests, part2tests);

	// Run tests
	test.beginTests();
	await test.section(async () => {
		for (const testCase of p1testsNormalized) {
			test.logTestResult(testCase, String(await p2024day16_part1(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	await test.section(async () => {
		for (const testCase of p2testsNormalized) {
			test.logTestResult(testCase, String(await p2024day16_part2(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	test.endTests();

	// Get input and run program while measuring performance
	const input = await util.getInput(DAY, YEAR);

	const part1Before = performance.now();
	const part1Solution = String(await p2024day16_part1(input));
	const part1After = performance.now();

	const part2Before = performance.now()
	const part2Solution = String(await p2024day16_part2(input));
	const part2After = performance.now();

	logSolution(16, 2024, part1Solution, part2Solution);

	log(chalk.gray("--- Performance ---"));
	log(chalk.gray(`Part 1: ${util.formatTime(part1After - part1Before)}`));
	log(chalk.gray(`Part 2: ${util.formatTime(part2After - part2Before)}`));
	log();
}

run()
	.then(() => {
		process.exit();
	})
	.catch(error => {
		throw error;
	});

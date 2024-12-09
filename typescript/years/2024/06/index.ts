import * as util from "../../../util/util";
import * as test from "../../../util/test";
import chalk from "chalk";
import { log, logSolution, trace } from "../../../util/log";
import { performance } from "perf_hooks";
import { normalizeTestCases } from "../../../util/test";
import { Cell, Grid, GridPos, Dir } from "../../../util/grid";

const YEAR = 2024;
const DAY = 6;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\06\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\06\data.txt
// problem url  : https://adventofcode.com/2024/day/6

function rotateDirectionClockwise(direction: string): string {
	const directions = ["north", "east", "south", "west"];
	const index = directions.indexOf(direction);
	return directions[(index + 1) % 4];
}

function moveUntil(grid: Grid, start: Cell, direction: string, wall: string): number {
	let steps = 1;
	let currDirection = direction;
	let row = start.position[0];
	let col = start.position[1];
	const visited = new Set<string>();

	let movement = Dir['N'];
	while (true) {
		const nextPos = grid.getCell([row + movement[0], col + movement[1]]);
		if(nextPos?.isEdge() && nextPos?.value !== "#" ) {
			steps++;
			break;
		} else if (nextPos!.value === "#") {
			currDirection = rotateDirectionClockwise(currDirection);
			movement = Dir[currDirection[0].toUpperCase()];
		} else {
			grid.setCell([row, col], 'X');
			const posKey = `${row},${col}`;
			if (!visited.has(posKey)) {
				visited.add(posKey);
				steps++;
			}
			row += movement[0];
			col += movement[1];
		}
	}

	return steps;
}

// function detectLoop(grid: Grid, start: Cell, direction: string): boolean {
// 	let currDirection = direction;
// 	let row = start.position[0];
// 	let col = start.position[1];
// 	const visited = new Set<string>();
//
// 	let movement = Dir['N'];
// 	while (true) {
// 		const nextPos = grid.getCell([row + movement[0], col + movement[1]]);
// 		if(nextPos?.isEdge() && nextPos?.value !== "#" ) {
// 			break;
// 		} else if (nextPos!.value === "#") {
// 			currDirection = rotateDirectionClockwise(currDirection);
// 			movement = Dir[currDirection[0].toUpperCase()];
// 		} else {
// 			const posKey = `${row},${col},${currDirection}`;
// 			if (!visited.has(posKey)) {
// 				visited.add(posKey);
// 			} else {
// 				return true;
// 			}
// 			row += movement[0];
// 			col += movement[1];
// 		}
// 	}
//
// 	return false;
// }
function detectLoop(grid: Grid, start: Cell, direction: string): boolean {
	let currDirection = direction;
	let row = start.position[0];
	let col = start.position[1];
	const visited: { [key: string]: boolean } = {};

	let movement = Dir['N'];
	while (true) {
		const nextPos = grid.getCell([row + movement[0], col + movement[1]]);
		if (nextPos?.isEdge() && nextPos?.value !== "#") {
			break;
		} else if (nextPos!.value === "#") {
			currDirection = rotateDirectionClockwise(currDirection);
			movement = Dir[currDirection[0].toUpperCase()];
		} else {
			const posKey = `${row},${col},${currDirection}`;
			if (!visited[posKey]) {
				visited[posKey] = true;
			} else {
				return true;
			}
			row += movement[0];
			col += movement[1];
		}
	}

	return false;
}

async function p2024day6_part1(input: string, ...params: any[]) {
	let steps = 0;
	const grid = new Grid({ serialized: input });
	const start = grid.getCell("^");
	if (start) {
		steps = moveUntil(grid, start, "north", "#");
	}
	return steps;
}

async function p2024day6_part2(input: string, ...params: any[]) {
	let count = 0;
	const grid = new Grid({ serialized: input });
	const start = grid.getCell("^");
	if (start) {
		for (let x = 0; x < grid.rowCount; x++) {
			for (let y = 0; y < grid.colCount; y++) {
				if (grid.getCell([x, y])?.value === ".") {
					grid.setCell([x, y], '#');
					if (detectLoop(grid, start, "north")) {
						count++;
					}
					grid.setCell([x, y], '.');

				}
			}
		}
	}

	return count;
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...`,
		extraArgs: [],
		expected: `41`
	}];

	const part2tests: TestCase[] = [{
		input: `....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...`,
		extraArgs: [],
		expected: `6`
	}];

	const [p1testsNormalized, p2testsNormalized] = normalizeTestCases(part1tests, part2tests);

	// Run tests
	test.beginTests();
	await test.section(async () => {
		for (const testCase of p1testsNormalized) {
			test.logTestResult(testCase, String(await p2024day6_part1(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	await test.section(async () => {
		for (const testCase of p2testsNormalized) {
			test.logTestResult(testCase, String(await p2024day6_part2(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	test.endTests();

	// Get input and run program while measuring performance
	const input = await util.getInput(DAY, YEAR);

	const part1Before = performance.now();
	const part1Solution = String(await p2024day6_part1(input));
	const part1After = performance.now();

	const part2Before = performance.now()
	const part2Solution = String(await p2024day6_part2(input));
	const part2After = performance.now();

	logSolution(6, 2024, part1Solution, part2Solution);

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

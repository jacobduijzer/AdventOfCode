import _ from "lodash";
import * as util from "../../../util/util";
import * as test from "../../../util/test";
import chalk from "chalk";
import { log, logSolution, trace } from "../../../util/log";
import { performance } from "perf_hooks";
import { normalizeTestCases } from "../../../util/test";
import { Grid } from "../../../util/grid";

const YEAR = 2024;
const DAY = 10;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\10\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\10\data.txt
// problem url  : https://adventofcode.com/2024/day/10

const directions = [
	[0, 1],  // right
	[1, 0],  // down
	[0, -1], // left
	[-1, 0], // up
];

function dfs(
	grid: Grid,
	x: number,
	y: number,
	currentValue: number,
	visitedNines: Set<string>
) {
	const rows = grid.rowCount;
	const cols = grid.colCount;

	if (x < 0 || y < 0 || x >= rows || y >= cols)
		return;

	const currentCellValue = Number(grid.getValue([x, y]));

	if (currentCellValue !== currentValue)
		return;

	const key = `${x},${y}`;
	if (currentCellValue === 9) {
		if (!visitedNines.has(key)) {
			visitedNines.add(key); // Mark this 9 as visited globally
		} else {
			// This 9 has already been counted
			return
		}
	}

	for (const [dx, dy] of directions) {
		const newX = x + dx;
		const newY = y + dy;
		dfs(grid, newX, newY, currentValue + 1, visitedNines);
	}
}

function dfs2(
	grid: Grid,
	x: number,
	y: number,
	currentValue: number,
	path: string,
	paths: Set<string>
): void {
	const rows = grid.rowCount;
	const cols = grid.colCount;

	if (x < 0 || y < 0 || x >= rows || y >= cols)
		return;

	const currentCellValue = Number(grid.getValue([x, y]));
	if (currentCellValue !== currentValue)
		return;

	const newPath = `${path}->(${x},${y})`;
	if (currentCellValue === 9) {
		paths.add(newPath);
		return;
	}

	for (const [dx, dy] of directions) {
		const newX = x + dx;
		const newY = y + dy;
		dfs2(grid, newX, newY, currentValue + 1, newPath, paths);
	}
}

async function p2024day10_part1(input: string, ...params: any[]) {
	const grid = new Grid({ serialized: input });
	const rows = grid.rowCount;
	const cols = grid.colCount;
	let pathCount = 0;

	for (let i = 0; i < rows; i++) {
		for (let j = 0; j < cols; j++) {
			if (Number(grid.getValue([i, j])) === 0) {
				const visitedNines = new Set<string>();
				dfs(grid, i, j, 0, visitedNines);
				pathCount += visitedNines.size;
			}
		}
	}

	return pathCount;
}

async function p2024day10_part2(input: string, ...params: any[]) {
	const grid = new Grid({ serialized: input });
	const rows = grid.rowCount;
	const cols = grid.colCount;
	const ratings: number[] = [];

	for (let i = 0; i < rows; i++) {
		for (let j = 0; j < cols; j++) {
			if (Number(grid.getValue([i, j])) === 0) {
				const paths = new Set<string>();
				dfs2(grid, i, j, 0, "", paths);
				ratings.push(paths.size);
			}
		}
	}

	return ratings.reduce((acc, rating) => acc + rating, 0);
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `0123
1234
8765
9876`,
		extraArgs: [],
		expected: `1`
	},
		{
			input: `89010123
78121874
87430965
96549874
45678903
32019012
01329801
10456732`,
			extraArgs: [],
			expected: `36`
		}];

	const part2tests: TestCase[] = [{
		input: `89010123
78121874
87430965
96549874
45678903
32019012
01329801
10456732`,
		extraArgs: [],
		expected: `81`
	}];

	const [p1testsNormalized, p2testsNormalized] = normalizeTestCases(part1tests, part2tests);

	// Run tests
	test.beginTests();
	await test.section(async () => {
		for (const testCase of p1testsNormalized) {
			test.logTestResult(testCase, String(await p2024day10_part1(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	await test.section(async () => {
		for (const testCase of p2testsNormalized) {
			test.logTestResult(testCase, String(await p2024day10_part2(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	test.endTests();

	// Get input and run program while measuring performance
	const input = await util.getInput(DAY, YEAR);

	const part1Before = performance.now();
	const part1Solution = String(await p2024day10_part1(input));
	const part1After = performance.now();

	const part2Before = performance.now()
	const part2Solution = String(await p2024day10_part2(input));
	const part2After = performance.now();

	logSolution(10, 2024, part1Solution, part2Solution);

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

import _ from "lodash";
import * as util from "../../../util/util";
import * as test from "../../../util/test";
import chalk from "chalk";
import { log, logSolution, trace } from "../../../util/log";
import { performance } from "perf_hooks";
import { normalizeTestCases } from "../../../util/test";
import { Grid } from "../../../util/grid";
import { Dir } from "node:fs";

const YEAR = 2024;
const DAY = 18;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\18\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\18\data.txt
// problem url  : https://adventofcode.com/2024/day/18

const directions = [
	[0, 1],
	[1, 0],
	[0, -1],
	[-1, 0],
];

function bfsShortestPath(grid: Grid, start: [number, number], end: [number, number]): number | null {
	const queue: [[number, number], number][] = [[start, 0]];
	const visited = new Set<string>();

	while (queue.length > 0) {
		const [[row, col], distance] = queue.shift()!;
		const posKey = `${row},${col}`;

		if (row === end[0] && col === end[1])
			return distance;

		if (visited.has(posKey))
			continue;

		visited.add(posKey);

		for (const [dRow, dCol] of directions) {
			const newRow = row + dRow;
			const newCol = col + dCol;
			const nextCell = grid.getCell([newRow, newCol]);

			if (nextCell && nextCell.value !== "#" && !visited.has(`${newRow},${newCol}`))
				queue.push([[newRow, newCol], distance + 1]);
		}
	}

	return null;
}

async function p2024day18_part1(input: string, ...params: any[]) {
	const coordinates = input.split("\n").map(line => line.split(",").map(Number));
	const gridSize = Number(params[0]) + 1;
	const numberOfBytes = Number(params[1]);
	let grid = new Grid({rowCount: gridSize, colCount: gridSize});

	// fill grid
	for(let i = 0; i <= numberOfBytes - 1; i++) {
		const coord = coordinates[i];
		grid.setCell([coord[1], coord[0]], "#");
	}

	// find shortest path
	return bfsShortestPath(grid, [0, 0], [gridSize - 1, gridSize - 1])!;
}

async function p2024day18_part2(input: string, ...params: any[]) {
	const coordinates = input.split("\n").map(line => line.split(",").map(Number));
	const gridSize = Number(params[0]) + 1;
	let grid = new Grid({rowCount: gridSize, colCount: gridSize});

	// fill grid
	for(let i = 0; i <= coordinates.length - 1; i++) {
		const coord = coordinates[i];
		grid.setCell([coord[1], coord[0]], "#");

		const result = bfsShortestPath(grid, [0, 0], [gridSize - 1, gridSize - 1])!;
		if(result === null)
			return `${coord[0]},${coord[1]}`;
	}

	// find shortest path
	return -1;
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `5,4
4,2
4,5
3,0
2,1
6,3
2,4
1,5
0,6
3,3
2,6
5,1
1,2
5,5
2,5
6,5
1,4
0,4
6,4
1,1
6,1
1,0
0,5
1,6
2,0`,
		extraArgs: [6, 12],
		expected: `22`
	}];

	const part2tests: TestCase[] = [{
		input: `5,4
4,2
4,5
3,0
2,1
6,3
2,4
1,5
0,6
3,3
2,6
5,1
1,2
5,5
2,5
6,5
1,4
0,4
6,4
1,1
6,1
1,0
0,5
1,6
2,0`,
		extraArgs: [6],
		expected: `6,1`
	}];
	const [p1testsNormalized, p2testsNormalized] = normalizeTestCases(part1tests, part2tests);

	// Run tests
	test.beginTests();
	await test.section(async () => {
		for (const testCase of p1testsNormalized) {
			test.logTestResult(testCase, String(await p2024day18_part1(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	await test.section(async () => {
		for (const testCase of p2testsNormalized) {
			test.logTestResult(testCase, String(await p2024day18_part2(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	test.endTests();

	// Get input and run program while measuring performance
	const input = await util.getInput(DAY, YEAR);

	const part1Before = performance.now();
	const part1Solution = String(await p2024day18_part1(input, 70, 1024));
	const part1After = performance.now();

	const part2Before = performance.now()
	const part2Solution = String(await p2024day18_part2(input, 70));
	const part2After = performance.now();

	logSolution(18, 2024, part1Solution, part2Solution);

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

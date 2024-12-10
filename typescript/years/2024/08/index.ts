import _ from "lodash";
import * as util from "../../../util/util";
import * as test from "../../../util/test";
import chalk from "chalk";
import { log, logSolution, trace } from "../../../util/log";
import { performance } from "perf_hooks";
import { normalizeTestCases } from "../../../util/test";
import { Cell, computeDirection, Grid, GridPos } from "../../../util/grid";

const YEAR = 2024;
const DAY = 8;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\08\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\08\data.txt
// problem url  : https://adventofcode.com/2024/day/8

function addCharactersInLine(grid: Grid, cell1: Cell, cell2: Cell, char1: string): Set<string> {
	const [row1, col1] = cell1.position;
	const [row2, col2] = cell2.position;

	const deltaRow = row2 - row1;
	const deltaCol = col2 - col1;

	const char1Pos: GridPos = [row1 - deltaRow, col1 - deltaCol];
	const char2Pos: GridPos = [row2 + deltaRow, col2 + deltaCol];

	// bounds?

	const uniquePositions = new Set<string>();
	uniquePositions.add(`${char1Pos[0]},${char1Pos[1]}`);
	uniquePositions.add(`${char2Pos[0]},${char2Pos[1]}`);

	return uniquePositions;
}

async function p2024day8_part1(input: string, ...params: any[]) {
	const grid = new Grid({ serialized: input });
	const visited = new Set<string>();
	const working = new Set<Cell>();

	for(let row = 0; row < grid.rowCount; row++) {
		for(let col = 0; col < grid.colCount; col++) {
			const cell = grid.getCell([row, col]);
			if(cell?.value !== '.') {
				working.add(cell!);

				if(working.size === 2) {
					const firstCell = Array.from(working)[0];
					const secondCell = Array.from(working)[1];
					let positions : Set<string> = addCharactersInLine(grid, firstCell, secondCell, '#');
					positions.forEach(pos => visited.add(pos));
					working.clear();
				}
			}
		}
	}
	return visited.size;
}

async function p2024day8_part2(input: string, ...params: any[]) {
	return "Not implemented";
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `..........
..........
..........
....a.....
..........
.....a....
..........
..........
..........
..........`,
		extraArgs: [],
		expected: `2`
	}, {
		input: `............
........0...
.....0......
.......0....
....0.......
......A.....
............
............
........A...
.........A..
............
............`,
		extraArgs: [],
		expected: `14`
}];

	const part2tests: TestCase[] = [];

	const [p1testsNormalized, p2testsNormalized] = normalizeTestCases(part1tests, part2tests);

	// Run tests
	test.beginTests();
	await test.section(async () => {
		for (const testCase of p1testsNormalized) {
			test.logTestResult(testCase, String(await p2024day8_part1(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	await test.section(async () => {
		for (const testCase of p2testsNormalized) {
			test.logTestResult(testCase, String(await p2024day8_part2(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	test.endTests();

	// Get input and run program while measuring performance
	const input = await util.getInput(DAY, YEAR);

	const part1Before = performance.now();
	const part1Solution = String(await p2024day8_part1(input));
	const part1After = performance.now();

	const part2Before = performance.now()
	const part2Solution = String(await p2024day8_part2(input));
	const part2After = performance.now();

	logSolution(8, 2024, part1Solution, part2Solution);

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

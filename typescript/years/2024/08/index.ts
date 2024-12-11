import * as util from "../../../util/util";
import * as test from "../../../util/test";
import chalk from "chalk";
import { log, logSolution, trace } from "../../../util/log";
import { performance } from "perf_hooks";
import { normalizeTestCases } from "../../../util/test";
import { Grid, GridPos } from "../../../util/grid";

const YEAR = 2024;
const DAY = 8;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\08\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\08\data.txt
// problem url  : https://adventofcode.com/2024/day/8

function findAntennas(grid: Grid): Map<string, GridPos[]> {
	const rows = grid.rowCount;
	const cols = grid.colCount;
	const antennas: Map<string, GridPos[]> = new Map();

	for (let r = 0; r < rows; r++) {
		for (let c = 0; c < cols; c++) {
			const cell = grid.getCell([r, c])!;
			if (cell.value !== '.') {
				if (!antennas.has(cell.value))
					antennas.set(cell.value, []);

				antennas.get(cell.value)!.push(cell.position);
			}
		}
	}
	return antennas;
}

function findAntinode(antenna1: GridPos, antenna2: GridPos): GridPos{
	const dy = antenna2[0] - antenna1[0];
	const dx = antenna2[1] - antenna1[1];

	return [antenna1[0] - dy, antenna1[1] - dx];
}

async function p2024day8_part1(input: string, ...params: any[]) {
	const antinodePositions = new Set<string>();
	const grid = new Grid({ serialized: input });
	const antennas = findAntennas(grid);
	for(const [_, value] of antennas) {
		for (const antenna1 of value) {
			for (const antenna2 of value) {
				if (antenna1 === antenna2)
					continue;

				const antinode = findAntinode(antenna1, antenna2);

				if(grid.isValidPos(antinode))
					antinodePositions.add(`${antinode[0]}, ${antinode[1]}`);
			}
		}
	}

	return antinodePositions.size;
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

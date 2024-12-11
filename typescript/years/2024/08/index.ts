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

function areCollinear(point: GridPos, antenna1: GridPos, antenna2: GridPos): boolean {
	return (point[1] - antenna1[1]) * (antenna2[0] - antenna1[0]) ===
		(point[0] - antenna1[0]) * (antenna2[1] - antenna1[1]);
}

async function p2024day8_part1(input: string, ...params: any[]) {
	const antinodePositions = new Set<string>();
	const grid = new Grid({ serialized: input });
	for(const [_, antennas] of findAntennas(grid)) {
		for (const antenna1 of antennas) {
			for (const antenna2 of antennas) {
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
	const antinodePositions = new Set<string>();
	const grid = new Grid({ serialized: input });
	for(const [_, antennas] of findAntennas(grid)) {
		if(antennas.length < 2)
			continue;

		for (let row = 0; row < grid.rowCount; row++) {
			for (let col = 0; col < grid.colCount; col++) {

				// For each pair of antennas
				for (let i = 0; i < antennas.length; i++) {
					let foundCollinear = false;

					for (let j = i + 1; j < antennas.length; j++) {
						const antenna1 = antennas[i];
						const antenna2 = antennas[j];

						if (areCollinear([row, col], antenna1, antenna2)) {
							antinodePositions.add(`${row}, ${col}`);
							foundCollinear = true;
							break;
						}
					}

					if (foundCollinear) break;
				}
			}
		}
	}

	return antinodePositions.size;
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

	const part2tests: TestCase[] = [{
		input: `T.........
...T......
.T........
..........
..........
..........
..........
..........
..........
..........`,
		extraArgs: [],
		expected: `9`
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
		expected: `34`
	}];

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

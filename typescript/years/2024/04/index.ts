import _ from "lodash";
import * as util from "../../../util/util";
import * as test from "../../../util/test";
import chalk from "chalk";
import { log, logSolution, trace } from "../../../util/log";
import { performance } from "perf_hooks";
import { normalizeTestCases } from "../../../util/test";

const YEAR = 2024;
const DAY = 4;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\04\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\04\data.txt
// problem url  : https://adventofcode.com/2024/day/4

const word = "XMAS";

const directions = [
	[0, 1],  // Horizontal (right)
	[0, -1], // Horizontal (left)
	[1, 0],  // Vertical (down)
	[-1, 0], // Vertical (up)
	[1, 1],  // Diagonal (down-right)
	[-1, -1],// Diagonal (up-left)
	[1, -1], // Diagonal (down-left)
	[-1, 1]  // Diagonal (up-right)
];

function findAllStartChars(startChar: string, grid: string[] ) {
	const xPositions: [number, number][] = [];
	for (let row = 0; row < grid.length; row++) {
		for (let col = 0; col < grid[row].length; col++) {
			if (grid[row][col] === startChar) {
				xPositions.push([row, col]);
			}
		}
	}
	return xPositions;
}


function findWord(grid: string[], word: string, startRow: number, startCol: number, direction: number[]): boolean {
	const [rowOffset, colOffset] = direction;
	for (let i = 0; i < word.length; i++) {
		const row = startRow + i * rowOffset;
		const col = startCol + i * colOffset;
		if (row < 0 || row >= grid.length || col < 0 || col >= grid[0].length || grid[row][col] !== word[i]) {
			return false;
		}
	}
	return true;
}

function isMASorSAM(diagonal: [number, number][], grid: string[]): boolean {
	const [first, second, third] = diagonal.map(([r, c]) => grid[r][c]);

	if (first === 'M' && second === 'A' && third === 'S')
		return true;

	return first === 'S' && second === 'A' && third === 'M';
}

async function p2024day4_part1(input: string, ...params: any[]) {
	const grid = input.split("\n");
	const xPositions = findAllStartChars('X', grid);
	let count = 0;
	for (const [startRow, startCol] of xPositions) {
		for (const direction of directions) {
			if (findWord(grid, word, startRow, startCol, direction)) {
				count++;
			}
		}
	}
	return count;
}

async function p2024day4_part2(input: string, ...params: any[]) {
	let count = 0;
	const grid = input.split("\n");
	const aPositions = findAllStartChars('A', grid);

	for( const [startRow, startCol] of aPositions) {
		const firstDiagonal: [number, number][] = [
			[startRow - 1, startCol - 1], [startRow, startCol], [startRow + 1, startCol + 1]  // Top-left to bottom-right
		];
		const secondDiagonal: [number, number][] = [
			[startRow - 1, startCol + 1], [startRow, startCol], [startRow + 1, startCol - 1]  // Top-right to bottom-left
		];

		const isFirstDiagonalValid = firstDiagonal.every(([row, col]) =>
			row >= 0 && row < grid.length && col >= 0 && col < grid[row].length
		);

		const isSecondDiagonalValid = secondDiagonal.every(([row, col]) =>
			row >= 0 && row < grid.length && col >= 0 && col < grid[row].length
		);

		if (isFirstDiagonalValid && isMASorSAM(firstDiagonal, grid) &&
			isSecondDiagonalValid && isMASorSAM(secondDiagonal, grid)) {
			count++;
		}
	}

	return count;
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX`,
		extraArgs: [],
		expected: `18`
	}];

	const part2tests: TestCase[] = [{
		input: `MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX`,
		extraArgs: [],
		expected: `9`
	}];
	const [p1testsNormalized, p2testsNormalized] = normalizeTestCases(part1tests, part2tests);

	// Run tests
	test.beginTests();
	await test.section(async () => {
		for (const testCase of p1testsNormalized) {
			test.logTestResult(testCase, String(await p2024day4_part1(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	await test.section(async () => {
		for (const testCase of p2testsNormalized) {
			test.logTestResult(testCase, String(await p2024day4_part2(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	test.endTests();

	// Get input and run program while measuring performance
	const input = await util.getInput(DAY, YEAR);

	const part1Before = performance.now();
	const part1Solution = String(await p2024day4_part1(input));
	const part1After = performance.now();

	const part2Before = performance.now()
	const part2Solution = String(await p2024day4_part2(input));
	const part2After = performance.now();

	logSolution(4, 2024, part1Solution, part2Solution);

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

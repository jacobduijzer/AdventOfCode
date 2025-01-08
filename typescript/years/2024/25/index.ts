import _ from "lodash";
import * as util from "../../../util/util";
import * as test from "../../../util/test";
import chalk from "chalk";
import { log, logSolution, trace } from "../../../util/log";
import { performance } from "perf_hooks";
import { normalizeTestCases } from "../../../util/test";

const YEAR = 2024;
const DAY = 25;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\25\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\25\data.txt
// problem url  : https://adventofcode.com/2024/day/25


// FIRST SOLUTION (my own)
function parseBlocks(input: string): { keys: number[][]; locks: number[][] } {
	const blocks = input.trim().split("\n\n");
	const keys: number[][] = [];
	const locks: number[][] = [];

	blocks.forEach((block) => {
		const lines = block.split("\n");
		let relevantLines: string[];

		if (block.startsWith("#####"))
			relevantLines = lines.slice(1);
		else if (block.startsWith("....."))
			relevantLines = lines.slice(0, -1);
		else return;

		const columnCounts = Array(relevantLines[0].length).fill(0);

		relevantLines.forEach((line) => {
			line.split("").forEach((char, index) => {
				if (char === "#")
					columnCounts[index]++;
			});
		});

		if (block.startsWith("#####"))
			locks.push(columnCounts);
		else if (block.startsWith("....."))
			keys.push(columnCounts);
	});

	return { keys, locks };
}

// BETTER SOLUTION
const mapKeyOrLock = (desc: string) =>
	desc
		.split('\n')
		.map(row => row.split('').map((char): number => (char === '#' ? 1 : 0)))
		.reduce((total, row) => total.map((x, i) => x + row[i]));

async function p2024day25_part1(input: string, ...params: any[]) {
	// my own parsing solution
	// const { locks, keys } = parseBlocks(input);
	const keysAndLocks = input.trim().split('\n\n');
	const keys = keysAndLocks.filter(s => s.startsWith('.')).map(mapKeyOrLock);
	const locks = keysAndLocks.filter(s => s.startsWith('#')).map(mapKeyOrLock);
	// const length = 5; // i am excluding the first row of each block
	const length = 7; // better solution is using all rows
	return keys
		.map(key => locks.filter(lock => !lock.some((x, i) => x + key[i] > length)).length)
		.reduce((sum, val) => sum + val, 0);
}

async function p2024day25_part2(input: string, ...params: any[]) {
	return "Not implemented";
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `#####
.####
.####
.####
.#.#.
.#...
.....

#####
##.##
.#.##
...##
...#.
...#.
.....

.....
#....
#....
#...#
#.#.#
#.###
#####

.....
.....
#.#..
###..
###.#
###.#
#####

.....
.....
.....
#....
#.#..
#.#.#
#####`,
		extraArgs: [],
		expected: `3`,
	}];

	const part2tests: TestCase[] = [];

	const [p1testsNormalized, p2testsNormalized] = normalizeTestCases(part1tests, part2tests);

	// Run tests
	test.beginTests();
	await test.section(async () => {
		for (const testCase of p1testsNormalized) {
			test.logTestResult(testCase, String(await p2024day25_part1(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	await test.section(async () => {
		for (const testCase of p2testsNormalized) {
			test.logTestResult(testCase, String(await p2024day25_part2(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	test.endTests();

	// Get input and run program while measuring performance
	const input = await util.getInput(DAY, YEAR);

	const part1Before = performance.now();
	const part1Solution = String(await p2024day25_part1(input));
	const part1After = performance.now();

	const part2Before = performance.now();
	const part2Solution = String(await p2024day25_part2(input));
	const part2After = performance.now();

	logSolution(25, 2024, part1Solution, part2Solution);

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

import _ from "lodash";
import * as util from "../../../util/util";
import * as test from "../../../util/test";
import chalk from "chalk";
import { log, logSolution, trace } from "../../../util/log";
import { performance } from "perf_hooks";
import { normalizeTestCases } from "../../../util/test";

const YEAR = 2024;
const DAY = 1;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\01\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\01\data.txt
// problem url  : https://adventofcode.com/2024/day/1

async function p2024day1_part1(input: string, ...params: any[]) {
	let data = parseRowsToArrays(input);

	data[0].sort((a, b) => a - b);
	data[1].sort((a, b) => a - b);

	let dist = 0;
	for(let i = 0; i < data[0].length; i++) {
		dist += Math.abs(data[0][i] - data[1][i]);
	}

	return dist;
}

async function p2024day1_part2(input: string, ...params: any[]) {
	let data = parseRowsToArrays(input);

	let sum = 0;
	for(let i = 0; i < data[0].length; i++) {
		const target = data[0][i];
		const count = data[1].filter(item => item === target).length;
		sum += target * count;
	}

	return sum;
}

function parseRowsToArrays(input: string): [number[], number[]] {
	const rows = input.trim().split('\n').map(row => row.split('  ').map(Number));

	const array1: number[] = [];
	const array2: number[] = [];

	rows.forEach(row => {
		array1.push(row[0]); // First number of the pair
		array2.push(row[1]); // Second number of the pair
	});

	return [array1, array2];
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `3   4
4   3
2   5
1   3
3   9
3   3`,
		extraArgs: [],
		expected: `11`
	}
	];
	const part2tests: TestCase[] = [{
		input: `3   4
4   3
2   5
1   3
3   9
3   3`,
		extraArgs: [],
		expected: `31`
	}
	];
	const [p1testsNormalized, p2testsNormalized] = normalizeTestCases(part1tests, part2tests);

	// Run tests
	test.beginTests();
	await test.section(async () => {
		for (const testCase of p1testsNormalized) {
			test.logTestResult(testCase, String(await p2024day1_part1(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	await test.section(async () => {
		for (const testCase of p2testsNormalized) {
			test.logTestResult(testCase, String(await p2024day1_part2(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	test.endTests();

	// Get input and run program while measuring performance
	const input = await util.getInput(DAY, YEAR);

	const part1Before = performance.now();
	const part1Solution = String(await p2024day1_part1(input));
	const part1After = performance.now();

	const part2Before = performance.now()
	const part2Solution = String(await p2024day1_part2(input));
	const part2After = performance.now();

	logSolution(1, 2024, part1Solution, part2Solution);

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

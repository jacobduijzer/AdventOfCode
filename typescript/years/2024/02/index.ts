import _ from "lodash";
import * as util from "../../../util/util";
import * as test from "../../../util/test";
import chalk from "chalk";
import { log, logSolution, trace } from "../../../util/log";
import { performance } from "perf_hooks";
import { normalizeTestCases } from "../../../util/test";

const YEAR = 2024;
const DAY = 2;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\02\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\02\data.txt
// problem url  : https://adventofcode.com/2024/day/2

async function p2024day2_part1(input: string, ...params: any[]) {
	const reports = input
		.trim()
		.split('\n')
		.map(row => row.split(" ").map(Number));	let sum = 0;

	return reports.filter(isSafe).length;
}

async function p2024day2_part2(input: string, ...params: any[]) {
	const reports = input
		.trim()
		.split('\n')
		.map(row => row.split(" ").map(Number));	let sum = 0;

	let safe = 0;

	for(const report of reports) {
		if(isSafe(report))
			safe++;
		else {
			for(let i = 0; i < report.length; i++) {
				const removed = [...report.slice(0, i), ...report.slice(i + 1)];
				if(isSafe(removed))
				{
					safe++;
					break;
				}
			}
		}
	}

	return safe;
}

function isSafe(levels: number[]) {
	const allowedDifferences = new Set([-1, -2, -3, 1, 2, 3]);
	const differences: number[] = [];

	for (let i = 1; i < levels.length; i++) {
		differences.push(levels[i] - levels[i - 1]);
	}

	// Low hanging fruit first
	const containsInvalidDifference = differences.some(d => !allowedDifferences.has(d));
	if (containsInvalidDifference)
		return false;

	const increasing = differences.every((d) => d >= 1 && d <= 3);
	const decreasing = differences.every((d) => d <= -1 && d >= -3);

	return increasing || decreasing;
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9`,
		extraArgs: [],
		expected: `2`
	}
	];
	const part2tests: TestCase[] = [{
		input: `7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9`,
		extraArgs: [],
		expected: `4`
	}
	];

	const [p1testsNormalized, p2testsNormalized] = normalizeTestCases(part1tests, part2tests);

	// Run tests
	test.beginTests();
	await test.section(async () => {
		for (const testCase of p1testsNormalized) {
			test.logTestResult(testCase, String(await p2024day2_part1(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	await test.section(async () => {
		for (const testCase of p2testsNormalized) {
			test.logTestResult(testCase, String(await p2024day2_part2(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	test.endTests();

	// Get input and run program while measuring performance
	const input = await util.getInput(DAY, YEAR);

	const part1Before = performance.now();
	const part1Solution = String(await p2024day2_part1(input));
	const part1After = performance.now();

	const part2Before = performance.now()
	const part2Solution = String(await p2024day2_part2(input));
	const part2After = performance.now();

	logSolution(2, 2024, part1Solution, part2Solution);

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

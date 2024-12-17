import _ from "lodash";
import * as util from "../../../util/util";
import * as test from "../../../util/test";
import chalk from "chalk";
import { log, logSolution, trace } from "../../../util/log";
import { performance } from "perf_hooks";
import { normalizeTestCases } from "../../../util/test";

const YEAR = 2015;
const DAY = 1;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2015\01\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2015\01\data.txt
// problem url  : https://adventofcode.com/2015/day/1

async function p2015day1_part1(input: string, ...params: any[]) {
	// SOLUTION 1
	// const stairPlan = input.split('');
	// let floor = 0;
	// for (const step of stairPlan) {
	// 	if (step === '(') {
	// 		floor++;
	// 	} else if (step === ')') {
	// 		floor--;
	// 	}
	// }
	//
	// return floor;

	// SOLUTION 2
	// return input
	// 	.split('')
	// 	.map(step => step === '(' ? 1 : -1)
	// 	.reduce((floor, change) => floor + change, 0);

	// SOLUTION 3
	let floor = 0;
	for (const step of input) {
		floor += step === '(' ? 1 : -1;
	}
	return floor;
}

async function p2015day1_part2(input: string, ...params: any[]) {
	let floor = 0;
	for (let i = 0; i < input.length; i++) {
		floor += input[i] === '(' ? 1 : -1;
		if (floor === -1)
			return i + 1;
	}
	return -1;
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `(())`,
		extraArgs: [],
		expected: `0`
	},{
		input: `()()`,
		extraArgs: [],
		expected: `0`
	},{
		input: `(((`,
		extraArgs: [],
		expected: `3`
	},{
		input: `(()(()(`,
		extraArgs: [],
		expected: `3`
	},{
		input: `))(((((`,
		extraArgs: [],
		expected: `3`
	},{
		input: `())`,
		extraArgs: [],
		expected: `-1`
	},{
		input: `))(`,
		extraArgs: [],
		expected: `-1`
	}, {
		input: `)))`,
		extraArgs: [],
		expected: `-3`
	}, {
		input: `)())())`,
		extraArgs: [],
		expected: `-3`
	}];

	const part2tests: TestCase[] = [{
		input: `)`,
		extraArgs: [],
		expected: `1`
	},{
		input: `()())`,
		extraArgs: [],
		expected: `5`
	}];

	const [p1testsNormalized, p2testsNormalized] = normalizeTestCases(part1tests, part2tests);

	// Run tests
	test.beginTests();
	await test.section(async () => {
		for (const testCase of p1testsNormalized) {
			test.logTestResult(testCase, String(await p2015day1_part1(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	await test.section(async () => {
		for (const testCase of p2testsNormalized) {
			test.logTestResult(testCase, String(await p2015day1_part2(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	test.endTests();

	// Get input and run program while measuring performance
	const input = await util.getInput(DAY, YEAR);

	const part1Before = performance.now();
	const part1Solution = String(await p2015day1_part1(input));
	const part1After = performance.now();

	const part2Before = performance.now()
	const part2Solution = String(await p2015day1_part2(input));
	const part2After = performance.now();

	logSolution(1, 2015, part1Solution, part2Solution);

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

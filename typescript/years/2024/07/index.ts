import _, { eq } from "lodash";
import * as util from "../../../util/util";
import * as test from "../../../util/test";
import chalk from "chalk";
import { log, logSolution, trace } from "../../../util/log";
import { performance } from "perf_hooks";
import { normalizeTestCases } from "../../../util/test";

const YEAR = 2024;
const DAY = 7;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\07\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\07\data.txt
// problem url  : https://adventofcode.com/2024/day/7


type Equation = {
	result: number;
	numbers: number[];
};

function parseData(input: string): Equation[] {
	return input
		.trim()
		.split('\n')
		.map(line => {
			const [mainNum, ...associatedNums] = line.split(':');
			return {
				result: parseInt(mainNum.trim()),
				numbers: associatedNums[0].trim().split(' ').map(num => parseInt(num))
			}
		});
}

function subtract(a: number, b: number): [number, number] {
	const bStr = b.toString();
	const aStr = a.toString();
	if (aStr.endsWith(bStr)) {
		const aSub = parseInt(aStr.slice(0, -bStr.length), 10);
		return [aSub, b];
	}
	return [a, 0];
}

function isPossibleReverse({ result, numbers }: Equation, withConcatenate = false): number {
	if (numbers.length === 0) return 0;

	const _isPossible = (result: number, idx = numbers.length - 1): boolean => {
		if (idx < 0)
			return result === 0;

		const n = numbers[idx];

		if (result % n === 0 && _isPossible(result / n, idx - 1))
			return true;

		if (_isPossible(result - n, idx - 1))
			return true;

		if (withConcatenate) {
			const [a, b] = subtract(result, n);
			if (b === n) return _isPossible(a, idx - 1);
		}
		return false;
	};

	return _isPossible(result) ? result : 0;
}

async function p2024day7_part1(input: string, ...params: any[]) {
	const data = parseData(input);
	let totalCalibrationResult = 0;

	for(const equation of data) {
		if (isPossibleReverse(equation, false))
			totalCalibrationResult += equation.result;
	}

	return totalCalibrationResult;
}

async function p2024day7_part2(input: string, ...params: any[]) {
	const data = parseData(input);
	let totalCalibrationResult = 0;

	for (const equation of data) {
		if (isPossibleReverse(equation, true))
		 	totalCalibrationResult += equation.result;
	}

	return totalCalibrationResult;
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20`,
		extraArgs: [],
		expected: `3749`
	}];

	const part2tests: TestCase[] = [{
			input: `156: 15 6`,
			extraArgs: [],
			expected: `156`
		}, {
			input: `192: 17 8 14`,
			extraArgs: [],
			expected: `192`
		}, {
		input: `190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20`,
		extraArgs: [],
		expected: `11387`
	}];

	const [p1testsNormalized, p2testsNormalized] = normalizeTestCases(part1tests, part2tests);

	// Run tests
	test.beginTests();
	await test.section(async () => {
		for (const testCase of p1testsNormalized) {
			test.logTestResult(testCase, String(await p2024day7_part1(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	await test.section(async () => {
		for (const testCase of p2testsNormalized) {
			test.logTestResult(testCase, String(await p2024day7_part2(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	test.endTests();

	// Get input and run program while measuring performance
	const input = await util.getInput(DAY, YEAR);

	const part1Before = performance.now();
	const part1Solution = String(await p2024day7_part1(input));
	const part1After = performance.now();

	const part2Before = performance.now()
	const part2Solution = String(await p2024day7_part2(input));
	const part2After = performance.now();

	logSolution(7, 2024, part1Solution, part2Solution);

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

import _ from "lodash";
import * as util from "../../../util/util";
import * as test from "../../../util/test";
import chalk, { reset } from "chalk";
import { log, logSolution, trace } from "../../../util/log";
import { performance } from "perf_hooks";
import { normalizeTestCases } from "../../../util/test";

const YEAR = 2024;
const DAY = 11;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\11\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\11\data.txt
// problem url  : https://adventofcode.com/2024/day/11

// Part 1, inefficient solution
function transformStones(initialStones: number[], blinks: number): number[] {

	const transformStone = (stone: number): number[] => {
		const stoneStr = stone.toString();

		if (stone === 0) {
			// Rule 1: Stone with number 0 becomes 1
			return [1];
		} else if (stoneStr.length % 2 === 0) {
			// Rule 2: Even number of digits - split in half
			const mid = stoneStr.length / 2;
			const left = parseInt(stoneStr.slice(0, mid), 10);
			const right = parseInt(stoneStr.slice(mid), 10);
			return [left, right];
		} else {
			// Rule 3: Multiply by 2024
			return [stone * 2024];
		}
	};

	let currentStones = [...initialStones];

	for (let i = 0; i < blinks; i++) {
		const newStones: number[] = [];
		for (const stone of currentStones)
			newStones.push(...transformStone(stone));
		
		currentStones = newStones;
	}

	return currentStones;
}

function transformStonesEfficient(initialStones: number[], blinks: number): { [stone: number]: number } {
	const transformStone = (stone: number, count: number, stoneCounts: { [key: number]: number }): void => {
		const stoneStr = stone.toString();
		if (stone === 0) {
			stoneCounts[1] = (stoneCounts[1] || 0) + count; // Rule 1
		} else if (stoneStr.length % 2 === 0) {
			// Rule 2: Split into two stones
			const mid = stoneStr.length / 2;
			const left = parseInt(stoneStr.slice(0, mid), 10);
			const right = parseInt(stoneStr.slice(mid), 10);
			stoneCounts[left] = (stoneCounts[left] || 0) + count;
			stoneCounts[right] = (stoneCounts[right] || 0) + count;
		} else {
			// Rule 3: Multiply by 2024
			const newStone = stone * 2024;
			stoneCounts[newStone] = (stoneCounts[newStone] || 0) + count;
		}
	};

	let stoneCounts: { [stone: number]: number } = {};
	for (const stone of initialStones)
		stoneCounts[stone] = (stoneCounts[stone] || 0) + 1;


	for (let i = 0; i < blinks; i++) {
		const newCounts: { [stone: number]: number } = {};
		for (const [stone, count] of Object.entries(stoneCounts))
			transformStone(Number(stone), count, newCounts);

		stoneCounts = newCounts;
	}

	return stoneCounts;
}


async function p2024day11_part1(input: string, ...params: any[]) {
	const numberOfBlinks = !isNaN(Number(params[0])) ? Number(params[0]) : 25;
	const stones = input.split(" ").map(Number);
	const result = transformStonesEfficient(stones, numberOfBlinks);
	return Object.values(result).reduce((a, b) => a + b, 0);
}

async function p2024day11_part2(input: string, ...params: any[]) {
	const numberOfBlinks = !isNaN(Number(params[0])) ? Number(params[0]) : 75;
	const stones = input.split(" ").map(Number);
	const result = transformStonesEfficient(stones, numberOfBlinks);
	return Object.values(result).reduce((a, b) => a + b, 0);
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `0 1 10 99 999`,
		extraArgs: [1],
		expected: `7`,
	},{
		input: `125 17`,
		extraArgs: [1],
		expected: `3`,
	}, {
		input: `125 17`,
		extraArgs: [6],
		expected: `22`,
	}];
	const part2tests: TestCase[] = [];

	const [p1testsNormalized, p2testsNormalized] = normalizeTestCases(part1tests, part2tests);

	// Run tests
	test.beginTests();
	await test.section(async () => {
		for (const testCase of p1testsNormalized) {
			test.logTestResult(testCase, String(await p2024day11_part1(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	await test.section(async () => {
		for (const testCase of p2testsNormalized) {
			test.logTestResult(testCase, String(await p2024day11_part2(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	test.endTests();

	// Get input and run program while measuring performance
	const input = await util.getInput(DAY, YEAR);

	const part1Before = performance.now();
	const part1Solution = String(await p2024day11_part1(input));
	const part1After = performance.now();

	const part2Before = performance.now();
	const part2Solution = String(await p2024day11_part2(input));
	const part2After = performance.now();

	logSolution(11, 2024, part1Solution, part2Solution);

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

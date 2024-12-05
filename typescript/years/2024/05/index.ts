import * as util from "../../../util/util";
import * as test from "../../../util/test";
import { normalizeTestCases } from "../../../util/test";
import chalk from "chalk";
import { log, logSolution } from "../../../util/log";
import { performance } from "perf_hooks";

const YEAR = 2024;
const DAY = 5;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\05\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\05\data.txt
// problem url  : https://adventofcode.com/2024/day/5

function parseStringToMapWithArrays(data: string): Map<number, number[]> {
	const map = new Map<number, number[]>();
	data.trim().split('\n').forEach(line => {
		const [key, value] = line.split('|').map(Number);
		if (!map.has(key)) {
			map.set(key, []);
		}
		map.get(key)?.push(value);
	});
	return map;
}

function parseInput(input: string): { orderingRules: Map<number, number[]>, pagesToProduce: number[][] } {
	const parts = input.split("\n\n");
	const orderingRules = parseStringToMapWithArrays(parts[0]);
	const pagesToProduce = parts[1].split('\n').map(line => line.split(',').map(Number));
	return { orderingRules, pagesToProduce };
}


function validateOrder(numbers: number[], rules: Map<number, number[]>): boolean {

	for (let i = 0; i < numbers.length; i++) {
		const current = numbers[i];

		// Check to the right (for all numbers except the last)
		if (i < numbers.length - 1) {
			const right = numbers[i + 1];
			if (rules.has(current)) {
				const validRight = rules.get(current)!;
				if (!validRight.includes(right))
					return false;
			}
		}

		// Check to the left (for all numbers except the first)
		if (i > 0) {
			const left = numbers[i - 1];
			if (rules.has(current)) {
				const validLeft = rules.get(current)!;
				if (validLeft.includes(left))
					return false;
			}
		}
	}

	return true;
}

function sumMiddleNumbers (arrays: number[][]): number {
	return arrays.reduce((sum, array) =>
		sum + array[Math.floor(array.length / 2)], 0);
}

async function p2024day5_part1(input: string, ...params: any[]) {
	const { orderingRules, pagesToProduce } = parseInput(input);
	const correctLines = pagesToProduce.filter(page => validateOrder(page, orderingRules));
	return sumMiddleNumbers(correctLines);
}

function topologicalSort(rules: Map<number, number[]>, numbers: number[]): number[] {
	const inDegree: { [key: number]: number } = {};
	numbers.forEach(num => {
		inDegree[num] = 0;
	});

	rules.forEach((dependencies, num) => {
		if (numbers.includes(num)) {
			dependencies.forEach(dep => {
				if (numbers.includes(dep)) {
					inDegree[dep]++;
				}
			});
		}
	});

	const queue: number[] = [];
	numbers.forEach(num => {
		if (inDegree[num] === 0) {
			queue.push(num);
		}
	});

	const sorted: number[] = [];
	while (queue.length > 0) {
		const current = queue.shift()!;
		sorted.push(current);
		if (rules.has(current)) {
			rules.get(current)!.forEach(dep => {
				if (numbers.includes(dep)) {
					inDegree[dep]--;
					if (inDegree[dep] === 0) {
						queue.push(dep);
					}
				}
			});
		}
	}

	numbers.forEach(num => {
		if (!sorted.includes(num)) {
			sorted.push(num);
		}
	});

	return sorted;
}

async function p2024day5_part2(input: string, ...params: any[]) {
	const { orderingRules, pagesToProduce } = parseInput(input);
	const incorrectLines = pagesToProduce.filter(page => !validateOrder(page, orderingRules));
	const correctedLines = incorrectLines.map(page => topologicalSort(orderingRules, page));
	return sumMiddleNumbers(correctedLines);
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `47|53
97|13
97|61
97|47
75|29
61|13
75|53
29|13
97|29
53|29
61|53
97|53
61|29
47|13
75|47
97|75
47|61
75|61
47|29
75|13
53|13

75,47,61,53,29
97,61,53,29,13
75,29,13
75,97,47,61,53
61,13,29
97,13,75,29,47`,
		extraArgs: [],
		expected: `143`
	}];

	const part2tests: TestCase[] = [{
		input: `47|53
97|13
97|61
97|47
75|29
61|13
75|53
29|13
97|29
53|29
61|53
97|53
61|29
47|13
75|47
97|75
47|61
75|61
47|29
75|13
53|13

75,47,61,53,29
97,61,53,29,13
75,29,13
75,97,47,61,53
61,13,29
97,13,75,29,47`,
		extraArgs: [],
		expected: `123`
	}];

	const [p1testsNormalized, p2testsNormalized] = normalizeTestCases(part1tests, part2tests);

	// Run tests
	test.beginTests();
	await test.section(async () => {
		for (const testCase of p1testsNormalized) {
			test.logTestResult(testCase, String(await p2024day5_part1(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	await test.section(async () => {
		for (const testCase of p2testsNormalized) {
			test.logTestResult(testCase, String(await p2024day5_part2(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	test.endTests();

	// Get input and run program while measuring performance
	const input = await util.getInput(DAY, YEAR);

	const part1Before = performance.now();
	const part1Solution = String(await p2024day5_part1(input));
	const part1After = performance.now();

	const part2Before = performance.now()
	const part2Solution = String(await p2024day5_part2(input));
	const part2After = performance.now();

	logSolution(5, 2024, part1Solution, part2Solution);

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

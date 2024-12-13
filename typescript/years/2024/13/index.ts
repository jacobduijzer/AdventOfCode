import _ from "lodash";
import * as util from "../../../util/util";
import * as test from "../../../util/test";
import chalk from "chalk";
import { log, logSolution, trace } from "../../../util/log";
import { performance } from "perf_hooks";
import { normalizeTestCases } from "../../../util/test";

const YEAR = 2024;
const DAY = 13;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\13\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\13\data.txt
// problem url  : https://adventofcode.com/2024/day/13

const PRICE_A = 3;
const PRICE_B = 1;

function parseMachines(input: string): number[][] {
	return input
		.trim()
		.split(/\n\n/)
		.map(claw => claw.match(/\d+/g)!.map(Number));
}

function calculateCoefficients(aX: number, aY: number, bX: number, bY: number, pX: number, pY: number, d: number): [number, number] {
	return [
		((pX + d) * bY - bX * (pY + d)) / (aX * bY - bX * aY),
		((pX + d) * aY - aX * (pY + d)) / (bX * aY - aX * bY),
	];
}

function calculateTokens(input: string, d: number): number {
	return parseMachines(input)
		.map(([aX, aY, bX, bY, pX, pY]) => calculateCoefficients(aX, aY, bX, bY, pX, pY, d))
		.filter(([a, b]) => !(a % 1 || b % 1))
		.map(([a, b]) => PRICE_A * a + PRICE_B * b)
		.reduce((acc, t) => acc + t, 0);
}

async function p2024day13_part1(input: string, ...params: any[]) {
	return calculateTokens(input, 0);
}

async function p2024day13_part2(input: string, ...params: any[]) {
	return calculateTokens(input, 10000000000000);
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `Button A: X+94, Y+34
Button B: X+22, Y+67
Prize: X=8400, Y=5400

Button A: X+26, Y+66
Button B: X+67, Y+21
Prize: X=12748, Y=12176

Button A: X+17, Y+86
Button B: X+84, Y+37
Prize: X=7870, Y=6450

Button A: X+69, Y+23
Button B: X+27, Y+71
Prize: X=18641, Y=10279`,
		extraArgs: [],
		expected: `480`
	}];

	const part2tests: TestCase[] = [];

	const [p1testsNormalized, p2testsNormalized] = normalizeTestCases(part1tests, part2tests);

	// Run tests
	test.beginTests();
	await test.section(async () => {
		for (const testCase of p1testsNormalized) {
			test.logTestResult(testCase, String(await p2024day13_part1(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	await test.section(async () => {
		for (const testCase of p2testsNormalized) {
			test.logTestResult(testCase, String(await p2024day13_part2(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	test.endTests();

	// Get input and run program while measuring performance
	const input = await util.getInput(DAY, YEAR);

	const part1Before = performance.now();
	const part1Solution = String(await p2024day13_part1(input));
	const part1After = performance.now();

	const part2Before = performance.now()
	const part2Solution = String(await p2024day13_part2(input));
	const part2After = performance.now();

	logSolution(13, 2024, part1Solution, part2Solution);

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

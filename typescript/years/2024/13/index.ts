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

type Vector = [number, number];
type Machine = {
	A: Vector;
	B: Vector;
	PrizeLocation: Vector;
};

const PRICE_A = 3;
const PRICE_B = 1;

function parseMachine(input: string): Machine {
	const lines = input.split('\n');

	// Regular expressions to extract values
	const buttonRegex = /X\+(-?\d+), Y\+(-?\d+)/;
	const prizeRegex = /X=(-?\d+), Y=(-?\d+)/;

	// Extract Button A
	const aMatch = lines[0].match(buttonRegex);
	const A: Vector = [parseInt(aMatch![1], 10), parseInt(aMatch![2], 10)];

	// Extract Button B
	const bMatch = lines[1].match(buttonRegex);
	const B: Vector = [parseInt(bMatch![1], 10), parseInt(bMatch![2], 10)];

	// Extract Prize
	const prizeMatch = lines[2].match(prizeRegex);
	const PrizeLocation: Vector = [parseInt(prizeMatch![1], 10), parseInt(prizeMatch![2], 10)];

	return { A, B, PrizeLocation };
}

function calculateTokens(machine: Machine): number | null {
	const [ax, ay] = machine.A;
	const [bx, by] = machine.B;
	const [px, py] = machine.PrizeLocation;

	let minTokens = Infinity;

	for (let a = 0; a <= 100; a++) {
		for (let b = 0; b <= 100; b++) {
			if (a * ax + b * bx === px && a * ay + b * by === py) {
				const tokens = a * PRICE_A + b * PRICE_B;
				if (tokens < minTokens) {
					minTokens = tokens;
				}
			}
		}
	}

	return minTokens === Infinity ? null : minTokens;
}

async function p2024day13_part1(input: string, ...params: any[]) {
	const games = input.split('\n\n').map(parseMachine);
	const result = games.map(game => calculateTokens(game)).filter(x => x !== null) as number[];
	return result.reduce((acc, val) => acc + val, 0);
}

async function p2024day13_part2(input: string, ...params: any[]) {
	return "Not implemented";
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

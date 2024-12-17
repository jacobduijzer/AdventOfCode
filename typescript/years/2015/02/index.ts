import * as util from "../../../util/util";
import * as test from "../../../util/test";
import { normalizeTestCases } from "../../../util/test";
import chalk from "chalk";
import { log, logSolution } from "../../../util/log";
import { performance } from "perf_hooks";

const YEAR = 2015;
const DAY = 2;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2015\02\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2015\02\data.txt
// problem url  : https://adventofcode.com/2015/day/2

type Gift = {
	length: number;
	width: number;
	height: number;
}

function parseGifts(input: string): Gift[] {
	return input.split('\n').map(line => {
		const [length, width, height] = line.split('x').map(Number);
		return { length, width, height };
	});
}

function calculateWrappingPaperSize(gift: Gift): number {
	const lw = gift.length * gift.width;
	const wh = gift.width * gift.height;
	const hl = gift.height * gift.length;
	const smallestSide = Math.min(lw, wh, hl);
	return 2 * lw + 2 * wh + 2 * hl + smallestSide;
}

function calculateRibbonLength(gift: Gift): number {
	const perimeters = [
		2 * (gift.length + gift.width),
		2 * (gift.width + gift.height),
		2 * (gift.height + gift.length)
	];
	const smallestPerimeter = Math.min(...perimeters);
	const bowLength = gift.length * gift.width * gift.height;
	return smallestPerimeter + bowLength;
}

async function p2015day2_part1(input: string, ...params: any[]) {
	const gifts = parseGifts(input);
	return gifts.reduce((total, gift) =>
		total + calculateWrappingPaperSize(gift), 0);
}

async function p2015day2_part2(input: string, ...params: any[]) {
	const gifts = parseGifts(input);
	return gifts.reduce((total, gift) =>
		total + calculateRibbonLength(gift), 0);
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `2x3x4`,
		extraArgs: [],
		expected: `58`
	}, {
		input: `1x1x10`,
		extraArgs: [],
		expected: `43`
	},{
		input: `2x3x4
		1x1x10`,
		extraArgs: [],
		expected: `101`
	}];

	const part2tests: TestCase[] = [{
		input: `2x3x4`,
		extraArgs: [],
		expected: `34`
	}, {
		input: `1x1x10`,
		extraArgs: [],
		expected: `14`
	},{
		input: `2x3x4
		1x1x10`,
		extraArgs: [],
		expected: `48`
	}];

	const [p1testsNormalized, p2testsNormalized] = normalizeTestCases(part1tests, part2tests);

	// Run tests
	test.beginTests();
	await test.section(async () => {
		for (const testCase of p1testsNormalized) {
			test.logTestResult(testCase, String(await p2015day2_part1(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	await test.section(async () => {
		for (const testCase of p2testsNormalized) {
			test.logTestResult(testCase, String(await p2015day2_part2(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	test.endTests();

	// Get input and run program while measuring performance
	const input = await util.getInput(DAY, YEAR);

	const part1Before = performance.now();
	const part1Solution = String(await p2015day2_part1(input));
	const part1After = performance.now();

	const part2Before = performance.now()
	const part2Solution = String(await p2015day2_part2(input));
	const part2After = performance.now();

	logSolution(2, 2015, part1Solution, part2Solution);

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

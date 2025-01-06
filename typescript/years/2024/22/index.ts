import * as util from "../../../util/util";
import * as test from "../../../util/test";
import chalk from "chalk";
import { log, logSolution, trace } from "../../../util/log";
import { performance } from "perf_hooks";
import { normalizeTestCases } from "../../../util/test";

const YEAR = 2024;
const DAY = 22;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\22\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\22\data.txt
// problem url  : https://adventofcode.com/2024/day/22

function calculateSecret(secret: number): number {
	// Step 1: Multiply by 64, mix, and prune
	secret ^= (secret * 64) % 16777216;
	secret %= 16777216;

	// Step 2: Divide by 32, round down, mix, and prune
	secret ^= Math.floor(secret / 32) % 16777216;
	secret %= 16777216;

	// Step 3: Multiply by 2048, mix, and prune
	secret ^= (secret * 2048) % 16777216;
	secret %= 16777216;

	return secret;
}

function get2000thSecretNumber(initialSecret: number): number {
	let secret = initialSecret;
	for (let i = 0; i < 2000; i++)
		secret = calculateSecret(secret);
	return secret;
}

function generatePrices(initialSecret: number): number[] {
	let secret = initialSecret;
	const prices = [];
	for (let i = 0; i < 2000; i++) {
		prices.push(secret % 10); // Take the ones digit as the price
		secret = calculateSecret(secret);
	}
	return prices;
}

function calculateBestSequence(initialSecrets: number[]): { bestSequence: number[]; totalBananas: number } {
	const allChanges = initialSecrets.map(secret => {
		const prices = generatePrices(secret);
		return prices.map((price, i, arr) => (i > 0 ? price - arr[i - 1] : 0)).slice(1);
	});

	const sequenceCount = new Map<string, { bananas: number; buyers: number[] }>();

	allChanges.forEach((changes, buyerIndex) => {
		const prices = generatePrices(initialSecrets[buyerIndex]);
		for (let i = 0; i <= changes.length - 4; i++) {
			const sequence = changes.slice(i, i + 4).join(",");

			if (!sequenceCount.has(sequence))
				sequenceCount.set(sequence, { bananas: 0, buyers: [] });

			if (!sequenceCount.get(sequence)?.buyers.includes(buyerIndex)) {
				sequenceCount.get(sequence)!.bananas += prices[i + 4];
				sequenceCount.get(sequence)!.buyers.push(buyerIndex);
			}
		}
	});

	// Find the best sequence
	let bestSequence = "";
	let maxBananas = 0;

	sequenceCount.forEach((value, sequence) => {
		if (value.bananas > maxBananas) {
			bestSequence = sequence;
			maxBananas = value.bananas;
		}
	});

	return { bestSequence: bestSequence.split(",").map(Number), totalBananas: maxBananas };
}

async function p2024day22_part1(input: string, ...params: any[]) {
	return input
		.split('\n')
		.map(Number)
		.map(get2000thSecretNumber)
		.reduce((sum, secret) => sum + secret, 0);
}

async function p2024day22_part2(input: string, ...params: any[]) {
	const numbers = input.split('\n').map(Number);
	return calculateBestSequence(numbers).totalBananas
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `1`,
		extraArgs: [],
		expected: `8685429`
	},{
		input: `1
10
100
2024`,
		extraArgs: [],
		expected: `37327623`
	}];

	const part2tests: TestCase[] = [{
		input: `1
2
3
2024`,
		extraArgs: [],
		expected: `23`
	}];

	const [p1testsNormalized, p2testsNormalized] = normalizeTestCases(part1tests, part2tests);

	// Run tests
	test.beginTests();
	await test.section(async () => {
		for (const testCase of p1testsNormalized) {
			test.logTestResult(testCase, String(await p2024day22_part1(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	await test.section(async () => {
		for (const testCase of p2testsNormalized) {
			test.logTestResult(testCase, String(await p2024day22_part2(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	test.endTests();

	// Get input and run program while measuring performance
	const input = await util.getInput(DAY, YEAR);

	const part1Before = performance.now();
	const part1Solution = String(await p2024day22_part1(input));
	const part1After = performance.now();

	const part2Before = performance.now()
	const part2Solution = String(await p2024day22_part2(input));
	const part2After = performance.now();

	logSolution(22, 2024, part1Solution, part2Solution);

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

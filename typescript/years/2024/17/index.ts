import * as util from "../../../util/util";
import * as test from "../../../util/test";
import { normalizeTestCases } from "../../../util/test";
import chalk from "chalk";
import { log, logSolution } from "../../../util/log";
import { performance } from "perf_hooks";


const YEAR = 2024;
const DAY = 17;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\17\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\17\data.txt
// problem url  : https://adventofcode.com/2024/day/17

type FullProgram = {
	A: bigint;
	B: bigint;
	C: bigint;

	Program: bigint[];
}
function parseInput(input: string): FullProgram {
	const registerMatches = input.match(/Register [A-Z]: (\d+)/g);
	const registers = registerMatches ? registerMatches.map(match => BigInt(match.split(': ')[1]), 10n) : [0n, 0n, 0n];

	const programMatch = input.match(/Program: ([\d,]+)/);
	const program = programMatch ? programMatch[1].split(',').map(BigInt) : [];

	return { A: registers[0], B: registers[1], C: registers[2], Program: program };
}

function getComboValue(operand: bigint, program: FullProgram): bigint{
	switch (operand) {
		case 0n: case 1n: case 2n: case 3n:
			return operand;
		case 4n: return program.A;
		case 5n: return program.B;
		case 6n: return program.C;
		default:
			throw new Error(`Invalid combo operand: ${operand}`);
	}
}

function adv(operand: bigint, program: FullProgram): bigint {
	const denominator = 2n ** getComboValue(operand, program);
	return program.A / denominator;
}

function bxl(operand: bigint, program: FullProgram): bigint {
	return program.B ^= operand;
}

function bst(operand: bigint, program: FullProgram): bigint {
	return getComboValue(operand, program) % 8n;
}

function jnz(operand: bigint, program: FullProgram, instructionPointer: bigint): bigint {
	if (program.A !== 0n) {
		return operand;
	}
	return instructionPointer + 2n; // Move to the next instruction as usual
}

function out(operand: bigint, program: FullProgram): bigint {
	return getComboValue(operand, program) % 8n;
}

function bdv(operand: bigint, program: FullProgram): bigint{
	const denominator = 2n ** getComboValue(operand, program);
	return program.A / denominator;
}

function cdv(operand: bigint, program: FullProgram): bigint {
	const denominator = 2n ** getComboValue(operand, program);
	return program.A / denominator;
}

function runProgram(program: FullProgram) : bigint[] {
	let instructionPointer = 0n;
	let output: bigint[] = [];

	while (instructionPointer < program.Program.length) {
		const opcode = program.Program[Number(instructionPointer)];
		const operand = program.Program[Number(instructionPointer) + 1];

		switch (opcode) {
			case 0n:
				program.A = adv(operand, program);
				break;
			case 1n:
				program.B = bxl(operand, program);
				break;
			case 2n:
				program.B = bst(operand, program);
				break;
			case 3n:
				instructionPointer = jnz(operand, program, instructionPointer);
				continue;
			case 4n:
				program.B ^= program.C;
				break;
			case 5n:
				output.push(out(operand, program));
				break;
			case 6n:
				program.B = bdv(operand, program);
				break;
			case 7n:
				program.C = cdv(operand, program);
				break;
			default:
				throw new Error(`Unknown opcode: ${opcode}`);
		}

		instructionPointer += 2n; // Move to the next instruction
	}
	return output;
}

function searchLowestA(value: bigint, current: number, fullProgram: FullProgram): bigint {
	if (current < 0) return value;

	for (let i = value << 3n; i < (value << 3n) + 8n; i++) {
		fullProgram.A = i;
		const output = runProgram(fullProgram);
		if (output[0] === fullProgram.Program[current]) {
			const finalVal = searchLowestA(i, current - 1, fullProgram);
			if (finalVal !== -1n) return finalVal;
		}
	}

	return -1n;
}

async function p2024day17_part1(input: string, ...params: any[]) {
	const fullProgram = parseInput(input);
	return runProgram(fullProgram).join(',');
}

async function p2024day17_part2(input: string, ...params: any[]) {
	const fullProgram = parseInput(input);
	return searchLowestA(0n, fullProgram.Program.length - 1, fullProgram);
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `Register A: 729
Register B: 0
Register C: 0

Program: 0,1,5,4,3,0`,
			extraArgs: [],
			expected: `4,6,3,5,6,3,5,2,1,0`
	}, {
		input: `Register A: 117440
Register B: 0
Register C: 0

Program: 0,3,5,4,3,0`,
		extraArgs: [],
		expected: `0,3,5,4,3,0`
	}];

	const part2tests: TestCase[] = [{
		input: `Register A: 117440
Register B: 0
Register C: 0

Program: 0,3,5,4,3,0`,
		extraArgs: [],
		expected: `117440`
	}];

	const [p1testsNormalized, p2testsNormalized] = normalizeTestCases(part1tests, part2tests);

	// Run tests
	test.beginTests();
	await test.section(async () => {
		for (const testCase of p1testsNormalized) {
			test.logTestResult(testCase, String(await p2024day17_part1(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	await test.section(async () => {
		for (const testCase of p2testsNormalized) {
			test.logTestResult(testCase, String(await p2024day17_part2(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	test.endTests();

	// Get input and run program while measuring performance
	const input = await util.getInput(DAY, YEAR);

	const part1Before = performance.now();
	const part1Solution = String(await p2024day17_part1(input));
	const part1After = performance.now();

	const part2Before = performance.now()
	const part2Solution = String(await p2024day17_part2(input));
	const part2After = performance.now();

	logSolution(17, 2024, part1Solution, part2Solution);

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

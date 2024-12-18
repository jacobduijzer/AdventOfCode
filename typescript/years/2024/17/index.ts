import * as util from "../../../util/util";
import * as test from "../../../util/test";
import { normalizeTestCases } from "../../../util/test";
import chalk from "chalk";
import { log, logSolution } from "../../../util/log";
import { performance } from "perf_hooks";
import { start } from "node:repl";

const YEAR = 2024;
const DAY = 17;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\17\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\17\data.txt
// problem url  : https://adventofcode.com/2024/day/17

type FullProgram = {
	A: number;
	B: number;
	C: number;

	Program: number[];
}
function parseInput(input: string): FullProgram {
	const registerMatches = input.match(/Register [A-Z]: (\d+)/g);
	const registers = registerMatches ? registerMatches.map(match => parseInt(match.split(': ')[1], 10)) : [0, 0, 0];

	const programMatch = input.match(/Program: ([\d,]+)/);
	const program = programMatch ? programMatch[1].split(',').map(Number) : [];

	return { A: registers[0], B: registers[1], C: registers[2], Program: program };
}

function getComboValue(operand: number, program: FullProgram): number {
	switch (operand) {
		case 0: case 1: case 2: case 3:
			return operand;
		case 4: return program.A;
		case 5: return program.B;
		case 6: return program.C;
		default:
			throw new Error(`Invalid combo operand: ${operand}`);
	}
}

function adv(operand: number, program: FullProgram): number {
	const denominator = Math.pow(2, getComboValue(operand, program));
	return Math.floor(program.A / denominator);
}

function bxl(operand: number, program: FullProgram): number {
	return program.B  ^= operand;
}

function bst(operand: number, program: FullProgram): number{
	return getComboValue(operand, program) % 8;
}

function jnz(operand: number, program: FullProgram, instructionPointer: number): number {
	if (program.A !== 0) {
		return operand;
	}
	return instructionPointer + 2; // Move to the next instruction as usual
}

function out(operand: number, program: FullProgram): number {
	return getComboValue(operand, program) % 8;
}

function bdv(operand: number, program: FullProgram): number {
	const denominator = Math.pow(2, getComboValue(operand, program));
	return Math.floor(program.A / denominator);
}

function cdv(operand: number, program: FullProgram): number {
	const denominator = Math.pow(2, getComboValue(operand, program));
	return Math.floor(program.A / denominator);
}

function runProgram(program: FullProgram) : number[] {
	let instructionPointer = 0;
	let output: number[] = [];

	while (instructionPointer < program.Program.length) {
		const opcode = program.Program[instructionPointer];
		const operand = program.Program[instructionPointer + 1];

		switch (opcode) {
			case 0:
				program.A = adv(operand, program);
				break;
			case 1:
				program.B = bxl(operand, program);
				break;
			case 2:
				program.B = bst(operand, program);
				break;
			case 3:
				instructionPointer= jnz(operand, program, instructionPointer);
				continue;
			case 4:
				program.B ^= program.C;
				break;
			case 5:
				output.push(out(operand, program));
				break;
			case 6:
				program.B = bdv(operand, program);
				break;
			case 7:
				program.C = cdv(operand, program);
				break;
			default:
				throw new Error(`Unknown opcode: ${opcode}`);
		}

		instructionPointer += 2; // Move to the next instruction
	}
	return output;
}

async function p2024day17_part1(input: string, ...params: any[]) {
	const fullProgram = parseInput(input);
	return runProgram(fullProgram).join(',');
}

const outFromA = (a: number) => {
	const interim = (a % 8) ^ 3;
	const c = Math.floor(a / Math.pow(2, interim));
	const postC = c ^ 6;
	return (postC ^ (a % 8)) % 8;
	// return (a >> 3) % 8; // for example
}

async function p2024day17_part2(input: string, ...params: any[]) {
	const fullProgram = parseInput(input);
	// let candidates = [0];
	// for (let i = fullProgram.Program.length - 1; i >= 0; i--) {
	// 	const newCandidates = [];
	// 	// console.log(`Instruction: ${fullProgram.Program[i]}, candidates: ${candidates}`);
	// 	for (const c of candidates) {
	// 		for (let j = 0; j < 8; j++) {
	// 			const num = (c << 3) + j;
	// 			const out1 = outFromA(num);
	// 			console.log(`Num: ${num}, Out: ${out1}, Instruction: ${fullProgram.Program[i]}`);
	//
	// 			fullProgram.A = num;
	// 			const out = runProgram(fullProgram);
	// 			if(out.join(',') === fullProgram.Program.join(',')) {
	// 				newCandidates.push(num);
	// 			}
	// 		}
	// 	}
	// 	candidates = newCandidates;
	// }
	let candidates = [0];
	for (let i = fullProgram.Program.length - 1; i >= 0; i--) {
		const newCandidates = [];
		console.log(`Instruction: ${fullProgram.Program[i]}, candidates: ${candidates}`);
		for (const c of candidates) {
			for (let j = 0; j < 8; j++) {
				const num = (c << 3) + j;
				const out = outFromA(num);
				console.log(`Num: ${num}, Out: ${out}, Instruction: ${fullProgram.Program[i]}`);
				fullProgram.A = num;
				const output = runProgram(fullProgram);
				if(output.join(',') === fullProgram.Program.join(',')) {
					console.log("bingo");
				}
				if (out === fullProgram.Program[i]) {
					newCandidates.push(num);
				}
			}
		}
		candidates = newCandidates;
	}

	// console.log(candidates);
	return Math.min(...candidates);
	//return findInitialA(0, fullProgram, fullProgram.Program.length - 1);
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
	const part2Solution = String(await p2024day17_part2(input, 5000000000));
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

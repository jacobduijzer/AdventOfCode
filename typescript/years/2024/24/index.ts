import _ from "lodash";
import * as util from "../../../util/util";
import * as test from "../../../util/test";
import chalk from "chalk";
import { log, logSolution, trace } from "../../../util/log";
import { performance } from "perf_hooks";
import { normalizeTestCases } from "../../../util/test";

const YEAR = 2024;
const DAY = 24;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\24\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\24\data.txt
// problem url  : https://adventofcode.com/2024/day/24

function parseWireValues(lines: string): Record<string, number> {
	const result: Record<string, number> = {};
	lines.split('\n').forEach(line => {
		const [key, value] = line.split(':').map(part => part.trim());
		if (key && value) {
			result[key] = parseInt(value, 10);
		}
	});
	return result;
}

function executeOperations(data: Record<string, number>, rules: string): Record<string, number> {
	const results = { ...data };
	const pendingOperations = rules.split('\n').map(rule => rule.trim());
	while (pendingOperations.length > 0) {
		for (let i = 0; i < pendingOperations.length; i++) {
			const rule = pendingOperations[i];
			const [operation, resultKey] = rule.split(' -> ').map(part => part.trim());
			const [operand1, operator, operand2] = operation.split(' ');

			if (results.hasOwnProperty(operand1) && results.hasOwnProperty(operand2)) {
				let result: number;
				switch (operator) {
					case 'AND':
						result = results[operand1] & results[operand2];
						break;
					case 'OR':
						result = results[operand1] | results[operand2];
						break;
					case 'XOR':
						result = results[operand1] ^ results[operand2];
						break;
					default:
						throw new Error(`Unsupported operator: ${operator}`);
				}

				results[resultKey] = result;
				pendingOperations.splice(i, 1);
				i--;
			}
		}
	}

	return results;
}

async function p2024day24_part1(input: string, ...params: any[]) {
	const [variables, operations] = input.split('\n\n');
	const wireValues = parseWireValues(variables);
	const computedResults = executeOperations(wireValues, operations);
	const binaryResult = Object.keys(computedResults)
		.filter(key => key.startsWith('z'))
		.sort((a, b) => parseInt(a.slice(1)) - parseInt(b.slice(1)))
		.map(key => computedResults[key])
		.reverse()
		.join('');
	return parseInt(binaryResult, 2);
 }

async function p2024day24_part2(input: string, ...params: any[]) {
	return "Not implemented";
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `x00: 1
x01: 1
x02: 1
y00: 0
y01: 1
y02: 0

x00 AND y00 -> z00
x01 XOR y01 -> z01
x02 OR y02 -> z02`,
		extraArgs: [],
		expected: `4`
	}, {
		input: `x00: 1
x01: 0
x02: 1
x03: 1
x04: 0
y00: 1
y01: 1
y02: 1
y03: 1
y04: 1

ntg XOR fgs -> mjb
y02 OR x01 -> tnw
kwq OR kpj -> z05
x00 OR x03 -> fst
tgd XOR rvg -> z01
vdt OR tnw -> bfw
bfw AND frj -> z10
ffh OR nrd -> bqk
y00 AND y03 -> djm
y03 OR y00 -> psh
bqk OR frj -> z08
tnw OR fst -> frj
gnj AND tgd -> z11
bfw XOR mjb -> z00
x03 OR x00 -> vdt
gnj AND wpb -> z02
x04 AND y00 -> kjc
djm OR pbm -> qhw
nrd AND vdt -> hwm
kjc AND fst -> rvg
y04 OR y02 -> fgs
y01 AND x02 -> pbm
ntg OR kjc -> kwq
psh XOR fgs -> tgd
qhw XOR tgd -> z09
pbm OR djm -> kpj
x03 XOR y03 -> ffh
x00 XOR y04 -> ntg
bfw OR bqk -> z06
nrd XOR fgs -> wpb
frj XOR qhw -> z04
bqk OR frj -> z07
y03 OR x01 -> nrd
hwm AND bqk -> z03
tgd XOR rvg -> z12
tnw OR pbm -> gnj`,
		extraArgs: [],
		expected: `2024`
	}];

	const part2tests: TestCase[] = [];

	const [p1testsNormalized, p2testsNormalized] = normalizeTestCases(part1tests, part2tests);

	// Run tests
	test.beginTests();
	await test.section(async () => {
		for (const testCase of p1testsNormalized) {
			test.logTestResult(testCase, String(await p2024day24_part1(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	await test.section(async () => {
		for (const testCase of p2testsNormalized) {
			test.logTestResult(testCase, String(await p2024day24_part2(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	test.endTests();

	// Get input and run program while measuring performance
	const input = await util.getInput(DAY, YEAR);

	const part1Before = performance.now();
	const part1Solution = String(await p2024day24_part1(input));
	const part1After = performance.now();

	const part2Before = performance.now()
	const part2Solution = String(await p2024day24_part2(input));
	const part2After = performance.now();

	logSolution(24, 2024, part1Solution, part2Solution);

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

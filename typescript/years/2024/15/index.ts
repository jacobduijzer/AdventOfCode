import * as util from "../../../util/util";
import * as test from "../../../util/test";
import chalk from "chalk";
import { log, logSolution, trace } from "../../../util/log";
import { performance } from "perf_hooks";
import { normalizeTestCases } from "../../../util/test";
import Part01 from "./part01";
import Part02 from "./part02";

const YEAR = 2024;
const DAY = 15;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\15\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\15\data.txt
// problem url  : https://adventofcode.com/2024/day/15

async function p2024day15_part1(input: string, ...params: any[]) {const performanceStart = performance.now();
	const part01: Part01 = new Part01();
	const {robot, map, directions} = part01.parseInput(input);

	for (const dir of directions)
		part01.move(robot, map, dir);

	return part01.calculateGPS(map);
}

async function p2024day15_part2(input: string, ...params: any[]) {
	const part02: Part02 = new Part02();
	const {robot, map, directions} = part02.parseInput(input);

	for (const dir of directions)
		part02.move(robot, map, dir);

	return part02.calculateGPS(map);
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `########
#..O.O.#
##@.O..#
#...O..#
#.#.O..#
#...O..#
#......#
########

<^^>>>vv<v>>v<<`,
		extraArgs: [],
		expected: `2028`}
	];

	const part2tests: TestCase[] = [{
		input: `##########
#..O..O.O#
#......O.#
#.OO..O.O#
#..O@..O.#
#O#..O...#
#O..O..O.#
#.OO.O.OO#
#....O...#
##########

<vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^
vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v
><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<
<<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^
^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><
^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^
>^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^
<><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>
^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>
v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^`,
		extraArgs: [],
		expected: `9021`}
	];
	const [p1testsNormalized, p2testsNormalized] = normalizeTestCases(part1tests, part2tests);

	// Run tests
	test.beginTests();
	await test.section(async () => {
		for (const testCase of p1testsNormalized) {
			test.logTestResult(testCase, String(await p2024day15_part1(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	await test.section(async () => {
		for (const testCase of p2testsNormalized) {
			test.logTestResult(testCase, String(await p2024day15_part2(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	test.endTests();

	// Get input and run program while measuring performance
	const input = await util.getInput(DAY, YEAR);

	const part1Before = performance.now();
	const part1Solution = String(await p2024day15_part1(input));
	const part1After = performance.now();

	const part2Before = performance.now()
	const part2Solution = String(await p2024day15_part2(input));
	const part2After = performance.now();

	logSolution(15, 2024, `${part1Solution} (expected: 1430536)`, `${part2Solution} (expected: 1452348)`);

	log(chalk.gray("--- Performance ---"));
	log(chalk.gray(`Part 1: ${util.formatTime(part1After - part1Before)} (expected: 1430536)`));
	log(chalk.gray(`Part 2: ${util.formatTime(part2After - part2Before)} (expected: 1452348)`));
	log();
}

run()
	.then(() => {
		process.exit();
	})
	.catch(error => {
		throw error;
	});

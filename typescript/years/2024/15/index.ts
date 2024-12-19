import _ from "lodash";
import * as util from "../../../util/util";
import * as test from "../../../util/test";
import chalk from "chalk";
import { log, logSolution, trace } from "../../../util/log";
import { performance } from "perf_hooks";
import { normalizeTestCases } from "../../../util/test";
import { Grid, GridPos } from "../../../util/grid";

const YEAR = 2024;
const DAY = 15;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\15\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\15\data.txt
// problem url  : https://adventofcode.com/2024/day/15
async function p2024day15_part1(input: string, ...params: any[]) {
	return "Not implemented";

	const [gridInput, movesInput] = input.split("\n\n");
	const grid = new Grid({ serialized: gridInput });
	const moves = movesInput.replace(/\n/g, "").split("");

	const directions: { [key: string]: [number, number] } = {
		"^": [-1, 0],
		"v": [1, 0],
		"<": [0, -1],
		">": [0, 1],
	};

	let robotPosition = grid.getCell("@")!;
	let moveCount = 0;

	for (const move of moves) {
		const [dRow, dCol] = directions[move];
		const newRobotPosition: GridPos = [
			robotPosition.position[0] + dRow,
			robotPosition.position[1] + dCol,
		];
		const newRobotCell = grid.getCell(newRobotPosition)!;

		if (newRobotCell.value === "#") // Wall, next step
			continue;

		if (newRobotCell.value === ".") { // Move to the next position
			grid.setCell(robotPosition.position, '.');
			grid.setCell(newRobotCell.position, '@');
			robotPosition = newRobotCell;
			moveCount++;
			continue;
		}

		if (newRobotCell.value === "O") { // Box, check if it can be pushed
			let canMove = true;
			let currentBoxPosition = newRobotPosition;
			const boxesToMove: GridPos[] = [];

			while (canMove) {
				const nextBoxPosition: GridPos = [
					currentBoxPosition[0] + dRow,
					currentBoxPosition[1] + dCol,
				];
				const nextBoxCell = grid.getCell(nextBoxPosition);

				if (nextBoxCell && (nextBoxCell.value === "." || nextBoxCell.value === "O")) {
					boxesToMove.push(currentBoxPosition);
					currentBoxPosition = nextBoxPosition;
				} else {
					canMove = false;
				}
			}

			// Move the boxes if possible
			if (boxesToMove.length > 0 && grid.getCell(currentBoxPosition)!.value === ".") {
				for (let i = boxesToMove.length - 1; i >= 0; i--) {
					const boxPos = boxesToMove[i];
					const newBoxPos: GridPos = [
						boxPos[0] + dRow,
						boxPos[1] + dCol,
					];
					grid.getCell(newBoxPos)!.setValue("O");
					grid.getCell(boxPos)!.setValue(".");
				}

				// Move the robot
				grid.setCell(robotPosition.position, '.');
				grid.setCell(newRobotCell.position, '@');
				robotPosition = newRobotCell;
				moveCount++;
			}
		}
	}

	console.log(grid.toString());
	return moveCount;
}


async function p2024day15_part2(input: string, ...params: any[]) {
	return "Not implemented";
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `#.@O.O.#

>>`,
		extraArgs: [],
		expected: `2`},
		{
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
		expected: `104`}
	];

		const part2tests: TestCase[] = [];

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

	logSolution(15, 2024, part1Solution, part2Solution);

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

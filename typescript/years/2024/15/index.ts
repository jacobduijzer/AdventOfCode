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

type Point = {x: number, y: number};
type Robot = Point;
type Dir = '<' | '>' | '^' | 'v';
type MapCell = '.' | '#' | 'O';
type Map = MapCell[][];

function parseInput(input: string) {
	const robot: Robot = {x:0 , y:0};
	const [mapInput, directionsInput] = input.split('\n\n');
	const map = mapInput.split('\n').map((line, row) => line.split('').map((cell, col) => {
		if (cell === '@') {
			robot.x = col;
			robot.y = row;
			return '.';
		}
		return cell;
	})) as Map;
	const directions = directionsInput.replaceAll("\n", "").split('') as Dir[];

	return {robot, map, directions};
}

function move(robot: Robot, map: Map, dir: Dir) {
	const {x, y} = getNewPosition(robot, dir);
	const desiredCellValue = map[y][x];

	// nothing in the way, just move
	if (desiredCellValue === '.') {
		robot.x = x;
		robot.y = y;
		// it's a wall, don't move
	} else if (desiredCellValue === '#') {
		return;
		// it's a box, see if it's moveable
	} else if (desiredCellValue === 'O') {
		moveBoxes(robot, {x, y}, map, dir);
	}
}

function getNewPosition({x, y}: Point, dir: Dir) {
	switch (dir) {
		case '<':
			x--;
			break;
		case '>':
			x++;
			break;
		case '^':
			y--;
			break;
		case 'v':
			y++;
			break;
	}

	return {x, y};
}

function moveBoxes(robot: Robot, boxPosition: Point, map: Map, dir: Dir) {
	const stack = [boxPosition];

	let canMove = false;
	let current = {x: boxPosition.x, y: boxPosition.y};

	while (true) {
		const {x, y} = getNewPosition(current, dir);
		const nextCellValue = map[y][x];

		switch (nextCellValue) {
			// can't move the boxes
			case '#':
				return;
			case '.':
				canMove = true;
				stack.push({x, y});
				break;
			case 'O':
				stack.push({x, y});
				break;
		}

		current = {x, y};

		if (canMove) {
			break;
		}
	}

	if (canMove) {
		while (stack.length) {
			const {x, y} = stack.pop()!;
			map[y][x] = stack.length > 0 ? 'O' : '.';
		}
		robot.x = boxPosition.x;
		robot.y = boxPosition.y;
	}
}

function calculateGPS(map: Map) {
	let sum = 0;
	for (let y = 1; y < map.length-1; y++) {
		for (let x = 1; x < map[0].length-1; x++) {
			if (map[y][x] === 'O') {
				sum += (100 * y) + x;
			}
		}
	}

	return sum;
}

async function p2024day15_part1(input: string, ...params: any[]) {const performanceStart = performance.now();
	const {robot, map, directions} = parseInput(input);

	for (const dir of directions)
		move(robot, map, dir);

	return calculateGPS(map);
}

async function p2024day15_part2(input: string, ...params: any[]) {
	return "Not implemented";
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

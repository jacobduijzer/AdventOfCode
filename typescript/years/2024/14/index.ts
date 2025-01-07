import _ from "lodash";
import * as util from "../../../util/util";
import * as test from "../../../util/test";
import chalk from "chalk";
import { log, logSolution, trace } from "../../../util/log";
import { performance } from "perf_hooks";
import { normalizeTestCases } from "../../../util/test";
import { Grid, GridOptions } from "../../../util/grid";

const YEAR = 2024;
const DAY = 14;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\14\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\14\data.txt
// problem url  : https://adventofcode.com/2024/day/14

const TIMING = 100;

type Vector2D = {
	x: number;
	y: number;
};

type Robot = {
	startPosition: Vector2D;
	velocity: Vector2D;
};

const parseLine = (line: string): Robot => {
	const positionMatch = line.match(/p=(-?\d+),(-?\d+)/);
	const velocityMatch = line.match(/v=(-?\d+),(-?\d+)/);

	if (!positionMatch || !velocityMatch) {
		throw new Error(`Invalid line format: "${line}"`);
	}

	return {
		startPosition: {
			x: parseInt(positionMatch[1], 10),
			y: parseInt(positionMatch[2], 10),
		},
		velocity: {
			x: parseInt(velocityMatch[1], 10),
			y: parseInt(velocityMatch[2], 10),
		},
	};
};

function parseInput(input: string): Robot[] {
	return input
		.trim()
		.split("\n")
		.map(line => line.trim())
		.filter(line => line.length > 0)
		.map(parseLine);
}


async function p2024day14_part1(input: string, ...params: any[]) {
	const width: number = Number(params[0]);
	const height: number = Number(params[1]);
	const robots = parseInput(input);

	const quadrants: number[] = [0, 0, 0, 0];
	for (let i = 0; i < robots.length; i++) {
		robots[i].startPosition.x = (robots[i].startPosition.x + robots[i].velocity.x * TIMING + width * TIMING) % width;
		robots[i].startPosition.y = (robots[i].startPosition.y + robots[i].velocity.y * TIMING + height * TIMING) % height;

		if (robots[i].startPosition.x === Math.floor(width / 2) || robots[i].startPosition.y === Math.floor(height / 2)) continue;

		const quadrant = Math.floor(robots[i].startPosition.x / Math.ceil(width / 2)) + Math.floor(robots[i].startPosition.y / Math.ceil(height / 2)) * 2;
		quadrants[quadrant]++;
	}

	return quadrants.reduce((mul, num) => mul * num, 1);
}

function showChristmasTree(positions: Set<string>) {
	const gridOptions: GridOptions = {
		colCount: 100,
		rowCount: 103,
		fillWith: '.',
	}
	const grid = new Grid(gridOptions);

	for (const position of positions) {
		const [x, y] = position.split(',').map(Number);
		grid.setCell([y, x], '#');
	}

	console.log(grid.toString());
}

async function p2024day14_part2(input: string, ...params: any[]) {
	const width: number = Number(params[0]);
	const height: number = Number(params[1]);
	const robots = parseInput(input);

	let step = 0;
	while (true) {
		step++;

		for (let i = 0; i < robots.length; i++) {
			robots[i].startPosition.x = (robots[i].startPosition.x + robots[i].velocity.x + width) % width;
			robots[i].startPosition.y = (robots[i].startPosition.y + robots[i].velocity.y + height) % height;
		}

		const positions = new Set(robots.map(robot => `${robot.startPosition.x},${robot.startPosition.y}`));

		for (const position of positions) {
			const [x, y] = position.split(',').map(num => parseInt(num));
			let hasGroup = true;

			for (let j = -2; j <= 2; j++) {
				for (let k = -2; k <= 2; k++) {
					if (!positions.has(`${x + k},${y + j}`)) {
						hasGroup = false;
						break;
					}
				}
				if (!hasGroup) break;
			}

			if (hasGroup) {
				showChristmasTree(positions);
				return step;
			}
		}
	}
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `p=0,4 v=3,-3
p=6,3 v=-1,-3
p=10,3 v=-1,2
p=2,0 v=2,-1
p=0,0 v=1,3
p=3,0 v=-2,-2
p=7,6 v=-1,-3
p=3,0 v=-1,-2
p=9,3 v=2,3
p=7,3 v=-1,2
p=2,4 v=2,-3
p=9,5 v=-3,-3`,
		extraArgs: [11, 7],
		expected: `12`,
	},
	];

	const part2tests: TestCase[] = [];

	const [p1testsNormalized, p2testsNormalized] = normalizeTestCases(part1tests, part2tests);

	// Run tests
	test.beginTests();
	await test.section(async () => {
		for (const testCase of p1testsNormalized) {
			test.logTestResult(testCase, String(await p2024day14_part1(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	await test.section(async () => {
		for (const testCase of p2testsNormalized) {
			test.logTestResult(testCase, String(await p2024day14_part2(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	test.endTests();

	// Get input and run program while measuring performance
	const input = await util.getInput(DAY, YEAR);

	const part1Before = performance.now();
	const part1Solution = String(await p2024day14_part1(input, 101, 103));
	const part1After = performance.now();

	const part2Before = performance.now()
	const part2Solution = String(await p2024day14_part2(input, 101, 103));
	const part2After = performance.now();

	logSolution(14, 2024, part1Solution, part2Solution);

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

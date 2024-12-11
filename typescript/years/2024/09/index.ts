import _ from "lodash";
import * as util from "../../../util/util";
import * as test from "../../../util/test";
import chalk from "chalk";
import { log, logSolution, trace } from "../../../util/log";
import { performance } from "perf_hooks";
import { normalizeTestCases } from "../../../util/test";

const YEAR = 2024;
const DAY = 9;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\09\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\09\data.txt
// problem url  : https://adventofcode.com/2024/day/9

export type Disk = (number | null)[];

function isFile(num: number): boolean {
	return num % 2 === 0;
}

function createDiskMap(input: string) : Disk {
	const res: Disk = [];
	let id = 0;
	for(let i = 0; i < input.length; i++) {
		if(isFile(i)) {
			for(let j = 0; j < Number(input[i]); j++)
				res.push(id);
			id++;
		} else {
			for(let j = 0; j < Number(input[i]); j++)
				res.push(null);
		}
	}

	return res;
}

function partitionDiskMap(disk: Disk): Disk {
	let l = 0;
	let r = disk.length - 1;
	while (l < r) {
		if (disk[l] !== null) {
			l++;
			continue;
		}

		if (disk[r] === null) {
			r--;
			continue;
		}

		disk[l] = disk[r];
		disk[r] = null;
		l++;
		r--;
	}

	return disk;
}

function compactDisk(blocks: (number | null)[]): (number | null)[] {
	// Step 1: Extract unique file IDs and sort them in descending order
	const fileIds =
		Array.from(new Set(blocks.filter(block => block !== null) as number[]))
			.sort((a, b) => b - a);

	for (const fileId of fileIds) {
		// Step 2: Find all indices occupied by the current file
		const fileIndices = blocks.reduce<number[]>((acc, block, idx) => {
			if (block === fileId) acc.push(idx);
			return acc;
		}, []);
		const fileSize = fileIndices.length;

		// Step 3: Find the leftmost span of nulls to the left of the file
		let targetStart = -1;
		for (let i = 0; i < fileIndices[0]; i++) {
			// Check if the span of `null` blocks is large enough
			const span = blocks.slice(i, i + fileSize);
			if (span.every(block => block === null)) {
				targetStart = i;
				break;
			}
		}

		// Step 4: If a valid span is found, move the file
		if (targetStart !== -1) {
			// Clear original file blocks
			for (const idx of fileIndices) {
				blocks[idx] = null;
			}
			// Place the file in the target span
			for (let i = 0; i < fileSize; i++) {
				blocks[targetStart + i] = fileId;
			}
		}
	}

	return blocks;
}

function calculateChecksum(disk: Disk) {
	return disk.reduce((acc, val, currentIndex) => {
		if(val === null) return acc;
		return acc! + val * currentIndex
	}, 0);
}

async function p2024day9_part1(input: string, ...params: any[]) {
		const disk = createDiskMap(input);
		const partitions = partitionDiskMap(disk);
		return calculateChecksum(partitions);
}

async function p2024day9_part2(input: string, ...params: any[]) {
	const disk = createDiskMap(input);
	const rearrangedDisk = compactDisk(disk);
	return calculateChecksum(rearrangedDisk);
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `2333133121414131402`,
		extraArgs: [],
		expected: `1928`
	}];

	const part2tests: TestCase[] = [{
		input: `2333133121414131402`,
		extraArgs: [],
		expected: `2858`
	}];
	const [p1testsNormalized, p2testsNormalized] = normalizeTestCases(part1tests, part2tests);

	// Run tests
	test.beginTests();
	await test.section(async () => {
		for (const testCase of p1testsNormalized) {
			test.logTestResult(testCase, String(await p2024day9_part1(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	await test.section(async () => {
		for (const testCase of p2testsNormalized) {
			test.logTestResult(testCase, String(await p2024day9_part2(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	test.endTests();

	// Get input and run program while measuring performance
	const input = await util.getInput(DAY, YEAR);

	const part1Before = performance.now();
	const part1Solution = String(await p2024day9_part1(input));
	const part1After = performance.now();

	const part2Before = performance.now()
	const part2Solution = String(await p2024day9_part2(input));
	const part2After = performance.now();

	logSolution(9, 2024, part1Solution, part2Solution);

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

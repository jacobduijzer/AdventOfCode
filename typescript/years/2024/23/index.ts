import _ from "lodash";
import * as util from "../../../util/util";
import * as test from "../../../util/test";
import chalk from "chalk";
import { log, logSolution, trace } from "../../../util/log";
import { performance } from "perf_hooks";
import { normalizeTestCases } from "../../../util/test";

const YEAR = 2024;
const DAY = 23;

// solution path: C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\23\index.ts
// data path    : C:\Users\jacob\Code\github\AdventOfCode\typescript\years\2024\23\data.txt
// problem url  : https://adventofcode.com/2024/day/23

type Graph = Map<string, Set<string>>;

function parseConnections(connections: string[]): Graph {
	const graph: Graph = new Map();

	for (const connection of connections) {
		const [a, b] = connection.split('-');

		if (!graph.has(a)) graph.set(a, new Set());
		if (!graph.has(b)) graph.set(b, new Set());

		graph.get(a)!.add(b);
		graph.get(b)!.add(a);
	}

	return graph;
}

function findTriangles(graph: Graph): string[][] {
	const triangles: Set<string> = new Set();

	for (const [node, neighbors] of graph.entries()) {
		const sortedNeighbors = Array.from(neighbors).sort();

		for (let i = 0; i < sortedNeighbors.length; i++) {
			const neighbor1 = sortedNeighbors[i];

			for (let j = i + 1; j < sortedNeighbors.length; j++) {
				const neighbor2 = sortedNeighbors[j];

				if (graph.get(neighbor1)?.has(neighbor2)) {
					const triangle = [node, neighbor1, neighbor2].sort().join(',');
					triangles.add(triangle);
				}
			}
		}
	}

	return Array.from(triangles).map(triangle => triangle.split(','));
}

function filterTrianglesWithT(triangles: string[][]): string[][] {
	return triangles.filter(triangle => triangle.some(name => name.startsWith('t')));
}

function findSets(graph: Graph): string[][] {
	const sets: string[][] = [];

	function bronKerbosch(
		r: Set<string>,
		p: Set<string>,
		x: Set<string>
	): void {
		if (p.size === 0 && x.size === 0) {
			sets.push(Array.from(r));
			return;
		}

		for (const node of Array.from(p)) {
			const neighbors = graph.get(node) || new Set();
			bronKerbosch(
				new Set(r).add(node),
				new Set([...p].filter(n => neighbors.has(n))),
				new Set([...x].filter(n => neighbors.has(n)))
			);
			p.delete(node);
			x.add(node);
		}
	}

	bronKerbosch(new Set(), new Set(graph.keys()), new Set());
	return sets;
}

function findLargestSet(graph: Graph): string[] {
	const sets = findSets(graph);
	let largestSet: string[] = [];

	for (const set of sets ) {
		if (set.length > largestSet.length) {
			largestSet = set;
		}
	}

	return largestSet;
}


async function p2024day23_part1(input: string, ...params: any[]) {
	const graph = parseConnections(input.split('\n'));
	const triangles = findTriangles(graph);
	const chiefs = filterTrianglesWithT(triangles);

	return chiefs.length;
}

async function p2024day23_part2(input: string, ...params: any[]) {
	const graph = parseConnections(input.split('\n'));
	const largestClique = findLargestSet(graph);
	return largestClique.sort().join(',');
}

async function run() {
	const part1tests: TestCase[] = [{
		input: `kh-tc
qp-kh
de-cg
ka-co
yn-aq
qp-ub
cg-tb
vc-aq
tb-ka
wh-tc
yn-cg
kh-ub
ta-co
de-co
tc-td
tb-wq
wh-td
ta-ka
td-qp
aq-cg
wq-ub
ub-vc
de-ta
wq-aq
wq-vc
wh-yn
ka-de
kh-ta
co-tc
wh-qp
tb-vc
td-yn`,
		extraArgs: [],
		expected: `7`
	}];

	const part2tests: TestCase[] = [{
		input: `kh-tc
qp-kh
de-cg
ka-co
yn-aq
qp-ub
cg-tb
vc-aq
tb-ka
wh-tc
yn-cg
kh-ub
ta-co
de-co
tc-td
tb-wq
wh-td
ta-ka
td-qp
aq-cg
wq-ub
ub-vc
de-ta
wq-aq
wq-vc
wh-yn
ka-de
kh-ta
co-tc
wh-qp
tb-vc
td-yn`,
		extraArgs: [],
		expected: `co,de,ka,ta`
	}];

	const [p1testsNormalized, p2testsNormalized] = normalizeTestCases(part1tests, part2tests);

	// Run tests
	test.beginTests();
	await test.section(async () => {
		for (const testCase of p1testsNormalized) {
			test.logTestResult(testCase, String(await p2024day23_part1(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	await test.section(async () => {
		for (const testCase of p2testsNormalized) {
			test.logTestResult(testCase, String(await p2024day23_part2(testCase.input, ...(testCase.extraArgs || []))));
		}
	});
	test.endTests();

	// Get input and run program while measuring performance
	const input = await util.getInput(DAY, YEAR);

	const part1Before = performance.now();
	const part1Solution = String(await p2024day23_part1(input));
	const part1After = performance.now();

	const part2Before = performance.now()
	const part2Solution = String(await p2024day23_part2(input));
	const part2After = performance.now();

	logSolution(23, 2024, part1Solution, part2Solution);

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

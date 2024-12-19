import { Point, Robot, Dir } from "./types";
import { WarehouseWoes } from "./warehousewoes";

export type MapCell = '.' | '#' | '[' | ']';
export type WarehouseMap = MapCell[][]

export default class Part02 extends WarehouseWoes{

	public parseInput(input: string) {

		const robot: Robot = { x: 0, y: 0 };
		const [mapInput, directionsInput] = input.split('\n\n');
		const map: WarehouseMap = [];
		mapInput.split('\n').forEach((line, y) => {
			const row: MapCell[] = [];
			line.split('').forEach((cell, x) => {
				switch (cell) {
					case '#':
						row.push('#');
						row.push('#');
						break;
					case '.':
						row.push('.');
						row.push('.');
						break;
					case 'O':
						row.push('[');
						row.push(']');
						break;
					case '@':
						row.push('.');
						row.push('.');
						robot.x = x * 2;
						robot.y = y;
						break;
				}
			});
			map.push(row);
		});
		const directions = directionsInput.replaceAll("\n", "").split('') as Dir[];

		return { robot, map, directions };
	}

	public move(robot: Robot, map: WarehouseMap, dir: Dir) {
		const { x, y } = this.getNewPosition(robot, dir);
		const desiredCellValue = map[y][x];

		// nothing in the way, just move
		if (desiredCellValue === '.') {
			robot.x = x;
			robot.y = y;
			// it's a wall, don't move
		} else if (desiredCellValue === '#') {
			return;
			// it's a box, see if it's moveable
		} else if (desiredCellValue === '[' || desiredCellValue === ']') {
			const moveableBoxes = this.getMoveableBoxes(robot, [], map, dir);
			if (moveableBoxes !== false && moveableBoxes.length > 0) {
				this.moveBoxes(moveableBoxes, robot, map, dir);
			}
		}
	}

	private getMoveableBoxes(current: Point, stack: Point[], map: WarehouseMap, dir: Dir): any {
		let nextPosition = this.getNewPosition(current, dir);
		let nextCellValue = map[nextPosition.y][nextPosition.x];

		// LEFT or RIGHT
		if (dir === '<' || dir === '>') {
			// can't move the boxes. They're butt up against a wall. Empty the stack and return it.
			if (nextCellValue === '#') {
				return false;
				// next one's a box, add it and keep checking
			} else if (nextCellValue === ']' && dir === '<') {
				return this.getMoveableBoxes({
					x: nextPosition.x - 1,
					y: nextPosition.y
				}, [...stack, { x: nextPosition.x - 1, y: nextPosition.y }], map, dir);
				// next one's a box, add it and keep checking
			} else if (nextCellValue === '[' && dir === '>') {
				return this.getMoveableBoxes({ x: nextPosition.x + 1, y: nextPosition.y }, [...stack, {
					x: nextPosition.x,
					y: nextPosition.y
				}], map, dir);
				// empty space. We're good to move these boxes. Return the stack.
			} else if (nextCellValue === '.') {
				return stack;
			}

			// UP or DOWN
		} else if (dir === '^' || dir === 'v') {
			// can't move the boxes. They're butt up against a wall. Empty the stack and return it.
			if (nextCellValue === '#') {
				return false;
				// next one's a box, add it and keep checking
			} else if (nextCellValue === ']') {
				const leftStack: any = this.getMoveableBoxes({
					x: nextPosition.x - 1,
					y: nextPosition.y
				}, [...stack, { x: nextPosition.x - 1, y: nextPosition.y }], map, dir);
				if (leftStack !== false) {
					return this.getMoveableBoxes(nextPosition, [...leftStack], map, dir);
				} else {
					return false;
				}
				// next one's a box, add it and keep checking
			} else if (nextCellValue === '[') {
				const rightStack: any = this.getMoveableBoxes({
					x: nextPosition.x + 1,
					y: nextPosition.y
				}, [...stack, { x: nextPosition.x, y: nextPosition.y }], map, dir);
				if (rightStack !== false) {
					return this.getMoveableBoxes(nextPosition, [...rightStack], map, dir);
				} else {
					return false;
				}
				// empty space. We're good to move these boxes. Return the stack.
			} else if (nextCellValue === '.') {
				return stack;
			}
		}

		return stack;
	}

	private	moveBoxes(moveableBoxes: Point[], robot: Robot, map: WarehouseMap, dir: Dir) {
		const uniqSet = new Set(moveableBoxes.map(b => JSON.stringify(b)));
		const stack = [...uniqSet].map(b => JSON.parse(b));

		if (stack.length) {
			const newRobotPosition = this.getNewPosition(robot, dir);
			robot.x = newRobotPosition.x;
			robot.y = newRobotPosition.y;

			stack.sort((a, b) => {
				switch (dir) {
					case '<':
						return b.x - a.x;
					case '>':
						return a.x - b.x;
					case '^':
						return b.y - a.y;
					case 'v':
						return a.y - b.y;
				}
			});
		}

		while (stack.length) {
			const { x, y } = stack.pop()!;
			switch (dir) {
				case '<':
					map[y][x + 1] = '.';
					map[y][x] = ']';
					map[y][x - 1] = '[';
					break;
				case '>':
					map[y][x] = '.';
					map[y][x + 1] = '[';
					map[y][x + 2] = ']';
					break;
				case '^':
					map[y - 1][x] = '[';
					map[y - 1][x + 1] = ']';
					map[y][x] = '.';
					map[y][x + 1] = '.';
					break;
				case 'v':
					map[y + 1][x] = '[';
					map[y + 1][x + 1] = ']';
					map[y][x] = '.';
					map[y][x + 1] = '.';
					break;
			}
		}
	}

	public calculateGPS(map: WarehouseMap) {
		let sum = 0;
		for (let y = 1; y < map.length - 1; y++) {
			for (let x = 2; x < map[0].length - 2; x++) {
				if (map[y][x] === '[') {
					sum += (100 * y) + x;
				}
			}
		}

		return sum;
	}

}
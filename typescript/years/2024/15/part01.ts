import { Point, Robot, Dir } from "./types";
import { WarehouseWoes } from "./warehousewoes";

type MapCell = '.' | '#' | 'O';
type WarehouseMap = MapCell[][];

export default class Part01 extends WarehouseWoes {

	public parseInput(input: string) {
		const robot: Robot = {x:0 , y:0};
		const [mapInput, directionsInput] = input.split('\n\n');
		const map = mapInput.split('\n').map((line, row) => line.split('').map((cell, col) => {
			if (cell === '@') {
				robot.x = col;
				robot.y = row;
				return '.';
			}
			return cell;
		})) as WarehouseMap;
		const directions = directionsInput.replaceAll("\n", "").split('') as Dir[];

		return {robot, map, directions};
	}

	public move(robot: Robot, map: WarehouseMap, dir: Dir) {
		const {x, y} = this.getNewPosition(robot, dir);
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
			this.moveBoxes(robot, {x, y}, map, dir);
		}
	}

	private moveBoxes(robot: Robot, boxPosition: Point, map: WarehouseMap, dir: Dir) {
		const stack = [boxPosition];

		let canMove = false;
		let current = {x: boxPosition.x, y: boxPosition.y};

		while (true) {
			const {x, y} = this.getNewPosition(current, dir);
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

	public calculateGPS(map: WarehouseMap) {
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

}
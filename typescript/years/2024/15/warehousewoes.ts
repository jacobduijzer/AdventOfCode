import { Dir, Point } from "./types";

export class WarehouseWoes {

	public getNewPosition({x, y}: Point, dir: Dir) {
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


}
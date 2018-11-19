﻿using Life.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Life
{
	public class Game
	{
		public Map Map { get; private set; }
		
		public Game(Map map)
		{
			Map = map ?? throw new ArgumentNullException(nameof(map));
		}

		public void Update()
		{
			var cellsToProcess = Map.AliveCells
					.SelectMany(cell => Get3x3Square(cell))
					.Distinct();

			var newCells = new HashSet<Point>();
			foreach (var cell in cellsToProcess)
			{
				var isAlive = Map.AliveCells.Contains(cell);
				var neighbours = GetAliveNeighbours(cell);

				if (UpdateCell(neighbours.Count(), isAlive) &&
					  (Map.Bounds?.Contains(cell) ?? true))
					newCells.Add(cell);
			}

			Map = new Map(newCells, Map.Bounds);
		}

		private IEnumerable<Point> GetAliveNeighbours(Point cell)
		{
			return Get3x3Square(cell)
				.Where(neighbour => neighbour != cell &&
				                    Map.AliveCells.Contains(neighbour));
		}

		private IEnumerable<Point> Get3x3Square(Point center)
		{
			for (var xOffset = -1; xOffset <= 1; ++xOffset)
				for (var yOffset = -1; yOffset <= 1; ++yOffset)
					yield return new Point(center.X + xOffset, center.Y + yOffset);
		}

		private bool UpdateCell(int neighboursCount, bool isAlive)
		{
			if (neighboursCount < 2)
				return false;
			if (neighboursCount == 2)
				return isAlive;
			if (neighboursCount == 3)
				return true;
			return false;
		}
	}
}

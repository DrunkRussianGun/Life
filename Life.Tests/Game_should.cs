﻿using FluentAssertions;
using Life.Infrastructure;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Life.Tests
{
	[TestFixture]
	public class Game_should
	{
		[Test]
		public void ContainsGivenCells_AfterCreation()
		{
			var game = new Game(OneCell);

			game.MapIsEqualTo(OneCell);
		}

		[Test]
		public void ManyNonNeighboursWillDie()
		{
			var game = new Game(Row);

			game.Update();

			game.Map.AliveCells.Should().BeEmpty();
		}

		[TestCaseSource("ConstantShapes")]
		[Test]
		public void TestConstantShapes(Map shape)
		{
			var game = new Game(shape);

			game.Update();

			game.MapIsEqualTo(shape);
		}

		[Test]
		public void BlinkerIsPeriodic()
		{
			var game = new Game(VertBlinker);

			game.Update();
			game.MapIsEqualTo(HorBlinker);

			game.Update();
			game.MapIsEqualTo(VertBlinker);
		}

		[Test]
		public void BoundsOfMapDoNotChange()
		{
			var game = new Game(Square);

			game.Update();

			game.Map.Bounds.Should().Be(Square.Bounds);
		}

		private static readonly Map OneCell = ParseMap(new[] { "#" });
		private static readonly Map Row = ParseMap(new[] { "# # # # # # # # # #" });
		private static readonly Map Square = ParseMap(new[]
		{
			"##",
			"##"
		});
		private static readonly Map Beehive = ParseMap(new[]
		{
			" ## ",
			"#  #",
			" ## "
		});
		private static readonly Map Loaf = ParseMap(new[]
		{
			" ## ",
			"#  #",
			" # #",
			"  # "
		});
		private static readonly Map Boat = ParseMap(new[]
		{
			"## ",
			"# #",
			" # "
		});
		private static readonly Map VertBlinker = ParseMap(new[]
		{
			" # ",
			" # ",
			" # "
		});
		private static readonly Map HorBlinker = ParseMap(new[]
		{
			"   ",
			"###",
			"   "
		});

		private static readonly IEnumerable<TestCaseData> ConstantShapes
			= new[] { Square, Beehive, Loaf, Boat }.ToTestCaseSource();

		private static Map ParseMap(string[] lines)
		{
			var cells = lines.SelectMany((line, y) =>
					line.Select((ch, x) =>
						ch == ' ' ? null : new Point?(new Point(x, y))))
				.Where(point => point != null)
				.Select(point => point.Value);
			return new Map(cells);
		}
	}

	internal static class GameExtensions
	{
		public static void MapIsEqualTo(this Game game, Map map)
		{
			game.Map.AliveCells.Should().BeEquivalentTo(map.AliveCells);
		}
	}
}

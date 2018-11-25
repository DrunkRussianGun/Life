using FluentAssertions;
using Life.GameEngine;
using Life.Infrastructure;
using NUnit.Framework;
using System.Drawing;

namespace Life.Tests
{
	[TestFixture]
	public class MapParser_should
	{
		[SetUp]
		public void SetUp()
		{
			_parser = new MapParser();
		}

		[Test]
		public void Parse_SimpleMap()
		{
			var map = _parser.Parse(new[]
			{
				"# ",
				"##"
			});
			
			map.IsEqualTo(new GameMap(
				new Point(0, 0),
				new Point(0, 1),
				new Point(1, 1)
			));
		}

		[TestCase("3 3")]
		[TestCase("3x3 ")]
		[TestCase(" 3 X 3 ")]
		public void Parse_MapWithBounds(string firstLine)
		{
			var map = _parser.Parse(new[]
			{
				firstLine,
				"",
				" # ",
				" # ",
				" # "
			});

			map.IsEqualTo(new GameMap(new[]
				{
					new Point(1, 0),
					new Point(1, 1),
					new Point(1, 2)
				}, 
				new Rectangle(new Point(), new Size(3, 3))
			));
		}

		[TestCase("3x3 3")]
		[TestCase("3")]
		public void Parse_Fails_WhenBoundsAreNotValid(string firstLine)
		{
			var map = _parser.Parse(new[]
			{
				firstLine,
				"",
				" # "
			});

			map.Should().BeNull();
		}

		private MapParser _parser;
	}

	internal static class MapExtensions
	{
		public static void IsEqualTo(this GameMap map, GameMap other)
		{
			map.AliveCells.Should().BeEquivalentTo(other.AliveCells);
			map.Bounds.Should().Be(other.Bounds);
		}
	}
}

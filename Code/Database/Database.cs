using System;

namespace Cassette_Builds.Code.Database
{
	public static class Database
	{
		public static readonly Monster[] Monsters;
		public static readonly Move[] Moves;
		public static readonly bool[,] MonsterMoves;

		static Database()
		{
			Monsters = DataDeserializer.DeserializeMonsters("Data/Monsters.csv");
			Moves = DataDeserializer.DeserializeMoves("Data/Moves.csv");

			MonsterMoves = new bool[Monsters.Length, Moves.Length];
			MoveMonsterPair[] movesPerMonster = DataDeserializer.DeserializeMoveMonsterPairs("Data/MovesPerMonster.csv");

			string lastMonster = "", lastMove = "";
			int monsterIndex = -1, moveIndex = -1;
			foreach (MoveMonsterPair item in movesPerMonster)
			{
				if (lastMonster != item.Monster)
				{
					monsterIndex = Array.FindIndex(Monsters, m => m.Name == item.Monster);
				}
				if (lastMove != item.Move)
				{
					moveIndex = Array.FindIndex(Moves, m => m.Name == item.Move);
				}
				MonsterMoves[monsterIndex, moveIndex] = true;
			}
		}
	}
}
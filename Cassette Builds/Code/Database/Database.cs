using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Cassette_Builds.Code.Database
{
	public static class Database
	{
		public static readonly ReadOnlyMemory<Monster> Monsters;
		public static readonly ReadOnlyMemory<Move> Moves;
		public static readonly ReadOnlyMemory<bool> MonsterMoves;
		public static readonly ReadOnlyDictionary<string, int> MovesReverseLookup;

		static Database()
		{
			Monsters = DataDeserializer.DeserializeMonsters("Data/Monsters.csv");
			Moves = DataDeserializer.DeserializeMoves("Data/Moves.csv");
			MovesReverseLookup = ComputeMovesReverseLookup(Moves.Span);
			MoveMonsterPair[] movesPerMonster = DataDeserializer.DeserializeMoveMonsterPairs("Data/MovesPerMonster.csv");
			MonsterMoves = ComputeMonsterMoves(movesPerMonster, Monsters.Span, Moves.Span);
		}

		public static ReadOnlyDictionary<string, int> ComputeMovesReverseLookup(ReadOnlySpan<Move> moves)
		{
			Dictionary<string, int> reverseLookup = new(moves.Length);
			for (int i = 0; i < moves.Length; i++)
			{
				reverseLookup[moves[i].Name] = i;
			}
			return reverseLookup.AsReadOnly();
		}

		public static bool[] ComputeMonsterMoves(ReadOnlySpan<MoveMonsterPair> movesPerMonster, ReadOnlySpan<Monster> monsters, ReadOnlySpan<Move> moves)
		{
			bool[] monsterMoves = new bool[monsters.Length * moves.Length];

			string lastMonster = "", lastMove = "";
			int monsterIndex = -1, moveIndex = -1;
			foreach (MoveMonsterPair item in movesPerMonster)
			{
				if (lastMonster != item.Monster)
				{
					lastMonster = item.Monster;
					monsterIndex = monsters.FindIndexByName(item.Monster);
				}
				if (lastMove != item.Move)
				{
					lastMove = item.Move;
					moveIndex = moves.FindIndexByName(item.Move);
				}
				monsterMoves.RefGet(monsters.Length, monsterIndex, moveIndex) = true;
			}
			return monsterMoves;
		}

		public static int FindIndexByName(this in ReadOnlySpan<Monster> monsters, string name)
		{
			for (int i = 0; i < monsters.Length; i++)
			{
				if (monsters[i].Name == name)
				{
					return i;
				}
			}
			return -1;
		}

		public static int FindIndexByName(this in ReadOnlySpan<Move> moves, string name)
		{
			for (int i = 0; i < moves.Length; i++)
			{
				if (moves[i].Name == name)
				{
					return i;
				}
			}
			return -1;
		}
	}
}
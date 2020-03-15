using UnityEngine;

namespace Entity
{
	public class DiffGame
	{
		private Vector2 sizeFiled;
		private Vector2 rangeMines;

		public DiffGame(Vector2 sizeFiled, Vector2 rangeMines)
		{
			this.sizeFiled = sizeFiled;
			this.rangeMines = rangeMines;
		}

		public Vector2 SizeFiled
		{
			get => sizeFiled;
			set => sizeFiled = value;
		}

		public Vector2 RangeMines
		{
			get => rangeMines;
			set => rangeMines = value;
		}
	}
}
using UniRx;
using UnityEngine;
using Util;

namespace Entity
{
	public class Field : MonoBehaviour
	{
		public Cell cell;
		public Vector2 sizeField;

		private Cell[][] arrCells;
		private Vector2 sizeCell;
		private int countRepeat;

		private void Start()
		{
			arrCells = new Cell[(int) sizeField.x][];
			sizeCell = cell.SizeCell;

			for (int i = 0; i < sizeField.x; i++)
			{
				arrCells[i] = new Cell[(int) sizeField.y];
				for (int j = 0; j < sizeField.y; j++)
				{
					Cell _c = Instantiate(cell,transform);
					_c.transform.position = new Vector2(sizeCell.x*i,sizeCell.y*j);
					arrCells[i][j] = _c;
				}
			}

			SettingBombs();

			MessageBroker.Default
				.Receive<GameMessage<Cell>>()
				.Where(message => message.Id == MessagesID.EmptyCellDown)
				.Subscribe(message =>
				{
					countRepeat = 0;
					OpenEmptyCells(message.MonoObj);
				}).AddTo(this);
		}

		private void OpenEmptyCells(Cell cell)
		{

			if(cell.IsOpen) return;
			
			cell.OpenCell();
			if(cell.CountBombAround > 0) return;
			
			Vector2 pos = cell.transform.position / sizeCell;

			int xPos = (int) pos.x;
			int yPos = (int) pos.y;
			
			int left = xPos - 1;
			int right = xPos + 1;
			int top = yPos + 1;
			int bottom = yPos - 1;
			
			// слева
			if (left >= 0)
			{
				Cell c = arrCells[left][yPos];
				if (!c.IsBomb  && !c.IsOpen)
				{
					OpenEmptyCells(c);
				}
			}
			
			// справа
			if (right < sizeField.x)
			{
				Cell c = arrCells[right][yPos];
				if (!c.IsBomb && !c.IsOpen)
				{
					OpenEmptyCells(c);
				}
			}
			
			//сверху

			if (top < sizeField.y)
			{
				Cell c = arrCells[xPos][top];
				if (!c.IsBomb && !c.IsOpen)
				{
					OpenEmptyCells(c);
				}
			}
			//снизу
			if (bottom >=0)
			{
				Cell c = arrCells[xPos][bottom];
				if (!c.IsBomb && !c.IsOpen)
				{
					OpenEmptyCells(c);
				}
			}
			//слева снизу
			if (left >= 0 && bottom >=0)
			{
				Cell c = arrCells[left][bottom];
				if (!c.IsBomb && !c.IsOpen)
				{
					OpenEmptyCells(c);
				}
			}
			// слева сверху
			if (left >= 0 && top < sizeField.y)
			{
				Cell c = arrCells[left][top];
				if (!c.IsBomb && !c.IsOpen)
				{
					OpenEmptyCells(c);
				}
			}

			
			
			
			//справа снизу
			if (right < sizeField.x && bottom >=0)
			{
				Cell c = arrCells[right][bottom];
				if (!c.IsBomb && !c.IsOpen)
				{
					OpenEmptyCells(c);
				}
			}
			// справа сверху
			if (right < sizeField.x && top < sizeField.y)
			{
				Cell c = arrCells[right][top];
				if (!c.IsBomb && !c.IsOpen)
				{
					OpenEmptyCells(c);
				}
			}
			
			
			
			
		}

		private void SettingBombs()
		{
			int bombCount = Random.Range(20,30);

			
			do
			{
				int xRandom = (int) Random.Range(0, sizeField.x);
				int yRandom = (int) Random.Range(0, sizeField.y);

				Cell randomCell = arrCells[xRandom][yRandom];
				if(randomCell.IsBomb) continue;
				
				randomCell.IsBomb = true;
				bombCount--;
			} while (bombCount > 0);
			
			for (var i = 0; i < arrCells.Length; i++)
			{
				for (int j = 0; j < arrCells[i].Length; j++)
				{
					if (arrCells[i][j].IsBomb)
					{
						CalcAroundBomb(i,j);
					}
				}
			}
		}

		private void CalcAroundBomb(int xPos, int yPos)
		{
			int left = xPos - 1;
			int right = xPos + 1;
			int top = yPos + 1;
			int bottom = yPos - 1;
			
			// слева
			if (left >= 0)
			{
				BombAround(left,yPos);
			}
			//слева снизу
			if (left >= 0 && bottom >=0)
			{
				BombAround(left,bottom);
			}
			// слева сверху
			if (left >= 0 && top < sizeField.y)
			{
				BombAround(left,top);
			}

			
			
			// справа
			if (right < sizeField.x)
			{
				BombAround(right,yPos);
			}
			//справа снизу
			if (right < sizeField.x && bottom >=0)
			{
				BombAround(right,bottom);
			}
			// справа сверху
			if (right < sizeField.x && top < sizeField.y)
			{
				BombAround(right,top);
			}
			
			
			//сверху

			if (top < sizeField.y)
			{
				BombAround(xPos,top);
			}
			//снизу
			if (bottom >=0)
			{
				BombAround(xPos,bottom);
			}
		}

		private void BombAround(int xPos,int yPos)
		{
			int countBomb = 0;
			Cell currCell = arrCells[xPos][yPos];

			if(currCell.CountBombAround > 0) return;

			int left = xPos - 1;
			int right = xPos + 1;
			int top = yPos + 1;
			int bottom = yPos - 1;

			// слева
			if (left >= 0)
			{
				if (arrCells[left][yPos].IsBomb)
					countBomb++;
			}
			//слева снизу
			if (left >= 0 && bottom >=0)
			{
				if (arrCells[left][bottom].IsBomb)
					countBomb++;
			}
			// слева сверху
			if (left >= 0 && top < sizeField.y)
			{
				if (arrCells[left][top].IsBomb)
					countBomb++;
			}

			
			
			// справа
			if (right < sizeField.x)
			{
				if (arrCells[right][yPos].IsBomb)
					countBomb++;
			}
			//справа снизу
			if (right < sizeField.x && bottom >=0)
			{
				if (arrCells[right][bottom].IsBomb)
					countBomb++;
			}
			// справа сверху
			if (right < sizeField.x && top < sizeField.y)
			{
				if (arrCells[right][top].IsBomb)
					countBomb++;
			}
			
			
			//сверху

			if (top < sizeField.y)
			{
				if (arrCells[xPos][top].IsBomb)
					countBomb++;
			}
			//снизу
			if (bottom >=0)
			{
				if (arrCells[xPos][bottom].IsBomb)
					countBomb++;
			}

			currCell.CountBombAround = countBomb;


		}
	}
}
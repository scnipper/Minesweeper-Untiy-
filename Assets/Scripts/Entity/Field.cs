using UnityEngine;

namespace Entity
{
	public class Field : MonoBehaviour
	{
		public Cell cell;
		public Vector2 sizeField;
		

		private void Start()
		{
			Vector2 sizeCell = cell.SizeCell;
			for (int i = 0; i < sizeField.x; i++)
			{
				for (int j = 0; j < sizeField.y; j++)
				{
					Cell _c = Instantiate(cell,transform);
					_c.transform.position = new Vector2(sizeCell.x*i,sizeCell.y*j);
				}
			}
		}
	}
}
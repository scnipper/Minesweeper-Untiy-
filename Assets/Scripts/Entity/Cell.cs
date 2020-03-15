using TMPro;
using UniRx;
using UnityEngine;
using Util;

namespace Entity
{
	public class Cell : MonoBehaviour
	{
		public GameObject openState;
		public GameObject closeState;
		public TextMeshPro textNum;
		public GameObject bomb;

		private bool isBomb;
		private int countBombAround;
		private bool isOpen;

		private void Start()
		{
			textNum.text = countBombAround+"";
			bomb.SetActive(isBomb);
		}


		private void OnMouseDown()
		{

			if (!isBomb && countBombAround == 0)
			{
				MessageBroker.Default.Publish(new GameMessage<Cell>(this,MessagesID.EmptyCellDown));
			}
			else
			{
				OpenCell();
			}
			
		}

		public void OpenCell()
		{
			isOpen = true;
			openState.SetActive(true);
			closeState.SetActive(false);

			if (countBombAround > 0 && !isBomb)
			{
				textNum.gameObject.SetActive(true);
			}
		}

		public Vector2 SizeCell => openState.transform.GetChild(0).GetComponent<SpriteRenderer>().size * transform.localScale;

		public int CountBombAround
		{
			get => countBombAround;
			set => countBombAround = value;
		}

		public bool IsBomb
		{
			get => isBomb;
			set => isBomb = value;
		}

		public bool IsOpen => isOpen;
	}
}

using TMPro;
using UnityEngine;

namespace Entity
{
	public class Cell : MonoBehaviour
	{
		public GameObject openState;
		public GameObject closeState;
		public TextMeshPro textNum;

		private bool isBomb;
		private int countBombAround;
		private void Start()
		{
			textNum.text = countBombAround+"";
		}


		private void OnMouseDown()
		{
			openState.SetActive(true);
			closeState.SetActive(false);

			if (countBombAround > 0)
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
	}
}
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Util;

namespace Entity
{
	public class DragField : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
	{
		private GameMessage<DragField> messageDrag;

		private void Start()
		{
			messageDrag = new GameMessage<DragField>(this,MessagesID.DragField,new Vector2());
		}

		public void OnDrag(PointerEventData eventData)
		{
			messageDrag.Data = eventData.delta;
			messageDrag.Id = MessagesID.DragField;
			MessageBroker.Default.Publish(messageDrag);
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			messageDrag.Data = true;
			messageDrag.Id = MessagesID.IsDragging;
			MessageBroker.Default.Publish(messageDrag);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			messageDrag.Data = false;
			messageDrag.Id = MessagesID.IsDragging;
			MessageBroker.Default.Publish(messageDrag);
		}


	}
}
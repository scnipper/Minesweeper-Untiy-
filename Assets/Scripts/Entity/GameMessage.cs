using UnityEngine;

namespace Entity
{
	public class GameMessage<T>  where T : MonoBehaviour
	{
		private T monoObj;
		private int id;
		private object data;

		public GameMessage(T monoObj, int id, object data = null)
		{
			this.monoObj = monoObj;
			this.id = id;
			this.data = data;
		}

		public T MonoObj
		{
			get => monoObj;
			set => monoObj = value;
		}

		public int Id
		{
			get => id;
			set => id = value;
		}

		public object Data
		{
			get => data;
			set => data = value;
		}
	}
}
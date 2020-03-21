
namespace Entity
{
	public class GameMessage
	{
		private int id;
		private object data;

		public GameMessage(int id, object data = null)
		{
			this.id = id;
			this.data = data;
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
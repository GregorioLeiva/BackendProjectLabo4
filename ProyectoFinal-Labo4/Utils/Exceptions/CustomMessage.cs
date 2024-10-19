namespace ProyectoFinal_Labo4.Utils.Exceptions
{
	public class CustomMessage
	{
		public string Message { get; set; } = null!;

		public CustomMessage(string message)
		{
			Message = message;
		}
	}
}

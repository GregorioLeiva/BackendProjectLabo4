using System.Net;

namespace ProyectoFinal_Labo4.Utils.Exceptions
{
	public class CustomHttpException : Exception
	{
		public HttpStatusCode StatusCode { get; }

		public CustomHttpException(string message, HttpStatusCode statusCode) : base(message)
		{
			StatusCode = statusCode;
		}
	}
}

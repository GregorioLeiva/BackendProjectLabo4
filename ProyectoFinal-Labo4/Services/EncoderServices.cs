namespace ProyectoFinal_Labo4.Services
{
	public interface IEncoderServices
	{
		string Encode(string str);
		bool Verify(string str, string strHash);
	}
	public class EncoderServices : IEncoderServices
	{
		public string Encode(string str)
		{
			string salt = BCrypt.Net.BCrypt.GenerateSalt(13);
			return BCrypt.Net.BCrypt.HashPassword(str, salt);
		}

		public bool Verify(string str, string strHash)
		{
			return BCrypt.Net.BCrypt.Verify(str, strHash);
		}
	}
}

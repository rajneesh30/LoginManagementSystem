using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.DataInfra.Entity
{
	/// <summary>
	/// POCO entity to setup AuthEntity object for application.
	/// </summary>
	public class AuthEntity
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string LoginIndex { get; set; }

		/// <summary>
		/// Default constructor
		/// </summary>
		public AuthEntity()
		{

		}

		/// <summary>
		/// Parametrised constructor to set entity values
		/// </summary>
		/// <param name="email"></param>
		/// <param name="password"></param>
		/// <param name="loginIndex"></param>
		public AuthEntity(string email, string password, string loginIndex)
		{
			Email = email;
			Password = password;
			LoginIndex = loginIndex;
		}
	}
}

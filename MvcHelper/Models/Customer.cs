using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcHelper.Models
{
	public class Customer
	{
		private string _firstName;
		public string FirstName
		{
			get
			{
				return _firstName;
			}
			set
			{
				_firstName = value;
			}
		}

		private string _lastName;
		public string LastName
		{
			get
			{
				return _lastName;
			}
			set
			{
				_lastName = value;
			}
		}
		private string _street;
		public string Street
		{
			get
			{
				return _street;
			}
			set
			{
				_street = value;
			}
		}

		private string _city;
		public string City
		{
			get
			{
				return _city;
			}
			set
			{
				_city = value;
			}
		}
		private string _stateCode;
		public string StateCode
		{
			get
			{
				return _stateCode;
			}
			set
			{
				_stateCode = value;
			}
		}
		private string _zip;
		public string Zip
		{
			get
			{
				return _zip;
			}
			set
			{
				_zip = value;
			}
		}
	}
}

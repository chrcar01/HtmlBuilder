using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcHelper.Models
{
	public class State
	{
		private string _code;
		public string Code
		{
			get
			{
				return _code;
			}
			set
			{
				_code = value;
			}
		}
		private string _display;
		public string Display
		{
			get
			{
				return _display;
			}
			set
			{
				_display = value;
			}
		}


	}
}
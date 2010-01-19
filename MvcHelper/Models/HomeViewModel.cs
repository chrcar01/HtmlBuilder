using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcHelper.Models
{
	public class HomeViewModel
	{
		private Customer _customer;
		public Customer Customer
		{
			get
			{
				return _customer;
			}
			set
			{
				_customer = value;
			}
		}
		private List<State> _states;
		public List<State> States
		{
			get
			{
				return _states;
			}
			set
			{
				_states = value;
			}
		}
	}
}

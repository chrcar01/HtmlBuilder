using System;
using System.Web.Mvc;
using HtmlBuilder;

namespace MvcHelper
{
	public class FluentViewPage<TModel> : ViewPage<TModel> where TModel : class
	{
		private FluentHelper<TModel> _fluent;
		public FluentHelper<TModel> Fluent
		{
			get
			{
				if (_fluent == null)
				{
					_fluent = new FluentHelper<TModel>(this.ViewContext, this);
				}
				return _fluent;
			}
		}
	}
}

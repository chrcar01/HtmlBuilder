using System;
using System.Web.Mvc;
using HtmlBuilder;
using System.Reflection;

namespace MvcHelper
{
	public class FluentHelper<TModel>
	{
		private ViewContext _viewContext;
		private IViewDataContainer _viewDataContainer;
		public ViewDataDictionary ViewData
		{
			get
			{
				return _viewContext.ViewData;
			}
		}
		public TModel Model
		{
			get
			{
				return (TModel)this.ViewData.Model;
			}
		}
		public FluentHelper(ViewContext viewContext, IViewDataContainer viewDataContainer)
		{
			_viewContext = viewContext;
			_viewDataContainer = viewDataContainer;
		}

		public Element TextBox(string id, string attributes)
		{
			Element result = new Element("input", "type=textbox")
				.AddAttribute("id", id)
				.AddAttribute("name", id)
				.AddAttribute(attributes);

			if (id.Contains("."))
			{
				string[] props = id.Split('.');
				object thing = this.Model;
				object[] index = null;
				foreach (string prop in props)
				{
					PropertyInfo propInfo = thing.GetType().GetProperty(prop);
					thing = propInfo.GetValue(thing, index);
				}
				result.AddAttribute("value", thing.ToString());
			}
			return result;
		}
	}
}

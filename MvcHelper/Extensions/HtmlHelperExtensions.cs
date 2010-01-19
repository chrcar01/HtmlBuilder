using System;

namespace System.Web.Mvc.Html
{
	public static class HtmlHelperExtensions
	{
		/// <summary>
		/// Generates a link element for referencing a css stylesheet with
		/// the href attribute pointing to the absolute path of the css file.
		/// </summary>
		/// <param name="this">The instance of the HtmlHelper being extended.</param>
		/// <param name="virtualPath">The virtual path in form of ~/path/to/stylesheet.css</param>
		/// <returns></returns>
		public static string Stylesheet(this HtmlHelper @this, string virtualPath)
		{
			string format = "<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" />";
			return String.Format(format, VirtualPathUtility.ToAbsolute(virtualPath));
		}

		/// <summary>
		/// Generates a javascript script element with the src attribute pointing to
		/// the absolute path of the js file.
		/// </summary>
		/// <param name="this">The instance of the HtmlHelper being extended.</param>
		/// <param name="virtualPath">The virtual path in form of ~/path/to/script.js</param>
		/// <returns></returns>
		public static string Script(this HtmlHelper @this, string virtualPath)
		{
			string format = "<script type=\"text/javascript\" src=\"{0}\"></script>";
			return String.Format(format, VirtualPathUtility.ToAbsolute(virtualPath));
		}

	}
}

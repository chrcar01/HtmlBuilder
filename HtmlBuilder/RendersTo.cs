using System;

namespace HtmlBuilder
{
	/// <summary>
	/// List of Writers that elements can render.
	/// </summary>
	public enum RendersTo
	{
		/// <summary>
		/// Render component through an XmlTextWriter.
		/// </summary>
		XmlTextWriter,

		/// <summary>
		/// Render component through an HtmlTextWriter.
		/// </summary>
		HtmlTextWriter
	}
}

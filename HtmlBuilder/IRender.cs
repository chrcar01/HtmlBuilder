using System;
using System.IO;
using System.Xml;
using System.Web.UI;

namespace HtmlBuilder
{
	/// <summary>
	/// Defines a contract for any implementor that can render itself to a
	/// TextWriter and/or Stream.
	/// </summary>
	public interface IRender
	{
		/// <summary>
		/// Renders the specified text.
		/// </summary>
		/// <param name="text">The text.</param>
		void Render(TextWriter text);
		/// <summary>
		/// Renders the specified stream.
		/// </summary>
		/// <param name="stream">The stream.</param>
		void Render(Stream stream);

		/// <summary>
		/// Renders the specified XML.
		/// </summary>
		/// <param name="xml">The XML.</param>
		void Render(XmlWriter xml);
		
		/// <summary>
		/// Renders the specified HtmlBuilder.
		/// </summary>
		/// <param name="html">The HtmlBuilder.</param>
		void Render(HtmlTextWriter html);
	}
}

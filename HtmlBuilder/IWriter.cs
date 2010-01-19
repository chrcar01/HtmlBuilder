using System;

namespace HtmlBuilder
{
	/// <summary>
	/// Identifies methods used by the rendering system
	/// for writing data to streams.  This was created strictly due
	/// to the differences between an XmlTextWriter and the other
	/// flavor of TextWriters like the StringWriter and HtmlTextWriter.
	/// </summary>
	public interface IWriter
	{
		/// <summary>
		/// Writes the specified s.
		/// </summary>
		/// <param name="s">The s.</param>
		void Write(string s);
		/// <summary>
		/// Writes the end tag.
		/// </summary>
		/// <param name="tagName">Name of the tag.</param>
		void WriteEndTag(string tagName);


		/// <summary>
		/// Writes the begin tag.
		/// </summary>
		/// <param name="tagName">Name of the tag.</param>
		void WriteBeginTag(string tagName);

		/// <summary>
		/// Writes the attribute.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="value">The value.</param>
		void WriteAttribute(string name, string value);

		/// <summary>
		/// Writes the specified c.
		/// </summary>
		/// <param name="c">The c.</param>
		void Write(char c);

		/// <summary>
		/// Closes the tag.
		/// </summary>
		void CloseTag();
	}
}

using System;
using System.Web.UI;
using System.IO;

namespace HtmlBuilder
{
	/// <summary>
	/// This class wraps a <see cref="HtmlTextWriter"/> behind the <see cref="IWriter"/> interface.
	/// </summary>
	public class HtmlTextWriterWrapper : IWriter
	{
		private HtmlTextWriter _html;

		/// <summary>
		/// Initializes a new instance of the <see cref="HtmlTextWriterWrapper"/> class.
		/// </summary>
		/// <param name="html">The HtmlTextWriter that will be doing the work.</param>
		public HtmlTextWriterWrapper(HtmlTextWriter html)
		{
			_html = html;
		}
		/// <summary>
		/// Initializes a new instance of the HtmlTextWriterWrapper class.
		/// </summary>
		/// <param name="text">The TextWriter used in creating an HtmlTextWriter internallly.</param>
		public HtmlTextWriterWrapper(TextWriter text)
			: this(new HtmlTextWriter(text))
		{
		}
		/// <summary>
		/// Closes a tag.  Writes an <see cref="HtmlTextWriter.TagRightChar"/> to the stream.
		/// </summary>
		public void CloseTag()
		{
			_html.Write(HtmlTextWriter.TagRightChar);
		}

		/// <summary>
		/// Writes a string to the underlying HtmlTextWriter.
		/// </summary>
		/// <param name="s">The string to render.</param>
		public void Write(string s)
		{
			_html.Write(s);
		}
		/// <summary>
		/// Writes an end tag.
		/// </summary>
		/// <param name="tagName">Name of the end tag.</param>
		public void WriteEndTag(string tagName)
		{
			_html.WriteEndTag(tagName);
		}


		/// <summary>
		/// Writes an attribute.
		/// </summary>
		/// <param name="name">The attribute name.</param>
		/// <param name="value">The attribute value.</param>
		public void WriteAttribute(string name, string value)
		{
			_html.WriteAttribute(name, value);
		}

		/// <summary>
		/// Writes a begin tag.
		/// </summary>
		/// <param name="tagName">Name of the tag.</param>
		public void WriteBeginTag(string tagName)
		{
			_html.WriteBeginTag(tagName);
		}

		/// <summary>
		/// Writes the specified char to the stream.
		/// </summary>
		/// <param name="c">The char to write.</param>
		public void Write(char c)
		{
			_html.Write(c);
		}

		
		void IDisposable.Dispose()
		{
			((IDisposable)_html).Dispose();
		}

	}
}

using System;
using System.Xml;
using System.IO;

namespace HtmlBuilder
{
	/// <summary>
	/// Wraps an XmlWriter so that it conforms to the IWriter interface.
	/// </summary>
	public class XmlWriterWrapper : IWriter
	{
		private XmlWriter _xml;
		/// <summary>
		/// Initializes a new instance of the XmlWriterWrapper class.
		/// </summary>
		/// <param name="xml"></param>
		public XmlWriterWrapper(XmlWriter xml)
		{
			_xml = xml;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="XmlWriterWrapper"/> class.
		/// </summary>
		/// <param name="text">The text writer.  Internally this is used the ctor of an XmlTextWriter.</param>
		public XmlWriterWrapper(TextWriter text)
			: this(new XmlTextWriter(text))
		{
		}

		/// <summary>
		/// Closes the tag.  This does nothing for the <see cref="HtmlBuilder.XmlWriterWrapper"/>
		/// </summary>
		public void CloseTag()
		{
			//do nothing
		}

		/// <summary>
		/// Writes the specified string to the underlying <see cref="XmlTextWriter"/>, calls the WriteRaw method.
		/// </summary>
		/// <param name="s">The string to write.</param>
		public void Write(string s)
		{
			_xml.WriteRaw(s);
		}

		/// <summary>
		/// Writes the begin tag.
		/// </summary>
		/// <param name="tagName">Name of the tag.</param>
		public void WriteBeginTag(string tagName)
		{
			_xml.WriteStartElement(tagName);
		}
		/// <summary>
		/// Writes the end tag.
		/// </summary>
		/// <param name="tagName">Name of the tag.</param>
		public void WriteEndTag(string tagName)
		{
			_xml.WriteEndElement();
		}

		/// <summary>
		/// Writes the attribute.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="value">The value.</param>
		public void WriteAttribute(string name, string value)
		{
			_xml.WriteAttributeString(name, value);
		}

		/// <summary>
		/// Writes the specified c.  Calls WriteRaw on the underlying <see cref="XmlTextWriter"/>
		/// </summary>
		/// <param name="c">The c.</param>
		public void Write(char c)
		{
			_xml.WriteRaw(c.ToString());
		}

		
		void IDisposable.Dispose()
		{
			((IDisposable)_xml).Dispose();
		}

	}
}

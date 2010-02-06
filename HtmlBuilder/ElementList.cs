using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.IO;
using System.Xml;

namespace HtmlBuilder
{
	/// <summary>
	/// List of elements.  This can be used to render a list of sibling elements.  For example,
	/// String blah = new ElementList(new Element("p").Update("test"), new Element("b").Update("poop"))
	/// would render <p>test</p><b>poop</b>
	/// </summary>
	public class ElementList : List<Element>, IRender, IRenderTo
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ElementList"/> class.
		/// </summary>
		/// <param name="elements">The sibling elements.</param>
		public ElementList(params Element[] elements)
		{
			foreach (Element element in elements)
			{
				if (element == null)
					continue;

				this.Add(element);
			}
		}


		/// <summary>
		/// Renders the specified HtmlBuilder.
		/// </summary>
		/// <param name="html">The HtmlBuilder.</param>
		public virtual void Render(HtmlTextWriter html)
		{
        	foreach(Element element in this)
            {
				element.Render(html);
            }
        }

		/// <summary>
		/// Renders the specified XML.
		/// </summary>
		/// <param name="xml">The XML.</param>
		public virtual void Render(XmlWriter xml)
		{
			foreach(Element element in this)
			{
				element.Render(xml);
			}
		}

		/// <summary>
		/// Renders the html to the TextWriter instance.  The Render(TextWriter) method
		/// will use an HtmlTextWriter instance to ultimately perform the rendering.
		/// </summary>
		/// <param name="writer">The TextWriter instance to which the html is written.</param>
		public virtual void Render(TextWriter writer)
		{
			using (HtmlTextWriter html = new HtmlTextWriter(writer))
			{
				this.Render(html);
			}
		}

		/// <summary>
		/// Renders the html to the Stream instance.
		/// </summary>
		/// <param name="stream">The Stream to which the html is written.</param>
		public virtual void Render(Stream stream)
		{
			using (StringWriter textWriter = new StringWriter())
			using (HtmlTextWriter html = new HtmlTextWriter(textWriter))
			{
				this.Render(html);
				using (StreamWriter streamWriter = new StreamWriter(stream))
				{
					streamWriter.Write(textWriter.ToString());
				}
			}
		}
		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.  Internally, this 
		/// creates a StringWriter, calls the Render(TextWriter writer) method and returns the result.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		public override string ToString()
		{
			string result = "";
			using (StringWriter writer = new StringWriter())
			{
				this.Render(writer);
				result = writer.ToString();
			}
			return result;
		}


        /// <summary>
        /// Performs an implicit conversion from <see cref="HtmlBuilder.ElementList"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="elements">The elements.</param>
        /// <returns>The result of the conversion.</returns>
		public static implicit operator string(ElementList elements)
		{
			return elements.ToString();
		}


		/// <summary>
		/// Renders a component to the specified writer.
		/// </summary>
		/// <param name="renderTo">The writer to use as the for rendering.</param>
		/// <returns></returns>
		public string Render(RendersTo renderTo)
		{
			string result = String.Empty;
			using (var textWriter = new StringWriter())
			using (IWriter renderWriter = CreateWrapper(textWriter, renderTo))
			{
				foreach (var element in this)
				{
					element.Render(renderWriter);
				}
				result = textWriter.ToString();
			}
			return result;
		}

		private IWriter CreateWrapper(StringWriter textWriter, RendersTo renderTo)
		{
			switch (renderTo)
			{
				case RendersTo.XmlTextWriter: return new XmlWriterWrapper(textWriter);
				case RendersTo.HtmlTextWriter:
				default: return new HtmlTextWriterWrapper(textWriter);
			}
		}
	}
}

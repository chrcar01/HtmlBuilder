using System;
using System.IO;
using System.Web.UI;
using System.Xml;

namespace HtmlBuilder
{
	/// <summary>
	/// This is the base class for any class that wants to render itself as HtmlBuilder.  
	/// </summary>
	public abstract class RenderableComponent : IRender, IRenderTo
	{

		/// <summary>
		/// Renders the specified writer.
		/// </summary>
		/// <param name="writer">The writer.</param>
		public abstract void Render(IWriter writer);

		/// <summary>
		/// Renders the specified HtmlBuilder.
		/// </summary>
		/// <param name="html">The HtmlBuilder.</param>
		public void Render(HtmlTextWriter html)
		{
			this.Render(new HtmlTextWriterWrapper(html));
		}

		/// <summary>
		/// Renders the specified XML writer.
		/// </summary>
		/// <param name="xmlWriter">The XML writer.</param>
		public void Render(XmlWriter xmlWriter)
		{
			this.Render(new XmlWriterWrapper(xmlWriter));
		}

		/// <summary>
		/// Renders the html to the TextWriter instance.
		/// </summary>
		/// <param name="writer">The TextWriter instance to which the html is written.</param>
		public void Render(TextWriter writer)
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
		public void Render(Stream stream)
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
		/// Renders the current component through different writers.
		/// </summary>
		/// <param name="renderTo">The type of writer to use when rendering.</param>
		/// <returns></returns>
		public string Render(RendersTo renderTo)
		{
			string result = String.Empty;
			using (var textWriter = new StringWriter())
			{
				switch (renderTo)
				{
					case RendersTo.XmlTextWriter:
						using(var xmlWriter = new XmlTextWriter(textWriter))
						{
							this.Render(xmlWriter);
						}
						break;

					case RendersTo.HtmlTextWriter:
					default:
						using (var htmlWriter = new HtmlTextWriter(textWriter))
						{
							this.Render(htmlWriter);
						}
						break;
				}
				result = textWriter.ToString();
			}
			return result;
		}

	}
}

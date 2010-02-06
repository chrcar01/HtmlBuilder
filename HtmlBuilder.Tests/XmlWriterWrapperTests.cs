using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml;
using System.IO;
using HtmlBuilder;

namespace HtmlBuilderBuilder.Tests
{
	[TestFixture]
	public class XmlWriterWrapperTests
	{
		[Test]
		public void XmlWriterWrapperCanBeUsedInUsingWithTextWriter()
		{
			var el = new Element("img", "src=myimage.gif");
			var expected = "<img src=\"myimage.gif\" />";
			var actual = String.Empty;
			using (var textWriter = new StringWriter())
			using (var xml = new XmlWriterWrapper(textWriter))
			{
				el.Render(xml);
				actual = textWriter.ToString();
			}
			Assert.AreEqual(expected, actual);
		}
		[Test]
		public void XmlWriterWrapperWrapsXmlTextWriterBehindIWriter()
		{
			string actual;
			using (StringWriter text = new StringWriter())
			{
				XmlTextWriter xml = new XmlTextWriter(text);
				IWriter writer = new XmlWriterWrapper(xml);
				writer.WriteBeginTag("span");
				writer.WriteAttribute("class", "test");
				writer.CloseTag();
				writer.Write('T');
				writer.Write("his rocks");
				writer.WriteEndTag("span");
				actual = text.ToString();
			}
			Assert.AreEqual("<span class=\"test\">This rocks</span>", actual);
		}
	}
}

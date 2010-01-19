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

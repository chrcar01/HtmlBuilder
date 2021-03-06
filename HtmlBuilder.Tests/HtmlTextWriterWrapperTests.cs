﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Web.UI;
using System.IO;
using HtmlBuilder;

namespace HtmlBuilderBuilder.Tests
{
	[TestFixture]
	public class HtmlTextWriterWrapperTests
	{
		[Test]
		public void HtmlTextWriterWrapperCanBeUsedInUsingBlockWithTextWriter()
		{
			var el = new Element("img", "src=myimage.gif");
			var expected = "<img src=\"myimage.gif\"></img>";
			var actual = String.Empty;
			using (var textWriter = new StringWriter())
			using (var htmlWrapper = new HtmlTextWriterWrapper(textWriter))
			{
				el.Render(htmlWrapper);
				actual = textWriter.ToString();
			}
			Assert.AreEqual(expected, actual);
		}
		[Test]
		public void HtmlTextWriterWrapperWrapsHtmlTextWriterBehindIWriter()
		{
			string actual;
			using (StringWriter text = new StringWriter())
			using (HtmlTextWriter html = new HtmlTextWriter(text))
            {
				IWriter writer = new HtmlTextWriterWrapper(html);
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

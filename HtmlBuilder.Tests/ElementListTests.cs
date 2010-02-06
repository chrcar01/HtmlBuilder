using System;
using System.IO;
using NUnit.Framework;
using HtmlBuilder;
using System.Web.UI;
using System.Xml;

namespace HtmlBuilderBuilder.Tests
{
	[TestFixture]
	public class ElementListTests
	{
		[Test]
		public void ElementListCanRenderAsHtml()
		{
			var list = new ElementList(new Element("input", "type=text"), new Element("img", "src=stoopid.gif"));
			var expected = "<input type=\"text\"></input><img src=\"stoopid.gif\"></img>";
			var actual = list.Render(RendersTo.HtmlTextWriter);
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void ElementListCanRenderAsXml()
		{
			var list = new ElementList(new Element("input", "type=text"), new Element("img", "src=stoopid.gif"));
			var expected = "<input type=\"text\" /><img src=\"stoopid.gif\" />";
			var actual = list.Render(RendersTo.XmlTextWriter);
			Assert.AreEqual(expected, actual);
		}
		[Test]
		public void NullElementsNotAddedToElementList()
		{
			Element nullelement = null;
			ElementList list = new ElementList(new Element("b").Update("blah"), nullelement);
			Assert.AreEqual("<b>blah</b>", list.ToString());
		}
		[Test]
		public void CanRenderToXmlWriter()
		{
			ElementList list = new ElementList(
				new Element("b").Update("Chris"),
				new Element("i").Update("Emmitt"));
			string expected = "<b>Chris</b><i>Emmitt</i>";
			string actual;
			using (StringWriter text = new StringWriter())
            {
				XmlTextWriter xml = new XmlTextWriter(text);
				list.Render(xml);
				actual = text.ToString();
            }
			Assert.AreEqual(expected, actual);
		}
		[Test]
		public void CanRenderToStream()
		{
			ElementList list = new ElementList(
				new Element("b").Update("Chris"),
				new Element("i").Update("Emmitt"));
			string expected = "<b>Chris</b><i>Emmitt</i>";
			string actual;
			byte[] buffer = new byte[expected.Length];
			using (MemoryStream stream = new MemoryStream(buffer))
            {
				list.Render(stream);
				actual = System.Text.ASCIIEncoding.ASCII.GetString(buffer);
            }
			Assert.AreEqual(expected, actual);
		}
		[Test]
		public void VerityCtor()
		{
			ElementList noargslist = new ElementList();
			noargslist.Add(new Element("b").Update("Chris"));
			Assert.AreEqual("<b>Chris</b>", noargslist.ToString());

			ElementList argslist = new ElementList(
				new Element("b").Update("Riley"),
				new Element("i").Update("Emmitt"));
			string actual = argslist;
			Assert.AreEqual("<b>Riley</b><i>Emmitt</i>", actual);
						
		}
		[Test]
		public void NullElementsAreNotAddedToList()
		{
			Element nullel = null;
			Element el = new Element("b").Update("Anja");
			ElementList list = new ElementList(nullel, el);
			Assert.AreEqual(1, list.Count);
			Assert.AreEqual("<b>Anja</b>", list.ToString());
		}
	}
}

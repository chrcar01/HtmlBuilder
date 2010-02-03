using System;
using NUnit.Framework;
using HtmlBuilder;
using System.Web.UI;
using System.IO;
using System.Xml;

namespace HtmlBuilderBuilder.Tests
{
	[TestFixture]
	public class ElementTests
	{
		[Test]
		public void CanRenderElementToHtmlTextWriter()
		{
			Assert.AreEqual("<img></img>", new Element("img").Render(RendersTo.HtmlTextWriter));
		}
		[Test]
		public void CanRenderElementAsXmlTextWriter()
		{
			Assert.AreEqual("<img />", new Element("img").Render(RendersTo.XmlTextWriter));
		}
		[Test]
		public void Test()
		{
			string input = "maxlength=30;style=font-weight:bold;font-size:20px;padding:30px;height=20px;";
			string expected = "<input maxlength=\"30\" height=\"20px\" style=\"font-weight:bold;font-size:20px;padding:30px;\"></input>";
			Assert.AreEqual(expected, new Element("input", input).ToString());
		}
		[Test]
		public void NullChildElementsAreIgnoredDuringRendering()
		{
			Element iAmNull = null;

			Element myelement = new Element("p");
			myelement.Children.Add(iAmNull);
			Assert.AreEqual("<p></p>", myelement.ToString());
		}
		[Test]
		public void NullAttributeNameDoesNotAffectElement()
		{
			Assert.AreEqual("<p></p>", new Element("p").AddAttribute(null, null).ToString());
		}
		[Test]
		public void CanSetInlineStyle()
		{
			Assert.AreEqual("<p style=\"margin:3;border:1;\"></p>", new Element("p", "style=margin:3;border:0;border:1;").ToString());
		}
		[Test]
		public void CanSetClassInline()
		{
			Assert.AreEqual("<span height=\"200px\" class=\"sooper\"></span>", new Element("span", "height=200px;class=sooper;").ToString());
		}
		[Test]
		public void SettingEmptyStyleDoesNotAffectElement()
		{
			Assert.AreEqual("<p></p>", new Element("p", "style=").ToString());
		}
		[Test]
		public void SettingEmptyClassDoesNotAffectTheElement()
		{
			Assert.AreEqual("<p></p>", new Element("p", "class=").ToString());
		}
		[Test]
		public void CanSetSingleValue()
		{
			Assert.AreEqual("<option value=\"200px\"></option>", new Element("option", "value=200px").ToString());
		}
		

		[Test]
		public void NullChildElementsDuringInsertReturnsTheInstanceUnchanged()
		{
			Element[] nullElements = null;
			Assert.AreEqual("<select></select>", new Element("select").Insert(nullElements).ToString());
			Element nullElement = null;
			nullElements = new Element[] { nullElement };
			Assert.AreEqual("<select></select>", new Element("select").Insert(nullElements).ToString());
			
		}
		[Test]
		public void NullChildElementsSkippedInCtor()
		{
			Element nullElement = null;
			Element select = new Element("select", nullElement, new Element("option").Update("chris"));
			Assert.AreEqual("<select><option>chris</option></select>", select.ToString());
		}
		[Test]
		public void CanAddAttributeWithFormat()
		{
			Element div = new Element("div");
			div.AddAttribute("id", "name_{0}", "chris");
			Assert.AreEqual("<div id=\"name_chris\"></div>", div.ToString());
			Assert.IsTrue(div.HasAttribute("id"));
		}
		[Test]
		public void VerifyNullElementsAreSkippedDuringInsert()
		{
			Element nullelement = null;
			Element select = new Element("select");
			select.Insert(new Element("option").Update("Item 1"), nullelement);
			Assert.AreEqual(1, select.Children.Count);
			Assert.AreEqual("<select><option>Item 1</option></select>", select.ToString());
		}
		[Test]
		public void VerifyInsertDefaultsToIndexZero()
		{
			Element select = new Element("select");
			select.Insert(new Element("option").Update("Item 1"));
			select.Insert(new Element("option").Update("Item 2"));
			Assert.AreEqual("<select><option>Item 2</option><option>Item 1</option></select>", select.ToString());
		}
		[Test]
		public void VerifyCanInsertByIndex()
		{
			Element select = new Element("select")
				.Append(new Element("option", "value=0").Update("Create New"))
				.Insert(0, new Element("option", "value=").Update(":: Select ::"));
			string expected = "<select><option value=\"\">:: Select ::</option><option value=\"0\">Create New</option></select>";
			Assert.AreEqual(expected, select.ToString());
			Assert.AreEqual("<option value=\"\">:: Select ::</option>", select.Children[0].ToString());
			
		}
		[Test]
		public void VerifyAddition()
		{
			string expected = "<h1></h1><b></b>hello";
			string actual = (new Element("h1")) + new Element("b") + "hello";
			Assert.AreEqual(expected, actual); 
		}
		[Test]
		public void CanRenderToStream()
		{
			string expected = "<p>Hello World</p>";
			byte[] buffer = new byte[expected.Length];
			using (MemoryStream stream = new MemoryStream(buffer))
			{
				Element para = new Element("p").Update("Hello World");
				para.Render(stream);
				string actual = System.Text.ASCIIEncoding.ASCII.GetString(buffer);
				Assert.AreEqual(expected, actual);
			}
		}

		[Test]
		public void CanRenderToTextWriter()
		{
			string expected = @"<span>Hello World</span>";
			string actual = "";
			using (StringWriter writer = new StringWriter())
			{
				new Element("span")
					.Update("Hello World")
					.Render(writer);
				actual = writer.ToString();
			}
			Assert.AreEqual(expected, actual);
		}
		[Test]
		public void CanRenderHtmlTextWriter()
		{
			string expected = @"<span>Hello World</span>";
			string actual = "";
			using (StringWriter writer = new StringWriter())
			using (HtmlTextWriter html = new HtmlTextWriter(writer))
			{
				new Element("span")
					.Update("Hello World")
					.Render(html);
				actual = writer.ToString();
			}
			Assert.AreEqual(expected, actual);
		}
		[Test]
		public void CanRenderToXmlTextWriter()
		{
			string expected = @"<p>
  <span>chris</span>
</p>";
			string actual = "";
			using (StringWriter text = new StringWriter())
            {
				XmlTextWriter xml = new XmlTextWriter(text);
				xml.Formatting = Formatting.Indented;
				new Element("p", new Element("span").Update("chris")).Render(xml);
				actual = text.ToString();
            }
			Assert.AreEqual(expected, actual);
		}
		[Test]
		public void VerifyToString()
		{
			string expected = @"<span>Hello World</span>";
			string actual = new Element("span").Update("Hello World").ToString();
			Assert.AreEqual(expected, actual);
		}


		[Test]
		public void VerifyInsert()
		{
			Element table = new Element("table");
			Element thead = new Element("thead");
			Element tr = new Element("tr");
			for (int i = 0; i < 3; i++)
			{
				tr.Insert(new Element("th").Update("Blah"));
			}
			table.Append(thead.Append(tr));
			string actual = table.ToString();
			string expected = "<table><thead><tr><th>Blah</th><th>Blah</th><th>Blah</th></tr></thead></table>";
			Assert.AreEqual(expected, actual);
		}
		[Test]
		public void CanCreateHtmlSelectWithOptions()
		{
			string expected =
				"<select id=\"select1\" name=\"select1\">" +
				"<option value=\"1\">Chris</option>" +
				"<option value=\"2\">Anja</option>" +
				"<option value=\"3\">Riley</option>" +
				"<option value=\"4\">Emmitt</option>" +
				"</select>";
			string actual = new Element("select", "id=select1;name=select1;")
				.Append(new Element("option", "value=1").Update("Chris"))
				.Append(new Element("option", "value=2").Update("Anja"))
				.Append(new Element("option", "value=3").Update("Riley"))
				.Append(new Element("option", "value=4").Update("Emmitt"))
				.ToString();
			Assert.AreEqual(expected, actual);
		}
		
		[Test]
		public void VerifyTagName()
		{
			Element para = new Element("p");
			Assert.AreEqual("p", para.TagName);
			Assert.AreEqual("<p></p>", para.ToString());
		}
		[Test]
		public void CanReturnStringImplicitlyFromElement()
		{
			string expected = "<p></p>";
			string actual = new Element("p");
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void NullOrEmptyChildElementsAsArgsToAppendMethodAreNotAppended()
		{
			Element body = new Element("body");
			Element[] nullarray = null;
			Assert.AreEqual("<body></body>", body.Append(nullarray).ToString());
			Assert.AreEqual(0, body.Children.Count);
			Element[] emptyarray = new Element[0] { };
			Assert.AreEqual(0, body.Children.Count);
		}
		[Test]
		public void NullElementsAreNotAppendedAndDoNotThrowExceptions()
		{
			Element body = new Element("body");
			Element iamnull = null;
			Element form = new Element("form");
			body.Append(iamnull, form);
			Assert.AreEqual(1, body.Children.Count);
			Assert.AreEqual("<body><form></form></body>", body.ToString());
		}
		[Test]
		public void CanUpdateContentsOfElementsWithInnerHtml()
		{
			Element div = new Element("div");
			div.InnerHtml = "chris";
			Assert.AreEqual("<div>chris</div>", div.ToString());
			div.InnerHtml = "anja";
			Assert.AreEqual("<div>anja</div>", div.ToString());
		}
		[Test]
		public void CanUpdateContentsOfElementsWithUpdate()
		{
			Assert.AreEqual("<b>chris</b>", new Element("b").Update("chris").ToString());
			Assert.AreEqual("<label>chris : 6/18/1970</label>", new Element("label").Update("chris : {0}", new DateTime(1970, 6, 18).ToString("M/dd/yyyy")).ToString());
		}
		[Test]
		public void AddingStyleAttributesWithAddStyle()
		{
			Assert.AreEqual("<div style=\"display:none;\"></div>", new Element("div").AddAttribute("style", "display:none").ToString());
			Assert.AreEqual("<div style=\"display:none;\"></div>", new Element("div").AddAttribute("style", "display:none;").ToString());
			Assert.AreEqual("<div style=\"display:block;border:solid 1px black;\"></div>", new Element("div").AddAttribute("style", "display:block;border:solid 1px black;").ToString());
			Assert.AreEqual("<p style=\"display:;\"></p>", new Element("p").AddAttribute("style", "display:").ToString());
			Assert.AreEqual("<p style=\"display:;border:0;\"></p>", new Element("p").AddAttribute("style", "display:;border:0;").ToString());
			
			Assert.AreEqual("<p style=\"font-weight:bold;\"></p>", new Element("p").AddAttribute("style", "font-weight:normal;").AddAttribute("style", "font-weight:bold;").ToString());
		}
		[Test]
		public void StylesAreLastAttributeValueWins()
		{
			string expected = "<span style=\"width:100px;\"></span>";
			string actual = new Element("span").AddAttribute("style=width:50px;").AddAttribute("style=width:100px;");
			Assert.AreEqual(expected, actual);
			Assert.AreEqual("<span width=\"100px\"></span>", new Element("span", "width=100px;").ToString());
		}
		
				
		[Test]
		public void IndexerTests()
		{
			Element span = new Element("p");
			span["id"] = "blah";
			Assert.AreEqual("blah", span["id"], "can get an attribute previously set");
			span["id"] = "poop";
			Assert.AreEqual("poop", span["id"], "you can update attribute values without creating duplicates");
			Assert.AreEqual(null, span["nonexistent"], "non existent attributes return a null value");
			Assert.AreEqual(null, span[null], "null attributename returns null value");
			Assert.AreEqual(null, span[String.Empty], "empty attributename returns null value");
		}
		[Test]
		public void CtorTests()
		{
			Assert.AreEqual("<span></span>", new Element("span").ToString());
			Assert.AreEqual("<span id=\"emmitt\"></span>", new Element("span", "id=emmitt").ToString());
			Assert.AreEqual("<table height=\"100%\" width=\"66%\"><tr><th>Yay</th></tr></table>",
				new Element("table", "height=100%;width=66%;", new Element("tr", new Element("th").Update("Yay"))).ToString());
		}
		[Test]
		public void AddingCssClasses()
		{
			Assert.AreEqual("<span class=\"required\"></span>", new Element("span").AddAttribute("class", "required").ToString());
			Assert.AreEqual("<span class=\"important data\"></span>", new Element("span").AddAttribute("class", "important").AddAttribute("class", "data").ToString());
			Assert.AreEqual("<p class=\"crazy funny\"></p>", new Element("p").AddAttribute("class", "crazy funny").ToString());
			Assert.AreEqual("<p class=\"crazy\"></p>", new Element("p").AddAttribute("class", "crazy").ToString(), "should skip null classes without error");
			Assert.AreEqual("<p class=\"crazy\"></p>", new Element("p").AddAttribute("class", "crazy").AddAttribute("").ToString(), "should skip empty classes without error");
			Assert.AreEqual("<p class=\"crazy\"></p>", new Element("p").AddAttribute("class", "crazy crazy").ToString(), "should not be able to add duplicate classes");
		}
		[Test]
		public void CanRemoveAttributes()
		{
			Element table = new Element("table", "border=0;width=100%;");
			table.RemoveAttribute("border");
			string expected = "<table width=\"100%\"></table>";
			string actual = table;
			Assert.AreEqual(expected, actual);
		}
		
				
		[Test]
		public void CanAddAttributeWithoutEquals()
		{
			Assert.AreEqual("<p display=\"\"></p>", 
				new Element("p").AddAttribute("display=").ToString());
		}
		[Test]
		public void CanAddNullAttributeValue()
		{
			string value = null;
			Assert.AreEqual("<span blah=\"\"></span>", 
				new Element("span").AddAttribute("blah", value).ToString());
		}

		[Test]
		public void CanAddEmptyAttributeValue()
		{
			Assert.AreEqual("<span blah=\"\"></span>", new Element("span").AddAttribute("blah", "").ToString());
			Assert.AreEqual("<option value=\"\"></option>", new Element("option", "value=").ToString());
		}

	}
}

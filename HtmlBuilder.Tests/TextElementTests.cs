using System;
using NUnit.Framework;
using HtmlBuilder;

namespace HtmlBuilderBuilder.Tests
{
	[TestFixture]
	public class TextElementTests
	{
		[Test]
		public void Test()
		{
			string expected = "<b>Hello</b><i>World</i>";
			string actual = new TextElement(expected);
			Assert.AreEqual(expected, actual);
			Assert.AreEqual(expected, new TextElement(expected).Text);
		}
	}
}

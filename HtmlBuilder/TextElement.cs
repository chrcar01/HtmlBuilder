using System;

namespace HtmlBuilder
{
	/// <summary>
	/// Represents a special element used for literal text.  Using this element
	/// allows the use of strings to act as Elements.  So "Hello World" can become
	/// an <see cref="HtmlBuilder.Element"></see> simply by wrapping it in a TextElement like
	/// this: new TextElement("Hello World")
	/// </summary>
	public class TextElement : Element
	{
		private string _text;

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text
		{
			get
			{
				return _text;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TextElement"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		public TextElement(string text)
		{
			_text = text;
		}

		/// <summary>
		/// Renders this element to the html writer.
		/// </summary>
		/// <param name="writer">The html writer this element and it's children are rendered.</param>
		/// <example>
		/// string expected = @"<span>Hello World</span>";
		/// string actual = "";
		/// using (StringWriter writer = new StringWriter())
		/// using (HtmlTextWriter html = new HtmlTextWriter(writer))
		/// {
		/// new Element("span")
		/// .Update("Hello World")
		/// .Render(html);
		/// actual = writer.ToString();
		/// }
		/// Assert.AreEqual(expected, actual);
		/// </example>
		public override void Render(IWriter writer)
		{
			writer.Write(Text);
		}
	}
}

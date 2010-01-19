using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace HtmlBuilder
{
	/// <summary>
	/// Represents a simple html element.
	/// </summary>
	public class Element : RenderableComponent
	{

		/// <summary>
		/// Gets or sets the attribute name.  If the attribute does not exist it
		/// is added, otherwise it is updated.
		/// </summary>
		/// <value></value>
		public string this[string attributeName]
		{
			get
			{
				if (String.IsNullOrEmpty(attributeName) || !this.Attributes.ContainsKey(attributeName))
				{
					return null;
				}
				return this.Attributes[attributeName];
			}
			set
			{
				this.Attributes[attributeName] = value;
			}
		}

		private string _tagName = "";
		/// <summary>
		/// Gets the name of the tag.  This can only be set in the ctor.
		/// </summary>
		/// <value>The name of the tag.</value>
		public string TagName
		{
			get
			{
				return _tagName;
			}
		}

		private List<Element> _children = null;
		/// <summary>
		/// Gets the child elements.
		/// </summary>
		/// <value>The child elements.</value>
		public List<Element> Children
		{
			get
			{
				if (_children == null)
					_children = new List<Element>();
				return _children;
			}
		}

		private string _innerHtml;
		/// <summary>
		/// Gets or sets the inner HTML of the element.
		/// </summary>
		/// <value>The inner HTML of the element.</value>
		public string InnerHtml
		{
			get
			{
				return _innerHtml;
			}
			set
			{
				_innerHtml = value;
			}
		}

		private IDictionary<string, string> _attributes = null;
		/// <summary>
		/// Gets the attributes for the element.  This returns a readonly collection.
		/// </summary>
		public IDictionary<string, string> Attributes
		{
			get
			{
				if (_attributes == null)
				{
					_attributes = new Dictionary<string, string>();
				}
				return _attributes;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Element"/> class.
		/// </summary>
		internal Element()
		{			
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Element"/> class.
		/// </summary>
		/// <param name="tagName">Name of the tag.</param>
		/// <example>
		/// 	string expected = "<span>Hello</span>";
		/// 	string actual =
		/// 		new Element("span")
		/// 			.Update("Hello")).ToString();
		/// 	Assert.AreEqual(expected, actual);
		/// </example>
		public Element(string tagName)
			: this(tagName, String.Empty, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Element"/> class.
		/// </summary>
		/// <param name="tagName">Name of the tag.</param>
		/// <param name="childElements">The child elements.</param>
		/// <example>
		/// 	string expected = "<span><b>Hello</b></span>";
		/// 	string actual =
		/// 		new Element("span",
		/// 			new Element("b")
		/// 				.Update("Hello")).ToString();
		/// 	Assert.AreEqual(expected, actual);
		/// </example>
		public Element(string tagName, params Element[] childElements)
			: this(tagName, String.Empty, childElements)
		{
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="Element"/> class.
		/// </summary>
		/// <param name="tagName">Name of the tag.</param>
		/// <param name="nameValuePairsSeparatedByAnEqualsSign">The name value pair separated by an 
		/// equals sign. eg: height=520px;width=100%;border=0;</param>
		/// <param name="childElements">The child elements.</param>
		public Element(string tagName, string nameValuePairsSeparatedByAnEqualsSign, params Element[] childElements)
		{
			_tagName = tagName;

			if (childElements != null && childElements.Length > 0)
			{
				foreach (Element element in childElements)
				{
					if (element == null)
						continue;

					this.Children.Add(element);
				}
			}

			this.AddAttribute(nameValuePairsSeparatedByAnEqualsSign);
		}


		/// <summary>
		/// Determines whether the specified name has attribute.
		/// </summary>
		/// <param name="attributeName">The name of the attribute to test.</param>
		/// <returns>
		/// 	<c>true</c> if the specified name has attribute; otherwise, <c>false</c>.
		/// </returns>
		public bool HasAttribute(string attributeName)
		{
			return this.Attributes.ContainsKey(attributeName);
		}

        /// <summary>
		/// Renders the attributes.
		/// </summary>
		/// <param name="writer">The writer.</param>
		protected virtual void RenderAttributes(IWriter writer)
		{
			foreach (KeyValuePair<string, string> kvp in this.Attributes)
			{
				writer.WriteAttribute(kvp.Key, kvp.Value);
			}

			string attr = "";
			foreach (KeyValuePair<string, string> style in this.Styles)
			{
				attr += String.Format("{0}:{1};", style.Key, style.Value);
			}
			if (!String.IsNullOrEmpty(attr))
				writer.WriteAttribute("style", attr);

			string cssclass = "";
			foreach (string cssClassName in this.CssClasses)
			{
				cssclass += cssClassName + " ";
			}
			if (!String.IsNullOrEmpty(cssclass))
				writer.WriteAttribute("class", cssclass.Trim());
		}

		/// <summary>
		/// Renders the start tag.
		/// </summary>
		/// <param name="writer">The HtmlBuilder.</param>
		protected virtual void RenderStartTag(IWriter writer)
		{
			writer.WriteBeginTag(this.TagName);
			RenderAttributes(writer);
			writer.CloseTag();
			
		}

		/// <summary>
		/// Renders the children.
		/// </summary>
		/// <param name="writer">The HtmlBuilder.</param>
		protected virtual void RenderChildren(IWriter writer)
		{
			foreach (Element child in Children)
			{
				if (child == null)
					continue;

				child.Render(writer);
			}
		}

		/// <summary>
		/// Renders the end tag.
		/// </summary>
		/// <param name="writer">The HtmlBuilder.</param>
		protected virtual void RenderEndTag(IWriter writer)
		{
			writer.WriteEndTag(this.TagName);
		}
		/// <summary>
		/// Renders the content.
		/// </summary>
		/// <param name="html">The HtmlBuilder.</param>
		protected virtual void RenderContent(IWriter html)
		{
			if (String.IsNullOrEmpty(this.InnerHtml))
				return;

			html.Write(this.InnerHtml);
			//this.WriteEndOfLineChar(html);
		}
		/// <summary>
		/// Renders this element to the html writer.
		/// </summary>
		/// <param name="writer">The html writer this element and it's children are rendered.</param>
		/// <example>
		///		string expected = @"<span>Hello World</span>";
		/// 	string actual = "";
		/// 	using (StringWriter writer = new StringWriter())
		/// 	using (HtmlTextWriter html = new HtmlTextWriter(writer))
        ///     {
        ///     	new Element("span")
		/// 			.Update("Hello World")
		/// 			.Render(html);
		/// 		actual = writer.ToString();
        ///     }
		/// 	Assert.AreEqual(expected, actual);
		/// </example>
		override public void Render(IWriter writer)
		{
			RenderStartTag(writer);
			RenderChildren(writer);
			RenderContent(writer);
			RenderEndTag(writer);
		}

		/// <summary>
		/// Replaces the content of this element with innerHtml
		/// </summary>
		/// <param name="innerHtml">The content to set as the inner html of this element.</param>
		/// <returns>This element with it's content updated to the specified HtmlBuilder.</returns>
		/// <example>
		/// string expected = @"<span>Hello World</span>";
		/// string actual = new Element("span").Update("Hello World").ToString();
		/// Assert.AreEqual(expected, actual);
		/// </example>
		public virtual Element Update(string innerHtml)
		{
			InnerHtml = innerHtml;
			return this;
		}

		/// <summary>
		/// Replaces the content of this element with innerHtml
		/// </summary>
		/// <param name="innerHtmlFormat">The inner HTML format.</param>
		/// <param name="args">The args.</param>
		/// <returns></returns>
		public virtual Element Update(string innerHtmlFormat, params object[] args)
		{
			return this.Update(String.Format(innerHtmlFormat, args));	
		}


		/// <summary>
		/// Inserts the elements at index zero and returns the updated element.
		/// </summary>
		/// <param name="childElements">The child elements to insert.</param>
		/// <returns>The current instance with the inserted elements</returns>
		public virtual Element Insert(params Element[] childElements)
		{
			int index = 0;
			return Insert(index, childElements);
		}

        /// <summary>
		/// Inserts the elements at the specified index and returns the updated
		/// element.
		/// </summary>
		/// <param name="index">The index where the elements are inserted.</param>
		/// <param name="childElements">The child elements to insert.</param>
		/// <returns>The current instance with the inserted elements</returns>
        public virtual Element Insert(int index, params Element[] childElements)
		{
			if (childElements == null || childElements.Length == 0)
				return this;

			foreach (Element element in childElements)
			{
				if (element == null)
					continue;

				this.Children.Insert(index, element);
			}
			return this;
		}

				
			
		/// <summary>
		/// Adds the specified name value pair separated by an equals sign.
		/// </summary>
		/// <param name="nameValuePairsSeparatedByAnEqualsSign">The name value pair separated by an 
		/// equals sign. eg: class=required;width=100%;border=0;</param>
		/// <returns></returns>
		public virtual Element AddAttribute(string nameValuePairsSeparatedByAnEqualsSign)
		{
			if (String.IsNullOrEmpty(nameValuePairsSeparatedByAnEqualsSign))
			{
				return this;
			}

			string[] chunks = nameValuePairsSeparatedByAnEqualsSign.Split('=');
			string attributeName = "";
			string attributeValue = "";
			foreach (string chunk in chunks)
			{
				bool attributeNameFound = !String.IsNullOrEmpty(attributeName);
				if (!attributeNameFound)
				{
					attributeName = chunk;
					continue;
				}
								
				string[] values = chunk.Split(';');
				if (values.Length == 1)
				{
					this.AddAttribute(attributeName, values[0]);
					continue;
				}
				if (values.Length == 2)
				{
					attributeValue = values[0];
					if (attributeName.Equals("class"))
					{
						this.AddCssClasses(attributeValue);
					}
					else
					{
						this.AddAttribute(attributeName, attributeValue);
					}
					attributeValue = String.Empty;
					attributeName = values[values.Length - 1];
					continue;
				}
				if (attributeName.Equals("style"))
				{
					string lastword = values[values.Length - 1];
					if (String.IsNullOrEmpty(lastword))
					{
						this.AddStyle(chunk);
					}
					else
					{
						this.AddStyle(chunk.Substring(0, chunk.LastIndexOf(lastword, chunk.Length - 1)));
						attributeName = lastword;
					}
					continue;
				}
			}
			return this;
		}

		/// <summary>
		/// Adds the specified attribute name.
		/// </summary>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <param name="format">The attribute value.</param>
		/// <param name="args">The args.</param>
		/// <returns></returns>
		public virtual Element AddAttribute(string attributeName, string format, params object[] args)
		{
			this.AddAttribute(attributeName, String.Format(format, args));
			return this;
		}

		/// <summary>
		/// Adds the specified attribute and corresponding value to the element.
		/// </summary>
		/// <param name="name">The name of the attribute.</param>
		/// <param name="value">The value of the attribute.</param>
		/// <returns></returns>
		public virtual Element AddAttribute(string name, string value)
		{
			if (String.IsNullOrEmpty(name))
			{
				return this;
			}

			switch (name.ToLower())
			{
				case "class":
					this.AddCssClasses(value.ToString());
					break;
				case "style":
					this.AddStyle(value.ToString());
					break;
				default:
					this.Attributes[name] = (value ?? "").ToString();
					break;
			}			
			return this;
		}

		
		/// <summary>
		/// Removes the specified attribute.
		/// </summary>
		/// <param name="attributeName">Name of the attribute to remove.</param>
		/// <returns>The element with the attribute removed.</returns>
		public virtual Element RemoveAttribute(string attributeName)
		{
			this.Attributes.Remove(attributeName);
			return this;
		}

		

		/// <summary>
		/// Performs an implicit conversion from <see cref="HtmlBuilder.Element"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator string(Element element)
		{
			return element.ToString();
		}

		/// <summary>
		/// Appends the specified child elements.
		/// </summary>
		/// <param name="childElements">The child elements.</param>
		/// <returns></returns>
		public virtual Element Append(params Element[] childElements)
		{
			if (childElements == null || childElements.Length == 0)
				return this;

			foreach (Element element in childElements)
			{
				if (element == null)
					continue;

				this.Children.Add(element);
			}
			return this;
		}

		
		private Dictionary<string, string> _styles;
		/// <summary>
		/// Gets the css styles.
		/// </summary>
		/// <value>The styles.</value>
		public Dictionary<string, string> Styles
		{
			get
			{
				if (_styles == null)
					_styles = new Dictionary<string, string>();
				return _styles;
			}
		}


        /// <summary>
        /// Adds the style.
        /// </summary>
        /// <param name="nameValuePairsSeparatedBySemiColons">The name value pairs separated by semi colons.</param>
        /// <returns></returns>
		private Element AddStyle(string nameValuePairsSeparatedBySemiColons)
		{
			if (String.IsNullOrEmpty(nameValuePairsSeparatedBySemiColons))
				return this;

			string[] nameValuePairs = nameValuePairsSeparatedBySemiColons.Split(';');
			foreach (string nameValuePair in nameValuePairs)
			{
				if (String.IsNullOrEmpty(nameValuePair))
					continue;

				string name = nameValuePair.Split(':')[0];
				string value = nameValuePair.Split(':')[1];
				this.Styles[name] = value;
			}
			return this;
		}

		private StringCollection _cssClasses;
		/// <summary>
		/// Gets or sets the CSS classes.
		/// </summary>
		/// <value>The CSS classes.</value>
		public StringCollection CssClasses
		{
			get
			{
				if (_cssClasses == null)
					_cssClasses = new StringCollection();
				return _cssClasses;
			}
		}





		/// <summary>
		/// Adds the CSS classes.
		/// </summary>
		/// <param name="cssClassNamesSeparatedBySpaces">The CSS class names separated by spaces. For example, yourElement.AddCssClasses("required dataentry") will
		/// produce &lt;yourElement class="required dataentry"&gt;&lt;/yourElement&gt;.</param>
		/// <returns>The current instance with the added css class.</returns>
		private Element AddCssClasses(string cssClassNamesSeparatedBySpaces)
		{
			if (String.IsNullOrEmpty(cssClassNamesSeparatedBySpaces))
				return this;
			string[] cssClassNames = Array.ConvertAll<string, string>(cssClassNamesSeparatedBySpaces.Split(' '), delegate(string s) { return s.Trim(); });
			foreach (string cssClassName in cssClassNames)
			{
				if (this.CssClasses.Contains(cssClassName))
					continue;

				this.CssClasses.Add(cssClassName);
			}
			return this;
		}

	}
	
}

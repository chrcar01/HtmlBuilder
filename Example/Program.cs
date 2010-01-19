using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlBuilder;
using System.IO;

namespace Example
{
	public class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Program app = new Program();
				app.Run();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			Console.WriteLine("Press <Enter> to Exit");
			Console.ReadLine();
		}
		public void Run()
		{
			HelloWorld();
			AddStyleAttributesAndCssClassesInOneString();
			NestedElements();
			//RenderToFile();
			MixWithStrings();
		}
		private void HelloWorld()
		{
			Element element = new Element("p").Update("Hello World");
			Console.WriteLine(element.ToString());
		}
		private void AddStyleAttributesAndCssClassesInOneString()
		{
			new Element("span", "style=font-weight:bold;width=200px;class=sooper;").Render(Console.Out);
		}
		private void MixWithStrings()
		{
			string s = new Element("b").Update("hello") + "<i>bye bye</i>";
			Console.WriteLine(s);
		}
		private void NestedElements()
		{
			new Element("span",
				new Element("b").Update("Hello")
			).Render(Console.Out);
		}
		private void RenderToFile()
		{
			string path = @"C:\htmlbuilder-test.html";
			using (FileStream file = new FileStream(path, FileMode.CreateNew))
			{
				new Element("html",
					new Element("body",
						new Element("h1").Update("Hello World")
					)
				).Render(file);
			}
		}
	}
}

using System;

namespace HtmlBuilder
{
	/// <summary>
	/// Represents a component capable of writing through different writers.
	/// </summary>
	public interface IRenderTo
	{
		/// <summary>
		/// Renders a component to the specified writer.
		/// </summary>
		/// <param name="renderTo">The writer to use as the for rendering.</param>
		/// <returns></returns>
		string Render(RendersTo renderTo);
	}
}

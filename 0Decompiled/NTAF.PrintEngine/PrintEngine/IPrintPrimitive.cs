using System;
using System.Drawing;

namespace NTAF.PrintEngine
{
	public interface IPrintPrimitive
	{
		float CalculateHeight(NTAF.PrintEngine.PrintEngine engine, Graphics graphics);

		void Draw(NTAF.PrintEngine.PrintEngine engine, float yPos, Graphics graphics, Rectangle elementBounds);
	}
}
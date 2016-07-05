using System.Drawing;

namespace NTAF.PrintEngine {
    public interface IPrintable {
        void Print( PrintElement element );
    }

    public interface IPrintPrimitive {
        // CalculateHeight - work out how tall the primitive is... 
        float CalculateHeight( PrintEngine engine, Graphics graphics );

        // Print - tell the primitive to physically draw itself... 
        void Draw( PrintEngine engine, float yPos, Graphics graphics, Rectangle elementBounds );
    }
}

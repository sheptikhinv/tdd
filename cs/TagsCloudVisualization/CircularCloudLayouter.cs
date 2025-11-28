using System.Drawing;

namespace TagsCloudVisualization;

public class CircularCloudLayouter
{
    private readonly Point _center;

    public CircularCloudLayouter(Point center)
    {
        _center = center;
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        return new Rectangle(x: _center.X, y: _center.Y, rectangleSize.Width, rectangleSize.Height);
    }
}
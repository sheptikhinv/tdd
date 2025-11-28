using System.Drawing;
using TagsCloudVisualization.CoordinateGenerators;

namespace TagsCloudVisualization.Tests;

[TestFixture]
public class CircularCloudLayouterTests
{
    [Test]
    public void PutNextRectangle_ShouldAddRectangleToCollection()
    {
        var center = new Point(100, 100);
        var generator = new SpiralCoordinateGenerator(center, 0.1);
        var layouter = new CircularCloudLayouter(center, generator);
        
        var rectangle = layouter.PutNextRectangle(new Size(50, 30));
        
        Assert.That(layouter.Rectangles, Contains.Item(rectangle));
    }

    [Test]
    public void PutNextRectangle_ShouldReturnRectangleWithCorrectSize()
    {
        var center = new Point(100, 100);
        var generator = new SpiralCoordinateGenerator(center, 0.1);
        var layouter = new CircularCloudLayouter(center, generator);
        
        var size = new Size(50, 30);
        var rectangle = layouter.PutNextRectangle(size);
        Assert.Multiple(() =>
        {
            Assert.That(rectangle.Width, Is.EqualTo(50));
            Assert.That(rectangle.Height, Is.EqualTo(30));
        });
    }

    [Test]
    public void PutNextRectangle_TwoRectangles_ShouldNotIntersect()
    {
        var center = new Point(100, 100);
        var generator = new SpiralCoordinateGenerator(center, 0.1);
        var layouter = new CircularCloudLayouter(center, generator);
        
        var rect1 = layouter.PutNextRectangle(new Size(50, 30));
        var rect2 = layouter.PutNextRectangle(new Size(40, 40));
        
        Assert.That(rect1.IntersectsWith(rect2), Is.False);
    }

    [Test]
    public void PutNextRectangle_MultipleRectangles_ShouldNotIntersect()
    {
        var center = new Point(100, 100);
        var generator = new SpiralCoordinateGenerator(center, 0.1);
        var layouter = new CircularCloudLayouter(center, generator);
        
        var rect1 = layouter.PutNextRectangle(new Size(50, 30));
        var rect2 = layouter.PutNextRectangle(new Size(40, 40));
        var rect3 = layouter.PutNextRectangle(new Size(60, 20));
        Assert.Multiple(() =>
        {
            Assert.That(rect1.IntersectsWith(rect2), Is.False);
            Assert.That(rect1.IntersectsWith(rect3), Is.False);
            Assert.That(rect2.IntersectsWith(rect3), Is.False);
        });
    }

    [Test]
    public void PutNextRectangle_TenRectangles_ShouldNotIntersect()
    {
        var center = new Point(100, 100);
        var generator = new SpiralCoordinateGenerator(center, 0.1);
        var layouter = new CircularCloudLayouter(center, generator);
        
        for (var i = 0; i < 10; i++)
        {
            layouter.PutNextRectangle(new Size(30, 30));
        }

        var rectangles = layouter.Rectangles.ToList();
        
        for (var i = 0; i < rectangles.Count; i++)
        {
            for (var j = i + 1; j < rectangles.Count; j++)
            {
                Assert.That(rectangles[i].IntersectsWith(rectangles[j]), Is.False);
            }
        }
    }

    [Test]
    public void Rectangles_InitiallyEmpty()
    {
        var center = new Point(100, 100);
        var generator = new SpiralCoordinateGenerator(center, 0.1);
        var layouter = new CircularCloudLayouter(center, generator);
        
        Assert.That(layouter.Rectangles, Is.Empty);
    }

    [Test]
    public void Rectangles_AfterAddingOne_CountIsOne()
    {
        var center = new Point(100, 100);
        var generator = new SpiralCoordinateGenerator(center, 0.1);
        var layouter = new CircularCloudLayouter(center, generator);
        
        layouter.PutNextRectangle(new Size(50, 30));
        
        Assert.That(layouter.Rectangles, Has.Count.EqualTo(1));
    }

    [Test]
    public void Rectangles_AfterAddingThree_CountIsThree()
    {
        var center = new Point(100, 100);
        var generator = new SpiralCoordinateGenerator(center, 0.1);
        var layouter = new CircularCloudLayouter(center, generator);
        
        layouter.PutNextRectangle(new Size(50, 30));
        layouter.PutNextRectangle(new Size(40, 40));
        layouter.PutNextRectangle(new Size(60, 20));
        
        Assert.That(layouter.Rectangles, Has.Count.EqualTo(3));
    }

    [Test]
    public void PutNextRectangle_DifferentSizes_ShouldNotIntersect()
    {
        var center = new Point(100, 100);
        var generator = new SpiralCoordinateGenerator(center, 0.1);
        var layouter = new CircularCloudLayouter(center, generator);
        
        var rect1 = layouter.PutNextRectangle(new Size(10, 10));
        var rect2 = layouter.PutNextRectangle(new Size(100, 50));
        var rect3 = layouter.PutNextRectangle(new Size(30, 80));
        Assert.Multiple(() =>
        {
            Assert.That(rect1.IntersectsWith(rect2), Is.False);
            Assert.That(rect1.IntersectsWith(rect3), Is.False);
            Assert.That(rect2.IntersectsWith(rect3), Is.False);
        });
    }

    [Test]
    public void PutNextRectangle_ManyRectangles_ShouldNotIntersect()
    {
        var center = new Point(100, 100);
        var generator = new SpiralCoordinateGenerator(center, 0.1);
        var layouter = new CircularCloudLayouter(center, generator);
        
        for (var i = 0; i < 50; i++)
        {
            layouter.PutNextRectangle(new Size(20, 20));
        }

        var rectangles = layouter.Rectangles.ToList();
        
        for (var i = 0; i < rectangles.Count; i++)
        {
            for (var j = i + 1; j < rectangles.Count; j++)
            {
                Assert.That(rectangles[i].IntersectsWith(rectangles[j]), Is.False);
            }
        }
    }
}
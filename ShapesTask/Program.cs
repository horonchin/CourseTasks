using ShapesTask;

namespace Academits.Courses;

class Program
{
	public static void Main()
	{
		IShape[] shapes = new IShape[]
		{
			new Square(10),
			new Triangle(0, 0, 4, 0, 2, 3),
			new Rectangle(3, 6),
			new Circle(2.5),
			new Square(3),
			new Rectangle(2, 8),
			new Triangle(1, 1, 5, 1, 3, 4),
			new Circle(1.8)
		};

		PrintMaxAreaShape(shapes);
	}

	public static void PrintMaxAreaShape(IShape[] shapes)
	{
		Array.Sort(shapes, new AreaComparer());
		IShape maxAreaShape = shapes[shapes.Length - 1];

		Console.WriteLine($"Фигура с самой большой площадью равной {maxAreaShape.GetArea()} является:");
		Console.WriteLine(maxAreaShape);
	}
}
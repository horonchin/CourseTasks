using ShapesTask.Shapes;
using ShapesTask.Comparators;

namespace ShapesTask;

class Program
{
	public static void Main()
	{
		IShape[] shapes =
		[
			new Square(10),
			new Triangle(0, 0, 4, 0, 2, 3),
			new Rectangle(3, 6),
			new Circle(2.5),
			new Square(3),
			new Rectangle(2, 100),
			new Triangle(1, 1, 5, 1, 3, 4),
			new Circle(1.8)
		];

		Console.WriteLine("Фигура с самой большой площадью:" + Environment.NewLine + GetMaxAreaShape(shapes));
		Console.WriteLine("Фигура со вторым по величине периметром:" + Environment.NewLine + GetSecondMaxPerimeterShape(shapes));
	}

	public static IShape GetMaxAreaShape(IShape[] shapes)
	{
		Array.Sort(shapes, new ShapeAreaComparer());

		return shapes[^1];
	}

	public static IShape GetSecondMaxPerimeterShape(IShape[] shapes)
	{
		Array.Sort(shapes, new ShapePerimeterComparer());

		return shapes[^2];
	}
}
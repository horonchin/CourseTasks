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

		PrintMaxAreaShape(shapes);
		PrintSecondMaxPerimeterShape(shapes);
	}

	public static void PrintMaxAreaShape(IShape[] shapes)
	{
		Array.Sort(shapes, new ShapeAreaComparer());

		IShape maxAreaShape = shapes[^1];

		Console.WriteLine($"Фигура с самой большой площадью является:");
		Console.WriteLine(maxAreaShape);
	}

	public static void PrintSecondMaxPerimeterShape(IShape[] shapes)
	{
		Array.Sort(shapes, new ShapePerimeterComparer());

		IShape secondMaxPerimeterShape = shapes[^2];

		Console.WriteLine($"Фигура со вторым по величине периметром является:");
		Console.WriteLine(secondMaxPerimeterShape);
	}
}
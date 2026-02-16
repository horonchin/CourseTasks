namespace ShapesTask.Comparators;

public class ShapePerimeterComparer : IComparer<IShape>
{
	public int Compare(IShape? shape1, IShape? shape2)
	{
		ArgumentNullException.ThrowIfNull(shape1);
		ArgumentNullException.ThrowIfNull(shape2);

		return shape1.GetPerimeter().CompareTo(shape2.GetPerimeter());
	}
}
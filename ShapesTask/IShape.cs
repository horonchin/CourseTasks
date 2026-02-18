namespace ShapesTask;

public interface IShape
{
	public const int prime = 37;

	double GetWidth();

	double GetHeight();

	double GetArea();

	double GetPerimeter();
}
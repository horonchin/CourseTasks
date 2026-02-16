namespace ShapesTask.Shapes;

class Circle : IShape
{
	public double Radius { get; set; }

	public Circle(double radius)
	{
		Radius = radius;
	}

	public double GetWidth() => Radius * 2;

	public double GetHeight() => Radius * 2;

	public double GetArea() => Math.PI * Radius * Radius;

	public double GetPerimeter() => Radius * 2 * Math.PI;

	public override string ToString()
	{
		return $"Круг со радиусом {Radius}, диаметром {GetHeight()}, площадью {GetArea()} и длиной окружности равной {GetPerimeter()}";
	}

	public override int GetHashCode()
	{
		return Radius.GetHashCode();
	}

	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(obj, this))
		{
			return true;
		}

		if (obj is null || GetType() != obj.GetType())
		{
			return false;
		}

		return Radius == ((Circle)obj).Radius;
	}
}

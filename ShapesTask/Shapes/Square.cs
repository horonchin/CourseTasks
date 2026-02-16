namespace ShapesTask.Shapes;

class Square : IShape
{
	public double SideLength { get; set; }

	public Square(double sideLength)
	{
		SideLength = sideLength;
	}

	public double GetWidth() => SideLength;

	public double GetHeight() => SideLength;

	public double GetArea() => SideLength * SideLength;

	public double GetPerimeter() => SideLength * 4;

	public override string ToString()
	{
		return $"Квадрат со стороной {SideLength}, площадью {GetArea()} и периметром равным {GetPerimeter()}";
	}

	public override int GetHashCode()
	{
		return SideLength.GetHashCode();
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

		return SideLength == ((Square)obj).SideLength;
	}
}
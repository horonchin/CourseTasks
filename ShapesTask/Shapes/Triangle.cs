namespace ShapesTask.Shapes;

class Triangle : IShape
{
	public double X1 { get; set; }

	public double X2 { get; set; }

	public double X3 { get; set; }

	public double Y1 { get; set; }

	public double Y2 { get; set; }

	public double Y3 { get; set; }

	public Triangle(double x1, double y1, double x2, double y2, double x3, double y3)
	{
		X1 = x1;
		X2 = x2;
		X3 = x3;
		Y1 = y1;
		Y2 = y2;
		Y3 = y3;
	}

	public double GetWidth() => Math.Max(Math.Max(X1, X2), X3) - Math.Min(Math.Min(X1, X2), X3);

	public double GetHeight() => Math.Max(Math.Max(Y1, Y2), Y3) - Math.Min(Math.Min(Y1, Y2), Y3);

	public double GetArea()
	{
		return Math.Abs(0.5 * (
			X1 * (Y2 - Y3) +
			X2 * (Y3 - Y1) +
			X3 * (Y1 - Y2)
		));
	}

	private static double GetDistance(double x1, double y1, double x2, double y2)
	{
		double dx = x2 - x1;
		double dy = y2 - y1;

		return Math.Sqrt(dx * dx + dy * dy);
	}

	public double GetPerimeter()
	{
		double side1 = GetDistance(X1, Y1, X2, Y2);
		double side2 = GetDistance(X2, Y2, X3, Y3);
		double side3 = GetDistance(X3, Y3, X1, Y1);

		return side1 + side2 + side3;
	}

	public override string ToString()
	{
		return $"Трегольник с координатами ({X1}, {Y1}) ({X2}, {Y2}) ({X3}, {Y3}), шириной {GetHeight()}, высотой {GetHeight()}, площадью {GetArea()} и периметром {GetPerimeter()}";
	}

	public override int GetHashCode()
	{
		const int Prime = 17;

		int hash = 1;

		hash = Prime * hash + X1.GetHashCode();
		hash = Prime * hash + X2.GetHashCode();
		hash = Prime * hash + X3.GetHashCode();
		hash = Prime * hash + Y1.GetHashCode();
		hash = Prime * hash + Y2.GetHashCode();
		hash = Prime * hash + Y3.GetHashCode();
		return hash;
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

		Triangle p = (Triangle)obj;

		return X1 == p.X1
			&& X2 == p.X2
			&& X3 == p.X3
			&& Y1 == p.Y1
			&& Y2 == p.Y2
			&& Y3 == p.Y3;
	}
}
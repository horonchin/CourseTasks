namespace ShapesTask;

public interface IShape
{
	double GetWidth();
	double GetHeight();
	double GetArea();
	double GetPerimeter();
}

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
		return $"Квадрат со стороной {SideLength} и периметром равным {GetPerimeter()}";
	}

	public override int GetHashCode()
	{
		return SideLength.GetHashCode();
	}

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(obj, this))
		{
			return true;
		}

		if (ReferenceEquals(obj, null) || GetType() != obj.GetType())
		{
			return false;
		}

		return (SideLength == ((Square)obj).SideLength);
	}
}

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

	private double GetDistance(double x1, double y1, double x2, double y2)
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
		return $"Трегольник с координатами ({X1},{Y1}) ({X2},{Y2}) ({X3},{Y3}), шириной {GetHeight()}, высотой {GetHeight()} и периметром {GetPerimeter()}";
	}

	public override int GetHashCode()
	{
		int prime = 37;
		int hash = 1;

		hash = prime * hash + X1.GetHashCode();
		hash = prime * hash + X2.GetHashCode();
		hash = prime * hash + X3.GetHashCode();
		hash = prime * hash + Y1.GetHashCode();
		hash = prime * hash + Y2.GetHashCode();
		hash = prime * hash + Y3.GetHashCode();

		return hash;
	}

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(obj, this))
		{
			return true;
		}

		if (ReferenceEquals(obj, null) || GetType() != obj.GetType())
		{
			return false;
		}

		return X1 == ((Triangle)obj).X1 &&
			X2 == ((Triangle)obj).X2 &&
			X3 == ((Triangle)obj).X3 &&
			Y1 == ((Triangle)obj).Y1 &&
			Y2 == ((Triangle)obj).Y2 &&
			Y3 == ((Triangle)obj).Y3;
	}
}


class Rectangle : IShape
{
	public double Width { get; set; }

	public double Height { get; set; }

	public Rectangle(double width, double height)
	{
		Width = width;
		Height = height;
	}

	public double GetWidth() => Width;

	public double GetHeight() => Height;

	public double GetArea() => Width * Height;

	public double GetPerimeter() => (Width + Height) * 2;

	public override string ToString()
	{
		return $"Прямоугольник со шириной {Width}, высотой {Height} и периметром равным {GetPerimeter()}";
	}

	public override int GetHashCode()
	{
		int prime = 37;
		int hash = 1;

		hash = prime * hash + Width.GetHashCode();
		hash = prime * hash + Height.GetHashCode();

		return hash;
	}

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(obj, this))
		{
			return true;
		}

		if (ReferenceEquals(obj, null) || GetType() != obj.GetType())
		{
			return false;
		}

		return (Width == ((Rectangle)obj).Width && Height == ((Rectangle)obj).Height);
	}
}

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
		return $"Круг со радиусом {Radius}, диаметром {GetHeight()} и длиной окружности равной {GetPerimeter()}";
	}

	public override int GetHashCode()
	{
		return Radius.GetHashCode();
	}

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(obj, this))
		{
			return true;
		}

		if (ReferenceEquals(obj, null) || GetType() != obj.GetType())
		{
			return false;
		}

		return (Radius == ((Square)obj).SideLength);
	}
}

public class AreaComparer : IComparer<IShape>
{
	public int Compare(IShape x, IShape y)
	{
		return x.GetArea().CompareTo(y.GetArea());
	}
}

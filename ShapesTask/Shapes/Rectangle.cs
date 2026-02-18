namespace ShapesTask.Shapes;

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
		return $"Прямоугольник с шириной {Width}, высотой {Height}, площадью {GetArea()} и периметром равным {GetPerimeter()}";
	}

	public override int GetHashCode()
	{
		int hash = 1;

		hash = IShape.prime * hash + Width.GetHashCode();
		hash = IShape.prime * hash + Height.GetHashCode();

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

		Rectangle p = (Rectangle)obj;

		return Width == p.Width && Height == p.Height;
	}
}

class Program
{
	public static void Main()
	{
		Console.Write("Введите начало числового диапазона: ");
		double rangeFrom = Convert.ToDouble(Console.ReadLine());

		Console.Write("Введите конец числового диапазона: ");
		double rangeTo = Convert.ToDouble(Console.ReadLine());

		Range range = new Range(rangeFrom, rangeTo);

		Console.WriteLine($"Длина отрезка равна {range.GetLength():F2}");

		Console.Write("Введите число: ");
		double number = Convert.ToDouble(Console.ReadLine());

		if (range.IsInside(number))
		{
			Console.WriteLine($"Данное число пренадлежит диапазону от {range.From} до {range.To}");
		}
		else
		{
			Console.WriteLine($"Данное число не пренадлежит диапазону от {range.From} до {range.To}");
		}
	}
}
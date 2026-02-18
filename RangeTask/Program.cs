namespace RangeTask;

class Program
{
	public static void Main()
	{
		Range rangeA = new Range(12, 13);
		Range rangeB = new Range(13, 15);

		Range? intersection = rangeA.GetIntersection(rangeB);

		if (intersection != null)
		{
			Console.WriteLine($"Пересечение интервалов A и B равно ({intersection.From}, {intersection.To}).");
		}
		else
		{
			Console.WriteLine("Интервалы А и B не пересекаются.");
		}

		Range[] union = rangeA.GetUnion(rangeB);

		if (union.Length == 1)
		{
			Console.WriteLine($"Объединение интервалов A и B равно ({union[0].From}, {union[0].To}).");
		}
		else
		{
			Console.WriteLine($"Объединение интервалов A и B равно ({union[0].From}, {union[0].To}) и ({union[1].From}, {union[1].To}).");
		}

		Range[] difference = rangeA.GetDifference(rangeB);

		if (difference.Length == 0)
		{
			Console.WriteLine("Разность интервалов A и B равна пустому множеству.");
		}
		else if (difference.Length == 1)
		{
			Console.WriteLine($"Разность интервалов A и B равна ({difference[0].From}, {difference[0].To}).");
		}
		else
		{
			Console.WriteLine($"Разность интервалов A и B равна ({difference[0].From}, {difference[0].To}) и ({difference[1].From}, {difference[1].To}].");
		}
	}
}
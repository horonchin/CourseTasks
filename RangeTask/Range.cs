namespace RangeTask;

class Range
{
	public double From { get; set; }

	public double To { get; set; }

	public Range(double from, double to)
	{
		From = from;
		To = to;
	}

	public double GetLength()
	{
		return To - From;
	}

	public bool IsInside(double number)
	{
		return number >= From && number <= To;
	}

	public Range? GetIntersection(Range range)
	{
		double intersectionFrom = Math.Max(From, range.From);
		double intersectionTo = Math.Min(To, range.To);

		if (intersectionFrom < intersectionTo)
		{
			return new Range(intersectionFrom, intersectionTo);
		}

		return null;
	}

	public Range[] GetUnion(Range range)
	{
		if (From <= range.To && range.From <= To)
		{
			double unionFrom = Math.Min(From, range.From);
			double unionTo = Math.Max(To, range.To);

			return new Range[] { new Range(unionFrom, unionTo) };
		}

		Range[] result = new Range[2];

		if (From < range.From)
		{
			result[0] = new Range(From, To);
			result[1] = new Range(range.From, range.To);
		}
		else
		{
			result[0] = new Range(range.From, range.To);
			result[1] = new Range(From, To);
		}

		return result;

	}

	public Range[] GetDifference(Range range)
	{
		double intersectionFrom = Math.Max(From, range.From);
		double intersectionTo = Math.Min(To, range.To);

		if (intersectionFrom >= intersectionTo)
		{
			return new[] { new Range(From, To) };
		}

		bool hasLeftPart = From < intersectionFrom;
		bool hasRightPart = To > intersectionTo;

		if (hasLeftPart && hasRightPart)
		{
			return new Range[] { new Range(From, range.From), new Range(range.To, To) };
		}
		else if (hasLeftPart)
		{
			return new[] { new Range(From, intersectionFrom) };
		}
		else if (hasRightPart)
		{
			return new[] { new Range(intersectionTo, To) };
		}

		return Array.Empty<Range>();
	}
}
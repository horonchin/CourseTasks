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
		double intersectionFrom = Math.Max(this.From, range.From);
		double intersectionTo = Math.Min(this.To, range.To);

		if (intersectionFrom < intersectionTo)
		{
			return new Range(intersectionFrom, intersectionTo);
		}

		return null;
	}

	public Range[] GetUnion(Range range)
	{
		if (this.From <= range.To && range.From <= this.To)
		{
			double unionFrom = Math.Min(this.From, range.From);
			double unionTo = Math.Max(this.To, range.To);

			return new Range[] { new Range(unionFrom, unionTo) };
		}
		else
		{
			Range[] result = new Range[2];

			if (this.From < range.From)
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
	}

	public Range[] GetDifference(Range range)
	{
		double intersectionFrom = Math.Max(this.From, range.From);
		double intersectionTo = Math.Min(this.To, range.To);

		if (intersectionFrom >= intersectionTo)
		{
			return new[] { new Range(this.From, this.To) };
		}

		bool hasLeftPart = this.From < intersectionFrom;
		bool hasRightPart = this.To > intersectionTo;

		if (hasLeftPart && hasRightPart)
		{
			return new Range[] { new Range(this.From, range.From), new Range(range.To, this.To) };
		}
		else if (hasLeftPart)
		{
			return new[] { new Range(this.From, intersectionFrom) };
		}
		else if (hasRightPart)
		{
			return new[] { new Range(intersectionTo, this.To) };
		}
		else
		{
			return Array.Empty<Range>();
		}
	}
}
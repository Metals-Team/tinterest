using System;

namespace MetalsTeam.Tinterest.Models
{
	public class RadiusRangeFilter
	{
		public float Latitude { get; set; }
		public float Longitude { get; set; }
		public int Radius { get; set; }
		public int InterestId { get; set; }
		public double MinLatitude { get; set; }
		public double MinLongitude { get; set; }
		public double MaxLatitude { get; set; }
		public double MaxLongitude { get; set; }

		public void CalculateRange()
		{
			MinLatitude = Latitude - Radius * 0.009;
			MaxLatitude = Latitude + Radius * 0.009;
			MinLongitude = Longitude - 1 / (111.320 * Math.Cos(Longitude));
			MaxLongitude = Longitude + 1 / (111.320 * Math.Cos(Longitude));
		}
	}
}

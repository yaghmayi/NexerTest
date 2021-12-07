using System;

namespace Alpacinator.Models
{
    public class Alpaca
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public string Color { get; set; }
        public Farm Farm { get; set; }

		public double Cost => Math.Round(Weight * Farm.Multiplier, 2);
	}
}
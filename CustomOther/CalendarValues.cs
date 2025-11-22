using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public static class Season
    {
        public static string Name
        {
            get
            {
                if (AApocrypha.hemisphere.Value) // southern hemisphere override is true
                {
                    switch (DateTime.Now.Month)
                    {
                        case 1: return "Summer";
                        case 2: return "Summer";
                        case 3: return "Autumn";
                        case 4: return "Autumn";
                        case 5: return "Autumn";
                        case 6: return "Winter";
                        case 7: return "Winter";
                        case 8: return "Winter";
                        case 9: return "Spring";
                        case 10: return "Spring";
                        case 11: return "Spring";
                        case 12: return "Summer";
                        default: return "Autumn";
                    }
                }
                else // southern hemisphere override is false
                {
                    switch (DateTime.Now.Month)
                    {
                        case 1: return "Winter";
                        case 2: return "Winter";
                        case 3: return "Spring";
                        case 4: return "Spring";
                        case 5: return "Spring";
                        case 6: return "Summer";
                        case 7: return "Summer";
                        case 8: return "Summer";
                        case 9: return "Autumn";
                        case 10: return "Autumn";
                        case 11: return "Autumn";
                        case 12: return "Winter";
                        default: return "Spring";
                    }
                }
            }
        }
        public static int Number
        {
            get
            {
                if (AApocrypha.hemisphere.Value) // southern hemisphere override is true
                {
                    switch (DateTime.Now.Month)
                    {
                        case 1: return 2;
                        case 2: return 2;
                        case 3: return 3;
                        case 4: return 3;
                        case 5: return 3;
                        case 6: return 4;
                        case 7: return 4;
                        case 8: return 4;
                        case 9: return 1;
                        case 10: return 1;
                        case 11: return 1;
                        case 12: return 2;
                        default: return 3;
                    }
                }
                else // southern hemisphere override is false
                {
                    switch (DateTime.Now.Month)
                    {
                        case 1: return 4;
                        case 2: return 4;
                        case 3: return 1;
                        case 4: return 1;
                        case 5: return 1;
                        case 6: return 2;
                        case 7: return 2;
                        case 8: return 2;
                        case 9: return 3;
                        case 10: return 3;
                        case 11: return 3;
                        case 12: return 4;
                        default: return 1;
                    }
                }
            }
        }
    }

    // moon phase calculator from https://github.com/khalidabuhakmeh/MoonPhaseConsole
    public static class Moon
    {
        //private static string Hemisphere = "north";

        private static readonly IReadOnlyList<string> NorthernHemisphere
            = new List<string> { "new", "crescentright", "halfright", "gibbousright", "full", "gibbousleft", "halfleft", "crescentleft", "new" };

        private static readonly IReadOnlyList<string> SouthernHemisphere
            = NorthernHemisphere.Reverse().ToList();

        private static readonly List<string> Names = new List<string>
        {
            Phase.NewMoon,
            Phase.WaxingCrescent, Phase.FirstQuarter, Phase.WaxingGibbous,
            Phase.FullMoon,
            Phase.WaningGibbous, Phase.ThirdQuarter, Phase.WaningCrescent
        };

        private const double TotalLengthOfCycle = 29.53;

        private static readonly List<Phase> allPhases = new List<Phase>();

        static Moon()
        {
            var period = TotalLengthOfCycle / Names.Count;
            // divide the phases into equal parts 
            // making sure there are no gaps
            allPhases = Names
                .Select((t, i) => new Phase(t, period * i, period * (i + 1)))
                .ToList();
        }

        /// <summary>
        /// Calculate the current phase of the moon.
        /// Note: this calculation uses the last recorded new moon to calculate the cycles of
        /// of the moon since then. Any date in the past before 1920 might not work.
        /// </summary>
        /// <param name="utcDateTime"></param>
        /// <remarks>https://www.subsystems.us/uploads/9/8/9/4/98948044/moonphase.pdf</remarks>
        /// <returns></returns>
        public static PhaseResult Calculate(DateTime utcDateTime,
            string viewFromEarth = "north")
        {
            const double julianConstant = 2415018.5;
            var julianDate = utcDateTime.ToOADate() + julianConstant;

            // London New Moon (1920)
            // https://www.timeanddate.com/moon/phases/uk/london?year=1920
            var daysSinceLastNewMoon =
                new DateTime(1920, 1, 21, 5, 25, 00, DateTimeKind.Utc).ToOADate() + julianConstant;

            var newMoons = (julianDate - daysSinceLastNewMoon) / TotalLengthOfCycle;
            var intoCycle = (newMoons - Math.Truncate(newMoons)) * TotalLengthOfCycle;

            var phase =
                allPhases.First(p => intoCycle >= p.Start && intoCycle <= p.End);

            var index = allPhases.IndexOf(phase);
            var currentPhase =
                viewFromEarth switch
                {
                    "north" => NorthernHemisphere[index],
                    _ => SouthernHemisphere[index]
                };

            return new PhaseResult
            (
                phase.Name,
                currentPhase,
                Math.Round(intoCycle, 2),
                viewFromEarth,
                utcDateTime
            );
        }

        public static PhaseResult UtcNow(string viewFromEarth = "north")
        {
            return Calculate(DateTime.UtcNow, viewFromEarth);
        }

        public static PhaseResult Now(string viewFromEarth = "north")
        {
            return Calculate(DateTime.Now.ToUniversalTime(), viewFromEarth);
        }

        public class PhaseResult
        {
            public PhaseResult(string name, string visual, double daysIntoCycle, string hemisphere,
                DateTime moment)
            {
                Name = name;
                Visual = visual;
                DaysIntoCycle = daysIntoCycle;
                Hemisphere = hemisphere;
                Moment = moment;
            }

            public string Name { get; }
            public string Visual { get; set; }
            public double DaysIntoCycle { get; set; }
            public string Hemisphere { get; set; }
            public DateTime Moment { get; }
            public double Visibility
            {
                get
                {
                    const int FullMoon = 15;
                    const double halfCycle = TotalLengthOfCycle / 2;

                    var numerator = DaysIntoCycle > FullMoon
                        // past the full moon, we want to count down
                        ? halfCycle - (DaysIntoCycle % halfCycle)
                        // leading up to the full moon
                        : DaysIntoCycle;

                    return numerator / halfCycle * 100;
                }
            }

            public override string ToString()
            {
                var percent = Math.Round(Visibility, 2);
                return $"The Moon for {Moment} is {DaysIntoCycle} days\n" +
                       $"into the cycle, and is showing as \"{Name}\"\n" +
                       $"with {percent}% visibility, and a face of {Visual} from the {Hemisphere} hemisphere.";
            }
        }

        public class Phase
        {
            public const string NewMoon = "New Moon";
            public const string WaxingCrescent = "Waxing Crescent";
            public const string FirstQuarter = "First Quarter";
            public const string WaxingGibbous = "Waxing Gibbous";
            public const string FullMoon = "Full Moon";
            public const string WaningGibbous = "Waning Gibbous";
            public const string ThirdQuarter = "Third Quarter";
            public const string WaningCrescent = "Waning Crescent";

            public Phase(string name, double start, double end)
            {
                Name = name;
                Start = start;
                End = end;
            }

            public string Name { get; }

            /// <summary>
            /// The days into the cycle this phase starts
            /// </summary>
            public double Start { get; }

            /// <summary>
            /// The days into the cycle this phase ends
            /// </summary>
            public double End { get; }
        }
    }
}

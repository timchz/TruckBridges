// Created by Tim Heinz - n8683981

using System.Collections.Generic;

namespace TruckBridges.Core.Models
{
    public class CombinedRoute : Route
    {
        public List<SnappedSpan> snappedSpan { get; set; }


        //public List<ManeuverExtra> maneuverExtra { get; set; }
        //public List<LegExtra> legExtra { get; set; }

        public CombinedRoute()
        {
            //legExtra = new List<LegExtra>();
            //maneuverExtra = new List<ManeuverExtra>();
            snappedSpan = new List<SnappedSpan>();
        }

        public CombinedRoute(Route route)
        {
            this.leg = route.leg;
            this.mode = route.mode;
            this.summary = route.summary;
            this.waypoint = route.waypoint;

            snappedSpan = new List<SnappedSpan>();
        }

        public List<Maneuver> InterpolateManeuvers(List<Maneuver> maneuvers)
        {
            List<Maneuver> newManeuvers = new List<Maneuver>();

            for (var m = 0; m < maneuvers.Count - 1; m++)
            {
                Maneuver maneuver = maneuvers[m];

                if (maneuver.length > 300)
                {
                    int subManeuvers = maneuver.length / 300;
                    if (subManeuvers < 2)
                        subManeuvers = 2;

                    int lengthInc = maneuver.length / subManeuvers;
                    int travelTimeInc = maneuver.travelTime / subManeuvers;

                    // update original maneuver's attributes
                    maneuver.length = lengthInc;
                    maneuver.travelTime = travelTimeInc;
                    newManeuvers.Add(maneuver);

                    Position startPosition = maneuver.position;
                    Position endPosition = maneuvers[m + 1].position;
                    double latInc = (endPosition.latitude - startPosition.latitude) / subManeuvers;
                    double lngInc = (endPosition.longitude - startPosition.longitude) / subManeuvers;

                    Position curPosition = startPosition;

                    for (var i = 0; i < subManeuvers - 1; i++)
                    {
                        Maneuver newManeuver = new Maneuver();
                        newManeuver.position = new Position();
                        newManeuver.position.latitude = curPosition.latitude + latInc;
                        newManeuver.position.longitude = curPosition.longitude + lngInc;
                        newManeuver.length = lengthInc;
                        newManeuver.travelTime = travelTimeInc;
                        newManeuvers.Add(newManeuver);
                        curPosition = newManeuver.position;
                    }
                }
                else
                {
                    newManeuvers.Add(maneuver);
                }
            }

            return newManeuvers;
        }

    }

    public class SnappedSpan
    {
        public Position start { get; set; }
        public Position end { get; set; }
        public List<SnappedPoints> snappedPoints { get; set; }

        public SnappedSpan()
        {
            snappedPoints = new List<SnappedPoints>();
        }

        public SnappedSpan(List<SnappedPoints> points)
        {
            this.snappedPoints = points;
        }

        public SnappedSpan(Position start, Position end, List<SnappedPoints> points)
        {
            this.start = start;
            this.end = end;
            this.snappedPoints = points;
        }
    }

    public class LegExtra
    {
        public List<ManeuverExtra> maneuver { get; set; }

        public LegExtra()
        {
            maneuver = new List<ManeuverExtra>();
        }
    }

    public class ManeuverExtra
    {
        public List<SnappedPoints> snappedPoints { get; set; }
    }
}

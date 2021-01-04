using System;
using System.Collections.Generic;
using System.Text;

namespace AirLineTravelGraph
{
    public class AirLine
    {
        public string Name { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }

        public static List<AirLine> LoadData()
        {
            List<AirLine> airLineInfos = new List<AirLine>
            {
                 new AirLine
                {
                    Name = "Indigo",
                    Departure = "Start",
                    Arrival = "City 1"
                },
                new AirLine
                {
                    Name = "Indigo",
                    Departure = "City 1",
                    Arrival = "City 2",
                    DepartureTime = "7 AM",
                    ArrivalTime = "8 AM",

                },
                 new AirLine
                {
                    Name = "Indigo",
                    Departure = "City 2",
                    Arrival = "City 3",
                    DepartureTime = "5 AM",
                    ArrivalTime = "6 AM"
                },
                  new AirLine
                {
                    Name = "Indigo",
                    Departure = "City 3",
                    Arrival = "City 4",
                    DepartureTime = "9 AM",
                    ArrivalTime = "10 AM"
                },
                   new AirLine
                {
                    Name = "Indigo",
                    Departure = "City 3",
                    Arrival = "City 5",
                    DepartureTime = "5 PM",
                    ArrivalTime = "6 PM"
                },
                    new AirLine
                {
                    Name = "Indigo",
                    Departure = "City 3",
                    Arrival = "City 6",
                    DepartureTime = "3 PM",
                    ArrivalTime = "5 PM"
                },
                     new AirLine
                {
                    Name = "Indigo",
                    Departure = "City 7",
                    Arrival = "City 8",
                    DepartureTime = "7 PM",
                    ArrivalTime = "8 PM"
                },
                      new AirLine
                {
                    Name = "Indigo",
                    Departure = "City 8",
                    Arrival = "City 7",
                    DepartureTime = "9 PM",
                    ArrivalTime = "10 PM"
                },
                       new AirLine
                {
                    Name = "Indigo",
                    Departure = "City 7",
                    Arrival = "City 9",
                    DepartureTime = "11 AM",
                    ArrivalTime = "11.45 AM"
                },
                        new AirLine
                {
                    Name = "Indigo",
                    Departure = "City 9",
                    Arrival = "City 10",
                    DepartureTime = "2 PM",
                    ArrivalTime = "3 PM"
                }
            };

            return airLineInfos;
        }
    }
}

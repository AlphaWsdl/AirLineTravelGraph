using System;
using System.Collections.Generic;
using System.Text;

namespace AirLineTravelGraph_GremLinqOptimization.Models
{
    public class AirPlane : Vertex
    {
        public string Name { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        // public VertexProperty<string, MetaData>? Name { get; set; }

        public static List<AirPlane> LoadAirPlaneInfos()
        {
            List<AirPlane> AirPlaneInfos = new List<AirPlane>()
            {
                new AirPlane
                {
                    Name = "Indigo",
                    Departure = "Start",
                    Arrival = "City 1",
                    Id = "Terminal 01",
                    Label ="Indigo"
                },
                new AirPlane
                {
                    Name = "Indigo",
                    Departure = "City 1",
                    Arrival = "City 2",
                    DepartureTime = "7 AM",
                    ArrivalTime = "8 AM",
                    Id = "Terminal 02",
                    Label ="Indigo"
                },
                new AirPlane
                {
                    Name = "Indigo",
                    Departure = "City 2",
                    Arrival = "City 3",
                    DepartureTime = "5 AM",
                    ArrivalTime = "6 AM",
                    Id = "Terminal 03",
                    Label ="Indigo"
                },
                new AirPlane
                {
                    Name = "Indigo",
                    Departure = "City 3",
                    Arrival = "City 4",
                    DepartureTime = "9 AM",
                    ArrivalTime = "10 AM",
                    Id = "Terminal 04",
                    Label ="Indigo"
                },
                new AirPlane
                {
                    Name = "Indigo",
                    Departure = "City 3",
                    Arrival = "City 5",
                    DepartureTime = "5 PM",
                    ArrivalTime = "6 PM",
                    Id = "Terminal 05",
                    Label ="Indigo"
                },
                new AirPlane
                {
                    Name = "Indigo",
                    Departure = "City 3",
                    Arrival = "City 6",
                    DepartureTime = "3 PM",
                    ArrivalTime = "5 PM",
                    Id = "Terminal 06",
                    Label ="Indigo"
                },
                new AirPlane
                {
                    Name = "Indigo",
                    Departure = "City 7",
                    Arrival = "City 8",
                    DepartureTime = "7 PM",
                    ArrivalTime = "8 PM",
                    Id = "Terminal 07",
                    Label ="Indigo"
                },
                new AirPlane
                {
                    Name = "Indigo",
                    Departure = "City 8",
                    Arrival = "City 7",
                    DepartureTime = "9 PM",
                    ArrivalTime = "10 PM",
                    Id = "Terminal 08",
                    Label ="Indigo"
                },
                new AirPlane
                {
                    Name = "Indigo",
                    Departure = "City 7",
                    Arrival = "City 9",
                    DepartureTime = "11 AM",
                    ArrivalTime = "11.45 AM",
                    Id = "Terminal 09",
                    Label ="Indigo"
                },
                new AirPlane
                {
                    Name = "Indigo",
                    Departure = "City 9",
                    Arrival = "City 10",
                    DepartureTime = "2 PM",
                    ArrivalTime = "3 PM",
                    Id = "Terminal 10",
                    Label ="Indigo"
                }
            };
            return AirPlaneInfos;
        }
    }
}


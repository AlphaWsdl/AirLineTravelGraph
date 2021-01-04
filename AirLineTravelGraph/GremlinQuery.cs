using System;
using System.Collections.Generic;
using System.Text;

namespace AirLineTravelGraph
{
    class GremlinQuery
    {
        public GremlinQuery(string description, string statement)
        {
            Description = description;
            Statement = statement;
        }

        public string Description { get; private set; }

        public string Statement { get; private set; }

        public static string SetInitialVertexStatement(AirLine airline) => $"g.addV('{airline.Name}').property('id', '{airline.Departure}').property('departure'," +
           $" '{airline.Departure}').property('arrival', '{airline.Arrival}').property('departureTime', '{airline.DepartureTime}')" +
           $".property('arrivalTime', '{airline.ArrivalTime}')";

        public static string SetVertexStatement(AirLine airline) => $"g.addV('{airline.Name}').property('id', '{airline.Arrival}').property('departure'," +
            $" '{airline.Departure}').property('arrival', '{airline.Arrival}').property('departureTime', '{airline.DepartureTime}')" +
            $".property('arrivalTime', '{airline.ArrivalTime}')";

        public static string SetEdgeStatement(string departure, string arrival) => $"g.V('{departure}').addE('flies').to(g.V('{arrival}'))";

        public static string CalculateCountOfAdjacentCities(string label) => $"g.V('{label}').both().count()";

    }
}

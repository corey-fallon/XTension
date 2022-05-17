using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.DesignScript.Geometry;

namespace XTension
{
    public class PolygonXTension
    {
        public static Point GetCentroid(Polygon polygon)
        {
            double Cx = GetCentriodX(polygon);
            double Cy = GetCentriodY(polygon);
            return Point.ByCoordinates(Cx, Cy);
        }

        public static double GetArea(Polygon polygon)
        {
            double A = 0;
            double xi;
            double yi;
            double xj;
            double yj;
            foreach (var curve in polygon.Curves())
            {
                xi = curve.StartPoint.X;
                yi = curve.StartPoint.Y;
                xj = curve.EndPoint.X;
                yj = curve.EndPoint.Y;
                A += xi * yj - xj * yi;
            }
            A = (1.0 / 2.0) * A;
            return A;
        }

        public static double GetCentriodX(Polygon polygon)
        {
            double Cx = 0;
            double xi;
            double yi;
            double xj;
            double yj;
            foreach (var curve in polygon.Curves())
            {
                xi = curve.StartPoint.X;
                yi = curve.StartPoint.Y;
                xj = curve.EndPoint.X;
                yj = curve.EndPoint.Y;
                Cx += (xi + xj) * (xi * yj - xj * yi);
            }
            double A = GetArea(polygon);
            Cx = (1.0 / (6.0 * A)) * Cx;
            return Cx;
        }

        public static double GetCentriodY(Polygon polygon)
        {
            double Cy = 0;
            double xi;
            double yi;
            double xj;
            double yj;
            foreach (var curve in polygon.Curves())
            {
                xi = curve.StartPoint.X;
                yi = curve.StartPoint.Y;
                xj = curve.EndPoint.X;
                yj = curve.EndPoint.Y;
                Cy += (yi + yj) * (xi * yj - xj * yi);
            }
            double A = GetArea(polygon);
            Cy = (1.0 / (6.0 * A)) * Cy;
            return Cy;
        }

        public static List<Point> Triangulate(Polygon polygon, double maximumArea)
        {
            var poly = new TriangleNet.Geometry.Polygon();
            var verticies = new List<TriangleNet.Geometry.Vertex>();
            foreach (var point in polygon.Points)
            {
                verticies.Add(new TriangleNet.Geometry.Vertex(point.X, point.Y, 1));
            }
            poly.Add(new TriangleNet.Geometry.Contour(verticies));
            var options = new TriangleNet.Meshing.ConstraintOptions() { ConformingDelaunay = true };
            var quality = new TriangleNet.Meshing.QualityOptions() { MinimumAngle = 30, MaximumArea = maximumArea };
            var mesh = TriangleNet.Geometry.ExtensionMethods.Triangulate(poly, options, quality);

            List<Point> points = new List<Point>();
            foreach (var vertex in mesh.Vertices)
            {
                points.Add(Point.ByCoordinates(vertex.X, vertex.Y));
            }
            return points;
        }

        public static List<Line> TriangulateToEdges(Polygon polygon, double maximumArea)
        {
            var poly = new TriangleNet.Geometry.Polygon();
            var verticies = new List<TriangleNet.Geometry.Vertex>();
            foreach (var point in polygon.Points)
            {
                verticies.Add(new TriangleNet.Geometry.Vertex(point.X, point.Y, 1));
            }
            poly.Add(new TriangleNet.Geometry.Contour(verticies));
            var options = new TriangleNet.Meshing.ConstraintOptions() { ConformingDelaunay = true };
            var quality = new TriangleNet.Meshing.QualityOptions() { MinimumAngle = 30, MaximumArea = maximumArea };
            var mesh = TriangleNet.Geometry.ExtensionMethods.Triangulate(poly, options, quality);

            var vertexByID = new Dictionary<int, TriangleNet.Geometry.Vertex>();

            foreach (var vertex in mesh.Vertices)
            {
                vertexByID.Add(vertex.ID, vertex);
            }

            List<Line> lines = new List<Line>();
            foreach (var edge in mesh.Edges)
            {
                var startVertex = vertexByID[edge.P0];
                var endVertex = vertexByID[edge.P1];
                Point startPoint = Point.ByCoordinates(startVertex.X, startVertex.Y);
                Point endPoint = Point.ByCoordinates(endVertex.X, endVertex.Y);
                Line line = Line.ByStartPointEndPoint(startPoint, endPoint);
                lines.Add(line);
                startPoint.Dispose();
                endPoint.Dispose();
            }
            return lines;
        }
    }
}

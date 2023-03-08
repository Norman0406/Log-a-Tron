using Mapsui.UI;
using Mapsui;
using Mapsui.UI.Wpf;
using System.Windows.Controls;
using Mapsui.Layers;
using Mapsui.Providers;
using Mapsui.Styles;
using System.Collections.Generic;
using System;
using System.Xml.Linq;
using BruTile.Predefined;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using Mapsui.Nts;
using Mapsui.Projections;
using Mapsui.Nts.Extensions;
using System.Windows.Automation;
using HarfBuzzSharp;
using System.Windows;

namespace Logatron.Views
{
    public partial class MapView : UserControl
    {
        public MapView()
        {
            InitializeComponent();

            //MapControl.Map.CRS = "EPSG:3857";
            MapControl.Map?.Layers.Add(Mapsui.Tiling.OpenStreetMap.CreateTileLayer());
            MapControl.MouseWheelAnimation.Duration = 50;

            //MapControl.Map?.Layers.Add(Mapsui.Utilities.OpenStreetMap.CreateTileLayer());
            ////MapControl.Map?.Layers.Add(new TileLayer(KnownTileSources.Create(KnownTileSource.BingAerial)));
            //MapControl.Map.Transformation = new MinimalTransformation();
            ////MapControl.Map.CRS = "EPSG:4326";
            ////MapControl.Provider


            //// Detail about custom transforms here
            //// https://github.com/garykindel/ShapefileProjectionDemo/blob/master/WpfTestApp/CustomMinimalTransformation.cs

            MapControl.Map?.Layers.Add(CreateLayer());

            //MapControl.Map.Transformation.
        }

        struct Point
        {
            public double Lat { get; set; }
            public double Lon { get; set; }
        }

        private LineString CreateLines(Coordinate a, Coordinate b)
        {
            var coordiantes = new Coordinate[] { a, b };

            var lineString = new LineString(coordiantes);

            //Point curPoint = a;

            //int iterations = 10;
            //for (int i = 0; i < iterations; i++)
            //{
            //    curPoint.Lat += (b.Lat - a.Lat) / iterations;
            //    curPoint.Lon += (b.Lon - a.Lon) / iterations;

            //    var point = SphericalMercator.FromLonLat(curPoint.Lon, curPoint.Lat);
            //    lineString.Vertices.Add(point);
            //}

            //lineString.Vertices.Add(SphericalMercator.FromLonLat(a.Lon, a.Lat));
            //lineString.Vertices.Add(SphericalMercator.FromLonLat(b.Lon, b.Lat));

            return lineString;
        }

        private ILayer CreateLayer()
        {
            Coordinate a = SphericalMercator.FromLonLat(-86.267832, 70.430615).ToCoordinate();
            Coordinate b = SphericalMercator.FromLonLat(70.303877, 01.816596).ToCoordinate();

            var lineString = CreateLines(a, b);

            var style = new VectorStyle
            {
                Fill = null,
                Outline = null,
                Line = { Color = Color.FromString("YellowGreen"), Width = 2 }
            };

            //var memoryLayer = new MemoryLayer
            //{
            //    Features = new[] { new GeometryFeature { Geometry = lineString } },
            //    Name = "LineStringLayer",
            //    Style = style,
            //};

            var memoryProvider = new MemoryProvider(new GeometryFeature { Geometry = lineString })
            {
                CRS = "EPSG:4326" // The DataSource CRS needs to be set
            };

            var provider = new ProjectingProvider(memoryProvider)
            {
                CRS = "EPSG:3857"
            };

            return new Layer
            {
                DataSource = memoryProvider,
                Name = "Grid",
                Style = style,
                //IsMapInfoLayer = true
            };
        }
    }
}

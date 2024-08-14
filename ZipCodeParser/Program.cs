using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ZipCodeParser
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var zipDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "usa-zips");
            var outputDirectoryPath = Path.Combine(zipDirectoryPath, "output");
            Directory.CreateDirectory(outputDirectoryPath);

            var inputFilePath = Path.Combine(zipDirectoryPath, "input-x.geojson");
            var inputJson = File.ReadAllText(inputFilePath);
            var features = JsonConvert.DeserializeObject<Root>(inputJson).features;

            var counter = 0;

            foreach (var feature in features)
            {
                var region = new ZipRegion
                {
                    code = feature.properties.zcta5_code.Single(),
                };

                var geometryJson = JsonConvert.SerializeObject(feature.geometry);

                try
                {
                    region.coordinates = new List<List<List<List<double>>>> { JsonConvert.DeserializeObject<ZipGeometry3>(geometryJson).coordinates };
                }
                catch
                {
                    try
                    {
                        region.coordinates = JsonConvert.DeserializeObject<ZipGeometry4>(geometryJson).coordinates;
                    }
                    catch
                    {
                        region.coordinates = new List<List<List<List<double>>>> {
                            new List<List<List<double>>> { JsonConvert.DeserializeObject<ZipGeometry2>(geometryJson).coordinates }
                        };
                    }
                }


                var json = JsonConvert.SerializeObject(region);
                File.WriteAllText(Path.Combine(outputDirectoryPath, $"{region.code}.json"), json);
                Console.WriteLine($"{++counter}/{features.Count}");
            }
        }
    }


    // input

    public class Geometry
    {
        public object coordinates { get; set; }
    }

    public class Properties
    {
        public List<string> zcta5_code { get; set; }
    }

    public class Feature
    {
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
    }

    public class Root
    {
        public List<Feature> features { get; set; }
    }

    // output

    public class ZipGeometry2
    {
        public List<List<double>> coordinates { get; set; }
    }

    public class ZipGeometry3
    {
        public List<List<List<double>>> coordinates { get; set; }
    }

    public class ZipGeometry4
    {
        public List<List<List<List<double>>>> coordinates { get; set; }
    }

    public class ZipRegion
    {
        public string code { get; set; }
        public List<List<List<List<double>>>> coordinates { get; set; }
    }
}

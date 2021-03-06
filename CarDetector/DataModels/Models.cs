﻿//autogenerated with https://app.quicktype.io/
using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace CarDetector.DataModels
{
    public partial class AiResponse
    {
        [JsonProperty("timings")]
        public List<Timing> Timings { get; set; }

        [JsonProperty("is_success")]
        public bool IsSuccess { get; set; }

        [JsonProperty("detected_models")]
        public List<DetectedModel> DetectedModels { get; set; }

        [JsonProperty("detected_objects")]
        public List<DetectedObject> DetectedObjects { get; set; }

        [JsonProperty("rubbish_predict")]
        public List<RubbishPredict> RubbishPredict { get; set; }

        [JsonProperty("pred_type")]
        public string PredType { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }

    public class DetectedModel
    {
        [JsonProperty("gen_name")]
        public string GenName { get; set; }

        [JsonProperty("model_prob")]
        public double ModelProb { get; set; }

        [JsonProperty("gen_db_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? GenDbId { get; set; }

        [JsonProperty("gen_net_id")]
        public long GenNetId { get; set; }

        [JsonProperty("model_name")]
        public string ModelName { get; set; }

        [JsonProperty("gen_prob")]
        public double GenProb { get; set; }

        [JsonProperty("make_name")]
        public string MakeName { get; set; }

        [JsonProperty("model_db_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ModelDbId { get; set; }

        [JsonProperty("make_db_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? MakeDbId { get; set; }
    }

    public class DetectedObject
    {
        [JsonProperty("class_name")]
        public string ClassName { get; set; }

        [JsonProperty("prob")]
        public double Prob { get; set; }

        [JsonProperty("bbox")]
        public Dictionary<string, double> Bbox { get; set; }

        [JsonProperty("class_net_id")]
        public long ClassNetId { get; set; }
        
        public Bbox GetBbox()
        {
            var bBox = new Bbox()
            {
                BrX = Bbox["br_x"],
                BrY = Bbox["br_y"],
                TlX = Bbox["tl_x"],
                TlY = Bbox["tl_y"]
            };
            return bBox;
        }
    }

    public class RubbishPredict
    {
        [JsonProperty("prob")]
        public double Prob { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public class Timing
    {
        [JsonProperty("time")]
        public double Time { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Bbox
    {
        public double BrX { get; set; }

        public double BrY { get; set; }

        public double TlX { get; set; }

        public double TlY { get; set; }

        public double Width => BrX - TlX;

        public double Height => BrY - TlY;
    }

    public static class Serialize
    {
        public static AiResponse FromJson(string json) => JsonConvert.DeserializeObject<AiResponse>(json, Converter.Settings);
        public static string ToJson(this AiResponse self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
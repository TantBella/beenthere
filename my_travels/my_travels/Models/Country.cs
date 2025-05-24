using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my_travels.Models
{
    [BsonIgnoreExtraElements]
    public class Country : CountryInfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("languages")]
        public string languages { get; set; }

        [BsonElement("flags")]
        public string flags { get; set; }

        [BsonElement("capital")]
        public string capital { get; set; }

        [BsonElement("continents")]
        public string continents { get; set; }

        [BsonElement("nativeName")]
        public string nativeName { get; set; }

    }
}

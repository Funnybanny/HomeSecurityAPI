using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeSecurityAPI.Models
{
    public class Picture
    {
        public ObjectId Id { get; set; }
        [BsonElement("userID")]
        public int userID { get; set; }
        [BsonElement("Base64")]
        public string Base64 { get; set; }
    }
}

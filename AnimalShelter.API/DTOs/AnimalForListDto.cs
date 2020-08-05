using System;

namespace AnimalShelter.API.DTOs
{
    public class AnimalForListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Gender { get; set; }
        public DateTime Posted { get; set; }
        public DateTime TimeLeftToAdopt { get; set; }
        public int UserId { get; set; } 
        public string PhotoUrl { get; set; }
    }
}
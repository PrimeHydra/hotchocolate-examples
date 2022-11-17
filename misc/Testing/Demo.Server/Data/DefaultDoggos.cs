using Demo.Server.Models;

namespace Demo.Server.Data
{
    public static class DefaultDoggos
    {
        public static List<Puppy> GetPuppies() => new List<Puppy>()
        {
            new Puppy
            {
                Id = "Id_Sophie",
                Name = "Sophie D Chillingshead",
                Breed = "Chihuahua",
            },
            new Puppy
            {
                Id = "Id_Hoakie",
                Name = "Hoakie Malone",
                Breed = "Sheltie",
            }
        };
    }
}
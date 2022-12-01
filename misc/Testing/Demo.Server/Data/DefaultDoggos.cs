using Demo.Server.Models;

namespace Demo.Server.Data
{
    public static class DefaultDoggos
    {
        public static List<Puppy> GetPuppies()
        {
            var list = new List<Puppy>()
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

            // Simulate large collection to more closely resemble our huge test bed
            for (int i = 0; i < 10000; i++)
            {
                list.Add(new Puppy()
                {
                    Id = $"Id_RoboPup_{i}",
                    Name = $"Robo Pup #{i}",
                    Breed = "Hyundai",
                });
            }

            return list;
        }
    }
}
namespace Playground.HåvardSamples;

public class PageViewModel
{
    public PageViewModel()
    {
        AnimalGroups = new List<AnimalGroup>()
        {
            new AnimalGroup("Bears",
                new List<Animal>()
                {
                    new Animal
                    {
                        Name = "American Black Bear",
                        Location = "North America",
                        Details = "Details about the bear go here.",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/0/08/01_Schwarzbär.jpg"
                    },
                    new Animal
                    {
                        Name = "Asian Black Bear",
                        Location = "Asia",
                        Details = "Details about the bear go here.",
                        ImageUrl =
                            "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b7/Ursus_thibetanus_3_%28Wroclaw_zoo%29.JPG/180px-Ursus_thibetanus_3_%28Wroclaw_zoo%29.JPG"
                    },
                }),
            new AnimalGroup("Monkeys", new List<Animal>
            {
                new Animal
                {
                    Name = "Baboon",
                    Location = "Africa & Asia",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
                },
                new Animal
                {
                    Name = "Capuchin Monkey",
                    Location = "Central & South America",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                }}),
            new AnimalGroup("Monkeys", new List<Animal>
            {
                new Animal
                {
                    Name = "Baboon",
                    Location = "Africa & Asia",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
                },
                new Animal
                {
                    Name = "Capuchin Monkey",
                    Location = "Central & South America",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                }}),
            new AnimalGroup("Monkeys", new List<Animal>
            {
                new Animal
                {
                    Name = "Baboon",
                    Location = "Africa & Asia",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
                },
                new Animal
                {
                    Name = "Capuchin Monkey",
                    Location = "Central & South America",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                }}),
            new AnimalGroup("Monkeys", new List<Animal>
            {
                new Animal
                {
                    Name = "Baboon",
                    Location = "Africa & Asia",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
                },
                new Animal
                {
                    Name = "Capuchin Monkey",
                    Location = "Central & South America",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                }}),
            new AnimalGroup("Monkeys", new List<Animal>
            {
                new Animal
                {
                    Name = "Baboon",
                    Location = "Africa & Asia",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
                },
                new Animal
                {
                    Name = "Capuchin Monkey",
                    Location = "Central & South America",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                }}),
            new AnimalGroup("Monkeys", new List<Animal>
            {
                new Animal
                {
                    Name = "Baboon",
                    Location = "Africa & Asia",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
                },
                new Animal
                {
                    Name = "Capuchin Monkey",
                    Location = "Central & South America",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                }}),
            new AnimalGroup("Monkeys", new List<Animal>
            {
                new Animal
                {
                    Name = "Baboon",
                    Location = "Africa & Asia",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
                },
                new Animal
                {
                    Name = "Capuchin Monkey",
                    Location = "Central & South America",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                }}),
            new AnimalGroup("Monkeys", new List<Animal>
            {
                new Animal
                {
                    Name = "Baboon",
                    Location = "Africa & Asia",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
                },
                new Animal
                {
                    Name = "Capuchin Monkey",
                    Location = "Central & South America",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                }}),
            new AnimalGroup("Monkeys", new List<Animal>
            {
                new Animal
                {
                    Name = "Baboon",
                    Location = "Africa & Asia",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
                },
                new Animal
                {
                    Name = "Capuchin Monkey",
                    Location = "Central & South America",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                }}),
            new AnimalGroup("Monkeys", new List<Animal>
            {
                new Animal
                {
                    Name = "Baboon",
                    Location = "Africa & Asia",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
                },
                new Animal
                {
                    Name = "Capuchin Monkey",
                    Location = "Central & South America",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                }}),
            new AnimalGroup("Monkeys", new List<Animal>
            {
                new Animal
                {
                    Name = "Baboon",
                    Location = "Africa & Asia",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
                },
                new Animal
                {
                    Name = "Capuchin Monkey",
                    Location = "Central & South America",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                }}),
            new AnimalGroup("Monkeys", new List<Animal>
            {
                new Animal
                {
                    Name = "Baboon",
                    Location = "Africa & Asia",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
                },
                new Animal
                {
                    Name = "Capuchin Monkey",
                    Location = "Central & South America",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                }}),
            new AnimalGroup("Monkeys", new List<Animal>
            {
                new Animal
                {
                    Name = "Baboon",
                    Location = "Africa & Asia",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
                },
                new Animal
                {
                    Name = "Capuchin Monkey",
                    Location = "Central & South America",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                }}),
            new AnimalGroup("Monkeys", new List<Animal>
            {
                new Animal
                {
                    Name = "Baboon",
                    Location = "Africa & Asia",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
                },
                new Animal
                {
                    Name = "Capuchin Monkey",
                    Location = "Central & South America",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                }}),
            new AnimalGroup("Monkeys", new List<Animal>
            {
                new Animal
                {
                    Name = "Baboon",
                    Location = "Africa & Asia",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
                },
                new Animal
                {
                    Name = "Capuchin Monkey",
                    Location = "Central & South America",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                },
                new Animal
                {
                    Name = "Blue Monkey",
                    Location = "Central and East Africa",
                    Details = "Details about the monkey go here.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                }})
        };
    }

    public List<AnimalGroup> AnimalGroups { get; set; }
}
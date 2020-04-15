namespace Dojodachi.Models
{
    public class Pet
    {
        public int Fullness {get; set;}
        public int Happiness {get; set;}
        public int Energy {get; set;}
        public int Meals {get; set;}
        public string MoodImage {get; set;}
        public string Message {get; set;}

        public Pet(int full, int happy, int energy, int meals, string mood, string message)
        {
            Fullness = full;
            Happiness = happy;
            Energy = energy;
            Meals = meals;
            MoodImage = mood;
            Message = message;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueMarble.Twitter
{
    public static class RandomPlaces
    {
        static List<string> Places = new List<string>();
        static List<string> Messages = new List<string>();

        static Random RandomGenerator;

        static RandomPlaces()
        {
            RandomGenerator = new Random((int)DateTime.Now.Ticks);
            ConstructPlaceList();
            ConstructMessageList();
        }

        public static string GetRandomLocation()
        {
            if (Places.Count == 0)
            {
                ConstructPlaceList();
            }

            int locationIndex = RandomGenerator.Next(Places.Count);
            string nextLocation = Places[locationIndex];
            Places.RemoveAt(locationIndex);

            return nextLocation;
        }

        public static void ConstructPlaceList()
        {
            Places.Add("New York City");
            Places.Add("Paris, France");
            Places.Add("Rome");
            Places.Add("Sydney, Australia");
            Places.Add("Dublin, Ireland");
            Places.Add("Sahara Desert");
            Places.Add("South Africa");
            Places.Add("Pyongyang");
            Places.Add("Cleveland, Ohio");
            Places.Add("Tokyo, Japan");
            Places.Add("Moscow, Russia");
            Places.Add("Washington DC");
            Places.Add("Nicaragua");
            Places.Add("Ethiopia");
            Places.Add("Mexico City");
            Places.Add("Rio, Brazil");
            Places.Add("Taiwan");
            Places.Add("Congo");
            Places.Add("Florida Everglades");
            Places.Add("Bhutan");
            Places.Add("Amsterdam");
            Places.Add("Mount Ararat, Turkey");
            Places.Add("Nile River");
            Places.Add("Dubai");
            Places.Add("Phoenix, Arizona");
            Places.Add("El Paso, Texas");
            Places.Add("Buenos Aires, Argentina");
            Places.Add("Athens, Greece");
            Places.Add("Lisbon, Portugal");
            Places.Add("Madrid, Spain");
            Places.Add("London, England");
            Places.Add("Seoul, South Korea");
            Places.Add("Guam");
            Places.Add("San Juan, Puerto Rico");
            Places.Add("Iceland");
            Places.Add("Sweden");
            Places.Add("Berlin, Germany");
            Places.Add("Syberia");
            Places.Add("Beijing, China");
            Places.Add("Kenya");
            Places.Add("Madagascar");
            Places.Add("Sri Lanka");
            Places.Add("Canary Islands");
            Places.Add("Ecuador");
            Places.Add("Amazon River");
			Places.Add("Juneau, Alaska");
        }

        public static void ConstructMessageList()
        {
            Messages.Add("Check out this photo of {0}!");
            Messages.Add("Wow, look at {0} from space.");
            Messages.Add("Astronaut's view of {0}.");
            Messages.Add("So much to see in {0}.");
            Messages.Add("{0}");
            Messages.Add("Flying over {0}.");
            Messages.Add("{0} from space.");
            Messages.Add("NASA photo of {0}.");
            Messages.Add("Look at {0}!");
            Messages.Add("Great view of {0}.");
            Messages.Add("Cool it's {0}!");
            Messages.Add("Unique view of {0}.");
            Messages.Add("Take a look at {0}!");
            Messages.Add("View of {0} from above the clouds.");
        }

        public static string GetRandomMessage()
        {
            if (Messages.Count == 0)
            {
                ConstructMessageList();
            }

            int messageIndex = RandomGenerator.Next(Messages.Count);
            string nextMessage = Messages[messageIndex];
            Messages.RemoveAt(messageIndex);

            return nextMessage;
        }
    }
}
namespace nexBart.DataModels
{
    public class Train
    {
        public string NumCars { get; set; }
        public string Bikes {get; set;}
        public string Time { get; set; }

        public Train(string mins, string length, string bikeFlag)
        {
            Time = mins;
            NumCars = length;

            if (bikeFlag == "1") Bikes = "bikes";
            else Bikes = "no bikes";
        }
    }
}

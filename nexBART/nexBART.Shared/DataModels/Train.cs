namespace nexBart.DataModels
{
    public class Train
    {
        public string NumCars { get; set; }
        public string BikeStatus {get; set;}

        public Train(string cars, string bikes)
        {
            NumCars = cars;
            BikeStatus = bikes;
        }
    }
}

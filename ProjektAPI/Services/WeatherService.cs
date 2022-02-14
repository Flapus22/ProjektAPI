using ProjektAPI.Model;

namespace ProjektAPI.Services
{
    public interface IWeatherService
    {
        public IEnumerable<Weather> GetAll();
        public Weather Get(int id);
        public IEnumerable<Weather> GetBetweenDate(DateTime from, DateTime to);
        public IEnumerable<Weather> GetByDate(DateTime date);
        public IEnumerable<Weather> GetBetweenTemperature(float from, float to);
        public void Post(NewWeather newWeather);
        public void Put(int id, NewWeather newWeather);
        public void Delete(int id);
    }
    public class WeatherService : IWeatherService
    {
        public readonly WeatherDataContext db;

        public WeatherService(WeatherDataContext db)
        {
            this.db = db;
        }

        public IEnumerable<Weather> GetAll()
        {
            var temp = db.Weathers;
            return temp;
        }
        public Weather Get(int id)
        {
            return db.Weathers.FirstOrDefault(x => x.IdWeather == id);
        }

        public void Post(NewWeather newWeather)
        {
            var temp = new Weather()
            {
                //IdWeather = db.Weathers.Max(x => x.IdWeather) + 1,
                Date = newWeather.Date != null ? (DateTime)newWeather.Date : DateTime.Today,
                Temperature = (float)newWeather.Temperature,
                Presure = (float)newWeather.Presure,
                RainFall = newWeather.RainFall,
                CloudCover = newWeather.CloudCover
            };
            db.Weathers.Add(temp);
            db.SaveChanges();
        }
        public void Put(int id, NewWeather newWeather)
        {
            var temp = db.Weathers.FirstOrDefault(x => x.IdWeather == id);
            if (temp is null)
                return;

            temp.Date = newWeather.Date != null ? (DateTime)newWeather.Date : temp.Date;
            temp.Temperature = (float)(newWeather.Temperature != null ? newWeather.Temperature : temp.CloudCover);
            temp.Presure = (float)(newWeather.Presure != null ? newWeather.Presure : temp.Presure);
            temp.RainFall = newWeather.RainFall;
            temp.CloudCover = newWeather.CloudCover;

            db.SaveChanges();
        }
        public void Delete(int id)
        {
            var temp = db.Weathers.FirstOrDefault(x => x.IdWeather == id);

            if (temp != null)
                db.Weathers.Remove(temp);

            db.SaveChanges();
        }

        public IEnumerable<Weather> GetBetweenDate(DateTime from, DateTime to)
        {
            if (from > to)
                return new List<Weather>();
            return db.Weathers.Where(x => x.Date >= from && x.Date <= to).ToList();
        }

        public IEnumerable<Weather> GetByDate(DateTime date)
        {
            return db.Weathers.Where(x => x.Date == date);
        }

        public IEnumerable<Weather> GetBetweenTemperature(float from, float to)
        {
            if (from > to)
                return new List<Weather>();
            return db.Weathers.Where(x => x.Temperature >= from && x.Temperature <= to).ToList();
        }
    }
}

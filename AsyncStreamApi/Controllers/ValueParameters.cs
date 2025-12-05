namespace ZoneWebApp.REST.Controllers
{
    public class ValueParameters
    {
		const int maxPageSize = 50;
		public int Offset { get; set; } = 1;

		private int _limit = 10;
		public int Limit
		{
			get
			{
				return _limit;
			}
			set
			{
				_limit = (value > maxPageSize) ? maxPageSize : value;
			}
		}
	}
}
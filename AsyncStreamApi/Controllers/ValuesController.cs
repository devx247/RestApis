using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ZoneWebApp.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly List<string> DbMock = GetDbMock(100);

        private static List<string> GetDbMock(int count)
        {
            List<string> data = new List<string>();

            for (int i = 0; i < count; i++) data.Add($"data-item-{i}");

            return data;
        }

        // GET api/values?offset={offset}&limit={limit}
        [HttpGet("/api/asyncstream-test")]
        public ActionResult<IEnumerable<string>> Get([FromQuery] ValueParameters valueParameters)
        {
            string[] result = DbMock.Skip(valueParameters.Offset).Take(valueParameters.Limit).ToArray();
            return result;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return DbMock;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return DbMock.ElementAtOrDefault(id) ?? "value not found at index:{id}";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            DbMock.Add(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            string element = DbMock.ElementAtOrDefault(id);

            if (element == null)
                return;

            DbMock[id] = value;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            string element = DbMock.ElementAtOrDefault(id);

            if (element == null)
                return;

            DbMock.RemoveAt(id);
        }
    }
}
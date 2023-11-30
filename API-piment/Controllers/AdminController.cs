using Dapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;


namespace API_piment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        #region DI
        private readonly SqlConnection? _conn = null;
        private readonly string str = @"Data Source=DESKTOP-FH1BO1J\SQLEXPRESS;Initial Catalog=Piments;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;Multi Subnet Failover=False";

        public AdminController()
        {
            _conn = new SqlConnection(str);
        }
        #endregion


        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>ActionResult representing the result of the operation.</returns>
        [HttpGet]
        [ProducesResponseType(200)] // OK
        [ProducesResponseType(204)] // No Content
        public ActionResult<IEnumerable<Models.UserAdminInfo>> Get()
        {
            string sql = "SELECT * FROM [User]";
            IEnumerable<Models.UserAdminInfo>? items = _conn?.Query<Models.UserAdminInfo>(sql).ToList();

            if (items is not null && items.Any())
                return Ok(items);

            return NoContent();
        }





        //// GET api/<AdminController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<AdminController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<AdminController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<AdminController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

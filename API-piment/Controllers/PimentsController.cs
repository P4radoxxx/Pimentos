using Dapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;


namespace API_piment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PimentsController : ControllerBase
    {

        #region DI
        private readonly SqlConnection? _conn = null;
        private readonly string str = @"Data Source=DESKTOP-FH1BO1J\SQLEXPRESS;Initial Catalog=Piments;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;Multi Subnet Failover=False";
        public PimentsController()
        {
            _conn = new SqlConnection(str);
        }
        #endregion


        /// <summary>
        /// Retrieves all Piments.
        /// </summary>
        /// <returns>ActionResult representing the result of the operation.</returns>
        [HttpGet]
        [ProducesResponseType(200)] // OK
        [ProducesResponseType(204)] // No Content
        public ActionResult<IEnumerable<Models.Piments>> Get()
        {
            string sql = "SELECT * FROM [Piments]";
            IEnumerable<Models.Piments>? items = _conn?.Query<Models.Piments>(sql).ToList();

            if (items is not null)
                return Ok(items); 

            return NoContent(); 
        }

        /// <summary>
        /// Retrieves a specific Piment by its ID.
        /// </summary>
        /// <param name="id">The ID of the Piment to retrieve.</param>
        /// <returns>ActionResult representing the result of the operation.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)] // OK
        [ProducesResponseType(204)] // No Content
        public ActionResult<Models.Piments>? Get(int id)
        {
            try
            {
                string sql = "SELECT * FROM Piments WHERE Id = @idP";
                Models.Piments? item = _conn?.QuerySingle<Models.Piments>(sql, new { idP = id });

                if (item is not null)
                    return Ok(item);
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }

            return NoContent();
        }


    }
}

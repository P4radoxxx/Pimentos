using Dapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;


namespace API_piment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        #region DI
        private readonly SqlConnection? _conn = null;
        private readonly Services.Token.JWTService? _jwtService = null;
        private readonly string str = @"Data Source=DESKTOP-FH1BO1J\SQLEXPRESS;Initial Catalog=Piments;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;Multi Subnet Failover=False";

        public UserController(Services.Token.JWTService? jwtService)
        {
            _conn = new SqlConnection(str);
            _jwtService = jwtService;
        }
        #endregion



        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="model">User information to register.</param>
        /// <returns>ActionResult representing the result of the operation.</returns>
        [HttpPost]
        [ProducesResponseType(201)] // Created
        [ProducesResponseType(400)] // Bad Request
        public IActionResult Post([FromBody] Models.UserRegisterDTO model)
        {
            if (model is not null)
            {
                Models.User user = new(model.Name, model.Email, model.Password, "Register");
   
                string sql = "INSERT INTO [User] (Name, Email, Password, Role) VALUES (@name, @email, @password, @role)";
                var paramFromModel = new { name = user.Name, email = user.Email, password = user.Password, role = user.Role };

                var rowAffected = _conn.Execute(sql, paramFromModel);

                if (rowAffected > 0)
                    return CreatedAtAction(nameof(Post), user);
            }

            return BadRequest();
        }



        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>ActionResult representing the result of the operation.</returns>
        [HttpGet]
        [ProducesResponseType(200)] // OK
        [ProducesResponseType(204)] // No Content
        public ActionResult<IEnumerable<Models.UserDisplayDTO>> Get()
        {
            string sql = "SELECT * FROM [User]";
            IEnumerable<Models.UserDisplayDTO>? items = _conn?.Query<Models.UserDisplayDTO>(sql).ToList();

            if (items is not null && items.Any())
                return Ok(items);

            return NoContent();
        }



        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <param name="model">User authentication information.</param>
        /// <returns>ActionResult representing the result of the operation.</returns>
        [HttpPost(nameof(Log))]
        [ProducesResponseType(200)] // OK
        [ProducesResponseType(400)] // Bad Request
        public ActionResult<Models.UserTokenDTO> Log(Models.UserLogDTO model)
        {
            string sql = "SELECT Id, Role FROM [User] WHERE Password = @pass AND Email = @mail";
            Models.UserTokenDTO? result = _conn?.Query<Models.UserTokenDTO>(sql, new { pass = model.Password, mail = model.Email }).FirstOrDefault();

            if (result is not null)
            {
                string id = (result.Id).ToString();
                string role = result.Role;

                string token = _jwtService?.GenerateToken(id, role) ?? "";

                if (!string.IsNullOrEmpty(token))
                {
                    // Retourne explicitement du JSON, l'api le convertira automatiquement si tu lui cree un nouvel objet pour le retour.
                    return Ok(new { token });
                }
            }

            return BadRequest();
        }




    }
}


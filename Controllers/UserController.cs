using CopaVale.Context;
using CopaVale.Models;
using CopaVale.Service;
using CopaVale.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaVale.Controllers
{
    public class UserController : ControllerBase
    {

        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("v1/user")]
        public async Task<ActionResult<List<User>>> Get()
        {
           
                var users = await _context.User.Include(x => x.Ticket).AsNoTracking().ToListAsync();
                if (users == null)
                {
                    return NotFound(new {erro = "Nenhum usuário encontrado!" });
                }

                return Ok(users);
            
            
        }

        //Criação do usuário
        [HttpPost]
        [Route("v1/user")]
        public async Task<ActionResult<dynamic>> Post([FromBody]User model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            try
            {
                model.Role = "usuario";
                _context.User.Add(model);
                await _context.SaveChangesAsync();
                
            }
            catch (Exception)
            {

                return BadRequest(new { Erro = "Não foi possível se conectar com o banco de dados!" });
            }
            return new
            {
                user = model,
                mesangem = "Usuário cadastrado com sucesso!"
            };
        }

        //Autenticação
        [HttpPost]
        [Route("v1/login")]
        public async Task<ActionResult<dynamic>> Login([FromBody] UserLoginModel model)
        {
            try
            {
                var user = await _context.User
                    .AsNoTracking()
                    .Where(x => x.Nickname == model.Nickname && x.Password == model.Password)
                    .FirstOrDefaultAsync();

                if (user == null)
                    return NotFound(new { Erro = "Usuário ou senha inválidos!" });

                var token = TokenService.GenerateToken(user);

                return new
                {
                    user = user,
                    token = token

                };

            }
            catch (Exception)
            {
                return BadRequest(new { Erro = "Não foi possível se conectar com o banco de dados para a criação do usuário!" });
            }



        }

    }
}

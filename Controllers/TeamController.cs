using CopaVale.Context;
using CopaVale.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaVale.Controllers
{
    [Route("v1/team")]
    public class TeamController : ControllerBase
    {
        private readonly DataContext _context;
        public TeamController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Team>>> Get()
        {

            var teams = await _context.Team.AsNoTracking().ToListAsync();
            if (teams == null)
            {
                return NotFound(new { erro = "Nenhum time encontrado!" });
            }

            return Ok(teams);


        }

        //Criação de um time
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<dynamic>> Post([FromBody] Team model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new { Erro = "Por favor verifique os dados digitados!" });
            }

            try
            {
                _context.Team.Add(model);
                await _context.SaveChangesAsync();

            }
            catch
            {

                return BadRequest(new { Erro = "Não foi possível se conectar com o banco de dados!" });
            }
            return new
            {
                team = model,
                mesangem = "Time cadastrado com sucesso!"
            };
        }

    }
}

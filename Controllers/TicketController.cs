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
    [Route("v1/ticket")]
    public class TicketController : Controller
    {

        private readonly DataContext _context;

        public TicketController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Ticket>>> Get()
        {
           

            try
            {
                var tickets = await _context.Ticket.AsNoTracking().ToListAsync();
                if (tickets == null)
                {
                    return NotFound(new { erro = "Nenhum ticket encontrado!" });
                }

                return Ok(tickets);

            }
            catch (Exception)
            {
                return BadRequest(new { Erro = "Não foi possível se conectar com o banco de dados para procura dos tickets!" });
            }
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Ticket>>> GetByUser(int Id)
        {
            try
            {
                var tickets =  await _context.Ticket.AsNoTracking().Where(x => x.User.UserId == Id).FirstOrDefaultAsync();

                if (tickets == null)
                {
                    return NotFound(new { erro = "Nenhum ticket encontrado!" });
                }

                return Ok(tickets);
            }
            catch (Exception)
            {

                return BadRequest(new { Erro = "Não foi possível se conectar com o banco de dados para procura dos tickets!" });
            }
        }


        [HttpPost]
        [Route("")]
        public async Task<ActionResult<dynamic>> Post([FromBody]Ticket model)
        {

            var data = DateTime.UtcNow.Date.ToString();
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Erro = "Por favor verifique os dados digitados!" });
            }

            try
            {

                _context.Ticket.Add(model);
                await _context.SaveChangesAsync();

                return new
                {
                    ticket = model,
                    mesangem = "Ticket criado com sucesso!"
                };
            }
            catch (Exception)
            {

                return BadRequest(new { Erro = "Não foi possível se conectar com o banco de dados!" });
            }
        }
    }
}

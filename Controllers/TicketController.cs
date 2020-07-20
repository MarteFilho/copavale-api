using CopaVale.Context;
using CopaVale.Models;
using CopaVale.Service;
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
                var tickets = await _context.Ticket.Include(x => x.User).AsNoTracking().ToListAsync();
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
                var tickets =  await _context.Ticket.AsNoTracking().Where(x => x.User.UserId == Id).ToListAsync();

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

            var data = DateTime.UtcNow.ToString();
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Erro = "Por favor verifique os dados digitados!" });
            }

            try
            {
                model.DateOpen = data;
                model.Awnser = "";
                model.Status = "Em Andamento";
                _context.Ticket.Add(model);
                await _context.SaveChangesAsync();


                EmailService sendEmail = new EmailService();
                string body = @"<style>
                            </style>
                            <h1>Ticket Criado com sucesso!</h1></br>
                            <h2>Status: " + model.Status + "</h2>";

                var userEmail = _context.User.AsNoTracking().Where(x => x.UserId == model.UserId).FirstOrDefault();



                var resultado = sendEmail.sendMail(userEmail.Email, "Copa Vale - Ticket ", body);

                string bodyAdmin = 
                            $@"<style>
                            </style>
                            <h1>Ticket Criado - Usuário - {userEmail.Nickname}</h1></br> <h2>Problema: " + model.Problem + "</h2><h2>Motivo: " + model.Reason + "</h2><h2>Status: " + model.Status + "</h2>";

                //sendEmail.sendMail(userEmail.Email, "Copa Vale - Ticket ", bodyAdmin);

                return new
                {
                    ticket = model,
                    mesangem = "Ticket criado com sucesso!",
                    resultado = resultado,
                };

                

            }
            catch (Exception)
            {

                return BadRequest(new { Erro = "Não foi possível se conectar com o banco de dados!" });
            }
        }
    }
}

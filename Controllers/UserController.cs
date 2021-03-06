﻿using CopaVale.Context;
using CopaVale.Models;
using CopaVale.Service;
using CopaVale.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "admin")]
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
                return BadRequest(new { Erro = "Por favor verifique os dados digitados!" });
            }

            try
            {
                model.Role = "usuario";
                model.Password = PasswordService.Encrypt(model.Password);
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
                    .Where(x => x.Nickname == model.Nickname)
                    .FirstOrDefaultAsync();

                if (user == null)
                    return NotFound(new { Erro = "Usuário não encontrado!" });

                else
                {
                    if (PasswordService.Compare(model.Password, user.Password))
                    {
                        var token = TokenService.GenerateToken(user);
                        user.Password = "";

                        return new
                        {
                            user = user,
                            token = token,
                            mesangem = "Autenticado com sucesso!"
                        };
                    }

                    else
                    {
                        return NotFound(new { Erro = "Senha inválida!" });
                    }
                }

                

            }
            catch (Exception)
            {
                return BadRequest(new { Erro = "Não foi possível realizar o login" });
            }
        }

    }
}

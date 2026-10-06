using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Models;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers;

[ApiController]
[Route("[Controller]")]

public class UsuarioController : ControllerBase
{
    private UsuarioService usuarioService;

    public UsuarioController(UsuarioService cadastroService)
    {
        this.usuarioService = cadastroService;
    }

    [HttpPost("cadastro")]
    public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto dto)
    {
        await usuarioService.Cadastra(dto);
        return Ok("Usuário cadastrado!");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login (LoginUsuarioDto dto)
    {
        var token = await usuarioService.Login(dto);
        return Ok(token);
    }
}

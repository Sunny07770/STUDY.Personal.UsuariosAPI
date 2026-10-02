using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services;

public class CadastroService
{
    private UserManager<Usuario> _userManager;
    private IMapper _mapper;

    public CadastroService(IMapper mapper, UserManager<Usuario> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    internal async Task Cadastra(CreateUsuarioDto dto)
    {
        Usuario usuario = _mapper.Map<Usuario>(dto);
        IdentityResult insercao = await _userManager
            .CreateAsync(usuario, dto.Password);
        if (!insercao.Succeeded)
        {
            throw new ApplicationException("Falha ao cadastrar usuário!");
        }
    }
}
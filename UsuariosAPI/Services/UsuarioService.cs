using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services;

public class UsuarioService
{
    private TokenService _tokenService;
    private SignInManager<Usuario> _singInManager;
    private UserManager<Usuario> _userManager;
    private IMapper _mapper;

    public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, TokenService tokenService, SignInManager<Usuario> singInManager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _singInManager = singInManager;
        _tokenService = tokenService;
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

    internal async Task<string> Login(LoginUsuarioDto dto)
    {
        var insercao = await _singInManager
            .PasswordSignInAsync(dto.Username, dto.Password, false, false);
        if (!insercao.Succeeded)
        {
            throw new ApplicationException("Usuario não autenticado!");
        }

        var usuario = _singInManager
            .UserManager
            .Users
            .FirstOrDefault(user => user.NormalizedUserName == 
            dto.Username.ToUpper());

        var token = _tokenService.GenerateToken(usuario);

        return token;
    }
}
using AutoMapper;
using MediatR;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Features.PanelAccounts.Commands;

public class LoginPanelAccountCommandHandler : IRequestHandler<LoginPanelAccountCommand, PanelAccountResponseDto>
{
    private readonly IPanelAccountRepository _panelAccountRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    public LoginPanelAccountCommandHandler(
        IPanelAccountRepository panelAccountRepository, 
        IMapper mapper, 
        IPasswordHasher passwordHasher)
    {
        this._panelAccountRepository = panelAccountRepository;
        this._mapper = mapper;
        this._passwordHasher = passwordHasher;
    }

    public async Task<PanelAccountResponseDto> Handle(
        LoginPanelAccountCommand request, 
        CancellationToken cancellationToken)
    {
        var panelAccount = await this._panelAccountRepository.GetByLoginAsync(request.Login);

        if (panelAccount is null)
            throw new Exception("Login or password is wrong!");

        var isValid = this._passwordHasher.Verify(request.Password, panelAccount.PasswordHash);

        if (!isValid)
            throw new Exception("Login or password is wrong!");

        return this._mapper.Map<PanelAccountResponseDto>(panelAccount);
    }
}
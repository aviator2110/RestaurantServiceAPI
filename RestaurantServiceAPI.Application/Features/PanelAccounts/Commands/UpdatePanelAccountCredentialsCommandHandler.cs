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

public class UpdatePanelAccountCredentialsCommandHandler 
    : IRequestHandler<UpdatePanelAccountCredentialsCommand, PanelAccountResponseDto>
{
    private readonly IPanelAccountRepository _panelAccountRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    public UpdatePanelAccountCredentialsCommandHandler(
        IPanelAccountRepository panelAccountRepository, 
        IMapper mapper, 
        IPasswordHasher passwordHasher)
    {
        this._panelAccountRepository = panelAccountRepository;
        this._mapper = mapper;
        this._passwordHasher = passwordHasher;
    }

    public async Task<PanelAccountResponseDto> Handle(UpdatePanelAccountCredentialsCommand request, CancellationToken cancellationToken)
    {
        var panelAccount = await this._panelAccountRepository.GetByIdAsync(request.Id);

        if (panelAccount is null)
            throw new Exception("Panel account with this Id doesn't exist!");

        var isValid = this._passwordHasher.Verify(request.prevPassword, panelAccount.PasswordHash);

        if (!isValid)
            throw new Exception("Wrong password!");

        var newPasswordHash = this._passwordHasher.Hash(request.newPassword);

        panelAccount.ChangePassword(newPasswordHash);

        await this._panelAccountRepository.UpdateAsync(panelAccount);

        return this._mapper.Map<PanelAccountResponseDto>(panelAccount);
    }
}

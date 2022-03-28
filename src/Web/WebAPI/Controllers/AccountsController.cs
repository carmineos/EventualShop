﻿using AutoMapper;
using ECommerce.Contracts.Account;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;

namespace WebAPI.Controllers;

[Route("api/[controller]/[action]")]
public class AccountsController : ApplicationController
{
    public AccountsController(IBus bus, IMapper mapper)
        : base(bus, mapper) { }

    [HttpGet]
    public Task<IActionResult> GetAccountsWithPagination([FromQuery] Queries.GetAccounts query, CancellationToken cancellationToken)
        => GetResponseAsync<Queries.GetAccounts, Responses.AccountsDetailsPagedResult>(query, cancellationToken);

    [HttpGet]
    public Task<IActionResult> GetAccountDetails([FromQuery] Queries.GetAccountDetails query, CancellationToken cancellationToken)
        => GetResponseAsync<Queries.GetAccountDetails, Responses.AccountDetails>(query, cancellationToken);

    [HttpPost]
    public Task<IActionResult> CreateAccount(Commands.CreateAccount command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPost]
    public Task<IActionResult> DefineProfessionalAddress(Commands.DefineProfessionalAddress command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPost]
    public Task<IActionResult> DefineResidenceAddress(Commands.DefineResidenceAddress command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut]
    public Task<IActionResult> UpdateProfile(Commands.UpdateProfile command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpDelete]
    public Task<IActionResult> DeleteAccount(Commands.DeleteAccount command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);
}
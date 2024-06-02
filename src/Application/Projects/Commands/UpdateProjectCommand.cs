﻿using Application.Projects.Requests;
using Domain.Primitives;

namespace Application.Projects.Commands;

public record UpdateProjectCommand(UpdateProjectRequest Project) : IRequest;

public class UpdateProjectCommandHandler
    : IRequestHandler<UpdateProjectCommand>
{
    private readonly IRepository<Project> _repository;

    public UpdateProjectCommandHandler(IRepository<Project> repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Project.Id, cancellationToken) ?? throw new Exception($"The request ID {request.Project.Id} was not found.");
        entity.Title = request.Project.Title;

        await _repository.UpdateAsync(entity, cancellationToken);
    }
}
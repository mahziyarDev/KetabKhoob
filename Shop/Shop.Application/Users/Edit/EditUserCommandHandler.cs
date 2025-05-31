﻿using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Domain.UserAgg.Repository;
using Shop.Domain.UserAgg.Services;
using Shop.Infrastructure._Utilities;

namespace Shop.Application.Users.Edit;

internal class EditUserCommandHandler : IBaseCommandHandler<EditUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDomainService _domainService;
    private readonly IFileService _fileService;
    /// <summary></summary>
    /// <param name="userRepository"></param>
    /// <param name="domainService"></param>
    /// <param name="fileService"></param>
    public EditUserCommandHandler(IUserRepository userRepository, IUserDomainService domainService, IFileService fileService)
    {
        _userRepository = userRepository;
        _domainService = domainService;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId,cancellationToken);
        if (user == null)
            return OperationResult.NotFound();

        var oldAvatar = user.AvatarName;
        user.Edit(request.Name, request.Family, request.PhoneNumber, request.Email, request.Gender, _domainService);
        if (request.Avatar != null)
        {
            var imageName = await _fileService
                .SaveFileAndGenerateName(request.Avatar, Directories.UserAvatars);
            user.SetAvatar(imageName);
        }

        DeleteOldAvatar(request.Avatar, oldAvatar);
        await _userRepository.SaveChangeAsync(cancellationToken);
        return OperationResult.Success();
    }

    private void DeleteOldAvatar(IFormFile? avatarFile, string oldImage)
    {
        if (avatarFile == null || oldImage == "avatar.png")
            return;

        _fileService.DeleteFile(Directories.UserAvatars, oldImage);
    }
}
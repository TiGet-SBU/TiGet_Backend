﻿using MediatR;
using Rhazes.BuildingBlocks.Common;
using Rhazes.BuildingBlocks.Common.Infrastructure;
using Rhazes.Services.Identity.API.Application.DTO;
using Rhazes.Services.Identity.Domain.AggregatesModel.UserAggregate;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rhazes.Services.Identity.API.Application.Queries
{
    public class GetUserByPhoneNumbersQueryHandler : IRequestHandler<GetUserByPhoneNumbersQuery, List<UserPatientDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper<ApplicationUser, UserPatientDTO> _mapper;
        public GetUserByPhoneNumbersQueryHandler(
            IUserRepository userRepository,
            IMapper<ApplicationUser, UserPatientDTO> mapper
            )
        {
            _userRepository = MethodParameterChecker.CheckUp(userRepository);
            _mapper = MethodParameterChecker.CheckUp(mapper);
        }

        public async Task<List<UserPatientDTO>> Handle(GetUserByPhoneNumbersQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByPhoneNumbersAsync(request.PhoneNumbers);
            return (_mapper.ToDtos(user)).ToList();
        }
    }
}
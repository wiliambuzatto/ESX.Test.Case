﻿using ESX.Test.Case.Domain.Entities;
using ESX.Test.Case.Domain.Queries;
using ESX.Test.Case.Domain.ValueObjects;
using System.Collections.Generic;
using ESX.Test.Case.Domain.Queries.Response;

namespace ESX.Test.Case.Domain.Repositories
{
	public interface IUserRepository : IEntityRepository<User>
	{
		IEnumerable<UserQueryResult> GetAll();
		bool CheckEmail(Email email);
	}
}

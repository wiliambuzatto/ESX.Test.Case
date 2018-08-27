﻿using ESX.Test.Case.Domain.Entities;
using ESX.Test.Case.Domain.ValueObjects;

namespace ESX.Test.Case.Infra.Repositories
{
	public interface IUserRepository : IEntityRepository<User>
	{
		bool CheckEmail(Email email);
		bool CheckPassword(SecurityPassword password);
	}
}
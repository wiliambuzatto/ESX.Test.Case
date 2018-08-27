﻿using ESX.Test.Case.Domain.Entities;
using ESX.Test.Case.Shared;
using ESX.Test.Case.Shared.Commands;
using Flunt.Validations;

namespace ESX.Test.Case.Domain.Commands.Request
{
	public sealed class BrandCommand : BaseCommand
	{
		public string Name { get; set;  }

		public override bool IsValid()
		{
			base.AddNotifications(new Contract()
				.Requires()
				.HasMinLength(this.Name, Brand.MINIMUM_NAME_SIZE, nameof(this.Name),
					$"The brand name should contain a minimum of {Brand.MINIMUM_NAME_SIZE} characters")
				.HasMaxLength(this.Name, Brand.MAXIMUM_NAME_SIZE, nameof(this.Name),
					$"The brand name should contain a maximum of {Brand.MAXIMUM_NAME_SIZE} characters")
			);

			return this.Valid;
		}
	}
}

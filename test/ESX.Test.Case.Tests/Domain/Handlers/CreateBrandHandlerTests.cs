﻿using System;
using System.Collections.Generic;
using System.Linq;
using ESX.Test.Case.Domain.Commands.Request;
using ESX.Test.Case.Domain.Handlers;
using ESX.Test.Case.Domain.Repositories;
using ESX.Test.Case.Domain.ValueObjects;
using ESX.Test.Case.Shared.Commands;
using Flunt.Notifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace ESX.Test.Case.Tests.Domain.Handlers
{
	[TestClass]
	public partial class BrandHandlerTests : UnitTestBase
	{
		private const string NOT_FOUND_BRAND_ID = "03A17D27-C260-4DE9-9B4E-11474CD9AC88";
		private readonly IBrandRepository repository;
		private readonly ICommandHandler<BrandCommand> brandHandler;
		private readonly BrandCommand brandCommand;

		public BrandHandlerTests()
		{
			this.brandCommand = new BrandCommand();
			this.repository = Substitute.For<IBrandRepository>();
			this.brandHandler = new BrandHandler(this.repository);
		}

		[TestInitialize]
		public void Setup()
		{
			this.brandCommand.Name = this.MockString();
			
			this.repository.CheckBrand(Arg.Is<string>(name => name.Equals(this.brandCommand.Name))).Returns(true);
			this.repository.CheckBrand(Arg.Is<string>(name => !name.Equals(this.brandCommand.Name))).Returns(false);
			this.repository.Delete(Arg.Any<Guid>()).Returns(true);
			this.repository.Delete(Arg.Is<Guid>(id => id.Equals(new Guid(NOT_FOUND_BRAND_ID)))).Returns(false);
		}

		[TestMethod]
		[Description("Given that I trying create brand, " +
					 "when brand name already exists" +
					 "then should contains conflict status code in command result")]
		public void Should_contains_conflict_status_code_in_command_result_when_email_already_exists_in_database()
		{
			var commandResult = this.brandHandler.Create(this.brandCommand);

			Assert.AreEqual(StatusCodeResult.Conflict, commandResult.StatusCode);
		}

		[TestMethod]
		[Description("Given that I trying create brand, " +
					 "when brand name already exists" +
					 "then should contains notification in command result")]
		public void Should_contains_notification_in_command_result_when_brand_already_exists_in_database()
		{
			var commandResult = this.brandHandler.Create(this.brandCommand);

			var notifications = (List<Notification>)commandResult.Data;

			Assert.IsTrue(notifications.First().Property.Equals("Name"));
		}

		[TestMethod]
		[Description("Given that brand command is invalid, " +
					 "when trying create a brand" +
					 "then should contains noticiations in command result")]
		public void Should_contains_notifications_command_result_when_brand_comand_is_invalid()
		{
			var commandResult = this.brandHandler.Create(new BrandCommand());

			var notifications = (List<Notification>)commandResult.Data;

			Assert.IsTrue(notifications.Any());
		}

		[TestMethod]
		[Description("Given that brand command is invalid, " +
					 "when trying create a brand" +
					 "then should contains bad request status code in command result")]
		public void Should_contains_bad_request_status_code_in_command_result_when_brand_comand_is_invalid()
		{
			var commandResult = this.brandHandler.Create(new BrandCommand());

			Assert.AreEqual(StatusCodeResult.BadRequest, commandResult.StatusCode);
		}

		[TestMethod]
		[Description("Given that brand command is valid and name not exists, " +
					 "when creating a brand on database" +
					 "then should return successful command result")]
		public void Should_contains_success_status_code_in_command_result_when_user_comand_is_valid()
		{
			var commandResult = this.brandHandler.Create(new BrandCommand() { Name = MockString() });

			Assert.AreEqual(StatusCodeResult.Success, commandResult.StatusCode);
		}
	}
}

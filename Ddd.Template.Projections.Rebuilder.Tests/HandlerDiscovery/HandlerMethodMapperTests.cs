﻿using System.Linq;
using Ddd.Template.Projections.Rebuilder.HandlerDiscovery;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ddd.Template.Projections.Rebuilder.Tests.HandlerDiscovery
{
	[TestClass]
	public sealed class HandlerMethodMapperTests
	{
		[TestMethod]
		public void GivenProperHandler_WhenMappingProperType_ThenMappingShouldBeReturned()
		{
			// Arrange
			var mapper = new HandlerMethodMapper();

			// Act
			var info = mapper.Map(typeof(EventAndVisitorArrivedHandlerStub)).ToArray();

			// Assert
			Assert.AreEqual(2, info.Count());
		}
	}
}

﻿/**
 * The MIT License
 * Copyright (c) 2016 Population Register Centre (VRK)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using FluentAssertions;
using Moq;
using PTV.Database.DataAccess.Interfaces.Repositories;
using PTV.Database.DataAccess.Services;
using PTV.Database.DataAccess.Tests.TestHelpers;
using PTV.Database.Model.Models;
using PTV.Domain.Logic;
using PTV.Domain.Model.Enums;
using PTV.Domain.Model.Models.Interfaces.OpenApi;
using PTV.Domain.Model.Models.OpenApi;
using PTV.Domain.Model.Models.OpenApi.V3;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PTV.Database.DataAccess.Tests.Services.OpenApi.Service
{
    public class GetServicesTests : ServiceServiceTestBase
    {
        [Theory]
        [InlineData(1, 1, 1, 1, 1)]
        [InlineData(2, 1, 1, 1, 0)]
        [InlineData(2, 1, 2, 2, 1)]
        [InlineData(3, 1, 2, 2, 0)]
        [InlineData(1, 2, 2, 1, 2)]
        [InlineData(2, 2, 2, 1, 0)]
        public void NoDateDefined_Published(int pageNumber, int pageSize, int count, int expectedPageCount, int expectedItemCountOnPage)
        {
            var list = EntityGenerator.GetServiceEntityList(count, PublishingStatusCache).ToList();

            var result = ArrangeAndAct(null, list, pageNumber, pageSize, false, false);

            // Assert
            result.Should().NotBeNull();
            result.PageCount.Should().Be(expectedPageCount);
            var vmResult = Assert.IsType<V3VmOpenApiGuidPage>(result);
            if (expectedItemCountOnPage == 0)
            {
                vmResult.ItemList.Should().BeNull();
            }
            else
            {
                vmResult.ItemList.Count.Should().Be(expectedItemCountOnPage);
            }
        }

        [Theory]
        [InlineData(1, 1, 1, 2, 1)]
        [InlineData(2, 1, 1, 2, 1)]
        [InlineData(2, 1, 2, 4, 1)]
        [InlineData(3, 1, 2, 4, 1)]
        [InlineData(5, 1, 2, 4, 0)]
        [InlineData(1, 2, 2, 2, 2)]
        [InlineData(2, 2, 2, 2, 2)]
        [InlineData(3, 2, 2, 2, 0)]
        public void NoDateDefined_Archived(int pageNumber, int pageSize, int count, int expectedPageCount, int expectedItemCountOnPage)
        {
            var list = EntityGenerator.GetServiceEntityList(count, PublishingStatusCache).ToList();

            var result = ArrangeAndAct(null, list, pageNumber, pageSize, true, false);

            // Assert
            result.Should().NotBeNull();
            result.PageCount.Should().Be(expectedPageCount);
            var vmResult = Assert.IsType<V3VmOpenApiGuidPage>(result);
            if (expectedItemCountOnPage == 0)
            {
                vmResult.ItemList.Should().BeNull();
            }
            else
            {
                vmResult.ItemList.Count.Should().Be(expectedItemCountOnPage);
            }
        }

        [Theory]
        [InlineData(2, 2, 1, 2, 1)]
        [InlineData(3, 2, 2, 3, 2)]
        [InlineData(4, 2, 2, 3, 0)]
        public void NoDateDefined_Active(int pageNumber, int pageSize, int count, int expectedPageCount, int expectedItemCountOnPage)
        {
            var list = EntityGenerator.GetServiceEntityList(count, PublishingStatusCache).ToList();

            var result = ArrangeAndAct(null, list, pageNumber, pageSize, false, true);

            // Assert
            result.Should().NotBeNull();
            result.PageCount.Should().Be(expectedPageCount);
            var vmResult = Assert.IsType<V3VmOpenApiGuidPage>(result);
            if (expectedItemCountOnPage == 0)
            {
                vmResult.ItemList.Should().BeNull();
            }
            else
            {
                vmResult.ItemList.Count.Should().Be(expectedItemCountOnPage);
            }
        }

        [Fact]
        public void Archived_OnlyOnePerRootIdReturned()
        {
            var rootId = Guid.NewGuid();
            // Let's create two version for same root service
            var deletedItem = EntityGenerator.CreateEntity<ServiceVersioned, Model.Models.Service, ServiceLanguageAvailability>(PublishingStatusCache.Get(PublishingStatus.Deleted), rootId);
            deletedItem.UnificRoot = new Model.Models.Service { Id = rootId };
            var oldPublishedItem = EntityGenerator.CreateEntity<ServiceVersioned, Model.Models.Service, ServiceLanguageAvailability>(PublishingStatusCache.Get(PublishingStatus.OldPublished), rootId);
            oldPublishedItem.UnificRoot = new Model.Models.Service { Id = rootId };
            var list = new List<ServiceVersioned> { deletedItem, oldPublishedItem };

            var result = ArrangeAndAct(null, list, 1, 1, true, false);

            // Assert
            result.Should().NotBeNull();
            result.PageCount.Should().Be(1);// Only one item per root should be found
            var vmResult = Assert.IsType<V3VmOpenApiGuidPage>(result);
            vmResult.ItemList.Count.Should().Be(1);
        }

        [Fact]
        public void Active_OnlyOnePerRootIdReturned()
        {
            var rootId = Guid.NewGuid();
            // Let's create two version for same root service
            var publishedItem = EntityGenerator.CreateEntity<ServiceVersioned, Model.Models.Service, ServiceLanguageAvailability>(PublishingStatusCache.Get(PublishingStatus.Published), rootId);
            publishedItem.UnificRoot = new Model.Models.Service { Id = rootId };
            var modifiedItem = EntityGenerator.CreateEntity<ServiceVersioned, Model.Models.Service, ServiceLanguageAvailability>(PublishingStatusCache.Get(PublishingStatus.Modified), rootId);
            modifiedItem.UnificRoot = new Model.Models.Service { Id = rootId };
            var list = new List<ServiceVersioned> { publishedItem, modifiedItem };

            var result = ArrangeAndAct(null, list, 1, 1, false, true);

            // Assert
            result.Should().NotBeNull();
            result.PageCount.Should().Be(1);// Only one item per root should be found
            var vmResult = Assert.IsType<V3VmOpenApiGuidPage>(result);
            vmResult.ItemList.Count.Should().Be(1);
        }

        private IVmOpenApiGuidPageVersionBase ArrangeAndAct(DateTime? date, List<ServiceVersioned> entityList, int pageNumber, int pageSize, bool archived, bool active)
        {
            // unitOfWork
            var channelRepoMock = new Mock<IRepository<ServiceVersioned>>();
            unitOfWorkMockSetup.Setup(uw => uw.CreateRepository<IRepository<ServiceVersioned>>()).Returns(channelRepoMock.Object);
            channelRepoMock.Setup(g => g.All()).Returns(entityList.AsQueryable());

            unitOfWorkMockSetup.Setup(uw => uw.ApplyIncludes(
                It.IsAny<IQueryable<ServiceVersioned>>(),
                It.IsAny<Func<IQueryable<ServiceVersioned>, IQueryable<ServiceVersioned>>>(),
                It.IsAny<bool>()
                )).Returns((IQueryable<ServiceVersioned> items, Func<IQueryable<ServiceVersioned>, IQueryable<ServiceVersioned>> func, bool applyFilters) =>
                {
                    return items;
                });

            var unitOfWorkMock = unitOfWorkMockSetup.Object;

            // Translation manager
            translationManagerMockSetup.Setup(t => t.TranslateAll<ServiceVersioned, VmOpenApiItem>(It.IsAny<IList<ServiceVersioned>>()))
                .Returns((ICollection<ServiceVersioned> collection) =>
                {
                    var list = new List<VmOpenApiItem>();
                    if (collection?.Count > 0)
                    {
                        collection.ToList().ForEach(c => list.Add(new VmOpenApiItem
                        {
                            Id = c.UnificRootId,
                        }));
                    }
                    return list;
                });

            var translationManagerMock = translationManagerMockSetup.Object;

            var contextManager = new TestContextManager(unitOfWorkMock, unitOfWorkMock);

            var serviceUtilities = new ServiceUtilities(UserIdentification, LockingManager, contextManager, UserOrganizationService,
                VersioningManager, UserInfoService, UserOrganizationChecker);

            var service = new ServiceService(contextManager, translationManagerMockSetup.Object, translationManagerVModelMockSetup.Object, Logger, serviceUtilities,
                DataUtils, CommonService, new VmOwnerReferenceLogic(), CacheManager.TypesCache, LanguageCache, PublishingStatusCache,
                VersioningManager, gdService, UserOrganizationChecker);

            // Act
            return service.GetServices(date, pageNumber, pageSize, archived, active);
        }
    }
}
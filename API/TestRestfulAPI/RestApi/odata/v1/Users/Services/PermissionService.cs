﻿using System.Linq;
using System.Web.OData;
using TestRestfulAPI.Infrastructure.Contexts;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.Infrastructure.Services;
using TestRestfulAPI.RestApi.odata.v1.Users.Entities;
using TestRestfulAPI.RestApi.odata.v1.Users.Repositories;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;

namespace TestRestfulAPI.RestApi.odata.v1.Users.Services
{
    public class PermissionService :ISingleService<Permission, int>
    {
        private PermissionRepository _permissionRepository;

        public IQueryable<Permission> All()
        {
            InitRepository();
            return this._permissionRepository.All();
        }

        public Permission Get(int id)
        {
            InitRepository();
            return this._permissionRepository.Get(id);
        }

        public Permission Create(Permission permission)
        {
            InitRepository();
            return this._permissionRepository.Create(permission);
        }
        public Permission Update(Permission permission)
        {
            InitRepository();
            return this._permissionRepository.Update(permission);
        }
        public Permission PartialUpdate(int id, Delta<Permission> permission)
        {
            InitRepository();
            return this._permissionRepository.PartialUpdate(id, permission);
        }
        public void Delete(int id)
        {
            InitRepository();
            this._permissionRepository.Delete(id);
        }

        private void InitRepository()
        {
            this._permissionRepository = new PermissionRepository(
                new ResourceContext(
                    "Permission",
                    DbContextFactory.Get<UserEntities>("TEST_TESS_USER"),
                    typeof(UserEntities)
                )
            );
        }
    }
}
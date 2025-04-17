using UnityEngine;
using System.Collections.Generic;
using ServiceLocator.Utilities;

/*  This script demonstrates the implementation of Object Pool design pattern.
 *  If you're interested in learning about Object Pooling, you can find
 *  a dedicated course on Outscal's website.
 *  Link: https://outscal.com/courses
 * */

namespace ServiceLocator.Player.Projectile
{
    public class ProjectilePool : GenericObjectPool<ProjectileController>
    {
        private ProjectileView projectilePrefab;
        private List<ProjectileScriptableObject> projectileScriptableObjects;
        private Transform projectileContainer;
        private PlayerService playerService;

        public ProjectilePool(ProjectileView projectilePrefab, List<ProjectileScriptableObject> projectileScriptableObjects, PlayerService playerService)
        {
            this.projectilePrefab = projectilePrefab;
            this.playerService = playerService;
            this.projectileScriptableObjects = projectileScriptableObjects;
            projectileContainer = new GameObject("Projectile Container").transform;
            this.playerService = playerService;
        }

        public ProjectileController GetProjectile(ProjectileType projectileType)
        {
            ProjectileController projectile = GetItem();
            ProjectileScriptableObject scriptableObjectToUse = projectileScriptableObjects.Find(so => so.Type == projectileType);
            projectile.Init(scriptableObjectToUse,playerService);
            return projectile;
        }

        protected override ProjectileController CreateItem() => new ProjectileController(projectilePrefab, projectileContainer);
    }
}
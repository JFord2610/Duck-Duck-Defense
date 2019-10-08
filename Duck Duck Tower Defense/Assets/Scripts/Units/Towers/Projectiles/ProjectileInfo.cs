using UnityEngine;

[CreateAssetMenu(menuName ="Projectile")]
public class ProjectileInfo : ScriptableObject
{
    public float speed = 0;
    public int bounces = 0;
    public int pierce = 1;
    public float timeBeforeDeath = 0;

    public ProjectileInfo CreateCopy()
    {
        ProjectileInfo pi = CreateInstance<ProjectileInfo>();
        pi.speed = speed;
        pi.bounces = bounces;
        pi.pierce = pierce;
        pi.timeBeforeDeath = timeBeforeDeath;
        return pi;
    }
}

public class EnemyStateController : IStateController
{
    private IZombie m_Enemy;
    public IPlant BeFoundPlant;
    public EnemyStateController(IZombie enemy)
    {
        m_Enemy = enemy;
    }
    public IZombie GetEnemy()
    {
        return m_Enemy;
    }
}

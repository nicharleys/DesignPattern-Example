using DesignPatternExample.Character.CharacterSetting;
public enum EnemyType {
    Null = 0,
    A = 1,
    B = 2,
    C = 3,
    Max,
}
public abstract class EnemyAbstract : CharacterAbstract {
    private EnemyType _enemyTypeEnum = EnemyType.Null;
    private EnemyAttr _enemyAttr = null;
    internal override void Initialize() {
        CharacterInit(new EnemyAttrStrategy());
    }
    internal override void Update() {

    }
    internal override void FixedUpdate() {
        UpdateCharacter(_enemyAttr);
    }
    internal override void CharacterAttrInit() {
        _enemyAttr = new EnemyAttr(1, 30, "Enemy");
        _enemyAttr.SetAttStrategy(AttrStrategy);
        SetCharacterAttr(_enemyAttr);
    }
    
    internal override void SettingInit() {
        Agent.updatePosition = true;
        Agent.updateRotation = false;
        Anim.SetBool("IsFighting", false);
    }
    internal override void LoopProcess() {
        CharacterAI.Update();
    }
    public EnemyType GetEnemyType() {
        return _enemyTypeEnum;
    }
}
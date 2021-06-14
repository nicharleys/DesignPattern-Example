using UnityEngine;
using UnityEngine.AI;

public abstract class ICharacter : MonoBehaviour
{
    protected string m_Name = "";
    protected GameObject m_GameObject = null;
    protected NavMeshAgent m_NavAgent = null;
    protected AudioSource m_Audio = null;

    protected bool m_bKilled = false;
    protected bool m_bCheckedKilled = false;
    protected float m_RemoveTimer = 1.5f;
    protected bool m_bCanRemove = false;
    public ICharacter() { }
    public void SetGameObject(GameObject theGameObject) {
        m_GameObject = theGameObject;
        m_NavAgent = m_GameObject.GetComponent<NavMeshAgent>();
        m_Audio = m_GameObject.GetComponent<AudioSource>();
    }
    public GameObject GetGameObject() {
        return m_GameObject;
    }
    public void Release() {
        if(m_GameObject != null)
            GameObject.Destroy(m_GameObject);
    }
    public string GetName() {
        return m_Name;
    }
}

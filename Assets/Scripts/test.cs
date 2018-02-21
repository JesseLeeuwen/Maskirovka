
using UnityEngine;

class asd : MonoBehaviour, ITemplate
{

    public void Function(bool b )
    {
        return;
    }
}

interface ITemplate 
{
    void Function( bool b );
}
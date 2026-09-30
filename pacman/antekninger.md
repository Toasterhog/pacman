
TODO: 10


#datatyp  klass 

##datatyp-typ    Entity, program 

####instans             new Entity(); 


<h1> AI </h1>

<div style="background-color : darksalmon; ">
    <a> http://dfgfdgg </a>


<h1>datatyp  deleate</h1>
datatyp-typ    Func, action, MyDelegate
instans             MyDelegate delInstance = MyFunc;



//////////////////// FATTA EVENT KOD

namespace pacman;

public <strong>delegate</strong> void CustomEventDelegateType(object s);
public delegate void CustomEventDelegateTypeWithParam<T>(object s, T otherParameter);

public class Edible : Entity
{



    public event CustomEventDelegateTypeWithParam<int> onCustomDelegateEvent;
 
    public void MyFunc(object s, int a)
    {
        if (a == 42)
        {
            Console.WriteLine("Yay");
        }
        return;
    }


    void FuncScope()
    {
        onCustomDelegateEvent += MyFunc;
        onCustomDelegateEvent += (s, a) => { Console.WriteLine(":>");
        };
        
        if (true) onCustomDelegateEvent.Invoke(this, 70);
        
    }

}

//###################################
class Player
{
private float health = 10f;
private float distanceToExplsive = 8;
private void OnExploded(Explosive thingThatExploded)
{
if (distanceToExplsive < thingThatExploded.radius)
{
health -= 1f;
}
}
public void StartSubscribe(Explosive explosiveToSubscribeTo)
{
explosiveToSubscribeTo.ExplodeEvent += OnExploded;
}
}

class Explosive
{
private float radius = 10;
public event CustomEventDelegateType ExplodeEvent;
private void Explode()
{
ExplodeEvent?.Invoke(this);
}
}

class world
{
private Explosive bomb = new Explosive();
private Player me = new Player();

    public world()
    {
        me.StartSubscribe(bomb);
        //bomb.ExplodeEvent += me.OnExploded;
    }
}
</div>

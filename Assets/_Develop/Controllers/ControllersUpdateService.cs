using System.Collections.Generic;

public class ControllersUpdateService 
{
    private List<Controller> _controllers = new();

    public void Add(Controller controller)
    { 
        _controllers.Add(controller);
    }

    public void Update(float deltaTime)
    { 
        foreach (Controller controller in _controllers) 
            controller.Update(deltaTime);
    }
}

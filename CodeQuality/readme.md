# How would you rate the quality of the quality of the following piece of code? Can you provide an alternative?

![alt tag](https://github.com/caetanomb/DataAuthority/blob/master/Solution%20Property.png)


## I would rate this code as low maintainability and coupled which makes it hard to extend. You have to keep adding **IF** statements in order to support new bussiness rules.

## Alternative solution
- Create a single interface with a method that can be implemented by each of the concrete classes.
- Use a Service Locator pattern in order to retrieve the concrete object instance
- Once we have the object instance provided by the service locator you can call the method from the qualified implementation

```
public void ExecuteMessage(IMessage message)
{
    //The ServiceLocator is responsible for getting the qualified implementation class based on the provided class type
    //ServiceLocator retrieves the instance from DI context
    IMessage messageService = 
        ServiceLocator.GetService<typeof(message)>();

    messageService.MyCustomMethod();        
}
```



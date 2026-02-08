using System;

public class Health
{
    public event Action Changed;
    private int _currentHealth;

    public Health(int currentHealth)
    {
        _currentHealth = currentHealth;
    }

    public void DecreaseHealth(int decreaseAmount)
    {
        _currentHealth -= decreaseAmount;

        if (_currentHealth <= 0)
            _currentHealth = 0;

        Changed?.Invoke();
    }

    public int CurrentHealth => _currentHealth;
    public bool HealthIsDrained => _currentHealth <= 0;
}
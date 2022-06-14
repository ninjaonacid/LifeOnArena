using UnityEngine;

public static class SimpleInputHelper
{
    public static void TriggerButtonClick(string button)
    {
        var buttonClick = new ButtonClickInput(button);
        buttonClick.StartTracking();
        SimpleInput.OnUpdate += buttonClick.OnUpdate;
    }

    public static void TriggerMouseButtonClick(int button)
    {
        var mouseButtonClick = new MouseButtonClickInput(button);
        mouseButtonClick.StartTracking();
        SimpleInput.OnUpdate += mouseButtonClick.OnUpdate;
    }

    public static void TriggerKeyClick(KeyCode key)
    {
        var keyClick = new KeyClickInput(key);
        keyClick.StartTracking();
        SimpleInput.OnUpdate += keyClick.OnUpdate;
    }

    private class ButtonClickInput : SimpleInput.ButtonInput
    {
        public ButtonClickInput(string key) : base(key)
        {
        }

        public void OnUpdate()
        {
            if (!value)
            {
                value = true;
            }
            else
            {
                StopTracking();
                SimpleInput.OnUpdate -= OnUpdate;
            }
        }
    }

    private class MouseButtonClickInput : SimpleInput.MouseButtonInput
    {
        public MouseButtonClickInput(int key) : base(key)
        {
        }

        public void OnUpdate()
        {
            if (!value)
            {
                value = true;
            }
            else
            {
                StopTracking();
                SimpleInput.OnUpdate -= OnUpdate;
            }
        }
    }

    private class KeyClickInput : SimpleInput.KeyInput
    {
        public KeyClickInput(KeyCode key) : base(key)
        {
        }

        public void OnUpdate()
        {
            if (!value)
            {
                value = true;
            }
            else
            {
                StopTracking();
                SimpleInput.OnUpdate -= OnUpdate;
            }
        }
    }
}
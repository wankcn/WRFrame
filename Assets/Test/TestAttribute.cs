using System;


public class TestAttribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TextAttribute : Attribute
    {
        private bool canSwitch;

        public TextAttribute(bool canSwitch)
        {
            this.canSwitch = canSwitch;
        }
    }
}
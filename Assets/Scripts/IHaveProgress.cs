using System;
public interface IHaveProgress 
{
    public event EventHandler<OnProgressChangeEventArgs> OnProgressChange;

    bool HasKitchenObject();

    public class OnProgressChangeEventArgs : EventArgs
    {
        public float ProgressNormalized;
    } 
}

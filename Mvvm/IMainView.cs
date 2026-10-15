namespace Mvvm
{
    public interface IMainView<TDialogResult, TDimension> : IDialogView<TDialogResult>
    {
        TDimension Left { get; set; }

        TDimension Top { get; set; }

        TDimension Width { get; set; }

        TDimension Height { get; set; }
    }
}

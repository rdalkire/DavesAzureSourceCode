namespace WatchFunction.Domain
{
    public interface IWatchInfoProvider
    {
        WatchItem ProvideWatchItem(string model);
    }

    public class WatchInfoProvider: IWatchInfoProvider
    {
        public WatchItem ProvideWatchItem(string model)
        {
            // NOTE: of course in real life this would be retrieved from
            // storage according to model number (from a separate storage
            // project)
            return new WatchItem()
            {
                Model = model,
                Bezel = "basically no bezel",
                CaseFinish = "mystery finish",
                CaseType = "not a real case",
                Dial = "undone dial",
                Jewels = 0,
                Manufacturer = "We Don't Make Watches, Inc."
            };
        }
    }
}

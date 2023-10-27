using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace Playground.Application.Common.Caching;

public abstract class BaseCache
{
    /// <summary>
    /// wrapper to make sure that the token source is initialized
    /// </summary>
    private class CancellationTokenWrapper
    {
        // testing only
        private string _cacheName;

        public CancellationTokenWrapper(string cacheName)
        {
            this._cacheName = cacheName;
        }

        private CancellationTokenSource? _tokenSource;

        public CancellationTokenSource Get(TimeSpan refreshInterval)
            => this._tokenSource ??= new CancellationTokenSource(refreshInterval);

        public CancellationTokenSource Refresh(TimeSpan refreshInterval)
        {
            if (this._tokenSource != null)
            {
                if (!this._tokenSource.IsCancellationRequested)
                {
                    this._tokenSource.Cancel();
                }

                this._tokenSource = null;
            }

            return this.Get(refreshInterval);
        }
    }

    public string Key => this.GetType().FullName!;

    private readonly CancellationTokenWrapper tokenWrapper;

    public BaseCache()
    {
        this.tokenWrapper ??= new(this.GetType().FullName!);
    }

    public virtual MemoryCacheEntryOptions MemoryCacheEntryOptions =>
        new MemoryCacheEntryOptions().AddExpirationToken(new CancellationChangeToken(this.tokenWrapper.Get(this.RefreshInterval).Token));

    protected abstract TimeSpan RefreshInterval { get; }

    public void Refresh()
    {
        this.tokenWrapper.Refresh(this.RefreshInterval);
    }
}

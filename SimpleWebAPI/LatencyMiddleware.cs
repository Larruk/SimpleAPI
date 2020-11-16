using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

public class LatencyMiddleware {
    private readonly RequestDelegate _next;
    private readonly int _minDelayInMs;
    private readonly int _maxDelayInMs;
    private readonly ThreadLocal<Random> _random;

    public LatencyMiddleware(RequestDelegate next, TimeSpan min, TimeSpan max) {
        _next = next;
        _minDelayInMs = (int)min.TotalMilliseconds;
        _maxDelayInMs = (int)max.TotalMilliseconds;
        _random = new ThreadLocal<Random>(() => new Random());
    }

    /// <summary>
    /// Intercept the request coming through, add random delay then resolve it
    /// </summary>
    public async Task Invoke(HttpContext context) {
        int delayInMs = _random.Value.Next(_minDelayInMs, _maxDelayInMs);

        await Task.Delay(delayInMs);
        await _next(context);
    }
}
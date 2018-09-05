using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FIVIL.Litentity
{
    public class SessionProvider : IDisposable
    {
        private readonly Dictionary<Guid, SessionBase> _sessions = new Dictionary<Guid, SessionBase>();
        private readonly object _lock = new object();
        internal TimeSpan IdleTime = TimeSpan.FromMinutes(45);
        internal Action<SessionBase> CallBackFunction = (U) => { };
        private DateTime lastCheck = DateTime.MinValue;
        private bool Initaited = false;
        private Timer _timer ;
        internal void Initiate()
        {
            if (Initaited) return;
            _timer = new Timer(DoWork, new object(), IdleTime, IdleTime);
            Initaited = true;
        }
        private void DoWork(object obj)
        {
            if (_sessions == null || _sessions.Count <= 1) return;
            if (lastCheck.Add(IdleTime) > DateTime.Now) return;
            lastCheck = DateTime.Now;
            lock (_lock)
            {
                var Now = DateTime.Now;
                var removes = _sessions
                    .Where(x => x.Value.LastSeen.Add(IdleTime) < Now)
                    .Select(x => x.Key)
                    .ToList();
                foreach (var item in removes)
                {
                    CallBackFunction(_sessions[item]);
                    _sessions.Remove(item);
                }
            }
        }

        internal (bool, Guid) Add(SessionBase sessionBase)
        {
            Guid g = Guid.Empty;
            bool s = false;
            lock (_lock)
            {
                g = Guid.NewGuid();
                s = _sessions.TryAdd(g, sessionBase);
            }
            return s ? (s, g) : (s, Guid.Empty);
        }

        internal bool Remove(Guid key)
        {
            bool s = false;
            lock (_lock)
            {
                s = _sessions.Remove(key);
            }
            return s;
        }

        internal (bool, SessionBase) Get(Guid key)
        {
            bool s;
            SessionBase S;
            lock (_lock)
            {
                s = _sessions.TryGetValue(key, out S);
            }
            return (s, S);
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
